using Dapper.Forge.Core.Caching;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;


namespace Dapper.Forge.Core.Utilities
{
    internal static class PropertyHelper
    {
        public static PropertyInfo GetProperty<TEntity, TSelector>(Expression<Func<TEntity, TSelector>> selector) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(selector);
            Expression body = selector.Body;

            if (body is UnaryExpression unaryExpression && body.NodeType == ExpressionType.Convert)
                body = unaryExpression.Operand;

            if (body is not MemberExpression memberExpression)
                throw new ArgumentException("The provided expression is not valid. Expected a simple member access expression.", nameof(selector));

            return GetProperty<TEntity>(memberExpression.Member.Name);
        }

        public static PropertyInfo GetProperty<TEntity>(string propertyName) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(propertyName);
            ImmutableDictionary<string, PropertyInfo> propertiesByPropertyName = EntityInfoCache<TEntity>.PropertiesByPropertyName;

            if (!propertiesByPropertyName.TryGetValue(propertyName, out PropertyInfo? property))
                throw new ArgumentException($"The property '{propertyName}' does not exist on entity '{typeof(TEntity).Name}'.");

            return property;
        }

        public static void EnsureValue<TEntity>(PropertyInfo property, object? value)
        {
            Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            Type? valueType = value?.GetType();

            if (valueType is not null && !propertyType.IsAssignableFrom(valueType))
                throw new ArgumentException($"The type of the provided value '{valueType}' does not match the type of the property '{propertyType}' for the entity '{typeof(TEntity).Name}'.");
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
