using Forget.Core.Abstractions.Strategies;
using Forget.Core.Models;
using System.Collections.Concurrent;
using System.Reflection;

 
namespace Forget.Core.Caching
{
    internal static class BuildInRangeInvokerCache
    {
        private delegate List<DbCommandInfo> BuildInRangeInvoker(ISqlDialectStrategy sqlDialectStrategy, SqlTemplate sqlTemplate, IReadOnlyList<object> idList, int batchSize, int chunkSize, PropertyInfo idProperty, bool useUnion);
        private static readonly ConcurrentDictionary<Type, BuildInRangeInvoker> _cache = new();

        public static List<DbCommandInfo> Invoke<TEntity>(object instance, ISqlDialectStrategy sqlDialectStrategy, SqlTemplate sqlTemplate, IReadOnlyList<object> idList, int batchSize, int chunkSize, PropertyInfo idProperty, bool useUnion) where TEntity : class
        {
            Type entityType = typeof(TEntity);

            BuildInRangeInvoker invoker = _cache.GetOrAdd(entityType, _ =>
            {
                MethodInfo method = instance.GetType()
                    .GetMethod("BuildInRangeCommands", BindingFlags.NonPublic | BindingFlags.Instance)!
                    .MakeGenericMethod(entityType, idProperty.PropertyType);

                return (BuildInRangeInvoker)Delegate.CreateDelegate(typeof(BuildInRangeInvoker), instance, method);
            });

            return invoker(sqlDialectStrategy, sqlTemplate, idList, batchSize, chunkSize, idProperty, useUnion);
        }
    }
}
