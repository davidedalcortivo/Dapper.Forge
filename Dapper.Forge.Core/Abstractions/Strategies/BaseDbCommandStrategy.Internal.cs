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

        protected virtual void AppendWhereClauseAndSorting<TEntity>(StringBuilder sqlBuffer, string? clause, IEnumerable<SortDescriptor>? sortDescriptors, bool forceSorting) where TEntity : class
        {
            List<SortDescriptor> sortDescriptorList = sortDescriptors is null ? [] : sortDescriptors.AsList();

            if (clause is not null)
            {
                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("WHERE");
                sqlBuffer.Append("    ");
                sqlBuffer.Append(clause);
            }

            if (forceSorting && sortDescriptorList.Count == 0)
                sortDescriptorList.Add(new(EntityInfoCache<TEntity>.IdPropertyInfo.Name));

            if (sortDescriptorList.Count > 0)
            {
                ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("ORDER BY");

                for (int i = 0; i < sortDescriptorList.Count; i++)
                {
                    SortDescriptor sortDescriptor = sortDescriptorList[i];

                    string columnName = columnNamesByPropertyName[sortDescriptor.PropertyName];
                    string sortDirection = sortDescriptor.SortDirection == SortDirection.Ascending ? "ASC" : "DESC";

                    sqlBuffer.Append("    ");
                    sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(columnName));
                    sqlBuffer.Append(' ');
                    sqlBuffer.Append(sortDirection);

                    if (i < sortDescriptorList.Count - 1)
                        sqlBuffer.AppendLine(",");
                }
            }
        }

        protected virtual DbCommandInfo BuildGetFirstCommand<TEntity>(string? clause, DynamicParameters? parameters, IEnumerable<SortDescriptor>? sortDescriptors, int take) where TEntity : class
        {
            StringBuilder sqlBuffer = new();
            parameters ??= new();

            AppendWhereClauseAndSorting<TEntity>(sqlBuffer, clause, sortDescriptors, true);

            string takeName = nameof(take);
            parameters.Add(takeName, take);

            string sql = SqlBuilderCache<TEntity, TStrategy>.GetFirstSql.Render(sqlBuffer, SqlDialectStrategy.RenderParameter(takeName));
            return new(sql, parameters);
        }

        protected virtual DbCommandInfo BuildGetPageCommand<TEntity>(string? clause, DynamicParameters? parameters, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take) where TEntity : class
        {
            bool useSkip = skip is not null && skip >= 0;
            bool useTake = take is not null && take >= 0;

            if (!useSkip && useTake)
                return BuildGetFirstCommand<TEntity>(clause, parameters, sortDescriptors, take!.Value);

            StringBuilder sqlBuffer = new();

            if (useSkip)
            {
                string skipName = nameof(skip);
                string takeName = nameof(take);

                AppendWhereClauseAndSorting<TEntity>(sqlBuffer, clause, sortDescriptors, true);
                sqlBuffer.Append(SqlDialectStrategy.Pagination(SqlDialectStrategy.RenderParameter(skipName), SqlDialectStrategy.RenderParameter(takeName)));

                parameters ??= new();
                parameters.Add(skipName, skip);

                if (useTake)
                    parameters.Add(takeName, take);
                else
                    parameters.Add(takeName, long.MaxValue);
            }
            else
            {
                AppendWhereClauseAndSorting<TEntity>(sqlBuffer, clause, sortDescriptors, false);
            }

            string sql = SqlBuilderCache<TEntity, TStrategy>.GetAllSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        protected virtual DbCommandInfo BuildUpdateCommand<TEntity>(IReadOnlyList<PropertyInfo> propertyInfos, Func<string, object?> getter, string? clause, DynamicParameters? parameters) where TEntity : class
        {
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuffer = new();
            parameters ??= new();

            for (int i = 0; i < propertyInfos.Count; i++)
            {
                string parameterName = propertyInfos[i].Name;
                object? parameterValue = getter(parameterName);

                sqlBuffer.Append("    ");
                sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[parameterName]));
                sqlBuffer.Append(" = ");

                if (parameterValue is null)
                {
                    sqlBuffer.Append(SqlDialectStrategy.NullValue);
                }
                else
                {
                    sqlBuffer.Append(SqlDialectStrategy.RenderParameter(parameterName));
                    parameters.Add(parameterName, parameterValue);
                }

                if (i < propertyInfos.Count - 1)
                    sqlBuffer.AppendLine(",");
            }

            if (clause is not null)
            {
                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("WHERE");
                sqlBuffer.Append("    ");
                sqlBuffer.Append(clause);
            }

            string sql = SqlBuilderCache<TEntity, TStrategy>.UpdateSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        protected virtual DbCommandInfo BuildDeleteCommand<TEntity>(string? clause, DynamicParameters? parameters) where TEntity : class
        {
            StringBuilder sqlBuffer = new();

            if (clause is not null)
            {
                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("WHERE");
                sqlBuffer.Append("    ");
                sqlBuffer.Append(clause);
            }

            string sql = SqlBuilderCache<TEntity, TStrategy>.DeleteSql.Render(sqlBuffer);
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

            StringBuilder batchBuffer = new();
            DynamicParameters parameters = new();
            int _batchSize = batchSize;
            int s = 0;

            for (int i = 0; i < idArray.Length; i += _batchSize)
            {
                if (chunkSize > 0 && chunkSize < _batchSize)
                    _batchSize = Math.Min(chunkSize, batchSize - s);

                int end = Math.Min(i + _batchSize, idArray.Length);
                StringBuilder sqlBuffer = new();

                string parameterName = idPropertyInfo.Name + "Array" + j;
                sqlBuffer.Append(SqlDialectStrategy.RenderParameter(parameterName));
                parameters.Add(parameterName, idArray[i..end]);
                j++;

                s += end - i;

                batchBuffer.Append(sqlTemplate.RenderWithoutLastTerminator(sqlBuffer));

                if (s >= batchSize || end >= idArray.Length)
                {
                    batchBuffer.Append(SqlDialectStrategy.Terminator);
                    commands.Add(new(batchBuffer.ToString(), parameters));
                    batchBuffer.Clear();
                    parameters = new();
                    s = 0;
                }
                else if (useUnion)
                {
                    batchBuffer.AppendLine();
                    batchBuffer.AppendLine("UNION ALL");
                }
                else
                {
                    batchBuffer.Append(SqlDialectStrategy.Terminator);
                }
            }

            return commands;
        }

        protected virtual DbCommandInfo BuildExistsCommand<TEntity>(string? clause, DynamicParameters? parameters) where TEntity : class
        {
            StringBuilder sqlBuffer = new();

            if (clause is not null)
            {
                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("        WHERE");
                sqlBuffer.Append("            ");
                sqlBuffer.Append(clause);
            }

            string sql = SqlBuilderCache<TEntity, TStrategy>.ExistsSql.Render(sqlBuffer);
            return new(sql, parameters);
        }
    
        protected virtual DbCommandInfo BuildAggregateCommand<TEntity>(SqlTemplate sqlTemplate, string? propertyName, string? clause, DynamicParameters? parameters) where TEntity : class
        {
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuffer = new();
            string column = propertyName is not null ? SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[propertyName]) : "*";

            if (clause is not null)
            {
                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("WHERE");
                sqlBuffer.Append("    ");
                sqlBuffer.Append(clause);
            }

            string sql = sqlTemplate.Render(column, sqlBuffer);
            return new(sql, parameters);
        }

        protected virtual DbCommandInfo BuildAggregateCommand<TEntity, TSelector>(SqlTemplate sqlTemplate, Expression<Func<TEntity, TSelector>>? selector, string? clause, DynamicParameters? parameters) where TEntity : class
        {
            return BuildAggregateCommand<TEntity>(sqlTemplate, selector is null ? null : PropertyHelper.GetPropertyName(selector), clause, parameters);
        }
    }
}
