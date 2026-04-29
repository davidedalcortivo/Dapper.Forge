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
            StringBuilder sqlBuffer = new();
            parameters ??= new();

            AppendWhereClauseAndSorting<TEntity>(sqlBuffer, clause, sortDescriptors, true);

            string takeName = "take";
            parameters.Add(takeName, take);

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.GetFirstSql.Render(SqlDialectStrategy.RenderParameter(takeName), sqlBuffer);
            return new(sql, parameters);
        }
    }
}
