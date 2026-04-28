using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    public abstract partial class BaseSqlBuilderStrategy<TStrategy> : ISqlBuilderStrategy where TStrategy : ISqlDialectStrategy
    {
        public ISqlDialectStrategy SqlDialectStrategy { get; }

        protected BaseSqlBuilderStrategy(TStrategy sqlDialectStrategy)
        {
            SqlDialectStrategy = sqlDialectStrategy;
        }

        public virtual SqlTemplate GetAllSqlBuilder<TEntity>() where TEntity : class
        {
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;

            StringBuilder sqlBuilder = new();

            sqlBuilder.Append("SELECT");
            AppendColumns<TEntity>(sqlBuilder, "    ", propertyInfos, true, null);
            AppendFromTable<TEntity>(sqlBuilder, string.Empty, null);
            sqlBuilder.Append("{}");
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public virtual SqlTemplate GetFirstSqlBuilder<TEntity>() where TEntity : class
        {
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;

            StringBuilder sqlBuilder = new();

            sqlBuilder.Append("SELECT");
            AppendColumns<TEntity>(sqlBuilder, "    ", propertyInfos, true, null);
            AppendFromTable<TEntity>(sqlBuilder, string.Empty, null);
            sqlBuilder.AppendLine("{}");
            sqlBuilder.AppendLine("FETCH FIRST");
            sqlBuilder.Append("    {} ROWS ONLY");
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public virtual SqlTemplate GetByIdSqlBuilder<TEntity>() where TEntity : class
        {
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuilder = new();
            string clause = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idPropertyInfo.Name]) + " = {}";

            sqlBuilder.Append("SELECT");
            AppendColumns<TEntity>(sqlBuilder, "    ", propertyInfos, true, null);
            AppendFromTable<TEntity>(sqlBuilder, string.Empty, null);
            AppendWhereClause(sqlBuilder, string.Empty, clause);
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public virtual SqlTemplate UpdateSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;

            StringBuilder sqlBuilder = new();

            sqlBuilder.Append("UPDATE ");
            sqlBuilder.AppendLine(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuilder.AppendLine("SET");
            sqlBuilder.Append("{}");
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public virtual SqlTemplate InsertSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;

            StringBuilder sqlBuilder = new();

            sqlBuilder.Append("INSERT INTO ");
            sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuilder.Append(" (");
            AppendColumns<TEntity>(sqlBuilder, "    ", propertyInfos, false, null);
            sqlBuilder.AppendLine();
            sqlBuilder.AppendLine(")");
            sqlBuilder.AppendLine("VALUES (");
            sqlBuilder.AppendLine("{}");
            sqlBuilder.Append(')');
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public virtual SqlTemplate DeleteSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;

            StringBuilder sqlBuilder = new();

            sqlBuilder.Append("DELETE FROM ");
            sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuilder.Append("{}");
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public abstract SqlTemplate UpsertSqlBuilder<TEntity>() where TEntity : class;

        public virtual SqlTemplate GetByIdRangeSqlBuilder<TEntity>() where TEntity : class
        {
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuilder = new();
            (string inPrefix, string inSuffix) = SqlDialectStrategy.In(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idPropertyInfo.Name]));
            string clause = inPrefix + "{}" + inSuffix;

            sqlBuilder.Append("SELECT");
            AppendColumns<TEntity>(sqlBuilder, "    ", propertyInfos, true, null);
            AppendFromTable<TEntity>(sqlBuilder, string.Empty, null);
            AppendWhereClause(sqlBuilder, string.Empty, clause);
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public abstract SqlTemplate UpdateRangeSqlBuilder<TEntity>() where TEntity : class;

        public virtual SqlTemplate InsertRangeSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;

            StringBuilder sqlBuilder = new();

            sqlBuilder.Append("INSERT INTO ");
            sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuilder.Append(" (");
            AppendColumns<TEntity>(sqlBuilder, "    ", propertyInfos, false, null);
            sqlBuilder.AppendLine();
            sqlBuilder.AppendLine(")");
            sqlBuilder.AppendLine("VALUES");
            sqlBuilder.Append("{}");
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public virtual SqlTemplate DeleteRangeSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuilder = new();
            (string inPrefix, string inSuffix) = SqlDialectStrategy.In(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idPropertyInfo.Name]));
            string clause = inPrefix + "{}" + inSuffix;

            sqlBuilder.Append("DELETE FROM ");
            sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            AppendWhereClause(sqlBuilder, string.Empty, clause);
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public abstract SqlTemplate UpsertRangeSqlBuilder<TEntity>() where TEntity : class;

        public virtual SqlTemplate ExistsSqlBuilder<TEntity>() where TEntity : class
        {
            StringBuilder sqlBuilder = new();

            sqlBuilder.AppendLine("SELECT");
            sqlBuilder.AppendLine("    CASE");
            sqlBuilder.AppendLine("        WHEN EXISTS (");
            sqlBuilder.AppendLine("            SELECT");
            sqlBuilder.Append("                1");
            AppendFromTable<TEntity>(sqlBuilder, "            ", null);
            sqlBuilder.AppendLine("{}");
            sqlBuilder.AppendLine("        )");
            sqlBuilder.AppendLine("        THEN 1");
            sqlBuilder.AppendLine("        ELSE 0");
            sqlBuilder.Append("    END");
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }

        public virtual SqlTemplate CountSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildAggregateSql<TEntity>("COUNT");
        }

        public virtual SqlTemplate AvgSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildAggregateSql<TEntity>("AVG");
        }

        public virtual SqlTemplate SumSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildAggregateSql<TEntity>("SUM");
        }

        public virtual SqlTemplate MinSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildAggregateSql<TEntity>("MIN");
        }

        public virtual SqlTemplate MaxSqlBuilder<TEntity>() where TEntity : class
        {
            return BuildAggregateSql<TEntity>("MAX");
        }
    }
}
