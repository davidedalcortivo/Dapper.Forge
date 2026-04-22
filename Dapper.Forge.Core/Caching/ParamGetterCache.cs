using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;


namespace Dapper.Forge.Core.Caching
{
    public static class ParamGetterCache
    {
        private static readonly ConcurrentDictionary<Type, ImmutableDictionary<string, Func<object, object?>>> _cache = new();

        public static ImmutableDictionary<string, Func<object, object?>> GetGetters(object param)
        {
            return _cache.GetOrAdd(param.GetType(), Create);
        }

        private static ImmutableDictionary<string, Func<object, object?>> Create(Type type)
        {
            PropertyInfo[] properties = type.GetProperties();
            ImmutableDictionary<string, Func<object, object?>>.Builder builder = ImmutableDictionary.CreateBuilder<string, Func<object, object?>>(StringComparer.OrdinalIgnoreCase);

            foreach (PropertyInfo propertyInfo in properties)
            {
                MethodInfo? getMethod = propertyInfo.GetMethod;

                if (getMethod is null)
                    continue;

                builder[propertyInfo.Name] = BuildGetter(type, propertyInfo);
            }

            return builder.ToImmutable();
        }

        private static Func<object, object?> BuildGetter(Type type, PropertyInfo propertyInfo)
        {
            ParameterExpression instanceParam = Expression.Parameter(typeof(object), "instance");

            Expression instanceCast = Expression.Convert(instanceParam, type);

            if (propertyInfo.DeclaringType is not null && propertyInfo.DeclaringType != type)
                instanceCast = Expression.Convert(instanceCast, propertyInfo.DeclaringType);

            Expression propertyAccess = Expression.Property(instanceCast, propertyInfo);

            UnaryExpression convertResult = Expression.Convert(propertyAccess, typeof(object));

            return Expression
                .Lambda<Func<object, object?>>(convertResult, instanceParam)
                .Compile();
        }
    }
}
