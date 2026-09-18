using Forget.Core.Caching;
using System.Collections;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;


namespace Forget.Core.Utilities
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
                throw new ArgumentException("The provided selector is not valid. Expected a simple member access expression.");

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
            if (value is not null)
            {
                if (value is IEnumerable enumerable && value is not string)
                {
                    foreach (object? _value in enumerable)
                    {
                        if (_value is not null)
                            EnsureValueType<TEntity>(property, _value.GetType());
                    }
                }
                else
                {
                    EnsureValueType<TEntity>(property, value.GetType());
                }
            }
        }

        public static void EnsureValueType<TEntity>(PropertyInfo property, Type valueType)
        {
            Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            valueType = Nullable.GetUnderlyingType(valueType) ?? valueType;

            if (!propertyType.IsAssignableFrom(valueType))
                throw new ArgumentException($"The type of the provided value '{valueType}' does not match the type of the property '{property.Name}' ('{propertyType}') for the entity '{typeof(TEntity).Name}'.");
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
