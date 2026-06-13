using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Models;
using System.Data;
using System.Data.Common;


namespace Dapper.Forge.SqlServer.Strategies
{
    internal sealed class DbExecutionStrategy : BaseDbExecutionStrategy<DbCommandStrategy>
    {
        public static DbExecutionStrategy Instance { get; } = new(DbCommandStrategy.Instance);

        private DbExecutionStrategy(DbCommandStrategy strategy) : base(strategy) { }

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

                result = await ExecuteImplAsync(connection, sync, command, transaction, commandTimeout, cancellationToken);

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
