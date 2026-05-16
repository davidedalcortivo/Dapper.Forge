using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Text;


namespace Dapper.Forge.Oracle.Strategies
{
    internal sealed partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
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
            sqlBuffer.AppendLine(SqlDialectStrategy.Placeholder);
            sqlBuffer.AppendLine("SELECT");
            sqlBuffer.AppendLine("    *");
            sqlBuffer.AppendLine("FROM");
            sqlBuffer.Append("    dual");
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }

        public override SqlTemplate UpsertRangeSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildUpsertSql<TEntity>(true, true);
        }

        public override SqlTemplate GetColumnsSqlBuilder<TEntity>()
        {
            string allTabColumnsTable = SqlDialectStrategy.RenderIdentifier("ALL_TAB_COLUMNS");
            string columnNameColumn = SqlDialectStrategy.RenderIdentifier("COLUMN_NAME");
            string columnIdColumn = SqlDialectStrategy.RenderIdentifier("COLUMN_ID");
            string tableNameColumn = SqlDialectStrategy.RenderIdentifier("TABLE_NAME");
            string ownerColumn = SqlDialectStrategy.RenderIdentifier("OWNER");

            StringBuilder sqlBuffer = new();

            sqlBuffer.AppendLine("SELECT");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(columnNameColumn);
            sqlBuffer.Append(" AS ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(nameof(DbColumnInfo.Name)));
            sqlBuffer.AppendLine(",");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(columnIdColumn);
            sqlBuffer.Append(" AS ");
            sqlBuffer.AppendLine(SqlDialectStrategy.RenderIdentifier(nameof(DbColumnInfo.OrdinalPosition)));
            sqlBuffer.AppendLine("FROM");
            sqlBuffer.Append("    ");
            sqlBuffer.AppendLine(allTabColumnsTable);
            sqlBuffer.AppendWhereClause(string.Empty, ownerColumn + " = " + SqlDialectStrategy.Placeholder + " AND " + tableNameColumn + " = " + SqlDialectStrategy.Placeholder);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine("ORDER BY");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(columnIdColumn);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }
    }
}
