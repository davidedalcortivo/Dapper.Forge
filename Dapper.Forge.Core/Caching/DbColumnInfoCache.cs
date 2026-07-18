using Dapper.Forge.Core.Models;
using System.Collections.Concurrent;
using System.Collections.Immutable;


namespace Dapper.Forge.Core.Caching
{
    internal static class DbColumnInfoCache<TEntity> where TEntity : class
    {
        private static readonly ConcurrentDictionary<string, IReadOnlyList<DbColumnInfo>> _cache = new();
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> _semaphores = new();

        public static IReadOnlyList<DbColumnInfo>? GetValueOrDefault(string connectionId)
        {
            return _cache.GetValueOrDefault(connectionId);
        }

        public static IReadOnlyList<DbColumnInfo> GetValue(string connectionId)
        {
            IReadOnlyList<DbColumnInfo> columns = _cache.GetValueOrDefault(connectionId) ??
                throw new InvalidOperationException("Database cache is not initialized for entity '" + typeof(TEntity).Name + "' and connection '" + connectionId + "'. Call LoadDbCache before performing this operation.");
            
            return columns;
        }

        public static bool TryAdd(string connectionId, IReadOnlyList<DbColumnInfo> columns)
        {
            return _cache.TryAdd(connectionId, columns);
        }

        public static SemaphoreSlim GetSemaphore(string connectionId)
        {
            return _semaphores.GetOrAdd(connectionId, static _ => new(1, 1));
        }
    }
}
