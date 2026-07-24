using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Oracle.Strategies;
using Oracle.ManagedDataAccess.Client;
using System.Collections;
using System.Linq.Expressions;


namespace Dapper.Forge.Oracle.Extensions
{
    public static partial class DbConnectionExtensions
    {
        public static void LoadDbCache<TEntity>(this OracleConnection connection, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            DbExecutionStrategy.Instance.LoadDbCacheImplAsync<TEntity>(connection, true, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetAllImplAsync(connection, true, (IFilterNode<TEntity>?)null, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetAllImplAsync(connection, true, predicate, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this OracleConnection connection, IFilterNode<TEntity> filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetAllImplAsync(connection, true, filterNode, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetFirst<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetFirstImplAsync(connection, true, (IFilterNode<TEntity>?)null, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetFirst<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetFirstImplAsync(connection, true, predicate, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetFirst<TEntity>(this OracleConnection connection, IFilterNode<TEntity> filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetFirstImplAsync(connection, true, filterNode, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync(connection, true, (IFilterNode<TEntity>?)null, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync(connection, true, predicate, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this OracleConnection connection, IFilterNode<TEntity> filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync(connection, true, filterNode, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetSingle<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetSingleImplAsync(connection, true, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetSingle<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetSingleImplAsync(connection, true, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetSingle<TEntity>(this OracleConnection connection, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetSingleImplAsync(connection, true, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync(connection, true, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync(connection, true, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this OracleConnection connection, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync(connection, true, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetById<TEntity>(this OracleConnection connection, object id, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetByIdImplAsync<TEntity>(connection, true, id, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetPageImplAsync(connection, true, (IFilterNode<TEntity>?)null, sortDescriptors, skip, take, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetPageImplAsync(connection, true, predicate, sortDescriptors, skip, take, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this OracleConnection connection, IFilterNode<TEntity> filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetPageImplAsync(connection, true, filterNode, sortDescriptors, skip, take, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Update<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.UpdateImplAsync(connection, true, entity, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Update<TEntity>(this OracleConnection connection, object param, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.UpdateImplAsync(connection, true, param, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Update<TEntity>(this OracleConnection connection, object param, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.UpdateImplAsync(connection, true, param, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Update<TEntity>(this OracleConnection connection, object param, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.UpdateImplAsync(connection, true, param, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Insert<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.InsertImplAsync(connection, true, entity, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.DeleteImplAsync(connection, true, entity, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this OracleConnection connection, object id, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.DeleteImplAsync<TEntity>(connection, true, id, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.DeleteImplAsync(connection, true, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.DeleteImplAsync(connection, true, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this OracleConnection connection, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.DeleteImplAsync(connection, true, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Upsert<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.UpsertImplAsync(connection, true, entity, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity?> GetByIdRange<TEntity>(this OracleConnection connection, IEnumerable ids, bool preserveDuplicates = false, bool preserveNulls = false, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetByIdRangeImplAsync<TEntity>(connection, true, ids, preserveDuplicates, preserveNulls, batchSize, SqlDialectStrategy.Instance.MaxInValueCount, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int UpdateRange<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            DbExecutionStrategy.Instance.LoadDbCacheImplAsync<TEntity>(connection, true, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
            return DbExecutionStrategy.Instance.UpdateRangeImplAsync(connection, true, entities, batchSize, 0, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int InsertRange<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            DbExecutionStrategy.Instance.LoadDbCacheImplAsync<TEntity>(connection, true, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
            return DbExecutionStrategy.Instance.InsertRangeImplAsync(connection, true, entities, batchSize, 0, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int DeleteRange<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            batchSize = batchSize > 0 ? Math.Min(batchSize, SqlDialectStrategy.Instance.MaxInValueCount) : SqlDialectStrategy.Instance.MaxInValueCount;
            return DbExecutionStrategy.Instance.DeleteRangeImplAsync(connection, true, entities, batchSize, 0, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int DeleteRange<TEntity>(this OracleConnection connection, IEnumerable ids, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            batchSize = batchSize > 0 ? Math.Min(batchSize, SqlDialectStrategy.Instance.MaxInValueCount) : SqlDialectStrategy.Instance.MaxInValueCount;
            return DbExecutionStrategy.Instance.DeleteRangeImplAsync<TEntity>(connection, true, ids, batchSize, 0, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int UpsertRange<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            DbExecutionStrategy.Instance.LoadDbCacheImplAsync<TEntity>(connection, true, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
            return DbExecutionStrategy.Instance.UpsertRangeImplAsync(connection, true, entities, batchSize, 0, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static bool Exists<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.ExistsImplAsync(connection, true, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static bool Exists<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.ExistsImplAsync(connection, true, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static bool Exists<TEntity>(this OracleConnection connection, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.ExistsImplAsync(connection, true, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, (string?)null, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, (string?)null, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this OracleConnection connection, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, (string?)null, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this OracleConnection connection, string propertyName, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this OracleConnection connection, string propertyName, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this OracleConnection connection, string propertyName, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Min<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MinImplAsync(connection, true, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Min<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MinImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Min<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MinImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Min<TEntity, TProperty>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MinImplAsync<TEntity, TProperty>(connection, true, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Min<TEntity, TProperty>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MinImplAsync<TEntity, TProperty>(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Min<TEntity, TProperty>(this OracleConnection connection, string propertyName, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MinImplAsync<TEntity, TProperty>(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Max<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MaxImplAsync(connection, true, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Max<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MaxImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Max<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MaxImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Max<TEntity, TProperty>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MaxImplAsync<TEntity, TProperty>(connection, true, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Max<TEntity, TProperty>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MaxImplAsync<TEntity, TProperty>(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Max<TEntity, TProperty>(this OracleConnection connection, string propertyName, IFilterNode<TEntity> filterNode, OracleTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MaxImplAsync<TEntity, TProperty>(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
