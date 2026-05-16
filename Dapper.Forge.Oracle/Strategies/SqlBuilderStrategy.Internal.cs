using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Oracle.Strategies
{
    internal sealed partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
    {
        private SqlTemplate BuildUpsertSql<TEntity>(bool isRange, bool appendInsert) where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            ImmutableArray<PropertyInfo> updateProperties = EntityInfoCache<TEntity>.UpdateProperties;
            ImmutableArray<PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            string idColumn = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idProperty.Name]);
            string sourceTable = SqlDialectStrategy.RenderIdentifier("SOURCE");
            string targetTable = SqlDialectStrategy.RenderIdentifier("TARGET");
            string clause = targetTable + "." + idColumn + " = " + sourceTable + "." + idColumn;

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("MERGE INTO ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.Append(" AS ");
            sqlBuffer.AppendLine(targetTable);
            sqlBuffer.AppendLine("USING (");

            if (isRange)
            {
                sqlBuffer.AppendLine("{}");
            }
            else
            {
                sqlBuffer.AppendLine("    SELECT");
                sqlBuffer.AppendLine("{}");
                sqlBuffer.AppendLine("    FROM");
                sqlBuffer.AppendLine("        dual");
            }

            sqlBuffer.Append(") AS ");
            sqlBuffer.Append(sourceTable);
            sqlBuffer.AppendOnClause(string.Empty, clause);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine("WHEN MATCHED THEN");
            sqlBuffer.Append("    UPDATE SET");
            sqlBuffer.AppendSetColumns<TEntity>(SqlDialectStrategy, "        ", updateProperties, sourceTable, targetTable);

            if (appendInsert)
            {
                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("WHEN NOT MATCHED THEN");
                sqlBuffer.Append("    INSERT (");
                sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "        ", insertProperties, null, false, false);
                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("    )");
                sqlBuffer.Append("    VALUES (");
                sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "        ", insertProperties, sourceTable, false, false);
                sqlBuffer.AppendLine();
                sqlBuffer.Append("    )");
            }

            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }
    }
}
