using DapperForge.Core.Abstractions.Strategies;
using DapperForge.Core.Caching;
using DapperForge.Core.Models;
using DapperForge.Core.Utilities;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace DapperForge.Oracle.Strategies
{
    internal sealed partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
    {
        private SqlTemplate BuildUpsertSql<TEntity>(bool isRange, bool updateOnly) where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            string schemaName = EntityInfoCache<TEntity>.SchemaName ?? SqlDialectStrategy.DefaultSchemaName;
            ImmutableArray<PropertyInfo> properties = updateOnly ? EntityInfoCache<TEntity>.UpdateProperties : EntityInfoCache<TEntity>.UpsertProperties;
            ImmutableArray<PropertyInfo> upsertKeyProperties = EntityInfoCache<TEntity>.UpsertKeyProperties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            string sourceTable = SqlDialectStrategy.RenderIdentifier("SOURCE");
            string targetTable = SqlDialectStrategy.RenderIdentifier("TARGET");

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("MERGE INTO ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(schemaName));
            sqlBuffer.Append('.');
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.Append(' ');
            sqlBuffer.AppendLine(targetTable);
            sqlBuffer.AppendLine("USING (");

            if (isRange)
            {
                sqlBuffer.AppendLine(SqlDialectStrategy.Placeholder);
            }
            else
            {
                sqlBuffer.AppendLine("    SELECT");
                sqlBuffer.AppendLine(SqlDialectStrategy.Placeholder);
                sqlBuffer.AppendLine("    FROM");
                sqlBuffer.AppendLine("        DUAL");
            }

            sqlBuffer.Append(") ");
            sqlBuffer.AppendLine(sourceTable);
            sqlBuffer.AppendLine("ON (");

            if (updateOnly)
            {
                PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
                string idColumn = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idProperty.Name]);

                sqlBuffer.Append("    ");
                sqlBuffer.Append(targetTable);
                sqlBuffer.Append('.');
                sqlBuffer.Append(idColumn);
                sqlBuffer.Append(" = ");
                sqlBuffer.Append(sourceTable);
                sqlBuffer.Append('.');
                sqlBuffer.Append(idColumn);
            }
            else
            {
                for (int i = 0; i < upsertKeyProperties.Length; i++)
                {
                    string propertyColumn = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[upsertKeyProperties[i].Name]);

                    if (i > 0)
                    {
                        sqlBuffer.AppendLine();
                        sqlBuffer.AppendLine("    AND");
                    }

                    sqlBuffer.AppendLine("    (");
                    sqlBuffer.Append("        ");
                    sqlBuffer.Append(targetTable);
                    sqlBuffer.Append('.');
                    sqlBuffer.Append(propertyColumn);
                    sqlBuffer.Append(" = ");
                    sqlBuffer.Append(sourceTable);
                    sqlBuffer.Append('.');
                    sqlBuffer.AppendLine(propertyColumn);
                    sqlBuffer.Append("        OR (");
                    sqlBuffer.Append(targetTable);
                    sqlBuffer.Append('.');
                    sqlBuffer.Append(propertyColumn);
                    sqlBuffer.Append(" IS NULL AND ");
                    sqlBuffer.Append(sourceTable);
                    sqlBuffer.Append('.');
                    sqlBuffer.Append(propertyColumn);
                    sqlBuffer.AppendLine(" IS NULL)");
                    sqlBuffer.Append("    )");
                }
            }

            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine(")");
            sqlBuffer.AppendLine("WHEN MATCHED THEN");
            sqlBuffer.Append("    UPDATE SET");
            sqlBuffer.AppendSetColumns<TEntity>(SqlDialectStrategy, properties, sourceTable, targetTable, "        ");

            if (!updateOnly)
            {
                ImmutableArray<PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;

                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("WHEN NOT MATCHED THEN");
                sqlBuffer.Append("    INSERT (");
                sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, insertProperties, null, "        ", false, false);
                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("    )");
                sqlBuffer.Append("    VALUES (");
                sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, insertProperties, sourceTable, "        ", false, false);
                sqlBuffer.AppendLine();
                sqlBuffer.Append("    )");
            }

            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }
    }
}
