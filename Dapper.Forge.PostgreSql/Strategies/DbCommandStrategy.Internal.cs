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
            StringBuilder sqlBuffer = new();

            if (clause is not null)
            {
                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("    WHERE");
                sqlBuffer.Append("        ");
                sqlBuffer.Append(clause);
            }

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.ExistsSql.Render(sqlBuffer);
            return new(sql, parameters);
        }
    }
}
