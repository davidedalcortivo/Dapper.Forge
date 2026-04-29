using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.SqlServer.Strategies
{
    internal partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
    {
        private void AppendUpdateRange<TEntity>(StringBuilder sqlBuffer, string[] sqlLocks, string sourceTable, string targetTable, string clause) where TEntity : class
        {
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            ImmutableArray<PropertyInfo> updatePropertyInfos = EntityInfoCache<TEntity>.UpdatePropertyInfos;

            sqlBuffer.Append("UPDATE ");
            sqlBuffer.AppendLine(targetTable);
            sqlBuffer.Append("SET");
            sqlBuffer.AppendSetColumns<TEntity>(SqlDialectStrategy, "    ", updatePropertyInfos, sourceTable, null);
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, string.Empty, targetTable);
            sqlBuffer.AppendLine();

            if (sqlLocks.Length > 0)
            {
                sqlBuffer.Append("    WITH (");

                for (int i = 0; i < sqlLocks.Length; i++)
                {
                    sqlBuffer.Append(sqlLocks[i]);

                    if (i < sqlLocks.Length - 1)
                        sqlBuffer.Append(", ");
                }

                sqlBuffer.AppendLine(")");
            }

            sqlBuffer.AppendLine("JOIN (");
            sqlBuffer.AppendLine("    VALUES");
            sqlBuffer.AppendLine("{}");
            sqlBuffer.AppendLine(") AS ");
            sqlBuffer.Append(sourceTable);

            sqlBuffer.Append(" (");
            sqlBuffer.AppendColumnsInline<TEntity>(SqlDialectStrategy, propertyInfos, false, null);
            sqlBuffer.Append(')');
            sqlBuffer.AppendOnClause(string.Empty, clause);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);
        }
    }
}
