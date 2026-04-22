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

        protected virtual void EnsureIdType<TEntity>(PropertyInfo idPropertyInfo, object id) where TEntity : class
        {
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

            if (!string.IsNullOrWhiteSpace(clause))
            {
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("WHERE");
                sqlBuilder.Append("    ");
                sqlBuilder.Append(clause);
            }

            if (forceSorting && sortDescriptorList.Count <= 0)
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

            string takeParameter = SqlDialectStrategy.RenderParameter("take");
            parameters.Add(takeParameter, take);

            string sql = SqlBuilderCache<TEntity, TStrategy>.GetFirstSql.Render(sqlBuilder, takeParameter);
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

                string skipParameter = SqlDialectStrategy.RenderParameter("skip");
                string takeParameter = SqlDialectStrategy.RenderParameter("take");

                sqlBuilder.Append(SqlDialectStrategy.Pagination(skipParameter, takeParameter));

                parameters ??= new();
                parameters.Add(skipParameter, skip);

                if (useTake)
                    parameters.Add(takeParameter, take);
                else
                    parameters.Add(takeParameter, long.MaxValue);
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

            if (!string.IsNullOrWhiteSpace(clause))
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

            if (!string.IsNullOrWhiteSpace(clause))
            {
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("WHERE");
                sqlBuilder.Append("    ");
                sqlBuilder.Append(clause);
            }

            string sql = SqlBuilderCache<TEntity, TStrategy>.DeleteSql.Render(sqlBuilder);
            return new(sql, parameters);
        }

        protected virtual List<DbCommandInfo> BuildInRangeCommands<TEntity>(SqlTemplate sqlTemplate, object[] idArray, int batchSize, int chunkSize, PropertyInfo? idPropertyInfo, bool useUnion) where TEntity : class
        {
            List<DbCommandInfo> commands = [];

            if (idArray.Length <= 0)
                return commands;

            if (idPropertyInfo is null)
            {
                idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
                EnsureIdType<TEntity>(idPropertyInfo, idArray[0]);
            }

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

            if (!string.IsNullOrWhiteSpace(clause))
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

            if (!string.IsNullOrWhiteSpace(clause))
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
            if (selector is null)
                return BuildAggregateCommand<TEntity>(sqlTemplate, null, clause, parameters);

            Expression body = selector.Body;

            if (body is UnaryExpression unaryExpr && body.NodeType == ExpressionType.Convert)
                body = unaryExpr.Operand;

            if (body is not MemberExpression memberExpr)
                throw new ArgumentException("The provided expression is not valid. Expected a simple member access expression.", nameof(selector));

            return BuildAggregateCommand<TEntity>(sqlTemplate, memberExpr.Member.Name, clause, parameters);
        }
    }
}
