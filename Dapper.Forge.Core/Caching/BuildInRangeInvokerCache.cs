using Dapper.Forge.Core.Models;
using System.Collections.Concurrent;
using System.Reflection;

 
namespace Dapper.Forge.Core.Caching
{
    internal static class BuildInRangeInvokerCache
    {
        private delegate List<DbCommandInfo> BuildInRangeInvoker(SqlTemplate sqlTemplate, List<object> idList, int batchSize, int chunkSize, PropertyInfo idPropertyInfo, bool useUnion);
        private static readonly ConcurrentDictionary<Type, BuildInRangeInvoker> _cache = [];

        public static List<DbCommandInfo> Invoke<TEntity>(object instance, SqlTemplate sqlTemplate, List<object> idList, int batchSize, int chunkSize, PropertyInfo idPropertyInfo, bool useUnion) where TEntity : class
        {
            Type entityType = typeof(TEntity);

            BuildInRangeInvoker invoker = _cache.GetOrAdd(entityType, _ =>
            {
                MethodInfo method = instance.GetType()
                    .GetMethod("BuildInRangeCommands", BindingFlags.NonPublic | BindingFlags.Instance)!
                    .MakeGenericMethod(entityType, idPropertyInfo.PropertyType);

                return (BuildInRangeInvoker)Delegate.CreateDelegate(typeof(BuildInRangeInvoker), instance, method);
            });

            return invoker(sqlTemplate, idList, batchSize, chunkSize, idPropertyInfo, useUnion);
        }
    }
}
