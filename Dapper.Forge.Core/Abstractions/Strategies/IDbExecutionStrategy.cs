using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Models;
using System.Collections;
using System.Data.Common;
using System.Linq.Expressions;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    public interface IDbExecutionStrategy
    {
        ISqlDialectStrategy SqlDialectStrategy { get; }

        #region Sync
        IReadOnlyList<TEntity> GetAll<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        IReadOnlyList<TEntity> GetAll<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        TEntity GetFirst<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        TEntity GetFirst<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        TEntity? GetFirstOrDefault<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        TEntity? GetFirstOrDefault<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        TEntity GetSingle<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        TEntity GetSingle<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        TEntity? GetSingleOrDefault<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        TEntity? GetSingleOrDefault<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        TEntity? GetById<TEntity>(DbConnection connection, object id, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        IReadOnlyList<TEntity> GetPage<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        IReadOnlyList<TEntity> GetPage<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Update<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Update<TEntity>(DbConnection connection, object param, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Update<TEntity>(DbConnection connection, object param, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Insert<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Delete<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Delete<TEntity>(DbConnection connection, object id, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Delete<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Delete<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Upsert<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        IReadOnlyList<TEntity?> GetByIdRange<TEntity>(DbConnection connection, IEnumerable ids, bool preserveDuplicates, bool preserveNulls, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int UpdateRange<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int InsertRange<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int DeleteRange<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int DeleteRange<TEntity>(DbConnection connection, IEnumerable ids, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int UpsertRange<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        bool Exists<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        bool Exists<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Count<TEntity>(DbConnection connection, Expression<Func<TEntity, object?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Count<TEntity>(DbConnection connection, Expression<Func<TEntity, object?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Count<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        int Count<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Avg<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Avg<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Avg<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Avg<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Sum<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Sum<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Sum<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Sum<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Min<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Min<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Min<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Min<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Max<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Max<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Max<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        decimal? Max<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class;
        #endregion

        #region Async
        Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity> GetFirstAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity> GetFirstAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity> GetSingleAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity> GetSingleAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<TEntity?> GetByIdAsync<TEntity>(DbConnection connection, object id, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> UpdateAsync<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> UpdateAsync<TEntity>(DbConnection connection, object param, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> UpdateAsync<TEntity>(DbConnection connection, object param, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> InsertAsync<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> DeleteAsync<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> DeleteAsync<TEntity>(DbConnection connection, object id, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> DeleteAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> DeleteAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> UpsertAsync<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<IReadOnlyList<TEntity?>> GetByIdRangeAsync<TEntity>(DbConnection connection, IEnumerable ids, bool preserveDuplicates, bool preserveNulls, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> UpdateRangeAsync<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> InsertRangeAsync<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> DeleteRangeAsync<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> DeleteRangeAsync<TEntity>(DbConnection connection, IEnumerable ids, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> UpsertRangeAsync<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<bool> ExistsAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<bool> ExistsAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> CountAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, object?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> CountAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, object?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> CountAsync<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<int> CountAsync<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> AvgAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> AvgAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> AvgAsync<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> AvgAsync<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> SumAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> SumAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> SumAsync<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> SumAsync<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> MinAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> MinAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> MinAsync<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> MinAsync<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> MaxAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> MaxAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> MaxAsync<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        Task<decimal?> MaxAsync<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;
        #endregion
    }
}
