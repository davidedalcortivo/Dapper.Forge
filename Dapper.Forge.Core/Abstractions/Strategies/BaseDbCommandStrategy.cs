using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    public abstract partial class BaseDbCommandStrategy<TStrategy> : IDbCommandStrategy where TStrategy : ISqlBuilderStrategy
    {
        public ISqlDialectStrategy SqlDialectStrategy { get; }

        protected readonly TStrategy sqlBuilderStrategy;

        protected BaseDbCommandStrategy(TStrategy sqlBuilderStrategy)
        {
            this.sqlBuilderStrategy = sqlBuilderStrategy;
            SqlDialectStrategy = this.sqlBuilderStrategy.SqlDialectStrategy;
        }

        public void WarmUpCache<TEntity>() where TEntity : class
        {
            SqlBuilderCache<TEntity, TStrategy>.Initialize(sqlBuilderStrategy);
        }

        public virtual DbCommandInfo GetAllCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, sortDescriptors, null, null);
        }

        public virtual DbCommandInfo GetAllCommand<TEntity>(IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, sortDescriptors, null, null);
        }

        public virtual DbCommandInfo GetFirstCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildGetFirstCommand<TEntity>(clause, parameters, sortDescriptors, 1);
        }

        public virtual DbCommandInfo GetFirstCommand<TEntity>(IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildGetFirstCommand<TEntity>(clause, parameters, sortDescriptors, 1);
        }

        public virtual DbCommandInfo GetFirstOrDefaultCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildGetFirstCommand<TEntity>(clause, parameters, sortDescriptors, 1);
        }

        public virtual DbCommandInfo GetFirstOrDefaultCommand<TEntity>(IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildGetFirstCommand<TEntity>(clause, parameters, sortDescriptors, 1);
        }

        public virtual DbCommandInfo GetSingleCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, null, null, null);
        }

        public virtual DbCommandInfo GetSingleCommand<TEntity>(IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, null, null, null);
        }

        public virtual DbCommandInfo GetSingleOrDefaultCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, null, null, null);
        }

        public virtual DbCommandInfo GetSingleOrDefaultCommand<TEntity>(IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, null, null, null);
        }

        public virtual DbCommandInfo GetByIdCommand<TEntity>(object id) where TEntity : class
        {
            WarmUpCache<TEntity>();
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            EnsureIdType<TEntity>(idPropertyInfo, id);

            DynamicParameters parameters = new();
            string idParameterName = idPropertyInfo.Name;

            string sql = SqlBuilderCache<TEntity, TStrategy>.GetByIdSql.Render(SqlDialectStrategy.RenderParameter(idParameterName));
            parameters.Add(idParameterName, id);
            return new(sql, parameters);
        }

        public virtual DbCommandInfo GetPageCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, sortDescriptors, skip, take);
        }

        public virtual DbCommandInfo GetPageCommand<TEntity>(IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildGetPageCommand<TEntity>(clause, parameters, sortDescriptors, skip, take);
        }

        public virtual DbCommandInfo UpdateCommand<TEntity>(TEntity entity) where TEntity : class
        {
            WarmUpCache<TEntity>();
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.UpdatePropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            DynamicParameters parameters = new();
            string idParameterName = idPropertyInfo.Name;

            string clause = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idParameterName]) + " = " + SqlDialectStrategy.RenderParameter(idParameterName);
            parameters.Add(idParameterName, propertyGettersByPropertyName[idParameterName](entity));

            return BuildUpdateCommand<TEntity>(propertyInfos, x => propertyGettersByPropertyName[x](entity), clause, parameters);
        }

        public virtual DbCommandInfo UpdateCommand<TEntity>(object param, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            PropertyInfo[] propertyInfos = ParamPropertyCache.GetProperties(param);
            ImmutableDictionary<string, Func<object, object?>> propertyGetters = ParamGetterCache.GetPropertyGetters(param);

            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildUpdateCommand<TEntity>(propertyInfos, x => propertyGetters[x](param), clause, parameters);
        }

        public virtual DbCommandInfo UpdateCommand<TEntity>(object param, IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            PropertyInfo[] propertyInfos = ParamPropertyCache.GetProperties(param);
            ImmutableDictionary<string, Func<object, object?>> propertyGetters = ParamGetterCache.GetPropertyGetters(param);

            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildUpdateCommand<TEntity>(propertyInfos, x => propertyGetters[x](param), clause, parameters);
        }

        public virtual DbCommandInfo InsertCommand<TEntity>(TEntity entity) where TEntity : class
        {
            WarmUpCache<TEntity>();
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            StringBuilder sqlBuffer = new();
            DynamicParameters parameters = new();

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                string parameterName = propertyInfos[i].Name;
                object? parameterValue = propertyGettersByPropertyName[parameterName](entity);

                sqlBuffer.Append("    ");
                sqlBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName, parameterValue);
                sqlBuffer.AppendSeparator(i, propertyInfos.Length, false);
            }

            string sql = SqlBuilderCache<TEntity, TStrategy>.InsertSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        public virtual DbCommandInfo DeleteCommand<TEntity>(TEntity entity) where TEntity : class
        {
            WarmUpCache<TEntity>();
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            DynamicParameters parameters = new();
            string idParameterName = idPropertyInfo.Name;

            string clause = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idParameterName]) + " = " + SqlDialectStrategy.RenderParameter(idParameterName);
            parameters.Add(idParameterName, propertyGettersByPropertyName[idParameterName](entity));

            return BuildDeleteCommand<TEntity>(clause, parameters);
        }

        public virtual DbCommandInfo DeleteCommand<TEntity>(object id) where TEntity : class
        {
            WarmUpCache<TEntity>();
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            EnsureIdType<TEntity>(idPropertyInfo, id);

            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            DynamicParameters parameters = new();
            string idParameterName = idPropertyInfo.Name;

            string clause = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idParameterName]) + " = " + SqlDialectStrategy.RenderParameter(idParameterName);
            parameters.Add(idParameterName, id);

            return BuildDeleteCommand<TEntity>(clause, parameters);
        }

        public virtual DbCommandInfo DeleteCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildDeleteCommand<TEntity>(clause, parameters);
        }

        public virtual DbCommandInfo DeleteCommand<TEntity>(IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildDeleteCommand<TEntity>(clause, parameters);
        }

        public abstract DbCommandInfo UpsertCommand<TEntity>(TEntity entity) where TEntity : class;

        public virtual IReadOnlyList<DbCommandInfo> GetByIdRangeCommands<TEntity>(IEnumerable ids, int batchSize, int chunkSize) where TEntity : class
        {
            WarmUpCache<TEntity>();
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            List<object> idList;

            if (ids is List<object?> list)
            {
                foreach (object? id in list)
                    EnsureIdType<TEntity>(idPropertyInfo, id);

                idList = list!;
            }
            else
            {
                idList = [];

                foreach (object? id in ids)
                {
                    EnsureIdType<TEntity>(idPropertyInfo, id);
                    idList.Add(id);
                }
            }

            return BuildInRangeInvokerCache.Invoke<TEntity>(this, SqlBuilderCache<TEntity, TStrategy>.GetByIdRangeSql, idList, batchSize, chunkSize, idPropertyInfo, true);
        }

        public abstract IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class;

        public virtual IReadOnlyList<DbCommandInfo> InsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            WarmUpCache<TEntity>();
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;
            SqlTemplate insertRangeSql = SqlBuilderCache<TEntity, TStrategy>.InsertRangeSql;
            batchSize = batchSize <= 0 ? entityArray.Length : batchSize;

            StringBuilder batchBuffer = new();
            DynamicParameters parameters = new();
            int _batchSize = batchSize;
            int s = 0;

            for (int i = 0; i < entityArray.Length; i += _batchSize)
            {
                if (chunkSize > 0 && chunkSize < _batchSize)
                    _batchSize = Math.Min(chunkSize, batchSize - s);

                int end = Math.Min(i + _batchSize, entityArray.Length);
                StringBuilder sqlBuffer = new();

                for (int j = i; j < end; j++)
                {
                    sqlBuffer.Append("    (");

                    for (int k = 0; k < propertyInfos.Length; k++)
                    {
                        string parameterName = propertyInfos[k].Name;
                        object? parameterValue = propertyGettersByPropertyName[parameterName](entityArray[j]);

                        sqlBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName + j, parameterValue);
                        sqlBuffer.AppendSeparator(k, propertyInfos.Length, true);
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
            }

            return commands;
        }

        public virtual IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class
        {
            WarmUpCache<TEntity>();
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            Func<TEntity, object?> propertyGetter = EntityInfoCache<TEntity>.PropertyGettersByPropertyName[idPropertyInfo.Name];

            List<object> idList = [.. entities.Select(x => propertyGetter(x)!)];
            return BuildInRangeInvokerCache.Invoke<TEntity>(this, SqlBuilderCache<TEntity, TStrategy>.DeleteRangeSql, idList, batchSize, chunkSize, idPropertyInfo, false);
        }

        public virtual IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(IEnumerable ids, int batchSize, int chunkSize) where TEntity : class
        {
            WarmUpCache<TEntity>();
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            List<object> idList = [];

            foreach (object? id in ids)
            {
                EnsureIdType<TEntity>(idPropertyInfo, id);
                idList.Add(id);
            }

            return BuildInRangeInvokerCache.Invoke<TEntity>(this, SqlBuilderCache<TEntity, TStrategy>.DeleteRangeSql, idList, batchSize, chunkSize, idPropertyInfo, false);
        }

        public abstract IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class;

        public virtual DbCommandInfo ExistsCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildExistsCommand<TEntity>(clause, parameters);
        }

        public virtual DbCommandInfo ExistsCommand<TEntity>(IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildExistsCommand<TEntity>(clause, parameters);
        }

        public virtual DbCommandInfo CountCommand<TEntity>(Expression<Func<TEntity, object?>>? selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.CountSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo CountCommand<TEntity>(Expression<Func<TEntity, object?>>? selector, IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.CountSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo CountCommand<TEntity>(string? propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.CountSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo CountCommand<TEntity>(string? propertyName, IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.CountSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo AvgCommand<TEntity>(Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.AvgSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo AvgCommand<TEntity>(Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.AvgSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo AvgCommand<TEntity>(string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.AvgSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo AvgCommand<TEntity>(string propertyName, IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.AvgSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo SumCommand<TEntity>(Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.SumSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo SumCommand<TEntity>(Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.SumSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo SumCommand<TEntity>(string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.SumSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo SumCommand<TEntity>(string propertyName, IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.SumSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo MinCommand<TEntity>(Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.MinSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo MinCommand<TEntity>(Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.MinSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo MinCommand<TEntity>(string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.MinSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo MinCommand<TEntity>(string propertyName, IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.MinSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo MaxCommand<TEntity>(Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.MaxSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo MaxCommand<TEntity>(Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand(SqlBuilderCache<TEntity, TStrategy>.MaxSql, selector, clause, parameters);
        }

        public virtual DbCommandInfo MaxCommand<TEntity>(string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, predicate, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.MaxSql, propertyName, clause, parameters);
        }

        public virtual DbCommandInfo MaxCommand<TEntity>(string propertyName, IFilterNode? filterNode) where TEntity : class
        {
            WarmUpCache<TEntity>();
            (string? clause, DynamicParameters? parameters) = Translate(SqlDialectStrategy, filterNode, null);
            return BuildAggregateCommand<TEntity>(SqlBuilderCache<TEntity, TStrategy>.MaxSql, propertyName, clause, parameters);
        }
    }
}
