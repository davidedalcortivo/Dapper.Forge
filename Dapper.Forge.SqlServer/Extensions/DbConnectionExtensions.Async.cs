using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.SqlServer.Strategies;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;


namespace Dapper.Forge.PostgreSql.Extensions
{
    public static partial class DbConnectionExtensions
    {
        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this SqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetAllAsync<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetAllAsync(connection, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(this SqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetAllAsync<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this SqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstAsync<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstAsync(connection, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetFirstAsync<TEntity>(this SqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstAsync<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this SqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultAsync<TEntity>(connection, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultAsync(connection, predicate, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(this SqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetFirstOrDefaultAsync<TEntity>(connection, filterNode, sortDescriptors, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleAsync<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleAsync(connection, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity> GetSingleAsync<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleAsync<TEntity>(connection, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultAsync<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultAsync(connection, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetSingleOrDefaultAsync<TEntity>(connection, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<TEntity?> GetByIdAsync<TEntity>(this SqlConnection connection, object id, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetByIdAsync<TEntity>(connection, id, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this SqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetPageAsync<TEntity>(connection, (IFilterNode?)null, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetPageAsync(connection, predicate, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(this SqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.GetPageAsync<TEntity>(connection, filterNode, sortDescriptors, skip, take, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this SqlConnection connection, TEntity entity, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateAsync(connection, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this SqlConnection connection, object param, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateAsync<TEntity>(connection, param, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this SqlConnection connection, object param, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateAsync(connection, param, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateAsync<TEntity>(this SqlConnection connection, object param, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpdateAsync<TEntity>(connection, param, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> InsertAsync<TEntity>(this SqlConnection connection, TEntity entity, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.InsertAsync(connection, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this SqlConnection connection, TEntity entity, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync(connection, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this SqlConnection connection, object id, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync<TEntity>(connection, id, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync(connection, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteAsync<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteAsync<TEntity>(connection, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpsertAsync<TEntity>(this SqlConnection connection, TEntity entity, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.UpsertAsync(connection, entity, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<IReadOnlyList<TEntity?>> GetByIdRangeAsync<TEntity>(this SqlConnection connection, IEnumerable ids, bool preserveDuplicates = false, bool preserveNulls = false, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            batchSize = Math.Min(batchSize, SqlDialectStrategy.Instance.MaxParameterCount);
            return await DbExecutionStrategy.Instance.GetByIdRangeAsync<TEntity>(connection, ids, preserveDuplicates, preserveNulls, batchSize, 0, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpdateRangeAsync<TEntity>(this SqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            int chunkSize = SqlDialectStrategy.Instance.MaxParameterCount / propertyInfos.Length;

            return await DbExecutionStrategy.Instance.UpdateRangeAsync(connection, entities, batchSize, chunkSize, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> InsertRangeAsync<TEntity>(this SqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            int chunkSize = Math.Min(SqlDialectStrategy.Instance.MaxParameterCount / propertyInfos.Length, SqlDialectStrategy.Instance.MaxInsertRowCount);

            return await DbExecutionStrategy.Instance.InsertRangeAsync(connection, entities, batchSize, chunkSize, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteRangeAsync<TEntity>(this SqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteRangeAsync(connection, entities, batchSize, SqlDialectStrategy.Instance.MaxParameterCount, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> DeleteRangeAsync<TEntity>(this SqlConnection connection, IEnumerable ids, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.DeleteRangeAsync<TEntity>(connection, ids, batchSize, SqlDialectStrategy.Instance.MaxParameterCount, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> UpsertRangeAsync<TEntity>(this SqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            int chunkSize = SqlDialectStrategy.Instance.MaxParameterCount / propertyInfos.Length;

            return await DbExecutionStrategy.Instance.UpsertRangeAsync(connection, entities, batchSize, chunkSize, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.ExistsAsync<TEntity>(connection, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.ExistsAsync(connection, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<bool> ExistsAsync<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.ExistsAsync<TEntity>(connection, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync<TEntity>(connection, (string?)null, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, (string?)null, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync<TEntity>(connection, (string?)null, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, object?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<int> CountAsync<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.CountAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> AvgAsync<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.AvgAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> SumAsync<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.SumAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MinAsync<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MinAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync(connection, selector, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync(connection, selector, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync(connection, selector, filterNode, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync<TEntity>(connection, propertyName, (IFilterNode?)null, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync(connection, propertyName, predicate, transaction, commandTimeout, cancellationToken);
        }

        public static async Task<decimal?> MaxAsync<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await DbExecutionStrategy.Instance.MaxAsync<TEntity>(connection, propertyName, filterNode, transaction, commandTimeout, cancellationToken);
        }
    }
}
