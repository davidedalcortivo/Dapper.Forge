using Dapper.Forge.Core.Models;
using System.Collections.Concurrent;
using System.Collections.Immutable;


namespace Dapper.Forge.Core.Caching
{
    internal static class DbColumnInfoCache<TEntity> where TEntity : class
    {
        private static readonly ConcurrentDictionary<string, IReadOnlyList<DbColumnInfo>> _cache = new();

        public static IReadOnlyList<DbColumnInfo>? GetValueOrDefault(string connectionId)
        {
            return _cache.GetValueOrDefault(connectionId);
        }

        public static IReadOnlyList<DbColumnInfo> GetValue(string connectionId)
        {
            IReadOnlyList<DbColumnInfo> columns = _cache.GetValueOrDefault(connectionId) ??
                throw new InvalidOperationException("DbCache is not initialized for entity '" + typeof(TEntity).Name + "' and connection '" + connectionId + "'. You must call LoadDbCache before performing this operation.");
            
            return columns;
        }

        public static bool TryAdd(string connectionId, IReadOnlyList<DbColumnInfo> columns)
        {
            return _cache.TryAdd(connectionId, columns);
        }
    }
}
