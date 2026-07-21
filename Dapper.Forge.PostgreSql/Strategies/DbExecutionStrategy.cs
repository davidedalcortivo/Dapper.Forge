using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections.Immutable;
using System.Data.Common;
using System.Reflection;


namespace Dapper.Forge.PostgreSql.Strategies
{
    internal sealed class DbExecutionStrategy : BaseDbExecutionStrategy<DbCommandStrategy>
    {
        public static DbExecutionStrategy Instance { get; } = new(DbCommandStrategy.Instance);

        private DbExecutionStrategy(DbCommandStrategy strategy) : base(strategy) { }

        public override async Task GetColumnsImplAsync<TEntity>(DbConnection connection, bool sync, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            string connectionId = SqlDialectStrategy.GetConnectionId(connection);
            IReadOnlyList<DbColumnInfo>? columns = DbColumnInfoCache<TEntity>.GetListValueOrDefault(connectionId);

            if (columns is null)
            {
                SemaphoreSlim semaphore = DbColumnInfoCache<TEntity>.GetSemaphore(connectionId);

                if (sync)
                    semaphore.Wait(cancellationToken);
                else
                    await semaphore.WaitAsync(cancellationToken);

                try
                {
                    columns = DbColumnInfoCache<TEntity>.GetListValueOrDefault(connectionId);

                    if (columns is null)
                    {
                        ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
                        ImmutableDictionary<string, PropertyInfo> propertiesByColumnName = EntityInfoCache<TEntity>.PropertiesByColumnName;

                        DbCommandInfo command = dbCommandStrategy.GetColumnsCommand<TEntity>(connection);
                        columns = await QueryImplAsync<DbColumnInfo>(connection, sync, command, null, null, commandTimeout, cancellationToken);

                        if (properties.Length != columns.Count)
                            throw new InvalidOperationException($"Database table schema mismatch for entity '{typeof(TEntity).Name}'. Expected {properties.Length} mapped properties but found {columns.Count} database columns.");

                        foreach (DbColumnInfo column in columns)
                        {
                            string columnName = column.Name;

                            if (!propertiesByColumnName.TryGetValue(columnName, out PropertyInfo? _))
                                throw new InvalidOperationException($"Database column mapping mismatch for entity '{typeof(TEntity).Name}'. Database column '{columnName}' is not mapped to any entity property.");
                        }

                        _ = DbColumnInfoCache<TEntity>.TryAdd(connectionId, columns);
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
        }
    }
}
