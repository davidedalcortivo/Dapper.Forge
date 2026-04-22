using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Models;
using Dapper.Forge.MySql.Strategies;
using MySqlConnector;
using System.Linq.Expressions;


namespace Dapper.Forge.MySql.Extensions
{
    public static partial class DbConnectionExtensions
    {
        public static DbCommandInfo GetAllCommand<TEntity>(this MySqlConnection _, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetAllCommand<TEntity>((IFilterNode?)null, sortDescriptors);
        }

        public static DbCommandInfo GetAllCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetAllCommand(predicate, sortDescriptors);
        }

        public static DbCommandInfo GetAllCommand<TEntity>(this MySqlConnection _, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetAllCommand<TEntity>(filterNode, sortDescriptors);
        }

        public static DbCommandInfo GetFirstCommand<TEntity>(this MySqlConnection _, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetFirstCommand<TEntity>((IFilterNode?)null, sortDescriptors);
        }

        public static DbCommandInfo GetFirstCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetFirstCommand(predicate, sortDescriptors);
        }

        public static DbCommandInfo GetFirstCommand<TEntity>(this MySqlConnection _, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetFirstCommand<TEntity>(filterNode, sortDescriptors);
        }

        public static DbCommandInfo GetFirstOrDefaultCommand<TEntity>(this MySqlConnection _, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetFirstOrDefaultCommand<TEntity>((IFilterNode?)null, sortDescriptors);
        }

        public static DbCommandInfo GetFirstOrDefaultCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetFirstOrDefaultCommand(predicate, sortDescriptors);
        }

        public static DbCommandInfo GetFirstOrDefaultCommand<TEntity>(this MySqlConnection _, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetFirstOrDefaultCommand<TEntity>(filterNode, sortDescriptors);
        }

        public static DbCommandInfo GetSingleCommand<TEntity>(this MySqlConnection _) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetSingleCommand<TEntity>((IFilterNode?)null);
        }

        public static DbCommandInfo GetSingleCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetSingleCommand(predicate);
        }

        public static DbCommandInfo GetSingleCommand<TEntity>(this MySqlConnection _, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetSingleCommand<TEntity>(filterNode);
        }

        public static DbCommandInfo GetSingleOrDefaultCommand<TEntity>(this MySqlConnection _) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetSingleOrDefaultCommand<TEntity>((IFilterNode?)null);
        }

        public static DbCommandInfo GetSingleOrDefaultCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetSingleOrDefaultCommand(predicate);
        }

        public static DbCommandInfo GetSingleOrDefaultCommand<TEntity>(this MySqlConnection _, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetSingleOrDefaultCommand<TEntity>(filterNode);
        }

        public static DbCommandInfo GetByIdCommand<TEntity>(this MySqlConnection _, object id) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetByIdCommand<TEntity>(id);
        }

        public static DbCommandInfo GetPageCommand<TEntity>(this MySqlConnection _, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetPageCommand<TEntity>((IFilterNode?)null, sortDescriptors, skip, take);
        }

        public static DbCommandInfo GetPageCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetPageCommand(predicate, sortDescriptors, skip, take);
        }

        public static DbCommandInfo GetPageCommand<TEntity>(this MySqlConnection _, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetPageCommand<TEntity>(filterNode, sortDescriptors, skip, take);
        }

        public static DbCommandInfo UpdateCommand<TEntity>(this MySqlConnection _, TEntity entity) where TEntity : class
        {
            return DbCommandStrategy.Instance.UpdateCommand(entity);
        }

        public static DbCommandInfo UpdateCommand<TEntity>(this MySqlConnection _, object param) where TEntity : class
        {
            return DbCommandStrategy.Instance.UpdateCommand<TEntity>(param, (IFilterNode?)null);
        }

        public static DbCommandInfo UpdateCommand<TEntity>(this MySqlConnection _, object param, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.UpdateCommand(param, predicate);
        }

        public static DbCommandInfo UpdateCommand<TEntity>(this MySqlConnection _, object param, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.UpdateCommand<TEntity>(param, filterNode);
        }

        public static DbCommandInfo InsertCommand<TEntity>(this MySqlConnection _, TEntity entity) where TEntity : class
        {
            return DbCommandStrategy.Instance.InsertCommand(entity);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this MySqlConnection _, TEntity entity) where TEntity : class
        {
            return DbCommandStrategy.Instance.DeleteCommand(entity);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this MySqlConnection _, object id) where TEntity : class
        {
            return DbCommandStrategy.Instance.DeleteCommand<TEntity>(id);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this MySqlConnection _) where TEntity : class
        {
            return DbCommandStrategy.Instance.DeleteCommand<TEntity>((IFilterNode?)null);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.DeleteCommand(predicate);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this MySqlConnection _, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.DeleteCommand<TEntity>(filterNode);
        }

        public static DbCommandInfo UpsertCommand<TEntity>(this MySqlConnection _, TEntity entity) where TEntity : class
        {
            return DbCommandStrategy.Instance.UpsertCommand(entity);
        }

        public static IReadOnlyList<DbCommandInfo> GetByIdRangeCommands<TEntity>(this MySqlConnection _, IEnumerable<object> ids, bool preserveDuplicates = false, int batchSize = 500) where TEntity : class
        {
            return DbCommandStrategy.Instance.GetByIdRangeCommands<TEntity>(ids, preserveDuplicates, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(this MySqlConnection _, IEnumerable<TEntity> entities, int batchSize = 500) where TEntity : class
        {
            return DbCommandStrategy.Instance.UpdateRangeCommands(entities, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> InsertRangeCommands<TEntity>(this MySqlConnection _, IEnumerable<TEntity> entities, int batchSize = 500) where TEntity : class
        {
            return DbCommandStrategy.Instance.InsertRangeCommands(entities, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(this MySqlConnection _, IEnumerable<TEntity> entities, int batchSize = 500) where TEntity : class
        {
            return DbCommandStrategy.Instance.DeleteRangeCommands(entities, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(this MySqlConnection _, IEnumerable<object> ids, int batchSize = 500) where TEntity : class
        {
            return DbCommandStrategy.Instance.DeleteRangeCommands<TEntity>(ids, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(this MySqlConnection _, IEnumerable<TEntity> entities, int batchSize = 500) where TEntity : class
        {
            return DbCommandStrategy.Instance.UpsertRangeCommands(entities, batchSize, 0);
        }

        public static DbCommandInfo ExistsCommand<TEntity>(this MySqlConnection _) where TEntity : class
        {
            return DbCommandStrategy.Instance.ExistsCommand<TEntity>((IFilterNode?)null);
        }

        public static DbCommandInfo ExistsCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.ExistsCommand(predicate);
        }

        public static DbCommandInfo ExistsCommand<TEntity>(this MySqlConnection _, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.ExistsCommand<TEntity>(filterNode);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection _) where TEntity : class
        {
            return DbCommandStrategy.Instance.CountCommand<TEntity>((string?)null, (IFilterNode?)null);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.CountCommand((string?)null, predicate);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection _, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.CountCommand<TEntity>((string?)null, filterNode);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, object?>> selector) where TEntity : class
        {
            return DbCommandStrategy.Instance.CountCommand(selector, (IFilterNode?)null);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.CountCommand(selector, predicate);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, object?>> selector, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.CountCommand(selector, filterNode);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection _, string propertyName) where TEntity : class
        {
            return DbCommandStrategy.Instance.CountCommand<TEntity>(propertyName, (IFilterNode?)null);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection _, string propertyName, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.CountCommand(propertyName, predicate);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection _, string propertyName, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.CountCommand<TEntity>(propertyName, filterNode);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, decimal?>> selector) where TEntity : class
        {
            return DbCommandStrategy.Instance.AvgCommand(selector, (IFilterNode?)null);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.AvgCommand(selector, predicate);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.AvgCommand(selector, filterNode);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this MySqlConnection _, string propertyName) where TEntity : class
        {
            return DbCommandStrategy.Instance.AvgCommand<TEntity>(propertyName, (IFilterNode?)null);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this MySqlConnection _, string propertyName, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.AvgCommand(propertyName, predicate);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this MySqlConnection _, string propertyName, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.AvgCommand<TEntity>(propertyName, filterNode);
        }

        public static DbCommandInfo SumCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, decimal?>> selector) where TEntity : class
        {
            return DbCommandStrategy.Instance.SumCommand(selector, (IFilterNode?)null);
        }

        public static DbCommandInfo SumCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.SumCommand(selector, predicate);
        }

        public static DbCommandInfo SumCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.SumCommand(selector, filterNode);
        }

        public static DbCommandInfo SumCommand<TEntity>(this MySqlConnection _, string propertyName) where TEntity : class
        {
            return DbCommandStrategy.Instance.SumCommand<TEntity>(propertyName, (IFilterNode?)null);
        }

        public static DbCommandInfo SumCommand<TEntity>(this MySqlConnection _, string propertyName, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.SumCommand(propertyName, predicate);
        }

        public static DbCommandInfo SumCommand<TEntity>(this MySqlConnection _, string propertyName, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.SumCommand<TEntity>(propertyName, filterNode);
        }

        public static DbCommandInfo MinCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, decimal?>> selector) where TEntity : class
        {
            return DbCommandStrategy.Instance.MinCommand(selector, (IFilterNode?)null);
        }

        public static DbCommandInfo MinCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.MinCommand(selector, predicate);
        }

        public static DbCommandInfo MinCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.MinCommand(selector, filterNode);
        }

        public static DbCommandInfo MinCommand<TEntity>(this MySqlConnection _, string propertyName) where TEntity : class
        {
            return DbCommandStrategy.Instance.MinCommand<TEntity>(propertyName, (IFilterNode?)null);
        }

        public static DbCommandInfo MinCommand<TEntity>(this MySqlConnection _, string propertyName, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.MinCommand(propertyName, predicate);
        }

        public static DbCommandInfo MinCommand<TEntity>(this MySqlConnection _, string propertyName, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.MinCommand<TEntity>(propertyName, filterNode);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, decimal?>> selector) where TEntity : class
        {
            return DbCommandStrategy.Instance.MaxCommand(selector, (IFilterNode?)null);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.MaxCommand(selector, predicate);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this MySqlConnection _, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.MaxCommand(selector, filterNode);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this MySqlConnection _, string propertyName) where TEntity : class
        {
            return DbCommandStrategy.Instance.MaxCommand<TEntity>(propertyName, (IFilterNode?)null);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this MySqlConnection _, string propertyName, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return DbCommandStrategy.Instance.MaxCommand(propertyName, predicate);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this MySqlConnection _, string propertyName, IFilterNode filterNode) where TEntity : class
        {
            return DbCommandStrategy.Instance.MaxCommand<TEntity>(propertyName, filterNode);
        }
    }
}
