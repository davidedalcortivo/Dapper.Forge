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
    internal abstract partial class BaseDbCommandStrategy<TStrategy> : IDbCommandStrategy where TStrategy : ISqlBuilderStrategy
    {
        protected virtual void EnsureIdType<TEntity>(PropertyInfo idProperty, object? id) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            Type idType = id.GetType();

            if (idProperty.PropertyType != idType)
                throw new ArgumentException("The type of the provided id '" + idType + "' does not match the type of the id property '" + idProperty.PropertyType + "' for the entity '" + typeof(TEntity).Name + "'.");
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

        protected virtual DbCommandInfo BuildGetFirstCommand<TEntity>(string? clause, DynamicParameters? parameters, IEnumerable<SortDescriptor>? sortDescriptors, int take) where TEntity : class
        {
            StringBuilder sqlBuffer = new();
            parameters ??= new();

            sqlBuffer.AppendWhereClause(string.Empty, clause);
            sqlBuffer.AppendSort<TEntity>(SqlDialectStrategy, sortDescriptors, true);

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
            sqlBuffer.AppendWhereClause(string.Empty, clause);

            if (useSkip)
            {
                string skipName = nameof(skip);
                string takeName = nameof(take);

                sqlBuffer.AppendSort<TEntity>(SqlDialectStrategy, sortDescriptors, true);
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
                sqlBuffer.AppendSort<TEntity>(SqlDialectStrategy, sortDescriptors, false);
            }

            string sql = SqlBuilderCache<TEntity, TStrategy>.GetAllSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        protected virtual DbCommandInfo BuildUpdateCommand<TEntity>(IReadOnlyList<PropertyInfo> properties, Func<string, object?> propertyGetter, string? clause, DynamicParameters? parameters) where TEntity : class
        {
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuffer = new();
            parameters ??= new();

            for (int i = 0; i < properties.Count; i++)
            {
                string parameterName = properties[i].Name;
                object? parameterValue = propertyGetter(parameterName);

                sqlBuffer.Append("    ");
                sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[parameterName]));
                sqlBuffer.Append(" = ");
                sqlBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName, parameterValue);
                sqlBuffer.AppendSeparator(i, properties.Count, false);
            }

            sqlBuffer.AppendWhereClause(string.Empty, clause);

            string sql = SqlBuilderCache<TEntity, TStrategy>.UpdateSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        protected virtual DbCommandInfo BuildDeleteCommand<TEntity>(string? clause, DynamicParameters? parameters) where TEntity : class
        {
            StringBuilder sqlBuffer = new();
            sqlBuffer.AppendWhereClause(string.Empty, clause);

            string sql = SqlBuilderCache<TEntity, TStrategy>.DeleteSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        protected virtual List<DbCommandInfo> BuildInRangeCommands<TEntity, TPrimaryKey>(ISqlDialectStrategy sqlDialectStrategy, SqlTemplate sqlTemplate, IReadOnlyList<object> idList, int batchSize, int chunkSize, PropertyInfo idProperty, bool useUnion) where TEntity : class where TPrimaryKey : notnull
        {
            List<DbCommandInfo> commands = [];

            if (idList.Count == 0)
                return commands;

            TPrimaryKey[] idArray = new TPrimaryKey[idList.Count];

            for (int i = 0; i < idArray.Length; i++)
                idArray[i] = (TPrimaryKey)idList[i];

            if (batchSize <= 0)
                batchSize = idArray.Length;

            StringBuilder batchBuffer = new();
            DynamicParameters parameters = new();
            int _batchSize = batchSize;
            int j = 0;
            int s = 0;

            for (int i = 0; i < idArray.Length; i += _batchSize)
            {
                if (chunkSize > 0 && chunkSize < batchSize)
                    _batchSize = Math.Min(chunkSize, batchSize - s);

                int end = Math.Min(i + _batchSize, idArray.Length);
                StringBuilder sqlBuffer = new();

                string parameterName = idProperty.Name + "Array" + j;
                sqlBuffer.Append(sqlDialectStrategy.RenderParameter(parameterName));
                parameters.Add(parameterName, idArray[i..end]);

                j++;
                s += end - i;

                batchBuffer.Append(sqlTemplate.RenderWithoutLastTerminator(sqlBuffer));

                if (s >= batchSize || end >= idArray.Length)
                {
                    batchBuffer.Append(sqlDialectStrategy.Terminator);
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
                    batchBuffer.Append(sqlDialectStrategy.Terminator);
                    batchBuffer.AppendLine();
                }
            }

            return commands;
        }

        protected virtual DbCommandInfo BuildExistsCommand<TEntity>(string? clause, DynamicParameters? parameters) where TEntity : class
        {
            StringBuilder sqlBuffer = new();
            sqlBuffer.AppendWhereClause("            ", clause);

            string sql = SqlBuilderCache<TEntity, TStrategy>.ExistsSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        protected virtual DbCommandInfo BuildAggregateCommand<TEntity>(SqlTemplate sqlTemplate, string? propertyName, string? clause, DynamicParameters? parameters) where TEntity : class
        {
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuffer = new();
            string column = propertyName is null ? "*" : SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[propertyName]);

            sqlBuffer.AppendWhereClause(string.Empty, clause);

            string sql = sqlTemplate.Render(column, sqlBuffer);
            return new(sql, parameters);
        }

        protected virtual DbCommandInfo BuildAggregateCommand<TEntity, TSelector>(SqlTemplate sqlTemplate, Expression<Func<TEntity, TSelector>>? selector, string? clause, DynamicParameters? parameters) where TEntity : class
        {
            return BuildAggregateCommand<TEntity>(sqlTemplate, selector is null ? null : PropertyHelper.GetPropertyName(selector), clause, parameters);
        }
    }
}
