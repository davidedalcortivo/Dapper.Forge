using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.PostgreSql.Strategies
{
    internal partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        protected override DbCommandInfo BuildExistsCommand<TEntity>(string? clause, DynamicParameters? parameters) where TEntity : class
        {
            StringBuilder sqlBuffer = new();
            sqlBuffer.AppendWhereClause("    ", clause);

            string sql = SqlBuilderCache<TEntity, SqlBuilderStrategy>.ExistsSql.Render(sqlBuffer);
            return new(sql, parameters);
        }

        private List<DbCommandInfo> BuildUpsertRangeCommands<TEntity>(ImmutableArray<PropertyInfo> propertyInfos, IEnumerable<TEntity> entities, int batchSize, SqlTemplate sqlTemplate, string indentation) where TEntity : class
        {
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;
            batchSize = batchSize <= 0 ? entityArray.Length : batchSize;

            for (int i = 0; i < entityArray.Length; i += batchSize)
            {
                int end = Math.Min(i + batchSize, entityArray.Length);

                StringBuilder sqlBuffer = new();
                DynamicParameters parameters = new();

                for (int j = i; j < end; j++)
                {
                    sqlBuffer.Append(indentation);
                    sqlBuffer.Append('(');

                    for (int k = 0; k < propertyInfos.Length; k++)
                    {
                        string parameterName = propertyInfos[k].Name;
                        object? parameterValue = propertyGettersByPropertyName[parameterName](entityArray[j]);

                        sqlBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName + j, parameterValue);
                        sqlBuffer.AppendSeparator(k, propertyInfos.Length, true);
                    }

                    sqlBuffer.Append(')');
                    sqlBuffer.AppendSeparator(j, end, false);
                }

                string sql = sqlTemplate.Render(sqlBuffer);
                commands.Add(new(sql, parameters));
            }

            return commands;
        }
    }
}
