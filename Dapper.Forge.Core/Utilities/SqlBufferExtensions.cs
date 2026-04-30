using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Core.Utilities
{
    public static class SqlBufferExtensions
    {
        public static StringBuilder AppendSeparator(this StringBuilder sqlBuffer, int index, int count, bool inLine)
        {
            if (index < count - 1)
            {
                if (inLine)
                    sqlBuffer.Append(", ");
                else
                    sqlBuffer.AppendLine(",");
            }

            return sqlBuffer;
        }

        public static StringBuilder AppendColumns<TEntity>(this StringBuilder sqlBuffer, ISqlDialectStrategy strategy, string indentation, ImmutableArray<PropertyInfo> propertyInfos, string? table, bool useAlias, bool inLine) where TEntity : class
        {
            if (propertyInfos.Length == 0)
                return sqlBuffer;

            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            if (!inLine)
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

                sqlBuffer.AppendSeparator(i, propertyInfos.Length, inLine);
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
                sqlBuffer.AppendSeparator(i, propertyInfos.Length, false);
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

        public static StringBuilder AppendSort<TEntity>(this StringBuilder sqlBuffer, ISqlDialectStrategy strategy, IEnumerable<SortDescriptor>? sortDescriptors, bool forceSorting) where TEntity : class
        {
            List<SortDescriptor> sortDescriptorList = sortDescriptors is null ? [] : sortDescriptors.AsList();

            if (forceSorting && sortDescriptorList.Count == 0)
                sortDescriptorList.Add(new(EntityInfoCache<TEntity>.IdPropertyInfo.Name));

            if (sortDescriptorList.Count > 0)
            {
                ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

                sqlBuffer.AppendLine();
                sqlBuffer.AppendLine("ORDER BY");

                for (int i = 0; i < sortDescriptorList.Count; i++)
                {
                    SortDescriptor sortDescriptor = sortDescriptorList[i];

                    string columnName = columnNamesByPropertyName[sortDescriptor.PropertyName];
                    string sortDirection = sortDescriptor.SortDirection == SortDirection.Ascending ? "ASC" : "DESC";

                    sqlBuffer.Append("    ");
                    sqlBuffer.Append(strategy.RenderIdentifier(columnName));
                    sqlBuffer.Append(' ');
                    sqlBuffer.Append(sortDirection);
                    sqlBuffer.AppendSeparator(i, sortDescriptorList.Count, false);
                }
            }

            return sqlBuffer;
        }

        public static StringBuilder AppendAndBindParameter(this StringBuilder sqlBuffer, ISqlDialectStrategy strategy, DynamicParameters parameters, string parameterName, object? parameterValue)
        {
            if (parameterValue is null)
            {
                sqlBuffer.Append(strategy.NullValue);
            }
            else
            {
                sqlBuffer.Append(strategy.RenderParameter(parameterName));
                parameters.Add(parameterName, parameterValue);
            }

            return sqlBuffer;
        }
    }
}
