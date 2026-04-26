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
        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetAllAsync<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetAllAsync(connection, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetAllAsync<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstAsync<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstAsync(connection, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstAsync<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultAsync<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultAsync(connection, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultAsync<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleAsync<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleAsync(connection, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleAsync<TEntity>(connection, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultAsync<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultAsync(connection, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultAsync<TEntity>(connection, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetByIdAsync<TEntity>(this OracleConnection connection, object id, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetByIdAsync<TEntity>(connection, id, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetPageAsync<TEntity>(connection, (IFilterNode?)null, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetPageAsync(connection, predicate, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetPageAsync<TEntity>(connection, filterNode, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateAsync(connection, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this OracleConnection connection, object param, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateAsync<TEntity>(connection, param, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this OracleConnection connection, object param, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateAsync(connection, param, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this OracleConnection connection, object param, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateAsync<TEntity>(connection, param, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> InsertAsync<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.InsertAsync(connection, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync(connection, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this OracleConnection connection, object id, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync<TEntity>(connection, id, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync(connection, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync<TEntity>(connection, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpsertAsync<TEntity>(this OracleConnection connection, TEntity entity, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpsertAsync(connection, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity?>> GetByIdRangeAsync<TEntity>(this OracleConnection connection, IEnumerable ids, bool preserveDuplicates = false, bool preserveNulls = false, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetByIdRangeAsync<TEntity>(connection, ids, preserveDuplicates, preserveNulls, batchSize, SqlDialectStrategy.Instance.MaxInValueCount, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateRangeAsync<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateRangeAsync(connection, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> InsertRangeAsync<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.InsertRangeAsync(connection, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteRangeAsync<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            batchSize = Math.Min(batchSize, SqlDialectStrategy.Instance.MaxInValueCount);
            return await DbExecutionStrategy.Instance.DeleteRangeAsync(connection, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteRangeAsync<TEntity>(this OracleConnection connection, IEnumerable ids, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            batchSize = Math.Min(batchSize, SqlDialectStrategy.Instance.MaxInValueCount);
            return await DbExecutionStrategy.Instance.DeleteRangeAsync<TEntity>(connection, ids, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpsertRangeAsync<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpsertRangeAsync(connection, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.ExistsAsync<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.ExistsAsync(connection, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.ExistsAsync<TEntity>(connection, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this OracleConnection connection, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync<TEntity>(connection, (string?)null, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, (string?)null, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this OracleConnection connection, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync<TEntity>(connection, (string?)null, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this OracleConnection connection, string propertyName, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this OracleConnection connection, string propertyName, IFilterNode filterNode, OracleTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }
    }
}
