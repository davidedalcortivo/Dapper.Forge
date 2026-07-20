using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Data.Common;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Oracle.Strategies
{
    internal sealed partial class DbCommandStrategy : BaseDbCommandStrategy<SqlBuilderStrategy>
    {
        private List<DbCommandInfo> BuildUpsertRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, IReadOnlyList<PropertyInfo> properties, int batchSize, SqlTemplate sqlTemplate, string indentation) where TEntity : class
        {
            TEntity[] entityArray = entities as TEntity[] ?? [.. entities];
            List<DbCommandInfo> commands = [];

            if (entityArray.Length == 0)
                return commands;

            ImmutableArray<PropertyInfo> _properties = EntityInfoCache<TEntity>.Properties;
            ImmutableDictionary<string, Func<TEntity, object?>> propertyGettersByPropertyName = EntityInfoCache<TEntity>.PropertyGettersByPropertyName;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;
            string connectionId = SqlDialectStrategy.GetConnectionId(connection);
            IDictionary<string, DbColumnInfo> columns = DbColumnInfoCache<TEntity>.GetDictValue(connectionId);

            if (_properties.Length != columns.Count)
                throw new InvalidOperationException($"Database table schema mismatch for entity '{typeof(TEntity).Name}'. Expected {_properties.Length} mapped properties but found {columns.Count} database columns.");

            foreach (PropertyInfo property in _properties)
            {
                string columnName = columnNamesByPropertyName[property.Name];

                if (!columns.TryGetValue(columnName, out DbColumnInfo? _))
                    throw new InvalidOperationException($"Database column mapping mismatch for entity '{typeof(TEntity).Name}'. Database column '{columnName}' is not mapped to any entity property.");
            }

            if (batchSize <= 0)
                batchSize = entityArray.Length;

            for (int i = 0; i < entityArray.Length; i += batchSize)
            {
                int end = Math.Min(i + batchSize, entityArray.Length);

                StringBuilder sqlBuffer = new();
                DynamicParameters parameters = new();

                for (int j = i; j < end; j++)
                {
                    sqlBuffer.Append(indentation);
                    sqlBuffer.Append("SELECT ");

                    for (int k = 0; k < properties.Count; k++)
                    {
                        PropertyInfo property = properties[k];
                        string propertyName = property.Name;
                        string parameterName = propertyName + j;
                        object? parameterValue = propertyGettersByPropertyName[propertyName](entityArray[j]);
                        DbColumnInfo column = columns[columnNamesByPropertyName[propertyName]];

                        if (column.IsCastable ?? false)
                            sqlBuffer.Append(column.CastExpression!.Replace(SqlDialectStrategy.Placeholder, SqlDialectStrategy.RenderParameter(parameterName)));
                        else
                            sqlBuffer.Append(SqlDialectStrategy.RenderParameter(parameterName));

                        parameters.Add(parameterName, parameterValue);

                        sqlBuffer.Append(" AS ");
                        sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(column.Name));
                        sqlBuffer.AppendSeparator(k, properties.Count, true);
                    }

                    sqlBuffer.Append(" FROM DUAL");

                    if (j < end - 1)
                    {
                        sqlBuffer.AppendLine();
                        sqlBuffer.Append(indentation);
                        sqlBuffer.AppendLine("UNION ALL");
                    }
                }

                string sql = sqlTemplate.Render(sqlBuffer);
                commands.Add(new(sql, parameters));
            }

            return commands;
        }
    }
}
