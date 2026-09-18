using Dapper;
using Forget.Core.Abstractions.Strategies;
using Forget.Core.Caching;
using Forget.Core.Models;
using Forget.Core.Utilities;
using System.Text;


namespace Forget.PostgreSql.Strategies
{
    internal sealed partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        protected override DbCommandInfo BuildExistsCommand<TEntity>(string? clause, DynamicParameters? parameters) where TEntity : class
        {
            StringBuilder sqlBuffer = new();
            sqlBuffer.AppendWhereClause(clause, "        ");

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.ExistsSql.Render(sqlBuffer);
            return new(sql, parameters);
        }
    }
}
