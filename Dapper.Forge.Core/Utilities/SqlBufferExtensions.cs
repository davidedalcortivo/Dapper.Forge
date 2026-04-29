using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Core.Utilities
{
    public static class SqlBufferExtensions
    {
        public static StringBuilder AppendColumns<TEntity>(this StringBuilder sqlBuffer, ISqlDialectStrategy strategy, string indentation, ImmutableArray<PropertyInfo> propertyInfos, bool useAlias, string? table) where TEntity : class
        {
            if (propertyInfos.Length == 0)
                return sqlBuffer;

            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            sqlBuffer.AppendLine();

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                PropertyInfo propertyInfo = propertyInfos[i];
                string columnName = columnNamesByPropertyName[propertyInfo.Name];

                sqlBuffer.Append(indentation);

                if (table is not null)
                {
                    sqlBuffer.Append(table);
                    sqlBuffer.Append('.');
                }

                sqlBuffer.Append(strategy.RenderIdentifier(columnName));

                if (useAlias && columnName != propertyInfo.Name)
                {
                    sqlBuffer.Append(" AS ");
                    sqlBuffer.Append(strategy.RenderIdentifier(propertyInfo.Name));
                }

                if (i < propertyInfos.Length - 1)
                    sqlBuffer.AppendLine(",");
            }

            return sqlBuffer;
        }

        public static StringBuilder AppendColumnsInline<TEntity>(this StringBuilder sqlBuffer, ISqlDialectStrategy strategy, ImmutableArray<PropertyInfo> propertyInfos, bool useAlias, string? table) where TEntity : class
        {
            if (propertyInfos.Length == 0)
                return sqlBuffer;

            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                PropertyInfo propertyInfo = propertyInfos[i];
                string columnName = columnNamesByPropertyName[propertyInfo.Name];

                if (table is not null)
                {
                    sqlBuffer.Append(table);
                    sqlBuffer.Append('.');
                }

                sqlBuffer.Append(strategy.RenderIdentifier(columnName));

                if (useAlias && columnName != propertyInfo.Name)
                {
                    sqlBuffer.Append(" AS ");
                    sqlBuffer.Append(strategy.RenderIdentifier(propertyInfo.Name));
                }

                if (i < propertyInfos.Length - 1)
                    sqlBuffer.Append(", ");
            }

            return sqlBuffer;
        }

        public static StringBuilder AppendSetColumns<TEntity>(this StringBuilder sqlBuffer, ISqlDialectStrategy strategy, string indentation, ImmutableArray<PropertyInfo> propertyInfos, string? sourceTable, string? targetTable) where TEntity : class
        {
            if (propertyInfos.Length == 0)
                return sqlBuffer;

            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            sqlBuffer.AppendLine();

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                string column = strategy.RenderIdentifier(columnNamesByPropertyName[propertyInfos[i].Name]);
                sqlBuffer.Append(indentation);

                if (targetTable is not null)
                {
                    sqlBuffer.Append(targetTable);
                    sqlBuffer.Append('.');
                }

                sqlBuffer.Append(column);
                sqlBuffer.Append(" = ");

                if (sourceTable is not null)
                {
                    sqlBuffer.Append(sourceTable);
                    sqlBuffer.Append('.');
                }

                sqlBuffer.Append(column);

                if (i < propertyInfos.Length - 1)
                    sqlBuffer.AppendLine(",");
            }

            return sqlBuffer;
        }

        public static StringBuilder AppendFromTable<TEntity>(this StringBuilder sqlBuffer, ISqlDialectStrategy strategy, string indentation, string? alias) where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;

            sqlBuffer.AppendLine();
            sqlBuffer.Append(indentation);
            sqlBuffer.AppendLine("FROM");
            sqlBuffer.Append(indentation);
            sqlBuffer.Append("    ");
            sqlBuffer.Append(strategy.RenderIdentifier(tableName));

            if (alias is not null)
            {
                sqlBuffer.Append(" AS ");
                sqlBuffer.Append(strategy.RenderIdentifier(alias));
            }

            return sqlBuffer;
        }

        public static StringBuilder AppendWhereClause(this StringBuilder sqlBuffer, string indentation, string? clause)
        {
            if (clause is not null)
            {
                sqlBuffer.AppendLine();
                sqlBuffer.Append(indentation);
                sqlBuffer.AppendLine("WHERE");
                sqlBuffer.Append(indentation);
                sqlBuffer.Append("    ");
                sqlBuffer.Append(clause);
            }

            return sqlBuffer;
        }

        public static StringBuilder AppendOnClause(this StringBuilder sqlBuffer, string indentation, string? clause)
        {
            if (clause is not null)
            {
                sqlBuffer.AppendLine();
                sqlBuffer.Append(indentation);
                sqlBuffer.AppendLine("ON");
                sqlBuffer.Append(indentation);
                sqlBuffer.Append("    ");
                sqlBuffer.Append(clause);
            }

            return sqlBuffer;
        }
    }
}
