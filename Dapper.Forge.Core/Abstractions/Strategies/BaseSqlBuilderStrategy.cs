using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    internal abstract partial class BaseSqlBuilderStrategy<TStrategy> : ISqlBuilderStrategy where TStrategy : ISqlDialectStrategy
    {
        public ISqlDialectStrategy SqlDialectStrategy { get; }

        protected BaseSqlBuilderStrategy(TStrategy sqlDialectStrategy)
        {
            SqlDialectStrategy = sqlDialectStrategy;
        }

        public abstract SqlTemplate GetColumnsSqlBuilder<TEntity>() where TEntity : class;

        public virtual SqlTemplate GetAllSqlBuilder<TEntity>() where TEntity : class
        {
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("SELECT");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, properties, null, "    ", true, false);
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, string.Empty, null);
            sqlBuffer.Append(SqlDialectStrategy.Placeholder);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }

        public virtual SqlTemplate GetFirstSqlBuilder<TEntity>() where TEntity : class
        {
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("SELECT");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, properties, null, "    ", true, false);
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, string.Empty, null);
            sqlBuffer.AppendLine(SqlDialectStrategy.Placeholder);
            sqlBuffer.AppendLine("FETCH FIRST");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(SqlDialectStrategy.Placeholder);
            sqlBuffer.Append(" ROWS ONLY");
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }

        public virtual SqlTemplate GetByIdSqlBuilder<TEntity>() where TEntity : class
        {
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuffer = new();
            string clause = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idProperty.Name]) + " = " + SqlDialectStrategy.Placeholder;

            sqlBuffer.Append("SELECT");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, properties, null, "    ", true, false);
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, string.Empty, null);
            sqlBuffer.AppendWhereClause(clause, string.Empty);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }

        public virtual SqlTemplate UpdateSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            string schemaName = EntityInfoCache<TEntity>.SchemaName ?? SqlDialectStrategy.DefaultSchemaName;

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("UPDATE ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(schemaName));
            sqlBuffer.Append('.');
            sqlBuffer.AppendLine(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.AppendLine("SET");
            sqlBuffer.Append(SqlDialectStrategy.Placeholder);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }

        public virtual SqlTemplate InsertSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            string schemaName = EntityInfoCache<TEntity>.SchemaName ?? SqlDialectStrategy.DefaultSchemaName;
            ImmutableArray<PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("INSERT INTO ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(schemaName));
            sqlBuffer.Append('.');
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.Append(" (");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, insertProperties, null, "    ", false, false);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine(")");
            sqlBuffer.AppendLine("VALUES (");
            sqlBuffer.AppendLine(SqlDialectStrategy.Placeholder);
            sqlBuffer.Append(')');
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }

        public virtual SqlTemplate DeleteSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            string schemaName = EntityInfoCache<TEntity>.SchemaName ?? SqlDialectStrategy.DefaultSchemaName;

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("DELETE FROM ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(schemaName));
            sqlBuffer.Append('.');
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.Append(SqlDialectStrategy.Placeholder);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }

        public abstract SqlTemplate UpsertSqlBuilder<TEntity>() where TEntity : class;

        public virtual SqlTemplate GetByIdRangeSqlBuilder<TEntity>() where TEntity : class
        {
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuffer = new();
            (string inPrefix, string inSuffix) = SqlDialectStrategy.In(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idProperty.Name]));
            string clause = inPrefix + SqlDialectStrategy.Placeholder + inSuffix;

            sqlBuffer.Append("SELECT");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, properties, null, "    ", true, false);
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, string.Empty, null);
            sqlBuffer.AppendWhereClause(clause, string.Empty);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }

        public abstract SqlTemplate UpdateRangeSqlBuilder<TEntity>() where TEntity : class;

        public virtual SqlTemplate InsertRangeSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            string schemaName = EntityInfoCache<TEntity>.SchemaName ?? SqlDialectStrategy.DefaultSchemaName;
            ImmutableArray<PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("INSERT INTO ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(schemaName));
            sqlBuffer.Append('.');
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.Append(" (");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, insertProperties, null, "    ", false, false);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine(")");
            sqlBuffer.AppendLine("VALUES");
            sqlBuffer.Append(SqlDialectStrategy.Placeholder);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }

        public virtual SqlTemplate DeleteRangeSqlBuilder<TEntity>() where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            string schemaName = EntityInfoCache<TEntity>.SchemaName ?? SqlDialectStrategy.DefaultSchemaName;
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuffer = new();
            (string inPrefix, string inSuffix) = SqlDialectStrategy.In(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idProperty.Name]));
            string clause = inPrefix + SqlDialectStrategy.Placeholder + inSuffix;

            sqlBuffer.Append("DELETE FROM ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(schemaName));
            sqlBuffer.Append('.');
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.AppendWhereClause(clause, string.Empty);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }

        public abstract SqlTemplate UpsertRangeSqlBuilder<TEntity>() where TEntity : class;

        public virtual SqlTemplate ExistsSqlBuilder<TEntity>() where TEntity : class
        {
            StringBuilder sqlBuffer = new();

            sqlBuffer.AppendLine("SELECT");
            sqlBuffer.AppendLine("    CASE");
            sqlBuffer.AppendLine("        WHEN EXISTS (");
            sqlBuffer.AppendLine("            SELECT");
            sqlBuffer.Append("                1");
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, "            ", null);
            sqlBuffer.AppendLine(SqlDialectStrategy.Placeholder);
            sqlBuffer.AppendLine("        )");
            sqlBuffer.AppendLine("        THEN");
            sqlBuffer.AppendLine("            1");
            sqlBuffer.AppendLine("        ELSE");
            sqlBuffer.AppendLine("            0");
            sqlBuffer.Append("    END");
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
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
