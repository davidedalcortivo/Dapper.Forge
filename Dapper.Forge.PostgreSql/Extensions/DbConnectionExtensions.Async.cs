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
        public static async Task LoadDbCacheAsync<TEntity>(this NpgsqlConnection connection, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            await DbExecutionStrategy.Instance.LoadDbCacheImplAsync<TEntity>(connection, false, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetAllImplAsync(connection, false, (IFilterNode<TEntity>?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetAllImplAsync(connection, false, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetAllImplAsync(connection, false, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetFirstImplAsync(connection, false, (IFilterNode<TEntity>?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetFirstImplAsync(connection, false, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetFirstImplAsync(connection, false, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync(connection, false, (IFilterNode<TEntity>?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync(connection, false, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync(connection, false, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetSingleImplAsync(connection, false, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetSingleImplAsync(connection, false, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetSingleImplAsync(connection, false, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync(connection, false, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync(connection, false, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync(connection, false, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetByIdAsync<TEntity>(this NpgsqlConnection connection, object id, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetByIdImplAsync<TEntity>(connection, false, id, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, int? skip = null, int? take = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetPageImplAsync(connection, false, (IFilterNode<TEntity>?)null, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, int? skip = null, int? take = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetPageImplAsync(connection, false, predicate, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, int? skip = null, int? take = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetPageImplAsync(connection, false, filterNode, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.UpdateImplAsync(connection, false, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this NpgsqlConnection connection, object param, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.UpdateImplAsync(connection, false, param, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this NpgsqlConnection connection, object param, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.UpdateImplAsync(connection, false, param, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this NpgsqlConnection connection, object param, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.UpdateImplAsync(connection, false, param, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> InsertAsync<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.InsertImplAsync(connection, false, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.DeleteImplAsync(connection, false, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this NpgsqlConnection connection, object id, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.DeleteImplAsync<TEntity>(connection, false, id, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.DeleteImplAsync(connection, false, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.DeleteImplAsync(connection, false, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.DeleteImplAsync(connection, false, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpsertAsync<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.UpsertImplAsync(connection, false, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity?>> GetByIdRangeAsync<TEntity>(this NpgsqlConnection connection, IEnumerable ids, bool preserveDuplicates = false, bool preserveNulls = false, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetByIdRangeImplAsync<TEntity>(connection, false, ids, preserveDuplicates, preserveNulls, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateRangeAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            await DbExecutionStrategy.Instance.LoadDbCacheImplAsync<TEntity>(connection, false, commandTimeout, cancellationToken);
            return await DbExecutionStrategy.Instance.UpdateRangeImplAsync(connection, false, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> InsertRangeAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.InsertRangeImplAsync(connection, false, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteRangeAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.DeleteRangeImplAsync(connection, false, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteRangeAsync<TEntity>(this NpgsqlConnection connection, IEnumerable ids, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.DeleteRangeImplAsync<TEntity>(connection, false, ids, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpsertRangeAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.UpsertRangeImplAsync(connection, false, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.ExistsImplAsync(connection, false, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.ExistsImplAsync(connection, false, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.ExistsImplAsync(connection, false, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, (string?)null, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, (string?)null, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this NpgsqlConnection connection, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, (string?)null, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, object?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MinAsync<TEntity, TProperty>(this NpgsqlConnection connection, Expression<Func<TEntity, TProperty?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MinImplAsync(connection, false, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MinAsync<TEntity, TProperty>(this NpgsqlConnection connection, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MinImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MinAsync<TEntity, TProperty>(this NpgsqlConnection connection, Expression<Func<TEntity, TProperty?>> selector, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MinImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MinAsync<TEntity, TProperty>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MinImplAsync<TEntity, TProperty>(connection, false, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MinAsync<TEntity, TProperty>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MinImplAsync<TEntity, TProperty>(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MinAsync<TEntity, TProperty>(this NpgsqlConnection connection, string propertyName, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MinImplAsync<TEntity, TProperty>(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MaxAsync<TEntity, TProperty>(this NpgsqlConnection connection, Expression<Func<TEntity, TProperty?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MaxImplAsync(connection, false, selector, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MaxAsync<TEntity, TProperty>(this NpgsqlConnection connection, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MaxImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MaxAsync<TEntity, TProperty>(this NpgsqlConnection connection, Expression<Func<TEntity, TProperty?>> selector, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MaxImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MaxAsync<TEntity, TProperty>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MaxImplAsync<TEntity, TProperty>(connection, false, propertyName, (IFilterNode<TEntity>?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MaxAsync<TEntity, TProperty>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MaxImplAsync<TEntity, TProperty>(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MaxAsync<TEntity, TProperty>(this NpgsqlConnection connection, string propertyName, IFilterNode<TEntity> filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MaxImplAsync<TEntity, TProperty>(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }
    }
}
