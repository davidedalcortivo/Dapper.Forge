using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.SqlServer.Strategies
{
    internal partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        public static DbCommandStrategy Instance { get; } = new(SqlBuilderStrategy.Instance);

        private DbCommandStrategy(SqlBuilderStrategy strategy) : base(strategy) { }

        public override DbCommandInfo UpsertCommand<TEntity>(TEntity entity) where TEntity : class
        {
            EnsureCache<TEntity>();
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableArray<PropertyInfo> updatePropertyInfos = EntityInfoCache<TEntity>.UpdatePropertyInfos;
            ImmutableArray<PropertyInfo> insertPropertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            StringBuilder updateBuilder = new();
            StringBuilder insertBuilder = new();
            DynamicParameters parameters = new();
            string idParameterName = idPropertyInfo.Name;
            string idParameter = SqlDialectStrategy.RenderParameter(idParameterName);

            parameters.Add(idParameterName, propertyGettersByPropertyName[idParameterName](entity));

            for (int i = 0; i < updatePropertyInfos.Length; i++)
            {
                string parameterName = updatePropertyInfos[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                updateBuilder.Append("        ");
                updateBuilder.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[parameterName]));
                updateBuilder.Append(" = ");

                if (parameterValue is null)
                {
                    updateBuilder.Append(SqlDialectStrategy.NullValue);
                }
                else
                {
                    updateBuilder.Append(SqlDialectStrategy.RenderParameter(parameterName));
                    parameters.Add(parameterName, parameterValue);
                }

                if (i < updatePropertyInfos.Length - 1)
                    updateBuilder.AppendLine(",");
            }

            for (int i = 0; i < insertPropertyInfos.Length; i++)
            {
                string parameterName = insertPropertyInfos[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                insertBuilder.Append("        ");

                if (parameterValue is null)
                    insertBuilder.Append(SqlDialectStrategy.NullValue);
                else
                    insertBuilder.Append(SqlDialectStrategy.RenderParameter(parameterName));

                if (i < insertPropertyInfos.Length - 1)
                    insertBuilder.AppendLine(",");
            }

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertSql.Render(idParameter, updateBuilder, idParameter, insertBuilder);
            return new(sql, parameters);
        }

        public override IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            EnsureCache<TEntity>();
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            batchSize = batchSize <= 0 ? entityArray.Length : batchSize;
            Func<TEntity, object?>[] propertyGetters = [.. propertyInfos.Select(x => propertyGettersByPropertyName[x.Name])];
            SqlTemplate updateRangeSql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpdateRangeSql;

            StringBuilder batchBuilder = new();
            DynamicParameters parameters = new();
            int _batchSize = batchSize;
            int s = 0;

            for (int i = 0; i < entityArray.Length; i += _batchSize)
            {
                if (chunkSize > 0 && chunkSize < _batchSize)
                    _batchSize = Math.Min(chunkSize, batchSize - s);

                int end = Math.Min(i + _batchSize, entityArray.Length);
                StringBuilder sqlBuilder = new();

                for (int j = i; j < end; j++)
                {
                    sqlBuilder.Append("        (");

                    for (int k = 0; k < propertyInfos.Length; k++)
                    {
                        object? parameterValue = propertyGetters[k](entityArray[j]);

                        if (parameterValue is null)
                        {
                            sqlBuilder.Append(SqlDialectStrategy.NullValue);
                        }
                        else
                        {
                            string parameterName = propertyInfos[k].Name + j;

                            sqlBuilder.Append(SqlDialectStrategy.RenderParameter(parameterName));
                            parameters.Add(parameterName, parameterValue);
                        }

                        if (k < propertyInfos.Length - 1)
                            sqlBuilder.Append(", ");
                    }

                    sqlBuilder.Append(')');

                    if (j < end - 1)
                        sqlBuilder.AppendLine(",");

                    s++;
                }

                batchBuilder.Append(updateRangeSql.Render(sqlBuilder));

                if (s >= batchSize || end >= entityArray.Length)
                {
                    commands.Add(new(batchBuilder.ToString(), parameters));
                    batchBuilder.Clear();
                    parameters = new();
                    s = 0;
                }
            }

            return commands;
        }

        public override IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            EnsureCache<TEntity>();
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            batchSize = batchSize <= 0 ? entityArray.Length : batchSize;
            Func<TEntity, object?>[] propertyGetters = [.. propertyInfos.Select(x => propertyGettersByPropertyName[x.Name])];
            SqlTemplate upsertRangeSql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertRangeSql;

            StringBuilder batchBuilder = new();
            DynamicParameters parameters = new();
            int _batchSize = batchSize;
            int s = 0;

            for (int i = 0; i < entityArray.Length; i += _batchSize)
            {
                if (chunkSize > 0 && chunkSize < _batchSize)
                    _batchSize = Math.Min(chunkSize, batchSize - s);

                int end = Math.Min(i + _batchSize, entityArray.Length);
                StringBuilder sqlBuilder = new();

                for (int j = i; j < end; j++)
                {
                    sqlBuilder.Append("        (");

                    for (int k = 0; k < propertyInfos.Length; k++)
                    {
                        object? parameterValue = propertyGetters[k](entityArray[j]);

                        if (parameterValue is null)
                        {
                            sqlBuilder.Append(SqlDialectStrategy.NullValue);
                        }
                        else
                        {
                            string parameterName = propertyInfos[k].Name + j;

                            sqlBuilder.Append(SqlDialectStrategy.RenderParameter(parameterName));
                            parameters.Add(parameterName, parameterValue);
                        }

                        if (k < propertyInfos.Length - 1)
                            sqlBuilder.Append(", ");
                    }

                    sqlBuilder.Append(')');

                    if (j < end - 1)
                        sqlBuilder.AppendLine(",");

                    s++;
                }

                batchBuilder.Append(upsertRangeSql.Render(sqlBuilder, sqlBuilder));

                if (s >= batchSize || end >= entityArray.Length)
                {
                    commands.Add(new(batchBuilder.ToString(), parameters));
                    batchBuilder.Clear();
                    parameters = new();
                    s = 0;
                }
            }

            return commands;
        }
    }
}
