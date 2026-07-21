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
        public static async Task LoadDbCacheAsync<TEntity>(this OracleConnection connection, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            await DbExecutionStrategy.Instance.LoadDbCacheImplAsync<TEntity>(connection, false, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetAllImplAsync<TEntity>(connection, false, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetAllImplAsync(connection, false, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetAllImplAsync<TEntity>(connection, false, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetFirstImplAsync<TEntity>(connection, false, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetFirstImplAsync(connection, false, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetFirstImplAsync<TEntity>(connection, false, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync<TEntity>(connection, false, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync(connection, false, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync<TEntity>(connection, false, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetSingleImplAsync<TEntity>(connection, false, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetSingleImplAsync(connection, false, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetSingleImplAsync<TEntity>(connection, false, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync<TEntity>(connection, false, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync(connection, false, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync<TEntity>(connection, false, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetByIdAsync<TEntity>(this OracleConnection connection, object id, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetByIdImplAsync<TEntity>(connection, false, id, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetPageImplAsync<TEntity>(connection, false, (IFilterNode?)null, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetPageImplAsync(connection, false, predicate, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetPageImplAsync<TEntity>(connection, false, filterNode, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.UpdateImplAsync(connection, false, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this OracleConnection connection, object param, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.UpdateImplAsync<TEntity>(connection, false, param, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this OracleConnection connection, object param, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.UpdateImplAsync(connection, false, param, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this OracleConnection connection, object param, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.UpdateImplAsync<TEntity>(connection, false, param, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> InsertAsync<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.InsertImplAsync(connection, false, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.DeleteImplAsync(connection, false, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this OracleConnection connection, object id, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.DeleteImplAsync<TEntity>(connection, false, id, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.DeleteImplAsync<TEntity>(connection, false, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.DeleteImplAsync(connection, false, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.DeleteImplAsync<TEntity>(connection, false, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpsertAsync<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.UpsertImplAsync(connection, false, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity?>> GetByIdRangeAsync<TEntity>(this OracleConnection connection, IEnumerable ids, bool preserveDuplicates = false, bool preserveNulls = false, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.GetByIdRangeImplAsync<TEntity>(connection, false, ids, preserveDuplicates, preserveNulls, batchSize, SqlDialectStrategy.Instance.MaxInValueCount, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateRangeAsync<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            await DbExecutionStrategy.Instance.LoadDbCacheImplAsync<TEntity>(connection, false, commandTimeout, cancellationToken);
            return await DbExecutionStrategy.Instance.UpdateRangeImplAsync(connection, false, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> InsertRangeAsync<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            await DbExecutionStrategy.Instance.LoadDbCacheImplAsync<TEntity>(connection, false, commandTimeout, cancellationToken);
            return await DbExecutionStrategy.Instance.InsertRangeImplAsync(connection, false, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteRangeAsync<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            batchSize = batchSize > 0 ? Math.Min(batchSize, SqlDialectStrategy.Instance.MaxInValueCount) : SqlDialectStrategy.Instance.MaxInValueCount;
            return await DbExecutionStrategy.Instance.DeleteRangeImplAsync(connection, false, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteRangeAsync<TEntity>(this OracleConnection connection, IEnumerable ids, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            batchSize = batchSize > 0 ? Math.Min(batchSize, SqlDialectStrategy.Instance.MaxInValueCount) : SqlDialectStrategy.Instance.MaxInValueCount;
            return await DbExecutionStrategy.Instance.DeleteRangeImplAsync<TEntity>(connection, false, ids, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpsertRangeAsync<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            await DbExecutionStrategy.Instance.LoadDbCacheImplAsync<TEntity>(connection, false, commandTimeout, cancellationToken);
            return await DbExecutionStrategy.Instance.UpsertRangeImplAsync(connection, false, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.ExistsImplAsync<TEntity>(connection, false, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.ExistsImplAsync(connection, false, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.ExistsImplAsync<TEntity>(connection, false, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync<TEntity>(connection, false, (string?)null, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, (string?)null, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync<TEntity>(connection, false, (string?)null, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync<TEntity>(connection, false, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<long> CountAsync<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.CountImplAsync<TEntity>(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.AvgImplAsync<TEntity>(connection, false, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.AvgImplAsync<TEntity>(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.SumImplAsync<TEntity>(connection, false, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.SumImplAsync<TEntity>(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MinAsync<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MinImplAsync(connection, false, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MinAsync<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MinImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MinAsync<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MinImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MinAsync<TEntity, TProperty>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MinImplAsync<TEntity, TProperty>(connection, false, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MinAsync<TEntity, TProperty>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MinImplAsync<TEntity, TProperty>(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MinAsync<TEntity, TProperty>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MinImplAsync<TEntity, TProperty>(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MaxAsync<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MaxImplAsync(connection, false, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MaxAsync<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MaxImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MaxAsync<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MaxImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MaxAsync<TEntity, TProperty>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MaxImplAsync<TEntity, TProperty>(connection, false, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MaxAsync<TEntity, TProperty>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MaxImplAsync<TEntity, TProperty>(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TProperty?> MaxAsync<TEntity, TProperty>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return await DbExecutionStrategy.Instance.MaxImplAsync<TEntity, TProperty>(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }
    }
}
