using System.Collections.Concurrent;
using System.Reflection;


namespace Dapper.Forge.Core.Caching
{
    public static class ParamPropertyCache
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _cache = new();

        public static PropertyInfo[] GetProperties(Type type)
        {
            return _cache.GetOrAdd(type, static x => x.GetProperties());
        }

        public static PropertyInfo[] GetProperties(object param)
        {
            return GetProperties(param.GetType());
        }
    }
}
