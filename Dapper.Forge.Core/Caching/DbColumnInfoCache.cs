using Dapper.Forge.Core.Models;
using System.Collections.Concurrent;
using System.Collections.Immutable;


namespace Dapper.Forge.Core.Caching
{
    internal static class DbColumnInfoCache<TEntity> where TEntity : class
    {
        private static readonly ConcurrentDictionary<string, IReadOnlyList<DbColumnInfo>> _listCache = new();
        private static readonly ConcurrentDictionary<string, IDictionary<string, DbColumnInfo>> _dictCache = new();
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> _semaphores = new();

        public static IReadOnlyList<DbColumnInfo>? GetListValueOrDefault(string connectionId)
        {
            return _listCache.GetValueOrDefault(connectionId);
        }

        public static IDictionary<string, DbColumnInfo>? GetDictValueOrDefault(string connectionId)
        {
            return _dictCache.GetValueOrDefault(connectionId);
        }

        public static IReadOnlyList<DbColumnInfo> GetListValue(string connectionId)
        {
            IReadOnlyList<DbColumnInfo> columns = _listCache.GetValueOrDefault(connectionId) ??
                throw new InvalidOperationException($"Database cache is not initialized for entity '{typeof(TEntity).Name}' and connection '{connectionId}'. Call LoadDbCache before performing this operation.");
            
            return columns;
        }

        public static IDictionary<string, DbColumnInfo> GetDictValue(string connectionId)
        {
            IDictionary<string, DbColumnInfo> columns = _dictCache.GetValueOrDefault(connectionId) ??
                throw new InvalidOperationException($"Database cache is not initialized for entity '{typeof(TEntity).Name}' and connection '{connectionId}'. Call LoadDbCache before performing this operation.");

            return columns;
        }

        public static bool TryAdd(string connectionId, IReadOnlyList<DbColumnInfo> columns)
        {
            return _listCache.TryAdd(connectionId, columns);
        }

        public static bool TryAdd(string connectionId, IDictionary<string, DbColumnInfo> columns)
        {
            return _dictCache.TryAdd(connectionId, columns);
        }

        public static SemaphoreSlim GetSemaphore(string connectionId)
        {
            return _semaphores.GetOrAdd(connectionId, static _ => new(1, 1));
        }
    }
}
