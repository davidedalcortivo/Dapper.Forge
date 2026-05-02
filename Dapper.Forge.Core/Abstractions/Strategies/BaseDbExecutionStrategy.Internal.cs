using Dapper.Forge.Core.Models;
using System.Data.Common;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    public abstract partial class BaseDbExecutionStrategy<TStrategy> : IDbExecutionStrategy where TStrategy : IDbCommandStrategy
    {
        protected virtual async Task<int> ExecuteRangeImplAsync(DbConnection connection, bool sync, IReadOnlyList<DbCommandInfo> commands, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken)
        {
            int result = 0;

            if (commands.Count == 0)
                return result;

            bool ownsTransaction = transaction is null;
            DbTransaction? _transaction = transaction;

            try
            {
                DbTransaction? tempTransaction = null;

                if (ownsTransaction)
                {
                    if (sync)
                        tempTransaction = connection.BeginTransaction();
                    else
                        tempTransaction = await connection.BeginTransactionAsync(cancellationToken);
                }

                using DbTransaction? _ = tempTransaction;
                _transaction ??= tempTransaction!;

                foreach (DbCommandInfo command in commands)
                {
                    if (sync)
                        result += connection.Execute(command.Sql, command.Parameters, _transaction, commandTimeout);
                    else
                        result += await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, _transaction, commandTimeout, cancellationToken: cancellationToken));
                }

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

            return result;
        }
    }
}
