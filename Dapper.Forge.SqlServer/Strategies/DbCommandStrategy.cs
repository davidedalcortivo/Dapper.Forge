using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
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
            WarmUpCache<TEntity>();
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableArray<PropertyInfo> updatePropertyInfos = EntityInfoCache<TEntity>.UpdatePropertyInfos;
            ImmutableArray<PropertyInfo> insertPropertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            StringBuilder updateBuffer = new();
            StringBuilder insertBuffer = new();
            DynamicParameters parameters = new();
            string idParameterName = idPropertyInfo.Name;
            string idParameter = SqlDialectStrategy.RenderParameter(idParameterName);

            parameters.Add(idParameterName, propertyGettersByPropertyName[idParameterName](entity));

            for (int i = 0; i < updatePropertyInfos.Length; i++)
            {
                string parameterName = updatePropertyInfos[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                updateBuffer.Append("        ");
                updateBuffer.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[parameterName]));
                updateBuffer.Append(" = ");
                updateBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName, parameterValue);
                updateBuffer.AppendSeparator(i, updatePropertyInfos.Length, false);
            }

            for (int i = 0; i < insertPropertyInfos.Length; i++)
            {
                string parameterName = insertPropertyInfos[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                insertBuffer.Append("        ");
                insertBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName, parameterValue);
                insertBuffer.AppendSeparator(i, insertPropertyInfos.Length, false);
            }

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertSql.Render(idParameter, updateBuffer, idParameter, insertBuffer);
            return new(sql, parameters);
        }

        public override IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            WarmUpCache<TEntity>();
            return BuildUpsertRangeCommands(entities, batchSize, chunkSize, SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpdateRangeSql, true);
        }

        public override IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            WarmUpCache<TEntity>();
            return BuildUpsertRangeCommands(entities, batchSize, chunkSize, SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertRangeSql, false);
        }
    }
}
