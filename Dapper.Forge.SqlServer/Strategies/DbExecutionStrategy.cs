using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections.Immutable;
using System.Data;
using System.Data.Common;
using System.Reflection;


namespace Dapper.Forge.SqlServer.Strategies
{
    internal sealed class DbExecutionStrategy : BaseDbExecutionStrategy<DbCommandStrategy>
    {
        public static DbExecutionStrategy Instance { get; } = new(DbCommandStrategy.Instance);

        private DbExecutionStrategy(DbCommandStrategy strategy) : base(strategy) { }

        public override async Task GetColumnsImplAsync<TEntity>(DbConnection connection, bool sync, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            string connectionId = SqlDialectStrategy.GetConnectionId(connection);
            IDictionary<string, DbColumnInfo>? columns = DbColumnInfoCache<TEntity>.GetDictValueOrDefault(connectionId);

            if (columns is null)
            {
                SemaphoreSlim semaphore = DbColumnInfoCache<TEntity>.GetSemaphore(connectionId);

                if (sync)
                    semaphore.Wait(cancellationToken);
                else
                    await semaphore.WaitAsync(cancellationToken);

                try
                {
                    columns = DbColumnInfoCache<TEntity>.GetDictValueOrDefault(connectionId);

                    if (columns is null)
                    {
                        ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
                        ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

                        DbCommandInfo command = dbCommandStrategy.GetColumnsCommand<TEntity>(connection);
                        columns = (await QueryImplAsync<DbColumnInfo>(connection, sync, command, null, null, commandTimeout, cancellationToken))
                            .ToDictionary(x => x.Name, x => x, StringComparer.OrdinalIgnoreCase);

                        if (properties.Length != columns.Count)
                            throw new InvalidOperationException($"Database table schema mismatch for entity '{typeof(TEntity).Name}'. Expected {properties.Length} mapped properties but found {columns.Count} database columns.");

                        foreach (PropertyInfo property in properties)
                        {
                            string columnName = columnNamesByPropertyName[property.Name];

                            if (!columns.TryGetValue(columnName, out DbColumnInfo? _))
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

        public override async Task<int> UpsertImplAsync<TEntity>(DbConnection connection, bool sync, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpsertCommand(connection, entity);
            int result = 0;

            bool ownsConnection = connection.State == ConnectionState.Closed;
            bool ownsTransaction = transaction is null;
            DbTransaction? _transaction = transaction;

            try
            {
                DbTransaction? tempTransaction = null;

                if (ownsConnection)
                {
                    if (sync)
                        connection.Open();
                    else
                        await connection.OpenAsync(cancellationToken);
                }

                if (ownsTransaction)
                {
                    if (sync)
                        tempTransaction = connection.BeginTransaction();
                    else
                        tempTransaction = await connection.BeginTransactionAsync(cancellationToken);
                }

                using DbTransaction? _ = tempTransaction;
                _transaction ??= tempTransaction!;

                result = await ExecuteImplAsync(connection, sync, command, _transaction, commandTimeout, cancellationToken);

                if (ownsTransaction)
                {
                    if (sync)
                        _transaction.Commit();
                    else
                        await _transaction.CommitAsync(cancellationToken);
                }
            }
            catch
            {
                try
                {
                    if (ownsTransaction && _transaction is not null)
                    {
                        if (sync)
                            _transaction.Rollback();
                        else
                            await _transaction.RollbackAsync(cancellationToken);
                    }
                }
                catch
                {

                }

                throw;
            }
            finally
            {
                if (ownsConnection)
                {
                    if (sync)
                        connection.Close();
                    else
                        await connection.CloseAsync();
                }
            }

            return result;
        }
    }
}
