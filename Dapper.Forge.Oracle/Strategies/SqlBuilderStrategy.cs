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
            StringBuilder sqlBuffer = new();

            sqlBuffer.AppendLine("INSERT ALL");
            sqlBuffer.AppendLine("{}");
            sqlBuffer.AppendLine("SELECT");
            sqlBuffer.AppendLine("    *");
            sqlBuffer.AppendLine("FROM");
            sqlBuffer.Append("    dual");
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpsertRangeSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildUpsertSql<TEntity>(true, true);
        }
    }
}
