using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
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

            StringBuilder sqlBuffer = new();

            sqlBuffer.Append("INSERT INTO ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.Append(" (");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, "    ", insertPropertyInfos, false, null);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine(")");
            sqlBuffer.Append("VALUES");

            if (isRange)
            {
                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("{}");
            }
            else
            {
                sqlBuffer.AppendLine(" (");
                sqlBuffer.AppendLine("{}");
                sqlBuffer.AppendLine(")");
            }
            
            sqlBuffer.Append("ON CONFLICT (");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[idPropertyInfo.Name]));
            sqlBuffer.AppendLine(")");
            sqlBuffer.AppendLine("DO UPDATE");
            sqlBuffer.Append("SET");
            sqlBuffer.AppendSetColumns<TEntity>(SqlDialectStrategy, "    ", updatePropertyInfos, "EXCLUDED", null);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);
            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }
    }
}
