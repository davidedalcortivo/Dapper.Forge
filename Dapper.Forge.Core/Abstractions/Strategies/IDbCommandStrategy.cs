using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Models;
using System.Collections;
using System.Data.Common;
using System.Linq.Expressions;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    public interface IDbCommandStrategy
    {
        ISqlDialectStrategy SqlDialectStrategy { get; }

        void LoadRuntimeCache<TEntity>(DbConnection connection) where TEntity : class;
        DbCommandInfo GetAllCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class;
        DbCommandInfo GetAllCommand<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class;
        DbCommandInfo GetFirstCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class;
        DbCommandInfo GetFirstCommand<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class;
        DbCommandInfo GetFirstOrDefaultCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class;
        DbCommandInfo GetFirstOrDefaultCommand<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class;
        DbCommandInfo GetSingleCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo GetSingleCommand<TEntity>(DbConnection connection, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo GetSingleOrDefaultCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo GetSingleOrDefaultCommand<TEntity>(DbConnection connection, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo GetByIdCommand<TEntity>(DbConnection connection, object id) where TEntity : class;
        DbCommandInfo GetPageCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take) where TEntity : class;
        DbCommandInfo GetPageCommand<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take) where TEntity : class;
        DbCommandInfo UpdateCommand<TEntity>(DbConnection connection, TEntity entity) where TEntity : class;
        DbCommandInfo UpdateCommand<TEntity>(DbConnection connection, object param, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo UpdateCommand<TEntity>(DbConnection connection, object param, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo InsertCommand<TEntity>(DbConnection connection, TEntity entity) where TEntity : class;
        DbCommandInfo DeleteCommand<TEntity>(DbConnection connection, TEntity entity) where TEntity : class;
        DbCommandInfo DeleteCommand<TEntity>(DbConnection connection, object id) where TEntity : class;
        DbCommandInfo DeleteCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo DeleteCommand<TEntity>(DbConnection connection, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo UpsertCommand<TEntity>(DbConnection connection, TEntity entity) where TEntity : class;
        IReadOnlyList<DbCommandInfo> GetByIdRangeCommands<TEntity>(DbConnection connection, IEnumerable ids, int batchSize, int chunkSize) where TEntity : class;
        IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class;
        IReadOnlyList<DbCommandInfo> InsertRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class;
        IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class;
        IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(DbConnection connection, IEnumerable ids, int batchSize, int chunkSize) where TEntity : class;
        IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class;
        DbCommandInfo ExistsCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo ExistsCommand<TEntity>(DbConnection connection, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo CountCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, object?>>? selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo CountCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, object?>>? selector, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo CountCommand<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo CountCommand<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo AvgCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo AvgCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo AvgCommand<TEntity>(DbConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo AvgCommand<TEntity>(DbConnection connection, string propertyName, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo SumCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo SumCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo SumCommand<TEntity>(DbConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo SumCommand<TEntity>(DbConnection connection, string propertyName, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo MinCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo MinCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo MinCommand<TEntity>(DbConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo MinCommand<TEntity>(DbConnection connection, string propertyName, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo MaxCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo MaxCommand<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo MaxCommand<TEntity>(DbConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo MaxCommand<TEntity>(DbConnection connection, string propertyName, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo GetColumnsCommand<TEntity>(DbConnection connection) where TEntity : class;
    }
}
