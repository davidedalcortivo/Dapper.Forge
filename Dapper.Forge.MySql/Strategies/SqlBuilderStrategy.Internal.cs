using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.MySql.Strategies
{
    internal partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
    {
        private SqlTemplate BuildUpsertSql<TEntity>(bool isRange) where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            ImmutableArray<PropertyInfo> updatePropertyInfos = EntityInfoCache<TEntity>.UpdatePropertyInfos;
            ImmutableArray<PropertyInfo> insertPropertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;

            StringBuilder sqlBuilder = new();

            sqlBuilder.Append("INSERT INTO ");
            sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuilder.Append(" (");
            AppendColumns<TEntity>(sqlBuilder, "    ", insertPropertyInfos, false, null);
            sqlBuilder.AppendLine();
            sqlBuilder.AppendLine(")");
            sqlBuilder.Append("VALUES");

            if (isRange)
            {
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine("{}");
            }
            else
            {
                sqlBuilder.AppendLine(" (");
                sqlBuilder.AppendLine("{}");
                sqlBuilder.Append(") ");
            }

            sqlBuilder.AppendLine("AS new");
            sqlBuilder.Append("ON DUPLICATE KEY UPDATE");
            AppendSetColumns<TEntity>(sqlBuilder, "    ", updatePropertyInfos, "new", null);
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }
    }
}
