using Dapper;
using DapperForge.Core.Abstractions.Strategies;
using DapperForge.Core.Caching;
using DapperForge.Core.Models;
using DapperForge.Core.Utilities;
using System.Text;


namespace DapperForge.MySql.Strategies
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
