using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
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
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("SELECT TOP ({})");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "    ", propertyInfos, true, null);
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, string.Empty, null);
            sqlBuffer.Append("{}");
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpsertSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuffer = new();
            string clause = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idPropertyInfo.Name]) + " = {}";

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
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "        ", propertyInfos, false, null);
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
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            string idColumn = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idPropertyInfo.Name]);
            string sourceTable = SqlDialectStrategy.RenderIdentifier("source");
            string targetTable = SqlDialectStrategy.RenderIdentifier("target");

            StringBuilder sqlBuffer = new();
            string clause = targetTable + "." + idColumn + " = " + sourceTable + "." + idColumn;

            AppendUpdateRange<TEntity>(sqlBuffer, ["UPDLOCK"], sourceTable, targetTable, clause);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpsertRangeSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableArray<PropertyInfo> insertPropertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            string idColumn = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idPropertyInfo.Name]);
            string sourceTable = SqlDialectStrategy.RenderIdentifier("source");
            string targetTable = SqlDialectStrategy.RenderIdentifier("target");

            StringBuilder sqlBuffer = new();
            string clause = targetTable + "." + idColumn + " = " + sourceTable + "." + idColumn;

            AppendUpdateRange<TEntity>(sqlBuffer, ["UPDLOCK", "HOLDLOCK"], sourceTable, targetTable, clause);
            sqlBuffer.Append("INSERT INTO ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.Append(" (");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "    ", insertPropertyInfos, false, null);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine(")");
            sqlBuffer.Append("SELECT");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "    ", insertPropertyInfos, true, null);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine("FROM (");
            sqlBuffer.AppendLine("    VALUES");
            sqlBuffer.AppendLine("{}");
            sqlBuffer.AppendLine(") AS ");
            sqlBuffer.Append(sourceTable);
            sqlBuffer.Append(" (");
            sqlBuffer.AppendColumnsInline<TEntity>(SqlDialectStrategy, propertyInfos, false, null);
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
    }
}
