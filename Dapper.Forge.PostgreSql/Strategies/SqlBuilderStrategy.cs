using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
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
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;

            StringBuilder sqlBuilder = new();

            sqlBuilder.Append("SELECT");
            AppendColumns<TEntity>(sqlBuilder, "    ", propertyInfos, true, null);
            AppendFromTable<TEntity>(sqlBuilder, string.Empty, null);
            sqlBuilder.AppendLine("{}");
            sqlBuilder.AppendLine("LIMIT");
            sqlBuilder.Append("    {}");
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpsertSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildUpsertSql<TEntity>(false);
        }

        public override SqlTemplate UpdateRangeSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableArray<PropertyInfo> updatePropertyInfos = EntityInfoCache<TEntity>.UpdatePropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            string idColumn = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idPropertyInfo.Name]);
            string sourceTable = SqlDialectStrategy.RenderIdentifier("source");
            string targetTable = SqlDialectStrategy.RenderIdentifier("target");
            string clause = targetTable + "." + idColumn + " = " + sourceTable + "." + idColumn;

            StringBuilder sqlBuilder = new();

            sqlBuilder.Append("UPDATE ");
            sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuilder.Append(" AS ");
            sqlBuilder.AppendLine(targetTable);
            sqlBuilder.Append("SET");
            AppendSetColumns<TEntity>(sqlBuilder, "    ", updatePropertyInfos, sourceTable, null);
            sqlBuilder.AppendLine();
            sqlBuilder.AppendLine("FROM (");
            sqlBuilder.AppendLine("    VALUES");
            sqlBuilder.AppendLine("{}");
            sqlBuilder.Append(") AS ");
            sqlBuilder.Append(sourceTable);
            sqlBuilder.Append(" (");
            AppendColumnsInline<TEntity>(sqlBuilder, propertyInfos, false, null);
            sqlBuilder.Append(')');
            AppendWhereClause(sqlBuilder, string.Empty, clause);
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public override SqlTemplate UpsertRangeSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildUpsertSql<TEntity>(true);
        }

        public override SqlTemplate ExistsSqlBuilder<TEntity>() where TEntity : class
        {
            StringBuilder sqlBuilder = new();

            sqlBuilder.AppendLine("SELECT EXISTS(");
            sqlBuilder.AppendLine("    SELECT");
            sqlBuilder.Append("        1");
            AppendFromTable<TEntity>(sqlBuilder, "    ", null);
            sqlBuilder.AppendLine("{}");
            sqlBuilder.Append(')');
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }
    }
}
