using DapperForge.Core.Abstractions.Strategies;
using DapperForge.Core.Caching;
using DapperForge.Core.Models;
using DapperForge.Core.Utilities;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace DapperForge.MySql.Strategies
{
    internal sealed partial class SqlBuilderStrategy : BaseSqlBuilderStrategy<SqlDialectStrategy>
    {
        private SqlTemplate BuildUpsertSql<TEntity>(bool isRange) where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;
            string schemaName = EntityInfoCache<TEntity>.SchemaName ?? SqlDialectStrategy.DefaultSchemaName;
            ImmutableArray<PropertyInfo> insertProperties = EntityInfoCache<TEntity>.InsertProperties;
            ImmutableArray<PropertyInfo> upsertProperties = EntityInfoCache<TEntity>.UpsertProperties;

            StringBuilder sqlBuffer = new();
            string newTable = SqlDialectStrategy.RenderIdentifier("new");

            sqlBuffer.Append("INSERT INTO ");
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(schemaName));
            sqlBuffer.Append('.');
            sqlBuffer.Append(SqlDialectStrategy.RenderIdentifier(tableName));
            sqlBuffer.Append(" (");
            sqlBuffer.AppendColumns<TEntity>(SqlDialectStrategy, insertProperties, null, "    ", false, false);
            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine(")");
            sqlBuffer.Append("VALUES");

            if (isRange)
            {
                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine(SqlDialectStrategy.Placeholder);
            }
            else
            {
                sqlBuffer.AppendLine(" (");
                sqlBuffer.AppendLine(SqlDialectStrategy.Placeholder);
                sqlBuffer.Append(") ");
            }

            sqlBuffer.Append("AS ");
            sqlBuffer.AppendLine(newTable);
            sqlBuffer.Append("ON DUPLICATE KEY UPDATE");
            sqlBuffer.AppendSetColumns<TEntity>(SqlDialectStrategy, upsertProperties, newTable, null, "    ");
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }
    }
}
