using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.SqlServer.Strategies
{
    internal partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
    {
        private void AppendUpdateRange<TEntity>(StringBuilder sqlBuilder, string[] sqlLocks, string sourceTable, string targetTable, string clause) where TEntity : class
        {
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableArray<PropertyInfo> updatePropertyInfos = EntityInfoCache<TEntity>.UpdatePropertyInfos;

            sqlBuilder.Append("UPDATE ");
            sqlBuilder.AppendLine(targetTable);
            sqlBuilder.AppendLine("SET");
            AppendSetColumns<TEntity>(sqlBuilder, "    ", updatePropertyInfos, sourceTable, null);
            sqlBuilder.AppendLine();
            AppendFromTable<TEntity>(sqlBuilder, string.Empty, targetTable);
            sqlBuilder.AppendLine();

            if (sqlLocks.Length > 0)
            {
                sqlBuilder.Append("    WITH (");

                for (int i = 0; i < sqlLocks.Length; i++)
                {
                    sqlBuilder.Append(sqlLocks[i]);

                    if (i < sqlLocks.Length - 1)
                        sqlBuilder.Append(", ");
                }

                sqlBuilder.AppendLine(")");
            }

            sqlBuilder.AppendLine("JOIN (");
            sqlBuilder.AppendLine("    VALUES");
            sqlBuilder.AppendLine("{}");
            sqlBuilder.AppendLine(") AS ");
            sqlBuilder.Append(sourceTable);

            sqlBuilder.Append(" (");
            AppendColumnsInline<TEntity>(sqlBuilder, propertyInfos, false, null);
            sqlBuilder.AppendLine(")");
            AppendOnClause<TEntity>(sqlBuilder, string.Empty, clause);
            sqlBuilder.Append(SqlDialectStrategy.Terminator);
        }
    }
}
