using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Oracle.Strategies
{
    internal partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
    {
        private SqlTemplate BuildUpsertSql<TEntity>(bool isRange, bool appendInsert) where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableArray<PropertyInfo> updatePropertyInfos = EntityInfoCache<TEntity>.UpdatePropertyInfos;
            ImmutableArray<PropertyInfo> insertPropertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            string idColumn = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idPropertyInfo.Name]);
            string sourceTable = SqlDialectStrategy.RenderIdentifier("source");
            string targetTable = SqlDialectStrategy.RenderIdentifier("target");
            string clause = targetTable + "." + idColumn + " = " + sourceTable + "." + idColumn;

            StringBuilder sqlBuilder = new();

            sqlBuilder.Append("MERGE INTO ");
            sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuilder.Append(" AS ");
            sqlBuilder.AppendLine(targetTable);
            sqlBuilder.AppendLine("USING (");

            if (isRange)
            {
                sqlBuilder.AppendLine("{}");
            }
            else
            {
                sqlBuilder.AppendLine("    SELECT");
                sqlBuilder.AppendLine("{}");
                sqlBuilder.AppendLine("    FROM");
                sqlBuilder.AppendLine("        dual");
            }

            sqlBuilder.Append(") AS ");
            sqlBuilder.AppendLine(sourceTable);
            AppendOnClause<TEntity>(sqlBuilder, string.Empty, clause);
            sqlBuilder.AppendLine();
            sqlBuilder.AppendLine("WHEN MATCHED THEN");
            sqlBuilder.AppendLine("    UPDATE SET");
            AppendSetColumns<TEntity>(sqlBuilder, "        ", updatePropertyInfos, sourceTable, targetTable);

            if (appendInsert)
            {
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("WHEN NOT MATCHED THEN");
                sqlBuilder.AppendLine("    INSERT (");
                AppendColumns<TEntity>(sqlBuilder, "        ", insertPropertyInfos, false, null);
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("    )");
                sqlBuilder.AppendLine("    VALUES (");
                AppendColumns<TEntity>(sqlBuilder, "        ", insertPropertyInfos, false, sourceTable);
                sqlBuilder.AppendLine();
                sqlBuilder.Append("    )");
            }

            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }
    }
}
