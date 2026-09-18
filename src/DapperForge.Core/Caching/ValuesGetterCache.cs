using DapperForge.Core.Utilities;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Reflection;


namespace DapperForge.Core.Caching
{
    internal static class ValuesGetterCache<TEntity> where TEntity : class
    {
        private static readonly ConcurrentDictionary<Type, ImmutableDictionary<string, Func<object, object?>>> _cache = new();

        public static ImmutableDictionary<string, Func<object, object?>> Get(object values)
        {
            ArgumentNullException.ThrowIfNull(values);

            return _cache.GetOrAdd(values.GetType(), Create);
        }

        private static ImmutableDictionary<string, Func<object, object?>> Create(Type type)
        {
            PropertyInfo[] properties = ValuesPropertyCache<TEntity>.Get(type);
            ImmutableDictionary<string, Func<object, object?>>.Builder builder = ImmutableDictionary.CreateBuilder<string, Func<object, object?>>(StringComparer.OrdinalIgnoreCase);

            foreach (PropertyInfo property in properties)
            {
                MethodInfo? getMethod = property.GetMethod;

                if (getMethod is null)
                    continue;

                builder[property.Name] = PropertyHelper.BuildGetterExpression<object>(property);
            }

            return builder.ToImmutable();
        }
    }
}
