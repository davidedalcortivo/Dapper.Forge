using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
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

            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            if (batchSize <= 0)
                batchSize = entityArray.Length;

            for (int i = 0; i < entityArray.Length; i += batchSize)
            {
                int end = Math.Min(i + batchSize, entityArray.Length);

                StringBuilder sqlBuffer = new();
                DynamicParameters parameters = new();

                for (int j = i; j < end; j++)
                {
                    sqlBuffer.Append("    SELECT ");

                    for (int k = 0; k < properties.Length; k++)
                    {
                        PropertyInfo property = properties[k];
                        string parameterName = property.Name;
                        object? parameterValue = propertyGettersByPropertyName[parameterName](entityArray[j]);

                        sqlBuffer.AppendAndBindParameter(SqlDialectStrategy, parameters, parameterName + j, parameterValue);
                        sqlBuffer.AppendSeparator(k, properties.Length, true);
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
