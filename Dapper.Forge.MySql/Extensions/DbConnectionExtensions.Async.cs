using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Models;
using Dapper.Forge.MySql.Strategies;
using MySqlConnector;
using System.Collections;
using System.Linq.Expressions;


namespace Dapper.Forge.MySql.Extensions
{
    public static partial class DbConnectionExtensions
    {
        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this MySqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetAllImplAsync<TEntity>(connection, false, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetAllImplAsync(connection, false, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this MySqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetAllImplAsync<TEntity>(connection, false, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this MySqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstImplAsync<TEntity>(connection, false, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstImplAsync(connection, false, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this MySqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstImplAsync<TEntity>(connection, false, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this MySqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync<TEntity>(connection, false, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync(connection, false, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this MySqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync<TEntity>(connection, false, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this MySqlConnection connection, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleImplAsync<TEntity>(connection, false, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleImplAsync(connection, false, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this MySqlConnection connection, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleImplAsync<TEntity>(connection, false, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this MySqlConnection connection, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync<TEntity>(connection, false, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync(connection, false, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this MySqlConnection connection, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync<TEntity>(connection, false, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetByIdAsync<TEntity>(this MySqlConnection connection, object id, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetByIdImplAsync<TEntity>(connection, false, id, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this MySqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetPageImplAsync<TEntity>(connection, false, (IFilterNode?)null, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetPageImplAsync(connection, false, predicate, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this MySqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetPageImplAsync<TEntity>(connection, false, filterNode, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this MySqlConnection connection, TEntity entity, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateImplAsync(connection, false, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this MySqlConnection connection, object param, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateImplAsync<TEntity>(connection, false, param, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this MySqlConnection connection, object param, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateImplAsync(connection, false, param, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this MySqlConnection connection, object param, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateImplAsync<TEntity>(connection, false, param, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> InsertAsync<TEntity>(this MySqlConnection connection, TEntity entity, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.InsertImplAsync(connection, false, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this MySqlConnection connection, TEntity entity, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteImplAsync(connection, false, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this MySqlConnection connection, object id, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteImplAsync<TEntity>(connection, false, id, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this MySqlConnection connection, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteImplAsync<TEntity>(connection, false, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteImplAsync(connection, false, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this MySqlConnection connection, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteImplAsync<TEntity>(connection, false, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpsertAsync<TEntity>(this MySqlConnection connection, TEntity entity, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpsertImplAsync(connection, false, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity?>> GetByIdRangeAsync<TEntity>(this MySqlConnection connection, IEnumerable ids, bool preserveDuplicates = false, bool preserveNulls = false, int batchSize = 500, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetByIdRangeImplAsync<TEntity>(connection, false, ids, preserveDuplicates, preserveNulls, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateRangeAsync<TEntity>(this MySqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateRangeImplAsync(connection, false, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> InsertRangeAsync<TEntity>(this MySqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.InsertRangeImplAsync(connection, false, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteRangeAsync<TEntity>(this MySqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteRangeImplAsync(connection, false, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteRangeAsync<TEntity>(this MySqlConnection connection, IEnumerable ids, int batchSize = 500, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteRangeImplAsync<TEntity>(connection, false, ids, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpsertRangeAsync<TEntity>(this MySqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpsertRangeImplAsync(connection, false, entities, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this MySqlConnection connection, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.ExistsImplAsync<TEntity>(connection, false, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.ExistsImplAsync(connection, false, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this MySqlConnection connection, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.ExistsImplAsync<TEntity>(connection, false, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this MySqlConnection connection, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountImplAsync<TEntity>(connection, false, (string?)null, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, (string?)null, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this MySqlConnection connection, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountImplAsync<TEntity>(connection, false, (string?)null, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, object?>> selector, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this MySqlConnection connection, string propertyName, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountImplAsync<TEntity>(connection, false, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this MySqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountImplAsync(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this MySqlConnection connection, string propertyName, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountImplAsync<TEntity>(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this MySqlConnection connection, string propertyName, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgImplAsync<TEntity>(connection, false, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this MySqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgImplAsync(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this MySqlConnection connection, string propertyName, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgImplAsync<TEntity>(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this MySqlConnection connection, string propertyName, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumImplAsync<TEntity>(connection, false, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this MySqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumImplAsync(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this MySqlConnection connection, string propertyName, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumImplAsync<TEntity>(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinImplAsync(connection, false, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this MySqlConnection connection, string propertyName, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinImplAsync<TEntity>(connection, false, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this MySqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinImplAsync(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this MySqlConnection connection, string propertyName, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinImplAsync<TEntity>(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxImplAsync(connection, false, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxImplAsync(connection, false, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxImplAsync(connection, false, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this MySqlConnection connection, string propertyName, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxImplAsync<TEntity>(connection, false, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this MySqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxImplAsync(connection, false, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this MySqlConnection connection, string propertyName, IFilterNode filterNode, MySqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxImplAsync<TEntity>(connection, false, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }
    }
}
