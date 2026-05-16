using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections;
using System.Linq.Expressions;


namespace Dapper.Forge.Core.Utilities
{
    internal sealed class ExpressionTranslator<TEntity> : ExpressionVisitor where TEntity : class
    {
        private readonly SqlTranslationContext ctx;

        private ExpressionTranslator(SqlTranslationContext ctx)
        {
            this.ctx = ctx;
        }

        public static (string, DynamicParameters?) Translate(ISqlDialectStrategy strategy, Expression<Func<TEntity, bool>> expression, DynamicParameters? parameters = null)
        {
            SqlTranslationContext ctx = new(strategy, parameters);
            ExpressionTranslator<TEntity> visitor = new(ctx);

            visitor.Visit(expression);

            return (ctx.SqlBuffer.ToString(), ctx.Parameters);
        }

        public (string, DynamicParameters?) Translate(ISqlDialectStrategy strategy, IFilterNode node, DynamicParameters? parameters = null)
        {
            throw new NotImplementedException();
        }

        private string ExtractSql(Expression expr, bool lower = false)
        {
            ctx.Push();
            Visit(expr);
            string sql = ctx.Pop();
            return lower ? ctx.Strategy.ToLower(sql) : sql;
        }

        private static bool TryEval(Expression expr, out object? value)
        {
            switch (expr)
            {
                case ConstantExpression ce:
                    value = ce.Value;
                    return true;

                case MemberExpression me:
                    return MemberEvaluatorCache.TryEvaluate(me, out value);
            }

            try { value = Expression.Lambda(expr).Compile().DynamicInvoke(); return true; }
            catch { value = null; return false; }
        }

        private static bool IsNull(Expression expr)
            => expr is ConstantExpression { Value: null }
               || (TryEval(expr, out object? v) && v is null);

        private static bool IsStringEquals(MethodCallExpression m)
            => m.Method.DeclaringType == typeof(string) &&
               m.Method.Name == nameof(string.Equals);

        private static bool IsStringCompare(MethodCallExpression m)
            => m.Method.DeclaringType == typeof(string) &&
               m.Method.Name == nameof(string.Compare);

        private bool ResolveIgnoreCase(MethodCallExpression m)
        {
            if (m.Method.Name == nameof(string.Compare) &&
                m.Arguments.Count == 3 &&
                m.Arguments[2] is ConstantExpression ceBool &&
                ceBool.Value is bool b)
                return b;

            foreach (Expression arg in m.Arguments)
            {
                if (arg is ConstantExpression ce &&
                    ce.Value is StringComparison sc &&
                    sc.ToString().EndsWith("IgnoreCase", StringComparison.Ordinal))
                    return true;
            }

            return false;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            ctx.SqlBuffer.Append(ctx.AddParameter(node.Value));
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ParameterExpression)
            {
                ctx.SqlBuffer.Append(ctx.Strategy.RenderIdentifier(node.Member.Name));
                return node;
            }

            if (!TryEval(node, out object? value))
                throw new NotSupportedException($"Impossibile valutare {node}");

            ctx.SqlBuffer.Append(ctx.AddParameter(value));
            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Not)
            {
                ctx.SqlBuffer.Append("NOT (");
                Visit(node.Operand);
                ctx.SqlBuffer.Append(')');
                return node;
            }

            if (node.NodeType is ExpressionType.Convert or ExpressionType.ConvertChecked)
                return Visit(node.Operand);

            return base.VisitUnary(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Coalesce)
            {
                if (node.Left.Type == typeof(string) && TryEval(node.Right, out object? value) && value is string str && str == string.Empty)
                {
                    Visit(node.Left);
                    return node;
                }
            }

            if (node.Left is MethodCallExpression mLeft && IsStringCompare(mLeft))
                return VisitCompareBinary(node, mLeft);

            if (node.Right is MethodCallExpression mRight && IsStringCompare(mRight))
            {
                BinaryExpression flip = Expression.MakeBinary(
                    Flip(node.NodeType),
                    node.Right,
                    node.Left
                );

                return VisitBinary(flip);
            }

            if (node.NodeType is ExpressionType.Equal or ExpressionType.NotEqual)
            {
                bool leftNull = IsNull(node.Left);
                bool rightNull = IsNull(node.Right);

                if (leftNull ^ rightNull)
                {
                    Expression col = leftNull ? node.Right : node.Left;
                    string sql = ExtractSql(col);

                    ctx.SqlBuffer.Append(
                        node.NodeType == ExpressionType.Equal
                            ? ctx.Strategy.IsNull(sql)
                            : ctx.Strategy.IsNotNull(sql)
                    );

                    return node;
                }
            }

            ctx.SqlBuffer.Append('(');
            Visit(node.Left);

            string op = node.NodeType switch
            {
                ExpressionType.Equal => "=",
                ExpressionType.NotEqual => "<>",
                ExpressionType.GreaterThan => ">",
                ExpressionType.GreaterThanOrEqual => ">=",
                ExpressionType.LessThan => "<",
                ExpressionType.LessThanOrEqual => "<=",
                ExpressionType.AndAlso => "AND",
                ExpressionType.OrElse => "OR",
                _ => throw new NotSupportedException($"Operatore non supportato: {node.NodeType}")
            };

            ctx.SqlBuffer.Append($" {op} ");
            Visit(node.Right);
            ctx.SqlBuffer.Append(')');
            return node;
        }

        private static ExpressionType Flip(ExpressionType type)
        {
            return type switch
            {
                ExpressionType.GreaterThan => ExpressionType.LessThan,
                ExpressionType.GreaterThanOrEqual => ExpressionType.LessThanOrEqual,
                ExpressionType.LessThan => ExpressionType.GreaterThan,
                ExpressionType.LessThanOrEqual => ExpressionType.GreaterThanOrEqual,
                _ => type
            };
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(string))
                return VisitStringMethod(node);

            if (node.Method.Name == "Contains")
                return VisitCollectionContains(node);

            throw new NotSupportedException($"Metodo non supportato: {node.Method.Name}");
        }

        private MethodCallExpression VisitStringMethod(MethodCallExpression m)
        {
            if (IsStringEquals(m))
            {
                Expression a = m.Arguments[0];
                Expression b = m.Arguments[1];

                bool ignoreCase = ResolveIgnoreCase(m);

                bool aNull = IsNull(a);
                bool bNull = IsNull(b);

                if (aNull && bNull) { ctx.SqlBuffer.Append("(1 = 1)"); return m; }
                if (aNull ^ bNull) { ctx.SqlBuffer.Append("(1 = 0)"); return m; }

                string left = ExtractSql(a, ignoreCase);
                string right = ExtractSql(b, ignoreCase);

                ctx.SqlBuffer.Append($"({left} = {right})");
                return m;
            }

            if (m.Method.Name is "Contains" or "StartsWith" or "EndsWith")
            {
                bool ignoreCase = ResolveIgnoreCase(m);

                if (!TryEval(m.Arguments[0], out object? raw))
                    throw new NotSupportedException("Argomento LIKE non valutabile.");

                if (raw is null)
                {
                    ctx.SqlBuffer.Append("(1 = 0)");
                    return m;
                }

                string val = ctx.Strategy.EscapeLike(raw.ToString()!);
                string p = ctx.AddParameter(val);

                string col = ExtractSql(m.Object!, ignoreCase);
                string pp = ignoreCase ? ctx.Strategy.ToLower(p) : p;

                string pat = m.Method.Name switch
                {
                    "Contains" => ctx.Strategy.Concat("'%'", pp, "'%'"),
                    "StartsWith" => ctx.Strategy.Concat(pp, "'%'"),
                    "EndsWith" => ctx.Strategy.Concat("'%'", pp),
                    _ => throw new NotSupportedException()
                };

                ctx.SqlBuffer.Append($"({ctx.Strategy.Like(col, pat)})");
                return m;
            }

            throw new NotSupportedException($"Metodo string non supportato: {m.Method.Name}");
        }

        private BinaryExpression VisitCompareBinary(BinaryExpression be, MethodCallExpression m)
        {
            if (be.Right is not ConstantExpression ce || (int?)ce.Value != 0)
                throw new NotSupportedException("string.Compare deve essere confrontato con 0.");

            bool ignoreCase = ResolveIgnoreCase(m);

            bool aNull = IsNull(m.Arguments[0]);
            bool bNull = IsNull(m.Arguments[1]);

            string a = ExtractSql(m.Arguments[0], ignoreCase);
            string b = ExtractSql(m.Arguments[1], ignoreCase);

            switch (be.NodeType)
            {
                case ExpressionType.Equal:
                    if (aNull && bNull) { ctx.SqlBuffer.Append("(1 = 1)"); return be; }
                    if (aNull ^ bNull) { ctx.SqlBuffer.Append("(1 = 0)"); return be; }
                    ctx.SqlBuffer.Append($"({a} = {b})");
                    return be;

                case ExpressionType.LessThan:
                    ctx.SqlBuffer.Append("(" +
                        $"({ctx.Strategy.IsNull(a)} AND {ctx.Strategy.IsNotNull(b)}) OR " +
                        $"({ctx.Strategy.IsNotNull(a)} AND {ctx.Strategy.IsNotNull(b)} AND {a} < {b})" +
                        ")");
                    return be;

                case ExpressionType.GreaterThan:
                    ctx.SqlBuffer.Append("(" +
                        $"({ctx.Strategy.IsNotNull(a)} AND {ctx.Strategy.IsNull(b)}) OR " +
                        $"({ctx.Strategy.IsNotNull(a)} AND {ctx.Strategy.IsNotNull(b)} AND {a} > {b})" +
                        ")");
                    return be;

                case ExpressionType.LessThanOrEqual:
                    ctx.SqlBuffer.Append("(" +
                        $"({ctx.Strategy.IsNull(a)} AND {ctx.Strategy.IsNotNull(b)}) OR " +
                        $"({ctx.Strategy.IsNotNull(a)} AND {ctx.Strategy.IsNotNull(b)} AND {a} <= {b})" +
                        ")");
                    return be;

                case ExpressionType.GreaterThanOrEqual:
                    ctx.SqlBuffer.Append("(" +
                        $"({ctx.Strategy.IsNotNull(a)} AND {ctx.Strategy.IsNull(b)}) OR " +
                        $"({ctx.Strategy.IsNotNull(a)} AND {ctx.Strategy.IsNotNull(b)} AND {a} >= {b})" +
                        ")");
                    return be;

                default:
                    throw new NotSupportedException();
            }
        }

        private MethodCallExpression VisitCollectionContains(MethodCallExpression m)
        {
            object? raw =
                m.Object is not null
                    ? Expression.Lambda(m.Object).Compile().DynamicInvoke()
                    : Expression.Lambda(m.Arguments[0]).Compile().DynamicInvoke();

            if (raw is null && typeof(IEnumerable).IsAssignableFrom(m.Object?.Type ?? m.Arguments[0].Type))
            {
                ctx.SqlBuffer.Append("(1 = 0)");
                return m;
            }

            if (raw is not IEnumerable coll)
                throw new NotSupportedException("Contains richiede IEnumerable.");

            List<object?> values = [];
            bool hasNull = false;

            foreach (object? item in coll)
            {
                if (item is null) hasNull = true;
                else values.Add(item);
            }

            Expression element = m.Object is not null ? m.Arguments[0] : m.Arguments[1];
            string colSql = ExtractSql(element);

            if (values.Count == 0 && !hasNull)
            {
                ctx.SqlBuffer.Append("(1 = 0)");
                return m;
            }

            List<string> parts = [];

            if (hasNull)
                parts.Add(ctx.Strategy.IsNull(colSql));

            if (values.Count > 0)
            {
                string p = ctx.AddParameter(values.ToArray());
                parts.Add(ctx.Strategy.In(colSql, p));
            }

            ctx.SqlBuffer.Append("(" + string.Join(" OR ", parts) + ")");
            return m;
        }
    }
}
