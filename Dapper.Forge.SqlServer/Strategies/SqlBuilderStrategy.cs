using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
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

            StringBuilder sqlBuilder = new();

            sqlBuilder.AppendLine("SELECT TOP ({})");
            AppendColumns<TEntity>(sqlBuilder, "    ", propertyInfos, true, null);
            sqlBuilder.AppendLine();
            AppendFromTable<TEntity>(sqlBuilder, string.Empty, null);
            sqlBuilder.Append("{}");
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpsertSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuilder = new();
            string clause = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idPropertyInfo.Name]) + " = {}";

            sqlBuilder.AppendLine("IF EXISTS (");
            sqlBuilder.AppendLine("    SELECT");
            sqlBuilder.AppendLine("        1");
            AppendFromTable<TEntity>(sqlBuilder, "    ", null);
            sqlBuilder.AppendLine();
            sqlBuilder.AppendLine("        WITH (UPDLOCK, HOLDLOCK)");
            AppendWhereClause<TEntity>(sqlBuilder, "    ", clause);
            sqlBuilder.AppendLine();
            sqlBuilder.AppendLine(")");
            sqlBuilder.AppendLine("BEGIN");
            sqlBuilder.Append("    UPDATE ");
            sqlBuilder.AppendLine(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuilder.AppendLine("    SET");
            sqlBuilder.AppendLine("{}");
            AppendWhereClause<TEntity>(sqlBuilder, "    ", clause);
            sqlBuilder.Append(SqlDialectStrategy.Terminator);
            sqlBuilder.AppendLine("END");
            sqlBuilder.AppendLine("ELSE");
            sqlBuilder.AppendLine("BEGIN");
            sqlBuilder.Append("    INSERT INTO ");
            sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuilder.AppendLine(" (");
            AppendColumns<TEntity>(sqlBuilder, "        ", propertyInfos, false, null);
            sqlBuilder.AppendLine();
            sqlBuilder.AppendLine("    )");
            sqlBuilder.AppendLine("    VALUES (");
            sqlBuilder.AppendLine("{}");
            sqlBuilder.Append("    )");
            sqlBuilder.Append(SqlDialectStrategy.Terminator);
            sqlBuilder.AppendLine("END");

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpdateRangeSqlBuilder<TEntity>() where TEntity : class
        {
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            string idColumn = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idPropertyInfo.Name]);
            string sourceTable = SqlDialectStrategy.RenderIdentifier("source");
            string targetTable = SqlDialectStrategy.RenderIdentifier("target");

            StringBuilder sqlBuilder = new();
            string clause = targetTable + "." + idColumn + " = " + sourceTable + "." + idColumn;

            AppendUpdateRange<TEntity>(sqlBuilder, ["UPDLOCK"], sourceTable, targetTable, clause);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
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

            StringBuilder sqlBuilder = new();
            string clause = targetTable + "." + idColumn + " = " + sourceTable + "." + idColumn;

            AppendUpdateRange<TEntity>(sqlBuilder, ["UPDLOCK", "HOLDLOCK"], sourceTable, targetTable, clause);
            sqlBuilder.Append("INSERT INTO ");
            sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuilder.AppendLine(" (");
            AppendColumns<TEntity>(sqlBuilder, "    ", insertPropertyInfos, false, null);
            sqlBuilder.AppendLine();
            sqlBuilder.AppendLine(")");
            sqlBuilder.AppendLine("SELECT");
            AppendColumns<TEntity>(sqlBuilder, "    ", insertPropertyInfos, true, null);
            sqlBuilder.AppendLine();
            sqlBuilder.AppendLine("FROM (");
            sqlBuilder.AppendLine("    VALUES");
            sqlBuilder.AppendLine("{}");
            sqlBuilder.AppendLine(") AS ");
            sqlBuilder.Append(sourceTable);
            sqlBuilder.Append(" (");
            AppendColumnsInline<TEntity>(sqlBuilder, propertyInfos, false, null);
            sqlBuilder.AppendLine(")");
            sqlBuilder.AppendLine("WHERE NOT EXISTS (");
            sqlBuilder.AppendLine("    SELECT");
            sqlBuilder.AppendLine("        1");
            AppendFromTable<TEntity>(sqlBuilder, "    ", targetTable);
            sqlBuilder.AppendLine();
            sqlBuilder.AppendLine("        WITH (UPDLOCK, HOLDLOCK)");
            AppendWhereClause<TEntity>(sqlBuilder, "    ", clause);
            sqlBuilder.AppendLine();
            sqlBuilder.Append(')');
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate ExistsSqlBuilder<TEntity>() where TEntity : class
        {
            StringBuilder sqlBuilder = new();

            sqlBuilder.AppendLine("SELECT");
            sqlBuilder.AppendLine("    CAST(");
            sqlBuilder.AppendLine("        EXISTS (");
            sqlBuilder.AppendLine("            SELECT");
            sqlBuilder.AppendLine("                1");
            AppendFromTable<TEntity>(sqlBuilder, "            ", null);
            sqlBuilder.AppendLine("{}");
            sqlBuilder.AppendLine("        )");
            sqlBuilder.Append("    AS BIT)");
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }
    }
}
