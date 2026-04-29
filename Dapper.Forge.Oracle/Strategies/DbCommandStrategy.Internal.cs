using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Oracle.Strategies
{
    internal partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        private List<DbCommandInfo> BuildUpsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, SqlTemplate sqlTemplate) where TEntity : class
        {
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            batchSize = batchSize <= 0 ? entityArray.Length : batchSize;
            Func<TEntity, object?>[] propertyGetters = [.. propertyInfos.Select(x => propertyGettersByPropertyName[x.Name])];

            for (int i = 0; i < entityArray.Length; i += batchSize)
            {
                int end = Math.Min(i + batchSize, entityArray.Length);

                StringBuilder sqlBuffer = new();
                DynamicParameters parameters = new();

                for (int j = i; j < end; j++)
                {
                    sqlBuffer.Append("    SELECT ");

                    for (int k = 0; k < propertyInfos.Length; k++)
                    {
                        PropertyInfo propertyInfo = propertyInfos[k];
                        object? parameterValue = propertyGetters[k](entityArray[j]);

                        if (parameterValue is null)
                            sqlBuffer.Append(SqlDialectStrategy.NullValue);
                        else
                        {
                            string parameterName = propertyInfo.Name + j;

                            sqlBuffer.Append(SqlDialectStrategy.RenderParameter(parameterName));
                            parameters.Add(parameterName, parameterValue);
                        }

                        if (j == i)
                        {
                            sqlBuffer.Append(" AS ");
                            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[propertyInfo.Name]));
                        }

                        if (k < propertyInfos.Length - 1)
                            sqlBuffer.Append(", ");
                    }

                    sqlBuffer.Append(" FROM dual");

                    if (j < end - 1)
                    {
                        sqlBuffer.AppendLine();
                        sqlBuffer.AppendLine("    UNION ALL");
                    }
                }

                string sql = sqlTemplate.Render(sqlBuffer);
                commands.Add(new(sql, parameters));
            }

            return commands;
        }
    }
}
