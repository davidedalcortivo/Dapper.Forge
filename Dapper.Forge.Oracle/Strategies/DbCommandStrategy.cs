using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Data.Common;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Oracle.Strategies
{
    internal sealed partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        public static DbCommandStrategy Instance { get; } = new(SqlBuilderStrategy.Instance);

        private DbCommandStrategy(SqlBuilderStrategy strategy) : base(strategy) { }

        public override DbCommandInfo UpsertCommand<TEntity>(DbConnection connection, TEntity entity) where TEntity : class
        {
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            StringBuilder sqlBuffer = new();
            DynamicParameters parameters = new();

            for (int i = 0; i < properties.Length; i++)
            {
                string parameterName = properties[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                sqlBuffer.Append("        ");
                sqlBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName, parameterValue);
                sqlBuffer.Append(" AS ");
                sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[parameterName]));
                sqlBuffer.AppendSeparator(i, properties.Length, false);
            }

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        public override IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            return BuildUpsertRangeCommands(entities, batchSize, SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpdateRangeSql);
        }

        public override IReadOnlyList<DbCommandInfo> InsertRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            string tableName = EntityInfoCache<TEntity>.TableName;
            ImmutableArray <PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            if (batchSize <= 0)
                batchSize = entityArray.Length;

            StringBuilder prefixBuffer = new();
            prefixBuffer.Append("    INTO ");
            prefixBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            prefixBuffer.Append(" (");

            for (int i = 0; i < insertProperties.Length; i++)
            {
                prefixBuffer.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[insertProperties[i].Name]));
                prefixBuffer.AppendSeparator(i, insertProperties.Length, true);
            }

            prefixBuffer.Append(") VALUES (");

            for (int i = 0; i < entityArray.Length; i += batchSize)
            {
                int end = Math.Min(i + batchSize, entityArray.Length);

                StringBuilder sqlBuffer = new();
                DynamicParameters parameters = new();

                for (int j = i; j < end; j++)
                {
                    sqlBuffer.Append(prefixBuffer);

                    for (int k = 0; k < insertProperties.Length; k++)
                    {
                        PropertyInfo property = insertProperties[k];
                        string parameterName = property.Name;
                        object? parameterValue = propertyGettersByPropertyName[parameterName](entityArray[j]);

                        sqlBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName + j, parameterValue);
                        sqlBuffer.AppendSeparator(k, insertProperties.Length, true);
                    }

                    sqlBuffer.Append(')');

                    if (j < end - 1)
                        sqlBuffer.AppendLine();
                }

                string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.InsertRangeSql.Render(sqlBuffer);
                commands.Add(new(sql, parameters));
            }

            return commands;
        }

        public override IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            return BuildUpsertRangeCommands(entities, batchSize, SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertRangeSql);
        }
    }
}
