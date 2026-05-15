using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Data.Common;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.SqlServer.Strategies
{
    internal partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
    {
        public static SqlBuilderStrategy Instance { get; } = new(Strategies.SqlDialectStrategy.Instance);

        private SqlBuilderStrategy(SqlDialectStrategy strategy) : base(strategy) { }

        public override SqlTemplate GetFirstSqlBuilder<TEntity>() where TEntity : class
        {
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("SELECT TOP ({})");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "    ", properties, null, true, false);
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, string.Empty, null);
            sqlBuffer.Append("{}");
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpsertSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            ImmutableArray<PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuffer = new();
            string clause = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idProperty.Name]) + " = {}";

            sqlBuffer.AppendLine("IF EXISTS (");
            sqlBuffer.AppendLine("    SELECT");
            sqlBuffer.Append("        1");
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, "    ", null);
            sqlBuffer.AppendLine();
            sqlBuffer.Append("        WITH (UPDLOCK, HOLDLOCK)");
            sqlBuffer.AppendWhereClause("    ", clause);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine(")");
            sqlBuffer.AppendLine("BEGIN");
            sqlBuffer.Append("    UPDATE ");
            sqlBuffer.AppendLine(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.AppendLine("    SET");
            sqlBuffer.Append("{}");
            sqlBuffer.AppendWhereClause("    ", clause);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);
            sqlBuffer.AppendLine("END");
            sqlBuffer.AppendLine("ELSE");
            sqlBuffer.AppendLine("BEGIN");
            sqlBuffer.Append("    INSERT INTO ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.Append(" (");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "        ", insertProperties, null, false, false);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine("    )");
            sqlBuffer.AppendLine("    VALUES (");
            sqlBuffer.AppendLine("{}");
            sqlBuffer.Append("    )");
            sqlBuffer.Append(SqlDialectStrategy.Terminator);
            sqlBuffer.AppendLine("END");

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpdateRangeSqlBuilder<TEntity>() where TEntity : class
        {
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            string idColumn = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idProperty.Name]);
            string sourceTable = SqlDialectStrategy.RenderIdentifier("Source");
            string targetTable = SqlDialectStrategy.RenderIdentifier("Target");

            StringBuilder sqlBuffer = new();
            string clause = targetTable + "." + idColumn + " = " + sourceTable + "." + idColumn;

            AppendUpdateRange<TEntity>(sqlBuffer, ["UPDLOCK"], sourceTable, targetTable, clause);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpsertRangeSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
            ImmutableArray<PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            string idColumn = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idProperty.Name]);
            string sourceTable = SqlDialectStrategy.RenderIdentifier("Source");
            string targetTable = SqlDialectStrategy.RenderIdentifier("Target");

            StringBuilder sqlBuffer = new();
            string clause = targetTable + "." + idColumn + " = " + sourceTable + "." + idColumn;

            AppendUpdateRange<TEntity>(sqlBuffer, ["UPDLOCK", "HOLDLOCK"], sourceTable, targetTable, clause);
            sqlBuffer.Append("INSERT INTO ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.Append(" (");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "    ", insertProperties, null, false, false);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine(")");
            sqlBuffer.Append("SELECT");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "    ", insertProperties, null, true, false);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine("FROM (");
            sqlBuffer.AppendLine("    VALUES");
            sqlBuffer.AppendLine("{}");
            sqlBuffer.AppendLine(") AS ");
            sqlBuffer.Append(sourceTable);
            sqlBuffer.Append(" (");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, string.Empty, properties, null, false, true);
            sqlBuffer.AppendLine(")");
            sqlBuffer.AppendLine("WHERE NOT EXISTS (");
            sqlBuffer.AppendLine("    SELECT");
            sqlBuffer.Append("        1");
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, "    ", targetTable);
            sqlBuffer.AppendLine();
            sqlBuffer.Append("        WITH (UPDLOCK, HOLDLOCK)");
            sqlBuffer.AppendWhereClause("    ", clause);
            sqlBuffer.AppendLine();
            sqlBuffer.Append(')');
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate ExistsSqlBuilder<TEntity>() where TEntity : class
        {
            StringBuilder sqlBuffer = new();

            sqlBuffer.AppendLine("SELECT");
            sqlBuffer.AppendLine("    CAST(");
            sqlBuffer.AppendLine("        EXISTS (");
            sqlBuffer.AppendLine("            SELECT");
            sqlBuffer.Append("                1");
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, "            ", null);
            sqlBuffer.AppendLine("{}");
            sqlBuffer.AppendLine("        )");
            sqlBuffer.Append("    AS BIT)");
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate GetColumnsSqlBuilder<TEntity>()
        {
            string sysTable = SqlDialectStrategy.RenderIdentifier("sys");
            string columnsTable = sysTable + "." + SqlDialectStrategy.RenderIdentifier("columns");
            string tablesTable = sysTable + "." + SqlDialectStrategy.RenderIdentifier("tables");
            string schemasTable = sysTable + "." + SqlDialectStrategy.RenderIdentifier("schemas");
            string columnIdColumn = columnsTable + "." + SqlDialectStrategy.RenderIdentifier("column_id");
            string nameColumn = SqlDialectStrategy.RenderIdentifier("name");
            string objectIdColumn = SqlDialectStrategy.RenderIdentifier("object_id");
            string schemaIdColumn = SqlDialectStrategy.RenderIdentifier("schema_id");

            StringBuilder sqlBuffer = new();

            sqlBuffer.AppendLine("SELECT");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(columnsTable);
            sqlBuffer.Append('.');
            sqlBuffer.Append(nameColumn);
            sqlBuffer.Append(" AS ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(nameof(DbColumnInfo.Name)));
            sqlBuffer.AppendLine(",");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(columnIdColumn);
            sqlBuffer.Append(" AS ");
            sqlBuffer.AppendLine(SqlDialectStrategy.RenderIdentifier(nameof(DbColumnInfo.OrdinalPosition)));
            sqlBuffer.AppendLine("FROM");
            sqlBuffer.Append("    ");
            sqlBuffer.AppendLine(columnsTable);
            sqlBuffer.AppendLine("JOIN");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(tablesTable);
            sqlBuffer.AppendOnClause(string.Empty, columnsTable + "." + objectIdColumn + " = " + tablesTable + "." + objectIdColumn);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine("JOIN");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(schemasTable);
            sqlBuffer.AppendOnClause(string.Empty, tablesTable + "." + schemaIdColumn + " = " + schemasTable + "." + schemaIdColumn);
            sqlBuffer.AppendWhereClause(string.Empty, schemasTable + "." + nameColumn + " = {} AND " + tablesTable + "." + nameColumn + " = {}");
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine("ORDER BY");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(columnIdColumn);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);


            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }
    }
}
