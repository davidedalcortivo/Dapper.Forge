using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Models;
using System.Collections;
using System.Linq.Expressions;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    public interface IDbCommandStrategy
    {
        ISqlDialectStrategy SqlDialectStrategy { get; }

        DbCommandInfo GetAllCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class;
        DbCommandInfo GetAllCommand<TEntity>(IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class;
        DbCommandInfo GetFirstCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class;
        DbCommandInfo GetFirstCommand<TEntity>(IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class;
        DbCommandInfo GetFirstOrDefaultCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class;
        DbCommandInfo GetFirstOrDefaultCommand<TEntity>(IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors) where TEntity : class;
        DbCommandInfo GetSingleCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo GetSingleCommand<TEntity>(IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo GetSingleOrDefaultCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo GetSingleOrDefaultCommand<TEntity>(IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo GetByIdCommand<TEntity>(object id) where TEntity : class;
        DbCommandInfo GetPageCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take) where TEntity : class;
        DbCommandInfo GetPageCommand<TEntity>(IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take) where TEntity : class;
        DbCommandInfo UpdateCommand<TEntity>(TEntity entity) where TEntity : class;
        DbCommandInfo UpdateCommand<TEntity>(object param, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo UpdateCommand<TEntity>(object param, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo InsertCommand<TEntity>(TEntity entity) where TEntity : class;
        DbCommandInfo DeleteCommand<TEntity>(TEntity entity) where TEntity : class;
        DbCommandInfo DeleteCommand<TEntity>(object id) where TEntity : class;
        DbCommandInfo DeleteCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo DeleteCommand<TEntity>(IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo UpsertCommand<TEntity>(TEntity entity) where TEntity : class;
        IReadOnlyList<DbCommandInfo> GetByIdRangeCommands<TEntity>(IEnumerable ids, int batchSize, int chunkSize) where TEntity : class;
        IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class;
        IReadOnlyList<DbCommandInfo> InsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class;
        IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class;
        IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(IEnumerable ids, int batchSize, int chunkSize) where TEntity : class;
        IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(IEnumerable<TEntity> entities, int batchSize, int chunkSize) where TEntity : class;
        DbCommandInfo ExistsCommand<TEntity>(Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo ExistsCommand<TEntity>(IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo CountCommand<TEntity>(Expression<Func<TEntity, object?>>? selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo CountCommand<TEntity>(Expression<Func<TEntity, object?>>? selector, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo CountCommand<TEntity>(string? propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo CountCommand<TEntity>(string? propertyName, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo AvgCommand<TEntity>(Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo AvgCommand<TEntity>(Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo AvgCommand<TEntity>(string? propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo AvgCommand<TEntity>(string? propertyName, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo SumCommand<TEntity>(Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo SumCommand<TEntity>(Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo SumCommand<TEntity>(string? propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo SumCommand<TEntity>(string? propertyName, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo MinCommand<TEntity>(Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo MinCommand<TEntity>(Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo MinCommand<TEntity>(string? propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo MinCommand<TEntity>(string? propertyName, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo MaxCommand<TEntity>(Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo MaxCommand<TEntity>(Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode) where TEntity : class;
        DbCommandInfo MaxCommand<TEntity>(string? propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class;
        DbCommandInfo MaxCommand<TEntity>(string? propertyName, IFilterNode? filterNode) where TEntity : class;
    }
}
