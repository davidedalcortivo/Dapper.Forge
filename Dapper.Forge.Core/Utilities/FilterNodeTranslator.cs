using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Models;
using System.Collections;


namespace Dapper.Forge.Core.Utilities
{
    internal static class FilterNodeTranslator
    {
        public static (string, DynamicParameters?) Translate(ISqlDialectStrategy strategy, IFilterNode node, DynamicParameters? parameters = null)
        {
            SqlTranslationContext ctx = new(strategy, parameters);
            TranslateNode(node, ctx);
            return (ctx.SqlBuffer.ToString(), ctx.Parameters);
        }

        private static void TranslateNode(IFilterNode node, SqlTranslationContext ctx)
        {
            switch (node)
            {
                case FilterDescriptor f:
                    TranslateDescriptor(f, ctx);
                    break;
                case FilterGroup g:
                    TranslateGroup(g, ctx);
                    break;
                default:
                    throw new NotSupportedException($"Tipo di filtro non supportato: {node.GetType()}");
            }
        }

        private static void TranslateDescriptor(FilterDescriptor f, SqlTranslationContext ctx)
        {
            string left = f.PropertyName;
            string sql;

            if (f.Value is null)
            {
                sql = f.ComparisonOperator switch
                {
                    ComparisonOperator.Equal => $"{left} IS NULL",
                    ComparisonOperator.NotEqual => $"{left} IS NOT NULL",
                    _ => "1 = 0"
                };
            }
            else if (f.ComparisonOperator == ComparisonOperator.Contains && f.Value is IEnumerable enumerable && f.Value is not string)
            {
                List<object?> values = [];
                bool hasNull = false;

                foreach (object? item in enumerable)
                {
                    if (item is null) hasNull = true;
                    else values.Add(item);
                }

                if (values.Count == 0 && !hasNull)
                {
                    sql = "1 = 0";
                }
                else
                {
                    List<string> parts = [];

                    if (hasNull)
                        parts.Add($"{left} IS NULL");

                    if (values.Count > 0)
                    {
                        string p = ctx.AddParameter(values.ToArray());
                        parts.Add($"{left} IN {p}");
                    }

                    sql = "(" + string.Join(" OR ", parts) + ")";
                }
            }
            else
            {
                string right = ctx.AddParameter(f.Value);

                if (f.IgnoreCase && f.Value is string)
                {
                    left = $"LOWER({left})";
                    right = $"LOWER({right})";
                }

                sql = f.ComparisonOperator switch
                {
                    ComparisonOperator.Equal => $"{left} = {right}",
                    ComparisonOperator.NotEqual => $"{left} <> {right}",
                    ComparisonOperator.GreaterThan => $"{left} > {right}",
                    ComparisonOperator.GreaterThanOrEqual => $"{left} >= {right}",
                    ComparisonOperator.LessThan => $"{left} < {right}",
                    ComparisonOperator.LessThanOrEqual => $"{left} <= {right}",

                    ComparisonOperator.Contains => $"{left} LIKE '%' || {right} || '%' ESCAPE '\\'",
                    ComparisonOperator.StartsWith => $"{left} LIKE {right} || '%' ESCAPE '\\'",
                    ComparisonOperator.EndsWith => $"{left} LIKE '%' || {right} ESCAPE '\\'",

                    _ => throw new NotSupportedException($"Operatore non supportato: {f.ComparisonOperator}")
                };
            }

            if (f.Not)
                sql = $"NOT ({sql})";

            ctx.SqlBuffer.Append(sql);
        }

        private static void TranslateGroup(FilterGroup g, SqlTranslationContext ctx)
        {
            if (g.FilterNodes.Count == 0)
            {
                ctx.SqlBuffer.Append("(1 = 1)");
                return;
            }

            string logical = g.LogicalOperator == LogicalOperator.AndAlso ? "AND" : "OR";

            ctx.SqlBuffer.Append('(');

            for (int i = 0; i < g.FilterNodes.Count; i++)
            {
                TranslateNode(g.FilterNodes[i], ctx);
                if (i < g.FilterNodes.Count - 1)
                    ctx.SqlBuffer.Append($" {logical} ");
            }

            ctx.SqlBuffer.Append(')');

            if (g.Not)
            {
                ctx.SqlBuffer.Insert(0, "NOT (");
                ctx.SqlBuffer.Append(')');
            }
        }
    }
}
