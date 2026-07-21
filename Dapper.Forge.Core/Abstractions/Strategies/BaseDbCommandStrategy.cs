using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections;
using System.Collections.Immutable;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    internal abstract partial class BaseDbCommandStrategy<TStrategy> : IDbCommandStrategy where TStrategy : ISqlBuilderStrategy
    {
        public ISqlDialectStrategy SqlDialectStrategy { get; }

        protected readonly TStrategy sqlBuilderStrategy;

        protected BaseDbCommandStrategy(TStrategy sqlBuilderStrategy)
        {
            this.sqlBuilderStrategy = sqlBuilderStrategy;
            SqlDialectStrategy = this.sqlBuilderStrategy.SqlDialectStrategy;
        }

        public virtual void LoadRuntimeCache<TEntity>(DbConnection connection) where TEntity : class
        {
            SqlDialectStrategy.Initialize(connection);
            SqlBuilderCache<TEntity, TStrategy>.Initialize(sqlBuilderStrategy);
        }

        public virtual DbCommandInfo GetColumnsCommand<TEntity>(DbConnection connection) where TEntity : class
        {
            string table = EntityInfoCache<TEntity>.TableName;
            string schema = EntityInfoCache<TEntity>.SchemaName ?? SqlDialectStrategy.DefaultSchemaName;

            DynamicParameters parameters = new();

            string schemaName = "SchemaName";
            string tableName = "TableName";
            parameters.Add(schemaName, schema);
            parameters.Add(tableName, table);

            string sql = SqlBuilderCache<TEntity, TStrategy>.GetColumnsSql.Render(SqlDialectStrategy.RenderParameter(schemaName), SqlDialectStrategy.RenderParameter(tableName));
            return new(sql, parameters);
        }

        public virtual DbCommandInfo GetAllCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, sortDescriptors, null, null);
        }

        public virtual DbCommandInfo GetAllCommand<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, sortDescriptors, null, null);
        }

        public virtual DbCommandInfo GetFirstCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildGetFirstCommand<TEntity>(clause, parameters, sortDescriptors, 1);
        }

        public virtual DbCommandInfo GetFirstCommand<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildGetFirstCommand<TEntity>(clause, parameters, sortDescriptors, 1);
        }

        public virtual DbCommandInfo GetFirstOrDefaultCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildGetFirstCommand<TEntity>(clause, parameters, sortDescriptors, 1);
        }

        public virtual DbCommandInfo GetFirstOrDefaultCommand<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildGetFirstCommand<TEntity>(clause, parameters, sortDescriptors, 1);
        }

        public virtual DbCommandInfo GetSingleCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, null, null, null);
        }

        public virtual DbCommandInfo GetSingleCommand<TEntity>(DbConnection connection, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, null, null, null);
        }

        public virtual DbCommandInfo GetSingleOrDefaultCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, null, null, null);
        }

        public virtual DbCommandInfo GetSingleOrDefaultCommand<TEntity>(DbConnection connection, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, null, null, null);
        }

        public virtual DbCommandInfo GetByIdCommand<TEntity>(DbConnection connection, object id) where TEntity : class
        {
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            EnsureIdType<TEntity>(idProperty, id);

            DynamicParameters parameters = new();
            string idParameterName = idProperty.Name;

            string sql = SqlBuilderCache<TEntity, TStrategy>.GetByIdSql.Render(SqlDialectStrategy.RenderParameter(idParameterName));
            parameters.Add(idParameterName, id);
            return new(sql, parameters);
        }

        public virtual DbCommandInfo GetPageCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, sortDescriptors, skip, take);
        }

        public virtual DbCommandInfo GetPageCommand<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, sortDescriptors, skip, take);
        }

        public virtual DbCommandInfo UpdateCommand<TEntity>(DbConnection connection, TEntity entity) where TEntity : class
        {
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            ImmutableArray<PropertyInfo> updateProperties = EntityInfoCache<TEntity>.UpdateProperties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            DynamicParameters parameters = new();
            string idParameterName = idProperty.Name;

            string clause = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idParameterName]) + " = " + SqlDialectStrategy.RenderParameter(idParameterName);
            parameters.Add(idParameterName, propertyGettersByPropertyName[idParameterName](entity));

            return BuildUpdateCommand<TEntity>(updateProperties, x => propertyGettersByPropertyName[x](entity), clause, parameters);
        }

        public virtual DbCommandInfo UpdateCommand<TEntity>(DbConnection connection, object param, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            PropertyInfo[] paramProperties = ParamPropertyCache.Get(param);
            ImmutableDictionary<string, Func<object, object?>> paramPropertyGetters = ParamGetterCache.Get(param);

            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildUpdateCommand<TEntity>(paramProperties, x => paramPropertyGetters[x](param), clause, parameters);
        }

        public virtual DbCommandInfo UpdateCommand<TEntity>(DbConnection connection, object param, IFilterNode? filterNode) where TEntity : class
        {
            PropertyInfo[] paramProperties = ParamPropertyCache.Get(param);
            ImmutableDictionary<string, Func<object, object?>> paramPropertyGetters = ParamGetterCache.Get(param);

            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildUpdateCommand<TEntity>(paramProperties, x => paramPropertyGetters[x](param), clause, parameters);
        }

        public virtual DbCommandInfo InsertCommand<TEntity>(DbConnection connection, TEntity entity) where TEntity : class
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

            string sql = SqlBuilderCache<TEntity, TStrategy>.InsertSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        public virtual DbCommandInfo DeleteCommand<TEntity>(DbConnection connection, TEntity entity) where TEntity : class
        {
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            DynamicParameters parameters = new();
            string idParameterName = idProperty.Name;

            string clause = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idParameterName]) + " = " + SqlDialectStrategy.RenderParameter(idParameterName);
            parameters.Add(idParameterName, propertyGettersByPropertyName[idParameterName](entity));

            return BuildDeleteCommand<TEntity>(clause, parameters);
        }

        public virtual DbCommandInfo DeleteCommand<TEntity>(DbConnection connection, object id) where TEntity : class
        {
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            EnsureIdType<TEntity>(idProperty, id);

            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            DynamicParameters parameters = new();
            string idParameterName = idProperty.Name;

            string clause = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idParameterName]) + " = " + SqlDialectStrategy.RenderParameter(idParameterName);
            parameters.Add(idParameterName, id);

            return BuildDeleteCommand<TEntity>(clause, parameters);
        }

        public virtual DbCommandInfo DeleteCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildDeleteCommand<TEntity>(clause, parameters);
        }

        public virtual DbCommandInfo DeleteCommand<TEntity>(DbConnection connection, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildDeleteCommand<TEntity>(clause, parameters);
        }

        public abstract DbCommandInfo UpsertCommand<TEntity>(DbConnection connection, TEntity entity) where TEntity : class;

        public virtual IReadOnlyList<DbCommandInfo> GetByIdRangeCommands<TEntity>(DbConnection connection, IEnumerable ids, int batchSize, int chunkSize) where TEntity : class
        {
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            List<object> idList = [];

            foreach (object? id in ids)
            {
                EnsureIdType<TEntity>(idProperty, id);
                idList.Add(id);
            }

            return BuildInRangeInvokerCache.Invoke<TEntity>(this, SqlDialectStrategy, SqlBuilderCache<TEntity, TStrategy>.GetByIdRangeSql, idList, batchSize, chunkSize, idProperty, true);
        }

        public abstract IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class;

        public virtual IReadOnlyList<DbCommandInfo> InsertRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            ImmutableArray<PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;
            SqlTemplate insertRangeSql = SqlBuilderCache<TEntity, TStrategy>.InsertRangeSql;

            if (batchSize <= 0)
                batchSize = entityArray.Length;

            StringBuilder batchBuffer = new();
            DynamicParameters parameters = new();
            int _batchSize = batchSize;
            int s = 0;

            for (int i = 0; i < entityArray.Length; i += _batchSize)
            {
                if (chunkSize > 0 && chunkSize < batchSize)
                    _batchSize = Math.Min(chunkSize, batchSize - s);

                int end = Math.Min(i + _batchSize, entityArray.Length);
                StringBuilder sqlBuffer = new();

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

                    s++;
                }

                batchBuffer.Append(insertRangeSql.Render(sqlBuffer));

                if (s >= batchSize || end >= entityArray.Length)
                {
                    commands.Add(new(batchBuffer.ToString(), parameters));
                    batchBuffer.Clear();
                    parameters = new();
                    s = 0;
                }
                else
                {
                    batchBuffer.AppendLine();
                }
            }

            return commands;
        }

        public virtual IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            Func<TEntity, object?> propertyGetter = EntityInfoCache<TEntity>.PropertyGettersByPropertyName[idProperty.Name];
            List<object> idList = [.. entities.Select(x => propertyGetter(x)!)];

            return BuildInRangeInvokerCache.Invoke<TEntity>(this, SqlDialectStrategy, SqlBuilderCache<TEntity, TStrategy>.DeleteRangeSql, idList, batchSize, chunkSize, idProperty, false);
        }

        public virtual IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(DbConnection connection, IEnumerable ids, int batchSize, int chunkSize) where TEntity : class
        {
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            List<object> idList = [];

            foreach (object? id in ids)
            {
                EnsureIdType<TEntity>(idProperty, id);
                idList.Add(id);
            }

            return BuildInRangeInvokerCache.Invoke<TEntity>(this, SqlDialectStrategy, SqlBuilderCache<TEntity, TStrategy>.DeleteRangeSql, idList, batchSize, chunkSize, idProperty, false);
        }

        public abstract IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class;

        public virtual DbCommandInfo ExistsCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildExistsCommand<TEntity>(clause, parameters);
        }

        public virtual DbCommandInfo ExistsCommand<TEntity>(DbConnection connection, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildExistsCommand<TEntity>(clause, parameters);
        }

        public virtual DbCommandInfo CountCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, object?>>? selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.CountSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo CountCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, object?>>? selector, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.CountSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo CountCommand<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.CountSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo CountCommand<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.CountSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo AvgCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.AvgSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo AvgCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.AvgSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo AvgCommand<TEntity>(DbConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.AvgSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo AvgCommand<TEntity>(DbConnection connection, string propertyName, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.AvgSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo SumCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.SumSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo SumCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.SumSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo SumCommand<TEntity>(DbConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.SumSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo SumCommand<TEntity>(DbConnection connection, string propertyName, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.SumSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo MinCommand<TEntity, TProperty>(DbConnection connection, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.MinSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo MinCommand<TEntity, TProperty>(DbConnection connection, Expression<Func<TEntity, TProperty?>> selector, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.MinSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo MinCommand<TEntity>(DbConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.MinSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo MinCommand<TEntity>(DbConnection connection, string propertyName, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.MinSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo MaxCommand<TEntity, TProperty>(DbConnection connection, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.MaxSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo MaxCommand<TEntity, TProperty>(DbConnection connection, Expression<Func<TEntity, TProperty?>> selector, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.MaxSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo MaxCommand<TEntity>(DbConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.MaxSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo MaxCommand<TEntity>(DbConnection connection, string propertyName, IFilterNode? filterNode) where TEntity : class
        {
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.MaxSql, propertyName, clause, parameters);
        }
    }
}
