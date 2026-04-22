using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Models;
using System.Text;


namespace Dapper.Forge.Oracle.Strategies
{
    internal partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
    {
        public static SqlBuilderStrategy Instance { get; } = new(Strategies.SqlDialectStrategy.Instance);

        private SqlBuilderStrategy(SqlDialectStrategy strategy) : base(strategy) { }

        public override SqlTemplate UpsertSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildUpsertSql<TEntity>(false, true);
        }

        public override SqlTemplate UpdateRangeSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildUpsertSql<TEntity>(true, false);
        }

        public override SqlTemplate InsertRangeSqlBuilder<TEntity>() where TEntity : class
        {
            StringBuilder sqlBuilder = new();

            sqlBuilder.AppendLine("INSERT ALL");
            sqlBuilder.AppendLine("{}");
            sqlBuilder.AppendLine("SELECT");
            sqlBuilder.AppendLine("    *");
            sqlBuilder.AppendLine("FROM");
            sqlBuilder.Append("    dual");
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpsertRangeSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildUpsertSql<TEntity>(true, true);
        }
    }
}
