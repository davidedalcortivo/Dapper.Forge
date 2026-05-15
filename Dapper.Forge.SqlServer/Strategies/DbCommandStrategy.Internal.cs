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
        protected override DbCommandInfo BuildGetFirstCommand<TEntity>(string? clause, DynamicParameters? parameters, IEnumerable<SortDescriptor>? sortDescriptors, int take) where TEntity : class
        {
            StringBuilder sqlBuffer = new();
            parameters ??= new();

            sqlBuffer.AppendWhereClause(string.Empty, clause);
            sqlBuffer.AppendSort<TEntity>(SqlDialectStrategy, sortDescriptors, true);

            string takeName = nameof(take);
            parameters.Add(takeName, take);

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.GetFirstSql.Render(SqlDialectStrategy.RenderParameter(takeName), sqlBuffer);
            return new(sql, parameters);
        }

        private List<DbCommandInfo> BuildUpsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize, SqlTemplate sqlTemplate, bool updateOnly) where TEntity : class
        {
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;

            if (batchSize <= 0)
                batchSize = entityArray.Length;

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
                    sqlBuffer.Append("        (");

                    for (int k = 0; k < properties.Length; k++)
                    {
                        string parameterName = properties[k].Name;
                        object? parameterValue = propertyGettersByPropertyName[parameterName](entityArray[j]);

                        sqlBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName + j, parameterValue);
                        sqlBuffer.AppendSeparator(k, properties.Length, true);
                    }

                    sqlBuffer.Append(')');
                    sqlBuffer.AppendSeparator(j, end, false);

                    s++;
                }

                if (updateOnly)
                    batchBuffer.Append(sqlTemplate.Render(sqlBuffer));
                else
                    batchBuffer.Append(sqlTemplate.Render(sqlBuffer, sqlBuffer));

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
    }
}
