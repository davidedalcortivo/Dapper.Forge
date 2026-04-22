using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Models;
using Dapper.Forge.PostgreSql.Strategies;
using Npgsql;
using System.Linq.Expressions;


namespace Dapper.Forge.PostgreSql.Extensions
{
    public static partial class DbConnectionExtensions
    {
        public static IReadOnlyList<TEntity> GetAll<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetAll<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetAll(connection, predicate, sortDescriptors, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetAll<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity GetFirst<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirst<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity GetFirst<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirst(connection, predicate, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity GetFirst<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirst<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstOrDefault<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstOrDefault(connection, predicate, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstOrDefault<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity GetSingle<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingle<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static TEntity GetSingle<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingle(connection, predicate, transaction, commandTimeout);
        }

        public static TEntity GetSingle<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingle<TEntity>(connection, filterNode, transaction, commandTimeout);
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleOrDefault<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleOrDefault(connection, predicate, transaction, commandTimeout);
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleOrDefault<TEntity>(connection, filterNode, transaction, commandTimeout);
        }

        public static TEntity? GetById<TEntity>(this NpgsqlConnection connection, object id, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetById<TEntity>(connection, id, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetPage<TEntity>(connection, (IFilterNode?)null, sortDescriptors, skip, take, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetPage(connection, predicate, sortDescriptors, skip, take, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetPage<TEntity>(connection, filterNode, sortDescriptors, skip, take, transaction, commandTimeout);
        }

        public static int Update<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Update(connection, entity, transaction, commandTimeout);
        }

        public static int Update<TEntity>(this NpgsqlConnection connection, object param, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Update<TEntity>(connection, param, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Update<TEntity>(this NpgsqlConnection connection, object param, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Update(connection, param, predicate, transaction, commandTimeout);
        }

        public static int Update<TEntity>(this NpgsqlConnection connection, object param, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Update<TEntity>(connection, param, filterNode, transaction, commandTimeout);
        }

        public static int Insert<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Insert(connection, entity, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete(connection, entity, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this NpgsqlConnection connection, object id, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete<TEntity>(connection, id, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete(connection, predicate, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete<TEntity>(connection, filterNode, transaction, commandTimeout);
        }

        public static int Upsert<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Upsert(connection, entity, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity?> GetByIdRange<TEntity>(this NpgsqlConnection connection, IEnumerable<object> ids, bool preserveDuplicates = false, bool preserveNulls = false, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetByIdRange<TEntity>(connection, ids, preserveDuplicates, preserveNulls, batchSize, 0, transaction, commandTimeout);
        }

        public static int UpdateRange<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.UpdateRange(connection, entities, batchSize, 0, transaction, commandTimeout);
        }

        public static int InsertRange<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.InsertRange(connection, entities, batchSize, 0, transaction, commandTimeout);
        }

        public static int DeleteRange<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.DeleteRange(connection, entities, batchSize, 0, transaction, commandTimeout);
        }

        public static int DeleteRange<TEntity>(this NpgsqlConnection connection, IEnumerable<object> ids, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.DeleteRange<TEntity>(connection, ids, batchSize, 0, transaction, commandTimeout);
        }

        public static int UpsertRange<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.UpsertRange(connection, entities, batchSize, 0, transaction, commandTimeout);
        }

        public static bool Exists<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Exists<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static bool Exists<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Exists(connection, predicate, transaction, commandTimeout);
        }

        public static bool Exists<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Exists<TEntity>(connection, filterNode, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count<TEntity>(connection, (string?)null, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, (string?)null, predicate, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count<TEntity>(connection, (string?)null, filterNode, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, object?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, selector, predicate, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg(connection, selector, predicate, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum(connection, selector, predicate, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min(connection, selector, predicate, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max(connection, selector, predicate, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }
    }
}
