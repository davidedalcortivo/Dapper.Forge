using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.SqlServer.Strategies
{
    internal sealed partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        public static DbCommandStrategy Instance { get; } = new(SqlBuilderStrategy.Instance);

        private DbCommandStrategy(SqlBuilderStrategy strategy) : base(strategy) { }

        public override DbCommandInfo UpsertCommand<TEntity>(DbConnection connection, TEntity entity) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(entity);

            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            ImmutableArray<PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;
            ImmutableArray<PropertyInfo> upsertProperties = EntityInfoCache<TEntity>.UpsertProperties;
            ImmutableArray<PropertyInfo> upsertKeyProperties = EntityInfoCache<TEntity>.UpsertKeyProperties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            StringBuilder updateBuffer = new();
            StringBuilder insertBuffer = new();
            StringBuilder upsertKeyBuffer = new();
            DynamicParameters parameters = new();

            for (int i = 0; i < upsertProperties.Length; i++)
            {
                string parameterName = upsertProperties[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                updateBuffer.Append("    ");
                updateBuffer.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[parameterName]));
                updateBuffer.Append(" = ");
                updateBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName, parameterValue);
                updateBuffer.AppendSeparator(i, upsertProperties.Length, false);
            }

            for (int i = 0; i < insertProperties.Length; i++)
            {
                string parameterName = insertProperties[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                insertBuffer.Append("        ");
                insertBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName, parameterValue);
                insertBuffer.AppendSeparator(i, insertProperties.Length, false);
            }

            for (int i = 0; i < upsertKeyProperties.Length; i++)
            {
                string parameterName = upsertKeyProperties[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                string column = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[parameterName]);
                string parameter = SqlDialectStrategy.RenderParameter(parameterName);

                if (i > 0)
                {
                    upsertKeyBuffer.AppendLine();
                    upsertKeyBuffer.AppendLine("    AND");
                }

                upsertKeyBuffer.AppendLine("    (");
                upsertKeyBuffer.Append("        ");
                upsertKeyBuffer.Append(column);
                upsertKeyBuffer.Append(" = ");
                upsertKeyBuffer.AppendLine(parameter);
                upsertKeyBuffer.Append("        OR (");
                upsertKeyBuffer.Append(column);
                upsertKeyBuffer.Append(" IS NULL AND ");
                upsertKeyBuffer.Append(parameter);
                upsertKeyBuffer.AppendLine(" IS NULL)");
                upsertKeyBuffer.Append("    )");

                parameters.Add(parameterName, parameterValue);
            }

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.UpsertSql.Render(updateBuffer, upsertKeyBuffer, insertBuffer);
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

        public override DbCommandInfo AvgCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            PropertyInfo property = PropertyHelper.GetProperty(selector);
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildSafeAggregateCommand<TEntity>(connection, SqlBuilderCache<TEntity, SqlBuilderStrategy>.AvgSql, property, clause, parameters);
        }

        public override DbCommandInfo AvgCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            PropertyInfo property = PropertyHelper.GetProperty(selector);
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildSafeAggregateCommand<TEntity>(connection, SqlBuilderCache<TEntity, SqlBuilderStrategy>.AvgSql, property, clause, parameters);
        }

        public override DbCommandInfo AvgCommand<TEntity>(DbConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            PropertyInfo property = PropertyHelper.GetProperty<TEntity>(propertyName);
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildSafeAggregateCommand<TEntity>(connection, SqlBuilderCache<TEntity, SqlBuilderStrategy>.AvgSql, property, clause, parameters);
        }

        public override DbCommandInfo AvgCommand<TEntity>(DbConnection connection, string propertyName, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            PropertyInfo property = PropertyHelper.GetProperty<TEntity>(propertyName);
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildSafeAggregateCommand<TEntity>(connection, SqlBuilderCache<TEntity, SqlBuilderStrategy>.AvgSql, property, clause, parameters);
        }

        public override DbCommandInfo SumCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            PropertyInfo property = PropertyHelper.GetProperty(selector);
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildSafeAggregateCommand<TEntity>(connection, SqlBuilderCache<TEntity, SqlBuilderStrategy>.SumSql, property, clause, parameters);
        }

        public override DbCommandInfo SumCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            PropertyInfo property = PropertyHelper.GetProperty(selector);
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildSafeAggregateCommand<TEntity>(connection, SqlBuilderCache<TEntity, SqlBuilderStrategy>.SumSql, property, clause, parameters);
        }

        public override DbCommandInfo SumCommand<TEntity>(DbConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            PropertyInfo property = PropertyHelper.GetProperty<TEntity>(propertyName);
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildSafeAggregateCommand<TEntity>(connection, SqlBuilderCache<TEntity, SqlBuilderStrategy>.SumSql, property, clause, parameters);
        }

        public override DbCommandInfo SumCommand<TEntity>(DbConnection connection, string propertyName, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            PropertyInfo property = PropertyHelper.GetProperty<TEntity>(propertyName);
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildSafeAggregateCommand<TEntity>(connection, SqlBuilderCache<TEntity, SqlBuilderStrategy>.SumSql, property, clause, parameters);
        }
    }
}
