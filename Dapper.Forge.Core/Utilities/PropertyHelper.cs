using System.Linq.Expressions;
using System.Reflection;


namespace Dapper.Forge.Core.Utilities
{
    internal static class PropertyHelper
    {
        public static string GetPropertyName<TEntity, TSelector>(Expression<Func<TEntity, TSelector>> selector) where TEntity : class
        {
            Expression body = selector.Body;

            if (body is UnaryExpression unaryExpression && body.NodeType == ExpressionType.Convert)
                body = unaryExpression.Operand;

            if (body is not MemberExpression memberExpression)
                throw new ArgumentException("The provided expression is not valid. Expected a simple member access expression.", nameof(selector));

            return memberExpression.Member.Name;
        }

        public static Func<T, object?> BuildGetterExpression<T>(PropertyInfo property) where T : class
        {
            Type type = typeof(T);
            ParameterExpression instanceParam = Expression.Parameter(type, "instance");
            Expression instanceCast = instanceParam;

            if (property.DeclaringType is not null && property.DeclaringType != type)
                instanceCast = Expression.Convert(instanceParam, property.DeclaringType);

            Expression propertyAccess = Expression.Property(instanceCast, property);
            UnaryExpression convertResult = Expression.Convert(propertyAccess, typeof(object));

            return Expression.Lambda<Func<T, object?>>(convertResult, instanceParam).Compile();
        }
    }
}
