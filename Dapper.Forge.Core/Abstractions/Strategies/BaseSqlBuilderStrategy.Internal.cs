using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections.Immutable;
using System.Reflection;
using System.Text;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    public abstract partial class BaseSqlBuilderStrategy<TStrategy> : ISqlBuilderStrategy where TStrategy : ISqlDialectStrategy
    {
        protected virtual void AppendColumns<TEntity>(StringBuilder sqlBuilder, string indentation, ImmutableArray<PropertyInfo> propertyInfos, bool useAlias, string? table) where TEntity : class
        {
            if (propertyInfos.Length == 0)
                return;

            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            sqlBuilder.AppendLine();

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                PropertyInfo propertyInfo = propertyInfos[i];
                string columnName = columnNamesByPropertyName[propertyInfo.Name];

                sqlBuilder.Append(indentation);

                if (table is not null)
                {
                    sqlBuilder.Append(table);
                    sqlBuilder.Append('.');
                }

                sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(columnName));

                if (useAlias && columnName != propertyInfo.Name)
                {
                    sqlBuilder.Append(" AS ");
                    sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(propertyInfo.Name));
                }

                if (i < propertyInfos.Length - 1)
                    sqlBuilder.AppendLine(",");
            }
        }

        protected virtual void AppendColumnsInline<TEntity>(StringBuilder sqlBuilder, ImmutableArray<PropertyInfo> propertyInfos, bool useAlias, string? table) where TEntity : class
        {
            if (propertyInfos.Length == 0)
                return;

            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                PropertyInfo propertyInfo = propertyInfos[i];
                string columnName = columnNamesByPropertyName[propertyInfo.Name];

                if (table is not null)
                {
                    sqlBuilder.Append(table);
                    sqlBuilder.Append('.');
                }

                sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(columnName));

                if (useAlias && columnName != propertyInfo.Name)
                {
                    sqlBuilder.Append(" AS ");
                    sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(propertyInfo.Name));
                }

                if (i < propertyInfos.Length - 1)
                    sqlBuilder.Append(", ");
            }
        }

        protected virtual void AppendSetColumns<TEntity>(StringBuilder sqlBuilder, string indentation, ImmutableArray<PropertyInfo> propertyInfos, string? sourceTable, string? targetTable) where TEntity : class
        {
            if (propertyInfos.Length == 0)
                return;

            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            sqlBuilder.AppendLine();

            for (int i = 0; i < propertyInfos.Length; i++)
            {
                string column = SqlDialectStrategy.RenderIdentifier(columnNamesByPropertyName[propertyInfos[i].Name]);
                sqlBuilder.Append(indentation);

                if (targetTable is not null)
                {
                    sqlBuilder.Append(targetTable);
                    sqlBuilder.Append('.');
                }

                sqlBuilder.Append(column);
                sqlBuilder.Append(" = ");

                if (sourceTable is not null)
                {
                    sqlBuilder.Append(sourceTable);
                    sqlBuilder.Append('.');
                }

                sqlBuilder.Append(column);

                if (i < propertyInfos.Length - 1)
                    sqlBuilder.AppendLine(",");
            }
        }

        protected virtual void AppendFromTable<TEntity>(StringBuilder sqlBuilder, string indentation, string? alias) where TEntity : class
        {
            string tableName = EntityInfoCache<TEntity>.TableName;

            sqlBuilder.AppendLine();
            sqlBuilder.Append(indentation);
            sqlBuilder.AppendLine("FROM");
            sqlBuilder.Append(indentation);
            sqlBuilder.Append("    ");
            sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(tableName));

            if (alias is not null)
            {
                sqlBuilder.Append(" AS ");
                sqlBuilder.Append(SqlDialectStrategy.RenderIdentifier(alias));
            }
        }

        protected virtual void AppendWhereClause(StringBuilder sqlBuilder, string indentation, string? clause)
        {
            if (clause is not null)
            {
                sqlBuilder.AppendLine();
                sqlBuilder.Append(indentation);
                sqlBuilder.AppendLine("WHERE");
                sqlBuilder.Append(indentation);
                sqlBuilder.Append("    ");
                sqlBuilder.Append(clause);
            }
        }

        protected virtual void AppendOnClause(StringBuilder sqlBuilder, string indentation, string? clause)
        {
            if (clause is not null)
            {
                sqlBuilder.AppendLine();
                sqlBuilder.Append(indentation);
                sqlBuilder.AppendLine("ON");
                sqlBuilder.Append(indentation);
                sqlBuilder.Append("    ");
                sqlBuilder.Append(clause);
            }
        }

        private SqlTemplate BuildAggregateSql<TEntity>(string aggregateName) where TEntity : class
        {
            StringBuilder sqlBuilder = new();

            sqlBuilder.AppendLine("SELECT");
            sqlBuilder.Append("    ");
            sqlBuilder.Append(aggregateName);
            sqlBuilder.Append("({})");
            AppendFromTable<TEntity>(sqlBuilder, string.Empty, null);
            sqlBuilder.Append("{}");
            sqlBuilder.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuilder.ToString(), SqlDialectStrategy.Terminator);
        }
    }
}
