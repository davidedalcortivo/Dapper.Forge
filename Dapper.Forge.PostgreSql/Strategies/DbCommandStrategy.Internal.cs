using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Text;


namespace Dapper.Forge.PostgreSql.Strategies
{
    internal partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        protected override DbCommandInfo BuildExistsCommand<TEntity>(string? clause, DynamicParameters? parameters) where TEntity : class
        {
            StringBuilder sqlBuilder = new();

            if (clause is not null)
            {
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("    WHERE");
                sqlBuilder.Append("        ");
                sqlBuilder.Append(clause);
            }

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.ExistsSql.Render(sqlBuilder);
            return new(sql, parameters);
        }
    }
}
