using Dapper.Forge.Core.Models;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Reflection;


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

        public static void Add(string connectionId, IReadOnlyList<DbColumnInfo> columns)
        {
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
            ImmutableDictionary<string, PropertyInfo> propertiesByColumnName = EntityInfoCache<TEntity>.PropertiesByColumnName;

            if (properties.Length != columns.Count)
                throw new InvalidOperationException($"Database table schema mismatch for entity '{typeof(TEntity).Name}'. Expected {properties.Length} mapped properties but found {columns.Count} database columns.");

            foreach (DbColumnInfo column in columns)
            {
                string columnName = column.Name;

                if (!propertiesByColumnName.TryGetValue(columnName, out PropertyInfo? _))
                    throw new InvalidOperationException($"Database column mapping mismatch for entity '{typeof(TEntity).Name}'. Database column '{columnName}' is not mapped to any entity property.");
            }

            _ = _listCache.TryAdd(connectionId, columns);
        }

        public static void Add(string connectionId, IDictionary<string, DbColumnInfo> columns)
        {
            ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
            ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

            if (properties.Length != columns.Count)
                throw new InvalidOperationException($"Database table schema mismatch for entity '{typeof(TEntity).Name}'. Expected {properties.Length} mapped properties but found {columns.Count} database columns.");

            foreach (PropertyInfo property in properties)
            {
                string columnName = columnNamesByPropertyName[property.Name];

                if (!columns.TryGetValue(columnName, out DbColumnInfo? _))
                    throw new InvalidOperationException($"Database column mapping mismatch for entity '{typeof(TEntity).Name}'. Database column '{columnName}' is not mapped to any entity property.");
            }

            _ = _dictCache.TryAdd(connectionId, columns);
        }

        public static SemaphoreSlim GetSemaphore(string connectionId)
        {
            return _semaphores.GetOrAdd(connectionId, static _ => new(1, 1));
        }
    }
}
