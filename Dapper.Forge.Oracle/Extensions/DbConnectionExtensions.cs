using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Oracle.Strategies;
using Oracle.ManagedDataAccess.Client;
using System.Linq.Expressions;


namespace Dapper.Forge.Oracle.Extensions
{
    public static partial class DbConnectionExtensions
    {
        public static IReadOnlyList<TEntity> GetAll<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetAll<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetAll(connection, predicate, sortDescriptors, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this OracleConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetAll<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity GetFirst<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirst<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity GetFirst<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirst(connection, predicate, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity GetFirst<TEntity>(this OracleConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirst<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstOrDefault<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstOrDefault(connection, predicate, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this OracleConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstOrDefault<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout);
        }

        public static TEntity GetSingle<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingle<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static TEntity GetSingle<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingle(connection, predicate, transaction, commandTimeout);
        }

        public static TEntity GetSingle<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingle<TEntity>(connection, filterNode, transaction, commandTimeout);
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleOrDefault<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleOrDefault(connection, predicate, transaction, commandTimeout);
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleOrDefault<TEntity>(connection, filterNode, transaction, commandTimeout);
        }

        public static TEntity? GetById<TEntity>(this OracleConnection connection, object id, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetById<TEntity>(connection, id, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetPage<TEntity>(connection, (IFilterNode?)null, sortDescriptors, skip, take, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetPage(connection, predicate, sortDescriptors, skip, take, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this OracleConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetPage<TEntity>(connection, filterNode, sortDescriptors, skip, take, transaction, commandTimeout);
        }

        public static int Update<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Update(connection, entity, transaction, commandTimeout);
        }

        public static int Update<TEntity>(this OracleConnection connection, object param, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Update<TEntity>(connection, param, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Update<TEntity>(this OracleConnection connection, object param, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Update(connection, param, predicate, transaction, commandTimeout);
        }

        public static int Update<TEntity>(this OracleConnection connection, object param, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Update<TEntity>(connection, param, filterNode, transaction, commandTimeout);
        }

        public static int Insert<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Insert(connection, entity, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete(connection, entity, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this OracleConnection connection, object id, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete<TEntity>(connection, id, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete(connection, predicate, transaction, commandTimeout);
        }

        public static int Delete<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Delete<TEntity>(connection, filterNode, transaction, commandTimeout);
        }

        public static int Upsert<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Upsert(connection, entity, transaction, commandTimeout);
        }

        public static IReadOnlyList<TEntity?> GetByIdRange<TEntity>(this OracleConnection connection, IEnumerable<object> ids, bool preserveDuplicates = false, bool preserveNulls = false, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetByIdRange<TEntity>(connection, ids, preserveDuplicates, preserveNulls, batchSize, SqlDialectStrategy.Instance.MaxInValueCount, transaction, commandTimeout);
        }

        public static int UpdateRange<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.UpdateRange(connection, entities, batchSize, 0, transaction, commandTimeout);
        }

        public static int InsertRange<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.InsertRange(connection, entities, batchSize, 0, transaction, commandTimeout);
        }

        public static int DeleteRange<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            batchSize = Math.Min(batchSize, SqlDialectStrategy.Instance.MaxInValueCount);
            return DbExecutionStrategy.Instance.DeleteRange(connection, entities, batchSize, 0, transaction, commandTimeout);
        }

        public static int DeleteRange<TEntity>(this OracleConnection connection, IEnumerable<object> ids, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            batchSize = Math.Min(batchSize, SqlDialectStrategy.Instance.MaxInValueCount);
            return DbExecutionStrategy.Instance.DeleteRange<TEntity>(connection, ids, batchSize, 0, transaction, commandTimeout);
        }

        public static int UpsertRange<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.UpsertRange(connection, entities, batchSize, 0, transaction, commandTimeout);
        }

        public static bool Exists<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Exists<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static bool Exists<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Exists(connection, predicate, transaction, commandTimeout);
        }

        public static bool Exists<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Exists<TEntity>(connection, filterNode, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count<TEntity>(connection, (string?)null, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, (string?)null, predicate, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count<TEntity>(connection, (string?)null, filterNode, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, selector, predicate, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static int Count<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Count<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg(connection, selector, predicate, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static decimal? Avg<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Avg<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum(connection, selector, predicate, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static decimal? Sum<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Sum<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min(connection, selector, predicate, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static decimal? Min<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Min<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max(connection, selector, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max(connection, selector, predicate, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max(connection, selector, filterNode, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max(connection, propertyName, predicate, transaction, commandTimeout);
        }

        public static decimal? Max<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.Max<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout);
        }
    }
}
