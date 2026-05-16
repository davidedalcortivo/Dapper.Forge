using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.SqlServer.Strategies
{
    internal sealed partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
    {
        private void AppendUpdateRange<TEntity>(StringBuilder sqlBuffer, string[] sqlLocks, string sourceTable, string targetTable, string clause) where TEntity : class
        {
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
            ImmutableArray<PropertyInfo> updateProperties = EntityInfoCache<TEntity>.UpdateProperties;

            sqlBuffer.Append("UPDATE ");
            sqlBuffer.AppendLine(targetTable);
            sqlBuffer.Append("SET");
            sqlBuffer.AppendSetColumns<TEntity>(SqlDialectStrategy, "    ", updateProperties, sourceTable, null);
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, string.Empty, targetTable);
            sqlBuffer.AppendLine();

            if (sqlLocks.Length > 0)
            {
                sqlBuffer.Append("    WITH (");

                for (int i = 0; i < sqlLocks.Length; i++)
                {
                    sqlBuffer.Append(sqlLocks[i]);
                    sqlBuffer.AppendSeparator(i, sqlLocks.Length, true);
                }

                sqlBuffer.AppendLine(")");
            }

            sqlBuffer.AppendLine("JOIN (");
            sqlBuffer.AppendLine("    VALUES");
            sqlBuffer.AppendLine(SqlDialectStrategy.Placeholder);
            sqlBuffer.AppendLine(") AS ");
            sqlBuffer.Append(sourceTable);

            sqlBuffer.Append(" (");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, string.Empty, properties, null, false, true);
            sqlBuffer.Append(')');
            sqlBuffer.AppendOnClause(string.Empty, clause);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);
        }
    }
}
