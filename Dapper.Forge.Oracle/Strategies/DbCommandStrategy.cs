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
            EnsureCache<TEntity>();
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            StringBuilder sqlBuilder = new();
            DynamicParameters parameters = new();

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                string parameterName = propertyInfos[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                sqlBuilder.Append("        ");

                if (parameterValue is null)
                {
                    sqlBuilder.Append(SqlDialectStrategy.NullValue);
                }
                else
                {
                    sqlBuilder.Append(SqlDialectStrategy.RenderParameter(parameterName));
                    parameters.Add(parameterName, parameterValue);
                }

                sqlBuilder.Append(" AS ");
                sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[parameterName]));

                if (i < propertyInfos.Length - 1)
                    sqlBuilder.AppendLine(",");
            }

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertSql.Render(sqlBuilder);
            return new(sql, parameters);
        }

        public override IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            EnsureCache<TEntity>();
            return BuildUpsertRangeCommands(entities, batchSize, SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpdateRangeSql);
        }

        public override IReadOnlyList<DbCommandInfo> InsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            EnsureCache<TEntity>();
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

            StringBuilder prefixBuilder = new();
            prefixBuilder.Append("    INTO ");
            prefixBuilder.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            prefixBuilder.Append(" (");

            for (int i = 0; i < insertPropertyInfos.Length; i++)
            {
                prefixBuilder.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[insertPropertyInfos[i].Name]));

                if (i < insertPropertyInfos.Length - 1)
                    prefixBuilder.Append(", ");
            }

            prefixBuilder.Append(") VALUES (");

            for (int i = 0; i < entityArray.Length; i += batchSize)
            {
                int end = Math.Min(i + batchSize, entityArray.Length);

                StringBuilder sqlBuilder = new();
                DynamicParameters parameters = new();

                for (int j = i; j < end; j++)
                {
                    sqlBuilder.Append(prefixBuilder);

                    for (int k = 0; k < insertPropertyInfos.Length; k++)
                    {
                        PropertyInfo propertyInfo = insertPropertyInfos[k];
                        object? parameterValue = propertyGetters[k](entityArray[j]);

                        if (parameterValue is null)
                            sqlBuilder.Append(SqlDialectStrategy.NullValue);
                        else
                        {
                            string parameterName = propertyInfo.Name + j;

                            sqlBuilder.Append(SqlDialectStrategy.RenderParameter(parameterName));
                            parameters.Add(parameterName, parameterValue);
                        }

                        if (k < insertPropertyInfos.Length - 1)
                            sqlBuilder.Append(", ");
                    }

                    sqlBuilder.Append(')');

                    if (j < end - 1)
                        sqlBuilder.AppendLine();
                }

                string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.InsertRangeSql.Render(sqlBuilder);
                commands.Add(new(sql, parameters));
            }

            return commands;
        }

        public override IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            EnsureCache<TEntity>();
            return BuildUpsertRangeCommands(entities, batchSize, SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertRangeSql);
        }
    }
}
