using System.Collections.Concurrent;
using System.Reflection;


namespace Dapper.Forge.Core.Caching
{
    internal static class ParamPropertyCache
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _cache = [];

        public static PropertyInfo[] GetProperties(object param)
        {
            Type type = param.GetType();
            return _cache.GetOrAdd(type, static x => x.GetProperties());
        }
    }
}
