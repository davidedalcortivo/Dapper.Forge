using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Text;


namespace Dapper.Forge.SqlServer.Strategies
{
    internal partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        protected override DbCommandInfo BuildGetFirstCommand<TEntity>(string? clause, DynamicParameters? parameters, IEnumerable<SortDescriptor>? sortDescriptors, int take) where TEntity : class
        {
            StringBuilder sqlBuilder = new();
            parameters ??= new();

            AppendClauseAndSort<TEntity>(sqlBuilder, clause, sortDescriptors, true);

            string takeName = "take";
            parameters.Add(takeName, take);

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.GetFirstSql.Render(SqlDialectStrategy.RenderParameter(takeName), sqlBuilder);
            return new(sql, parameters);
        }
    }
}
