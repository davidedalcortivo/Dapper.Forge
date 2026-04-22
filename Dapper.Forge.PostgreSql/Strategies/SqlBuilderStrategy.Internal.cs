using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.PostgreSql.Strategies
{
    internal partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
    {
        private SqlTemplate BuildUpsertSql<TEntity>(bool isRange) where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            ImmutableArray<PropertyInfo> updatePropertyInfos = EntityInfoCache<TEntity>.UpdatePropertyInfos;
            ImmutableArray<PropertyInfo> insertPropertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            StringBuilder sqlBuilder = new();

            sqlBuilder.Append("INSERT INTO ");
            sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuilder.AppendLine(" (");
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
                sqlBuilder.AppendLine(")");
            }
            
            sqlBuilder.Append("ON CONFLICT (");
            sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idPropertyInfo.Name]));
            sqlBuilder.AppendLine(")");
            sqlBuilder.AppendLine("DO UPDATE");
            sqlBuilder.AppendLine("SET");
            AppendSetColumns<TEntity>(sqlBuilder, "    ", updatePropertyInfos, "EXCLUDED", null);
            sqlBuilder.Append(SqlDialectStrategy.Terminator);
            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }
    }
}
