using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    public abstract partial class BaseDbCommandStrategy<TStrategy> : IDbCommandStrategy where TStrategy : ISqlBuilderStrategy
    {
        protected virtual void EnsureCache<TEntity>() where TEntity : class
        {
            SqlBuilderCache<TEntity, TStrategy>.Initialize(sqlBuilderStrategy);
        }

        protected virtual void EnsureIdType<TEntity>(PropertyInfo idPropertyInfo, object? id) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            Type idType = id.GetType();

            if (idPropertyInfo.PropertyType != idType)
                throw new ArgumentException("The type of the provided id (" + idType + ") does not match the type of the id property (" + idPropertyInfo.PropertyType + ") for the entity " + typeof(TEntity) + ".");
        }

        protected virtual (string?, DynamicParameters?) Translate<TEntity>(ISqlDialectStrategy sqlDialectStrategy, Expression<Func<TEntity, bool>>? predicate, DynamicParameters? parameters) where TEntity : class
        {
            string? clause = null;

            if (predicate is not null)
                (clause, parameters) = ExpressionTranslator<TEntity>.Translate(SqlDialectStrategy, predicate, parameters);

            return (clause, parameters);
        }

        protected virtual (string?, DynamicParameters?) Translate(ISqlDialectStrategy sqlDialectStrategy, IFilterNode? filterNode, DynamicParameters? parameters)
        {
            string? clause = null;

            if (filterNode is not null)
                (clause, parameters) = FilterNodeTranslator.Translate(SqlDialectStrategy, filterNode, parameters);

            return (clause, parameters);
        }

        protected virtual void AppendClauseAndSort<TEntity>(StringBuilder sqlBuilder, string? clause, IEnumerable<SortDescriptor>? sortDescriptors, bool forceSorting) where TEntity : class
        {
            List<SortDescriptor> sortDescriptorList = sortDescriptors is null ? [] : sortDescriptors.AsList();

            if (clause is not null)
            {
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("WHERE");
                sqlBuilder.Append("    ");
                sqlBuilder.Append(clause);
            }

            if (forceSorting && sortDescriptorList.Count == 0)
                sortDescriptorList.Add(new(EntityInfoCache<TEntity>.IdPropertyInfo.Name));

            if (sortDescriptorList.Count > 0)
            {
                ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("ORDER BY");

                for (int i = 0; i < sortDescriptorList.Count; i++)
                {
                    SortDescriptor sortDescriptor = sortDescriptorList[i];

                    string columnName = columnNamesByPropertyName[sortDescriptor.PropertyName];
                    string sortDirection = sortDescriptor.SortDirection == SortDirection.Ascending ? "ASC" : "DESC";

                    sqlBuilder.Append("    ");
                    sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(columnName));
                    sqlBuilder.Append(' ');
                    sqlBuilder.Append(sortDirection);

                    if (i < sortDescriptorList.Count - 1)
                        sqlBuilder.AppendLine(",");
                }
            }
        }

        protected virtual DbCommandInfo BuildGetFirstCommand<TEntity>(string? clause, DynamicParameters? parameters, IEnumerable<SortDescriptor>? sortDescriptors, int take) where TEntity : class
        {
            StringBuilder sqlBuilder = new();
            parameters ??= new();

            AppendClauseAndSort<TEntity>(sqlBuilder, clause, sortDescriptors, true);

            string takeName = "take";
            parameters.Add(takeName, take);

            string sql = SqlBuilderCache<TEntity, TStrategy>.GetFirstSql.Render(sqlBuilder, SqlDialectStrategy.RenderParameter(takeName));
            return new(sql, parameters);
        }

        protected virtual DbCommandInfo BuildGetPageCommand<TEntity>(string? clause, DynamicParameters? parameters, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take) where TEntity : class
        {
            bool useSkip = skip is not null && skip >= 0;
            bool useTake = take is not null && take >= 0;

            if (!useSkip && useTake)
                return BuildGetFirstCommand<TEntity>(clause, parameters, sortDescriptors, take!.Value);

            StringBuilder sqlBuilder = new();

            if (useSkip)
            {
                AppendClauseAndSort<TEntity>(sqlBuilder, clause, sortDescriptors, true);

                string skipName = "skip";
                string takeName = "take";

                sqlBuilder.Append(SqlDialectStrategy.Pagination(SqlDialectStrategy.RenderParameter(skipName), SqlDialectStrategy.RenderParameter(takeName)));

                parameters ??= new();
                parameters.Add(skipName, skip);

                if (useTake)
                    parameters.Add(takeName, take);
                else
                    parameters.Add(takeName, long.MaxValue);
            }
            else
            {
                AppendClauseAndSort<TEntity>(sqlBuilder, clause, sortDescriptors, false);
            }

            string sql = SqlBuilderCache<TEntity, TStrategy>.GetAllSql.Render(sqlBuilder);
            return new(sql, parameters);
        }

        protected virtual DbCommandInfo BuildUpdateCommand<TEntity>(IReadOnlyList<PropertyInfo> propertyInfos, Func<string, object?> getter, string? clause, DynamicParameters? parameters) where TEntity : class
        {
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuilder = new();
            parameters ??= new();

            for (int i = 0; i < propertyInfos.Count; i++)
            {
                string parameterName = propertyInfos[i].Name;
                object? parameterValue = getter(parameterName);

                sqlBuilder.Append("    ");
                sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[parameterName]));
                sqlBuilder.Append(" = ");

                if (parameterValue is null)
                {
                    sqlBuilder.Append(SqlDialectStrategy.NullValue);
                }
                else
                {
                    sqlBuilder.Append(SqlDialectStrategy.RenderParameter(parameterName));
                    parameters.Add(parameterName, parameterValue);
                }

                if (i < propertyInfos.Count - 1)
                    sqlBuilder.AppendLine(",");
            }

            if (clause is not null)
            {
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("WHERE");
                sqlBuilder.Append("    ");
                sqlBuilder.Append(clause);
            }

            string sql = SqlBuilderCache<TEntity, TStrategy>.UpdateSql.Render(sqlBuilder);
            return new(sql, parameters);
        }

        protected virtual DbCommandInfo BuildDeleteCommand<TEntity>(string? clause, DynamicParameters? parameters) where TEntity : class
        {
            StringBuilder sqlBuilder = new();

            if (clause is not null)
            {
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("WHERE");
                sqlBuilder.Append("    ");
                sqlBuilder.Append(clause);
            }

            string sql = SqlBuilderCache<TEntity, TStrategy>.DeleteSql.Render(sqlBuilder);
            return new(sql, parameters);
        }

        protected virtual List<DbCommandInfo> BuildInRangeCommands<TEntity, TKey>(SqlTemplate sqlTemplate, List<object> idList, int batchSize, int chunkSize, PropertyInfo idPropertyInfo, bool useUnion) where TEntity : class where TKey : notnull
        {
            List<DbCommandInfo> commands = [];

            if (idList.Count == 0)
                return commands;

            TKey[] idArray = new TKey[idList.Count];

            for (int i = 0; i < idArray.Length; i++)
                idArray[i] = (TKey)idList[i];

            batchSize = batchSize <= 0 ? idArray.Length : batchSize;
            int j = 0;

            StringBuilder batchBuilder = new();
            DynamicParameters parameters = new();
            int _batchSize = batchSize;
            int s = 0;

            for (int i = 0; i < idArray.Length; i += _batchSize)
            {
                if (chunkSize > 0 && chunkSize < _batchSize)
                    _batchSize = Math.Min(chunkSize, batchSize - s);

                int end = Math.Min(i + _batchSize, idArray.Length);
                StringBuilder sqlBuilder = new();

                string parameterName = idPropertyInfo.Name + "Array" + j;
                sqlBuilder.Append(SqlDialectStrategy.RenderParameter(parameterName));
                parameters.Add(parameterName, idArray[i..end]);
                j++;

                s += end - i;

                batchBuilder.Append(sqlTemplate.RenderWithoutLastTerminator(sqlBuilder));

                if (s >= batchSize || end >= idArray.Length)
                {
                    batchBuilder.Append(SqlDialectStrategy.Terminator);
                    commands.Add(new(batchBuilder.ToString(), parameters));
                    batchBuilder.Clear();
                    parameters = new();
                    s = 0;
                }
                else if (useUnion)
                {
                    batchBuilder.AppendLine();
                    batchBuilder.AppendLine("UNION ALL");
                }
                else
                {
                    batchBuilder.Append(SqlDialectStrategy.Terminator);
                }
            }

            return commands;
        }

        protected virtual DbCommandInfo BuildExistsCommand<TEntity>(string? clause, DynamicParameters? parameters) where TEntity : class
        {
            StringBuilder sqlBuilder = new();

            if (clause is not null)
            {
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("        WHERE");
                sqlBuilder.Append("            ");
                sqlBuilder.Append(clause);
            }

            string sql = SqlBuilderCache<TEntity, TStrategy>.ExistsSql.Render(sqlBuilder);
            return new(sql, parameters);
        }
    
        protected virtual DbCommandInfo BuildAggregateCommand<TEntity>(SqlTemplate sqlTemplate, string? propertyName, string? clause, DynamicParameters? parameters) where TEntity : class
        {
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuilder = new();
            string column = propertyName is not null ? SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[propertyName]) : "*";

            if (clause is not null)
            {
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("WHERE");
                sqlBuilder.Append("    ");
                sqlBuilder.Append(clause);
            }

            string sql = sqlTemplate.Render(column, sqlBuilder);
            return new(sql, parameters);
        }

        protected virtual DbCommandInfo BuildAggregateCommand<TEntity, TSelector>(SqlTemplate sqlTemplate, Expression<Func<TEntity, TSelector>>? selector, string? clause, DynamicParameters? parameters) where TEntity : class
        {
            return BuildAggregateCommand<TEntity>(sqlTemplate, selector is null ? null : PropertyHelper.GetPropertyName(selector), clause, parameters);
        }
    }
}
