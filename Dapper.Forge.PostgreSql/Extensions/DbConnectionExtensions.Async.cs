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
        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetAllAsync<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetAllAsync(connection, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetAllAsync<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstAsync<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstAsync(connection, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstAsync<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultAsync<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultAsync(connection, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultAsync<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleAsync<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleAsync(connection, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleAsync<TEntity>(connection, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultAsync<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultAsync(connection, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultAsync<TEntity>(connection, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetByIdAsync<TEntity>(this NpgsqlConnection connection, object id, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetByIdAsync<TEntity>(connection, id, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetPageAsync<TEntity>(connection, (IFilterNode?)null, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetPageAsync(connection, predicate, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetPageAsync<TEntity>(connection, filterNode, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateAsync(connection, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this NpgsqlConnection connection, object param, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateAsync<TEntity>(connection, param, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this NpgsqlConnection connection, object param, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateAsync(connection, param, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this NpgsqlConnection connection, object param, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateAsync<TEntity>(connection, param, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> InsertAsync<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.InsertAsync(connection, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync(connection, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this NpgsqlConnection connection, object id, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync<TEntity>(connection, id, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync(connection, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync<TEntity>(connection, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpsertAsync<TEntity>(this NpgsqlConnection connection, TEntity entity, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpsertAsync(connection, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity?>> GetByIdRangeAsync<TEntity>(this NpgsqlConnection connection, IEnumerable ids, bool preserveDuplicates = false, bool preserveNulls = false, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetByIdRangeAsync<TEntity>(connection, ids, preserveDuplicates, preserveNulls, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateRangeAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateRangeAsync(connection, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> InsertRangeAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.InsertRangeAsync(connection, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteRangeAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteRangeAsync(connection, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteRangeAsync<TEntity>(this NpgsqlConnection connection, IEnumerable ids, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteRangeAsync<TEntity>(connection, ids, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpsertRangeAsync<TEntity>(this NpgsqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpsertRangeAsync(connection, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.ExistsAsync<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.ExistsAsync(connection, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.ExistsAsync<TEntity>(connection, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this NpgsqlConnection connection, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync<TEntity>(connection, (string?)null, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, (string?)null, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this NpgsqlConnection connection, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync<TEntity>(connection, (string?)null, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, object?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this NpgsqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this NpgsqlConnection connection, string propertyName, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this NpgsqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this NpgsqlConnection connection, string propertyName, IFilterNode filterNode, NpgsqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }
    }
}
