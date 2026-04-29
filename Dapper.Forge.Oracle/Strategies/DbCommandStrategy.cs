using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Oracle.Strategies
{
    internal partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        public static DbCommandStrategy Instance { get; } = new(SqlBuilderStrategy.Instance);

        private DbCommandStrategy(SqlBuilderStrategy strategy) : base(strategy) { }

        public override DbCommandInfo UpsertCommand<TEntity>(TEntity entity) where TEntity : class
        {
            WarmUpCache<TEntity>();
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            StringBuilder sqlBuffer = new();
            DynamicParameters parameters = new();

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                string parameterName = propertyInfos[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                sqlBuffer.Append("        ");

                if (parameterValue is null)
                {
                    sqlBuffer.Append(SqlDialectStrategy.NullValue);
                }
                else
                {
                    sqlBuffer.Append(SqlDialectStrategy.RenderParameter(parameterName));
                    parameters.Add(parameterName, parameterValue);
                }

                sqlBuffer.Append(" AS ");
                sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[parameterName]));

                if (i < propertyInfos.Length - 1)
                    sqlBuffer.AppendLine(",");
            }

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        public override IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            WarmUpCache<TEntity>();
            return BuildUpsertRangeCommands(entities, batchSize, SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpdateRangeSql);
        }

        public override IReadOnlyList<DbCommandInfo> InsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            WarmUpCache<TEntity>();
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            string tableName = EntityInfoCache<TEntity>.TableName;
            ImmutableArray <PropertyInfo> insertPropertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            batchSize = batchSize <= 0 ? entityArray.Length : batchSize;
            Func<TEntity, object?>[] propertyGetters = [.. insertPropertyInfos.Select(x => propertyGettersByPropertyName[x.Name])];

            StringBuilder prefixBuffer = new();
            prefixBuffer.Append("    INTO ");
            prefixBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            prefixBuffer.Append(" (");

            for (int i = 0; i < insertPropertyInfos.Length; i++)
            {
                prefixBuffer.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[insertPropertyInfos[i].Name]));

                if (i < insertPropertyInfos.Length - 1)
                    prefixBuffer.Append(", ");
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

                    for (int k = 0; k < insertPropertyInfos.Length; k++)
                    {
                        PropertyInfo propertyInfo = insertPropertyInfos[k];
                        object? parameterValue = propertyGetters[k](entityArray[j]);

                        if (parameterValue is null)
                            sqlBuffer.Append(SqlDialectStrategy.NullValue);
                        else
                        {
                            string parameterName = propertyInfo.Name + j;

                            sqlBuffer.Append(SqlDialectStrategy.RenderParameter(parameterName));
                            parameters.Add(parameterName, parameterValue);
                        }

                        if (k < insertPropertyInfos.Length - 1)
                            sqlBuffer.Append(", ");
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

        public override IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            WarmUpCache<TEntity>();
            return BuildUpsertRangeCommands(entities, batchSize, SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertRangeSql);
        }
    }
}
