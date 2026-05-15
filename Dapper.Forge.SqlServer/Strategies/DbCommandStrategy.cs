using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Data.Common;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.SqlServer.Strategies
{
    internal partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        public static DbCommandStrategy Instance { get; } = new(SqlBuilderStrategy.Instance);

        private DbCommandStrategy(SqlBuilderStrategy strategy) : base(strategy) { }

        public override DbCommandInfo UpsertCommand<TEntity>(DbConnection connection, TEntity entity) where TEntity : class
        {
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            ImmutableArray<PropertyInfo> updateProperties = EntityInfoCache<TEntity>.UpdateProperties;
            ImmutableArray<PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            StringBuilder updateBuffer = new();
            StringBuilder insertBuffer = new();
            DynamicParameters parameters = new();
            string idParameterName = idProperty.Name;
            string idParameter = SqlDialectStrategy.RenderParameter(idParameterName);

            parameters.Add(idParameterName, propertyGettersByPropertyName[idParameterName](entity));

            for (int i = 0; i < updateProperties.Length; i++)
            {
                string parameterName = updateProperties[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                updateBuffer.Append("        ");
                updateBuffer.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[parameterName]));
                updateBuffer.Append(" = ");
                updateBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName, parameterValue);
                updateBuffer.AppendSeparator(i, updateProperties.Length, false);
            }

            for (int i = 0; i < insertProperties.Length; i++)
            {
                string parameterName = insertProperties[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                insertBuffer.Append("        ");
                insertBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName, parameterValue);
                insertBuffer.AppendSeparator(i, insertProperties.Length, false);
            }

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertSql.Render(idParameter, updateBuffer, idParameter, insertBuffer);
            return new(sql, parameters);
        }

        public override IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            return BuildUpsertRangeCommands(entities, batchSize, chunkSize, SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpdateRangeSql, true);
        }

        public override IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            return BuildUpsertRangeCommands(entities, batchSize, chunkSize, SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertRangeSql, false);
        }
    }
}
