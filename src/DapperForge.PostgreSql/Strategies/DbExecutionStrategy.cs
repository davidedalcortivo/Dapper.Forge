using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Data.Common;


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
                        DbCommandInfo command = dbCommandStrategy.GetColumnsCommand<TEntity>(connection);
                        columns = await QueryImplAsync<DbColumnInfo>(connection, sync, command, null, null, commandTimeout, cancellationToken);

                        DbColumnInfoCache<TEntity>.Add(connectionId, columns);
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
