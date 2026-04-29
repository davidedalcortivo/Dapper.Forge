using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.PostgreSql.Strategies
{
    internal partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        public static DbCommandStrategy Instance { get; } = new(SqlBuilderStrategy.Instance);

        private DbCommandStrategy(SqlBuilderStrategy strategy) : base(strategy) { }

        public override DbCommandInfo UpsertCommand<TEntity>(TEntity entity) where TEntity : class
        {
            WarmUpCache<TEntity>();
            ImmutableArray<PropertyInfo> insertPropertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            StringBuilder sqlBuffer = new();
            DynamicParameters parameters = new();

            for (int i = 0; i < insertPropertyInfos.Length; i++)
            {
                string parameterName = insertPropertyInfos[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                sqlBuffer.Append("    ");

                if (parameterValue is null)
                {
                    sqlBuffer.Append(SqlDialectStrategy.NullValue);
                }
                else
                {
                    sqlBuffer.Append(SqlDialectStrategy.RenderParameter(parameterName));
                    parameters.Add(parameterName, parameterValue);
                }

                if (i < insertPropertyInfos.Length - 1)
                    sqlBuffer.AppendLine(",");
            }

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        public override IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            WarmUpCache<TEntity>();
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;
            
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            batchSize = batchSize <= 0 ? entityArray.Length : batchSize;
            Func<TEntity, object?>[] propertyGetters = [.. propertyInfos.Select(x => propertyGettersByPropertyName[x.Name])];
            SqlTemplate updateRangeSql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpdateRangeSql;

            for (int i = 0; i < entityArray.Length; i += batchSize)
            {
                int end = Math.Min(i + batchSize, entityArray.Length);

                StringBuilder sqlBuffer = new();
                DynamicParameters parameters = new();

                for (int j = i; j < end; j++)
                {
                    sqlBuffer.Append("        (");

                    for (int k = 0; k < propertyInfos.Length; k++)
                    {
                        object? parameterValue = propertyGetters[k](entityArray[j]);

                        if (parameterValue is null)
                            sqlBuffer.Append(SqlDialectStrategy.NullValue);
                        else
                        {
                            string parameterName = propertyInfos[k].Name + j;

                            sqlBuffer.Append(SqlDialectStrategy.RenderParameter(parameterName));
                            parameters.Add(parameterName, parameterValue);
                        }

                        if (k < propertyInfos.Length - 1)
                            sqlBuffer.Append(", ");
                    }

                    sqlBuffer.Append(')');

                    if (j < end - 1)
                        sqlBuffer.AppendLine(",");
                }

                string sql = updateRangeSql.Render(sqlBuffer);
                commands.Add(new(sql, parameters));
            }

            return commands;
        }

        public override IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            WarmUpCache<TEntity>();
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            ImmutableArray<PropertyInfo> insertPropertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            batchSize = batchSize <= 0 ? entityArray.Length : batchSize;
            SqlTemplate upsertRangeSql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertRangeSql;

            for (int i = 0; i < entityArray.Length; i += batchSize)
            {
                int end = Math.Min(i + batchSize, entityArray.Length);

                StringBuilder sqlBuffer = new();
                DynamicParameters parameters = new();

                for (int j = i; j < end; j++)
                {
                    sqlBuffer.Append("    (");

                    for (int k = 0; k < insertPropertyInfos.Length; k++)
                    {
                        string parameterName = insertPropertyInfos[k].Name;
                        object? parameterValue = propertyGettersByPropertyName[parameterName](entityArray[j]);

                        if (parameterValue is null)
                        {
                            sqlBuffer.Append(SqlDialectStrategy.NullValue);
                        }
                        else
                        {
                            sqlBuffer.Append(SqlDialectStrategy.RenderParameter(parameterName));
                            parameters.Add(parameterName, parameterValue);
                        }

                        if (k < insertPropertyInfos.Length - 1)
                            sqlBuffer.Append(", ");
                    }

                    sqlBuffer.Append(')');

                    if (j < end - 1)
                        sqlBuffer.AppendLine(",");
                }

                string sql = upsertRangeSql.Render(sqlBuffer);
                commands.Add(new(sql, parameters));
            }

            return commands;
        }
    }
}
