using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.SqlServer.Strategies;
using Microsoft.Data.SqlClient;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;


namespace Dapper.Forge.PostgreSql.Extensions
{
    public static partial class DbConnectionExtensions
    {
        public static IReadOnlyList<TEntity> GetAll<TEntity>(this SqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetAll<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetAll(connection, predicate, sortDescriptors, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this SqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetAll<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity GetFirst<TEntity>(this SqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirst<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity GetFirst<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirst(connection, predicate, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity GetFirst<TEntity>(this SqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirst<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this SqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstOrDefault<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstOrDefault(connection, predicate, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this SqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstOrDefault<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity GetSingle<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingle<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static TEntity GetSingle<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingle(connection, predicate, transaction, commandTimeout);
        }

        public static TEntity GetSingle<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingle<TEntity>(connection, filterNode, transaction, commandTimeout);
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleOrDefault<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleOrDefault(connection, predicate, transaction, commandTimeout);
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleOrDefault<TEntity>(connection, filterNode, transaction, commandTimeout);
        }

        public static TEntity? GetById<TEntity>(this SqlConnection connection, object id, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetById<TEntity>(connection, id, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this SqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetPage<TEntity>(connection, (IFilterNode?)null, sortDescriptors, skip, take, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetPage(connection, predicate, sortDescriptors, skip, take, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this SqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetPage<TEntity>(connection, filterNode, sortDescriptors, skip, take, transaction, commandTimeout);
        }

        public static int Update<TEntity>(this SqlConnection connection, TEntity entity, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Update(connection, entity, transaction, commandTimeout);
        }

        public static int Update<TEntity>(this SqlConnection connection, object param, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Update<TEntity>(connection, param, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Update<TEntity>(this SqlConnection connection, object param, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Update(connection, param, predicate, transaction, commandTimeout);
        }

        public static int Update<TEntity>(this SqlConnection connection, object param, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Update<TEntity>(connection, param, filterNode, transaction, commandTimeout);
        }

        public static int Insert<TEntity>(this SqlConnection connection, TEntity entity, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Insert(connection, entity, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this SqlConnection connection, TEntity entity, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete(connection, entity, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this SqlConnection connection, object id, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete<TEntity>(connection, id, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete(connection, predicate, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete<TEntity>(connection, filterNode, transaction, commandTimeout);
        }

        public static int Upsert<TEntity>(this SqlConnection connection, TEntity entity, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Upsert(connection, entity, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity?> GetByIdRange<TEntity>(this SqlConnection connection, IEnumerable<object> ids, bool preserveDuplicates = false, bool preserveNulls = false, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            batchSize = Math.Min(batchSize, SqlDialectStrategy.Instance.MaxParameterCount);
            return DbExecutionStrategy.Instance.GetByIdRange<TEntity>(connection, ids, preserveDuplicates, preserveNulls, batchSize, 0, transaction, commandTimeout);
        }

        public static int UpdateRange<TEntity>(this SqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            int chunkSize = SqlDialectStrategy.Instance.MaxParameterCount / propertyInfos.Length;

            return DbExecutionStrategy.Instance.UpdateRange(connection, entities, batchSize, chunkSize, transaction, commandTimeout);
        }

        public static int InsertRange<TEntity>(this SqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            int chunkSize = Math.Min(SqlDialectStrategy.Instance.MaxParameterCount / propertyInfos.Length, SqlDialectStrategy.Instance.MaxInsertRowCount);

            return DbExecutionStrategy.Instance.InsertRange(connection, entities, batchSize, chunkSize, transaction, commandTimeout);
        }

        public static int DeleteRange<TEntity>(this SqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.DeleteRange(connection, entities, batchSize, SqlDialectStrategy.Instance.MaxParameterCount, transaction, commandTimeout);
        }

        public static int DeleteRange<TEntity>(this SqlConnection connection, IEnumerable<object> ids, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.DeleteRange<TEntity>(connection, ids, batchSize, SqlDialectStrategy.Instance.MaxParameterCount, transaction, commandTimeout);
        }

        public static int UpsertRange<TEntity>(this SqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            int chunkSize = SqlDialectStrategy.Instance.MaxParameterCount / propertyInfos.Length;

            return DbExecutionStrategy.Instance.UpsertRange(connection, entities, batchSize, chunkSize, transaction, commandTimeout);
        }

        public static bool Exists<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Exists<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static bool Exists<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Exists(connection, predicate, transaction, commandTimeout);
        }

        public static bool Exists<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Exists<TEntity>(connection, filterNode, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count<TEntity>(connection, (string?)null, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, (string?)null, predicate, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count<TEntity>(connection, (string?)null, filterNode, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this SqlConnection connection, Expression<Func<TEntity, object?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this SqlConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, selector, predicate, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this SqlConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg(connection, selector, predicate, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum(connection, selector, predicate, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min(connection, selector, predicate, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max(connection, selector, predicate, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }
    }
}
