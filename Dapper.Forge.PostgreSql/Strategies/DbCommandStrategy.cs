using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Data.Common;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.PostgreSql.Strategies
{
    internal partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        public static DbCommandStrategy Instance { get; } = new(SqlBuilderStrategy.Instance);

        private DbCommandStrategy(SqlBuilderStrategy strategy) : base(strategy) { }

        public override DbCommandInfo UpsertCommand<TEntity>(DbConnection connection, TEntity entity) where TEntity : class
        {
            ImmutableArray<PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            StringBuilder sqlBuffer = new();
            DynamicParameters parameters = new();

            for (int i = 0; i < insertProperties.Length; i++)
            {
                string parameterName = insertProperties[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                sqlBuffer.Append("    ");
                sqlBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName, parameterValue);
                sqlBuffer.AppendSeparator(i, insertProperties.Length, false);
            }

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        public override IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            string tableName = EntityInfoCache<TEntity>.TableName;
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
            ImmutableDictionary<string, PropertyInfo> propertiesByColumnName = EntityInfoCache<TEntity>.PropertiesByColumnName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;
            SqlTemplate updateRangeSql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpdateRangeSql;

            string table = SqlDialectStrategy.RenderIdentifier(tableName);
            string connectionId = SqlDialectStrategy.GetConnectionId(connection);
            IReadOnlyList<DbColumnInfo> columns = DbColumnInfoCache<TEntity>.GetValue(connectionId);

            if (properties.Length != columns.Count)
                throw new InvalidOperationException("The entity '" + typeof(TEntity).Name + "' does not match the database table schema.");

            PropertyInfo[] sortProperties = new PropertyInfo[properties.Length];
            StringBuilder columnBuffer = new();

            for (int i = 0; i < sortProperties.Length; i++)
                sortProperties[i] = propertiesByColumnName[columns[i].Name];

            columnBuffer.AppendColumns<TEntity>(SqlDialectStrategy, string.Empty, sortProperties, null, false, true);

            if (batchSize <= 0)
                batchSize = entityArray.Length;

            for (int i = 0; i < entityArray.Length; i += batchSize)
            {
                int end = Math.Min(i + batchSize, entityArray.Length);

                StringBuilder valueBuffer = new();
                DynamicParameters parameters = new();

                for (int j = i; j < end; j++)
                {
                    valueBuffer.Append("            (ROW(");

                    for (int k = 0; k < sortProperties.Length; k++)
                    {
                        string parameterName = sortProperties[k].Name;
                        object? parameterValue = propertyGettersByPropertyName[parameterName](entityArray[j]);

                        valueBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName + j, parameterValue);
                        valueBuffer.AppendSeparator(k, sortProperties.Length, true);
                    }

                    valueBuffer.Append(")::");
                    valueBuffer.Append(table);
                    valueBuffer.Append(')');
                    valueBuffer.AppendSeparator(j, end, false);
                }

                string sql = updateRangeSql.Render(valueBuffer, columnBuffer);
                commands.Add(new(sql, parameters));
            }

            return commands;
        }

        public override IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            ImmutableArray<PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;
            SqlTemplate upsertRangeSql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertRangeSql;

            if (batchSize <= 0)
                batchSize = entityArray.Length;

            for (int i = 0; i < entityArray.Length; i += batchSize)
            {
                int end = Math.Min(i + batchSize, entityArray.Length);

                StringBuilder sqlBuffer = new();
                DynamicParameters parameters = new();

                for (int j = i; j < end; j++)
                {
                    sqlBuffer.Append("    (");

                    for (int k = 0; k < insertProperties.Length; k++)
                    {
                        string parameterName = insertProperties[k].Name;
                        object? parameterValue = propertyGettersByPropertyName[parameterName](entityArray[j]);

                        sqlBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName + j, parameterValue);
                        sqlBuffer.AppendSeparator(k, insertProperties.Length, true);
                    }

                    sqlBuffer.Append(')');
                    sqlBuffer.AppendSeparator(j, end, false);
                }

                string sql = upsertRangeSql.Render(sqlBuffer);
                commands.Add(new(sql, parameters));
            }

            return commands;
        }
    }
}
