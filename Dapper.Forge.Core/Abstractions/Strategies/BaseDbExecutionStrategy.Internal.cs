using Dapper.Forge.Core.Models;
using System.Data.Common;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    public abstract partial class BaseDbExecutionStrategy<TStrategy> : IDbExecutionStrategy where TStrategy : IDbCommandStrategy
    {
        protected virtual int ExecuteRange(DbConnection connection, IReadOnlyList<DbCommandInfo> commands, DbTransaction? transaction, int? commandTimeout)
        {
            int result = 0;

            if (commands.Count == 0)
                return result;

            bool ownsTransaction = transaction is null;
            DbTransaction? _transaction = transaction;

            try
            {
                using DbTransaction? tempTransaction = ownsTransaction ? connection.BeginTransaction() : null;
                _transaction ??= tempTransaction!;

                foreach (DbCommandInfo command in commands)
                    result += connection.Execute(command.Sql, command.Parameters, _transaction, commandTimeout);

                if (ownsTransaction)
                    _transaction.Commit();
            }
            catch
            {
                try
                {
                    if (ownsTransaction)
                        _transaction?.Rollback();
                }
                catch
                {

                }

                throw;
            }

            return result;
        }

        protected virtual async Task<int> ExecuteRangeAsync(DbConnection connection, IReadOnlyList<DbCommandInfo> commands, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken)
        {
            int result = 0;

            if (commands.Count == 0)
                return result;

            bool ownsTransaction = transaction is null;
            DbTransaction? _transaction = transaction;

            try
            {
                using DbTransaction? tempTransaction = ownsTransaction ? await connection.BeginTransactionAsync(cancellationToken) : null;
                _transaction ??= tempTransaction!;

                foreach (DbCommandInfo command in commands)
                    result += await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));

                if (ownsTransaction)
                    await _transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                try
                {
                    if (ownsTransaction && _transaction is not null)
                        await _transaction.RollbackAsync(cancellationToken);
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
