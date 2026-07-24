using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Models;
using Dapper.Forge.PostgreSql.Strategies;
using Npgsql;
using System.Collections;
using System.Linq.Expressions;


namespace Dapper.Forge.PostgreSql.Extensions
{
    public static partial class DbConnectionExtensions
    {
        public static void LoadDbCache<TEntity>(this NpgsqlConnection connection, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            DbExecutionStrategy.Instance.LoadDbCacheImplAsync<TEntity>(connection, true, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetAllImplAsync(connection, true, (IFilterNode<TEntity>?)null, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetAllImplAsync(connection, true, predicate, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetAllImplAsync(connection, true, filterNode, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetFirst<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetFirstImplAsync(connection, true, (IFilterNode<TEntity>?)null, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetFirst<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetFirstImplAsync(connection, true, predicate, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetFirst<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetFirstImplAsync(connection, true, filterNode, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync(connection, true, (IFilterNode<TEntity>?)null, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync(connection, true, predicate, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync(connection, true, filterNode, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetSingle<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetSingleImplAsync(connection, true, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetSingle<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetSingleImplAsync(connection, true, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetSingle<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetSingleImplAsync(connection, true, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync(connection, true, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync(connection, true, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync(connection, true, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetById<TEntity>(this NpgsqlConnection connection, object id, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetByIdImplAsync<TEntity>(connection, true, id, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetPageImplAsync(connection, true, (IFilterNode<TEntity>?)null, sortDescriptors, skip, take, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetPageImplAsync(connection, true, predicate, sortDescriptors, skip, take, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetPageImplAsync(connection, true, filterNode, sortDescriptors, skip, take, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Update<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.UpdateImplAsync(connection, true, entity, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Update<TEntity>(this NpgsqlConnection connection, object param, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.UpdateImplAsync(connection, true, param, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Update<TEntity>(this NpgsqlConnection connection, object param, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.UpdateImplAsync(connection, true, param, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Update<TEntity>(this NpgsqlConnection connection, object param, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.UpdateImplAsync(connection, true, param, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Insert<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.InsertImplAsync(connection, true, entity, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.DeleteImplAsync(connection, true, entity, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this NpgsqlConnection connection, object id, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.DeleteImplAsync<TEntity>(connection, true, id, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.DeleteImplAsync(connection, true, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.DeleteImplAsync(connection, true, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.DeleteImplAsync(connection, true, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Upsert<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.UpsertImplAsync(connection, true, entity, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity?> GetByIdRange<TEntity>(this NpgsqlConnection connection, IEnumerable ids, bool preserveDuplicates = false, bool preserveNulls = false, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.GetByIdRangeImplAsync<TEntity>(connection, true, ids, preserveDuplicates, preserveNulls, batchSize, 0, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int UpdateRange<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            DbExecutionStrategy.Instance.LoadDbCacheImplAsync<TEntity>(connection, true, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
            return DbExecutionStrategy.Instance.UpdateRangeImplAsync(connection, true, entities, batchSize, 0, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int InsertRange<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.InsertRangeImplAsync(connection, true, entities, batchSize, 0, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int DeleteRange<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.DeleteRangeImplAsync(connection, true, entities, batchSize, 0, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int DeleteRange<TEntity>(this NpgsqlConnection connection, IEnumerable ids, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.DeleteRangeImplAsync<TEntity>(connection, true, ids, batchSize, 0, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int UpsertRange<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.UpsertRangeImplAsync(connection, true, entities, batchSize, 0, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static bool Exists<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.ExistsImplAsync(connection, true, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static bool Exists<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.ExistsImplAsync(connection, true, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static bool Exists<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.ExistsImplAsync(connection, true, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, (string?)null, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, (string?)null, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, (string?)null, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, object?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static long Count<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Min<TEntity, TProperty>(this NpgsqlConnection connection, Expression<Func<TEntity, TProperty?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MinImplAsync(connection, true, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Min<TEntity, TProperty>(this NpgsqlConnection connection, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MinImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Min<TEntity, TProperty>(this NpgsqlConnection connection, Expression<Func<TEntity, TProperty?>> selector, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MinImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Min<TEntity, TProperty>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MinImplAsync<TEntity, TProperty>(connection, true, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Min<TEntity, TProperty>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MinImplAsync<TEntity, TProperty>(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Min<TEntity, TProperty>(this NpgsqlConnection connection, string propertyName, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MinImplAsync<TEntity, TProperty>(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Max<TEntity, TProperty>(this NpgsqlConnection connection, Expression<Func<TEntity, TProperty?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MaxImplAsync(connection, true, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Max<TEntity, TProperty>(this NpgsqlConnection connection, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MaxImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Max<TEntity, TProperty>(this NpgsqlConnection connection, Expression<Func<TEntity, TProperty?>> selector, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MaxImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Max<TEntity, TProperty>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MaxImplAsync<TEntity, TProperty>(connection, true, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Max<TEntity, TProperty>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MaxImplAsync<TEntity, TProperty>(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TProperty? Max<TEntity, TProperty>(this NpgsqlConnection connection, string propertyName, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbExecutionStrategy.Instance.MaxImplAsync<TEntity, TProperty>(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
