using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.PostgreSql.Strategies
{
    internal partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
    {
        public static SqlBuilderStrategy Instance { get; } = new(Strategies.SqlDialectStrategy.Instance);

        private SqlBuilderStrategy(SqlDialectStrategy strategy) : base(strategy) { }

        public override SqlTemplate GetFirstSqlBuilder<TEntity>() where TEntity : class
        {
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("SELECT");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "    ", properties, null, true, false);
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, string.Empty, null);
            sqlBuffer.AppendLine("{}");
            sqlBuffer.AppendLine("LIMIT");
            sqlBuffer.Append("    {}");
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpsertSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildUpsertSql<TEntity>(false);
        }

        public override SqlTemplate UpdateRangeSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
            ImmutableArray<PropertyInfo> updateProperties = EntityInfoCache<TEntity>.UpdateProperties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            string idColumn = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idProperty.Name]);
            string sourceTable = SqlDialectStrategy.RenderIdentifier("source");
            string targetTable = SqlDialectStrategy.RenderIdentifier("target");
            string dataTable = SqlDialectStrategy.RenderIdentifier("data");
            string row = SqlDialectStrategy.RenderIdentifier("row");
            string clause = targetTable + "." + idColumn + " = " + sourceTable + "." + idColumn;

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("UPDATE ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.Append(" AS ");
            sqlBuffer.AppendLine(targetTable);
            sqlBuffer.Append("SET");
            sqlBuffer.AppendSetColumns<TEntity>(SqlDialectStrategy, "    ", updateProperties, sourceTable, null);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine("FROM (");
            sqlBuffer.Append("    SELECT");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "        ", properties, "(" + dataTable + "." + row + ")", false, false);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine("    FROM (");
            sqlBuffer.AppendLine("        VALUES");
            sqlBuffer.AppendLine("{}");
            sqlBuffer.Append("    ) AS ");
            sqlBuffer.Append(dataTable);
            sqlBuffer.Append(" (");
            sqlBuffer.Append(row);
            sqlBuffer.AppendLine(")");
            sqlBuffer.Append(") AS ");
            sqlBuffer.Append(sourceTable);
            sqlBuffer.Append(" (");
            sqlBuffer.Append("{}");
            sqlBuffer.Append(')');
            sqlBuffer.AppendWhereClause(string.Empty, clause);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpsertRangeSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildUpsertSql<TEntity>(true);
        }

        public override SqlTemplate ExistsSqlBuilder<TEntity>() where TEntity : class
        {
            StringBuilder sqlBuffer = new();

            sqlBuffer.AppendLine("SELECT EXISTS (");
            sqlBuffer.AppendLine("    SELECT");
            sqlBuffer.Append("        1");
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, "    ", null);
            sqlBuffer.AppendLine("{}");
            sqlBuffer.Append(')');
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate GetColumnsSqlBuilder<TEntity>()
        {
            string pgAttributeTable = SqlDialectStrategy.RenderIdentifier("pg_attribute");
            string pgClassTable = SqlDialectStrategy.RenderIdentifier("pg_class");
            string pgNamespaceTable = SqlDialectStrategy.RenderIdentifier("pg_namespace");
            string attnameColumn = pgAttributeTable + "." + SqlDialectStrategy.RenderIdentifier("attname");
            string attnumColumn = pgAttributeTable + "." + SqlDialectStrategy.RenderIdentifier("attnum");
            string attrelidColumn = pgAttributeTable + "." + SqlDialectStrategy.RenderIdentifier("attrelid");
            string relnamespaceColumn = pgClassTable + "." + SqlDialectStrategy.RenderIdentifier("relnamespace");
            string relnameColumn = pgClassTable + "." + SqlDialectStrategy.RenderIdentifier("relname");
            string nspnameColumn = pgNamespaceTable + "." + SqlDialectStrategy.RenderIdentifier("nspname");
            string attisdroppedColumn = pgAttributeTable + "." + SqlDialectStrategy.RenderIdentifier("attisdropped");
            string oidColumn = SqlDialectStrategy.RenderIdentifier("oid");

            StringBuilder sqlBuffer = new();

            sqlBuffer.AppendLine("SELECT");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(attnameColumn);
            sqlBuffer.Append(" AS ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(nameof(DbColumnInfo.Name)));
            sqlBuffer.AppendLine(",");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(attnumColumn);
            sqlBuffer.Append(" AS ");
            sqlBuffer.AppendLine(SqlDialectStrategy.RenderIdentifier(nameof(DbColumnInfo.OrdinalPosition)));
            sqlBuffer.AppendLine("FROM");
            sqlBuffer.Append("    ");
            sqlBuffer.AppendLine(pgAttributeTable);
            sqlBuffer.AppendLine("JOIN");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(pgClassTable);
            sqlBuffer.AppendOnClause(string.Empty, attrelidColumn + " = " + pgClassTable + "." + oidColumn);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine("JOIN");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(pgNamespaceTable);
            sqlBuffer.AppendOnClause(string.Empty, relnamespaceColumn + " = " + pgNamespaceTable + "." + oidColumn);
            sqlBuffer.AppendWhereClause(string.Empty, nspnameColumn + " = {} AND " + relnameColumn + " = {} AND " + attnumColumn + " > 0 AND NOT " + attisdroppedColumn);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine("ORDER BY");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(attnumColumn);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new SqlTemplate(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }
    }
}
