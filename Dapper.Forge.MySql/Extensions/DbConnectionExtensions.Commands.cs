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
        public static DbCommandInfo GetAllCommand<TEntity>(this MySqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetAllCommand<TEntity>(connection, (IFilterNode?)null, sortDescriptors);
        }

        public static DbCommandInfo GetAllCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetAllCommand(connection, predicate, sortDescriptors);
        }

        public static DbCommandInfo GetAllCommand<TEntity>(this MySqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetAllCommand<TEntity>(connection, filterNode, sortDescriptors);
        }

        public static DbCommandInfo GetFirstCommand<TEntity>(this MySqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetFirstCommand<TEntity>(connection, (IFilterNode?)null, sortDescriptors);
        }

        public static DbCommandInfo GetFirstCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetFirstCommand(connection, predicate, sortDescriptors);
        }

        public static DbCommandInfo GetFirstCommand<TEntity>(this MySqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetFirstCommand<TEntity>(connection, filterNode, sortDescriptors);
        }

        public static DbCommandInfo GetFirstOrDefaultCommand<TEntity>(this MySqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetFirstOrDefaultCommand<TEntity>(connection, (IFilterNode?)null, sortDescriptors);
        }

        public static DbCommandInfo GetFirstOrDefaultCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetFirstOrDefaultCommand(connection, predicate, sortDescriptors);
        }

        public static DbCommandInfo GetFirstOrDefaultCommand<TEntity>(this MySqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetFirstOrDefaultCommand<TEntity>(connection, filterNode, sortDescriptors);
        }

        public static DbCommandInfo GetSingleCommand<TEntity>(this MySqlConnection connection) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetSingleCommand<TEntity>(connection, (IFilterNode?)null);
        }

        public static DbCommandInfo GetSingleCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetSingleCommand(connection, predicate);
        }

        public static DbCommandInfo GetSingleCommand<TEntity>(this MySqlConnection connection, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetSingleCommand<TEntity>(connection, filterNode);
        }

        public static DbCommandInfo GetSingleOrDefaultCommand<TEntity>(this MySqlConnection connection) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetSingleOrDefaultCommand<TEntity>(connection, (IFilterNode?)null);
        }

        public static DbCommandInfo GetSingleOrDefaultCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetSingleOrDefaultCommand(connection, predicate);
        }

        public static DbCommandInfo GetSingleOrDefaultCommand<TEntity>(this MySqlConnection connection, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetSingleOrDefaultCommand<TEntity>(connection, filterNode);
        }

        public static DbCommandInfo GetByIdCommand<TEntity>(this MySqlConnection connection, object id) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetByIdCommand<TEntity>(connection, id);
        }

        public static DbCommandInfo GetPageCommand<TEntity>(this MySqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetPageCommand<TEntity>(connection, (IFilterNode?)null, sortDescriptors, skip, take);
        }

        public static DbCommandInfo GetPageCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetPageCommand(connection, predicate, sortDescriptors, skip, take);
        }

        public static DbCommandInfo GetPageCommand<TEntity>(this MySqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetPageCommand<TEntity>(connection, filterNode, sortDescriptors, skip, take);
        }

        public static DbCommandInfo UpdateCommand<TEntity>(this MySqlConnection connection, TEntity entity) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpdateCommand(connection, entity);
        }

        public static DbCommandInfo UpdateCommand<TEntity>(this MySqlConnection connection, object param) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpdateCommand<TEntity>(connection, param, (IFilterNode?)null);
        }

        public static DbCommandInfo UpdateCommand<TEntity>(this MySqlConnection connection, object param, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpdateCommand(connection, param, predicate);
        }

        public static DbCommandInfo UpdateCommand<TEntity>(this MySqlConnection connection, object param, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpdateCommand<TEntity>(connection, param, filterNode);
        }

        public static DbCommandInfo InsertCommand<TEntity>(this MySqlConnection connection, TEntity entity) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.InsertCommand(connection, entity);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this MySqlConnection connection, TEntity entity) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.DeleteCommand(connection, entity);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this MySqlConnection connection, object id) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.DeleteCommand<TEntity>(connection, id);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this MySqlConnection connection) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.DeleteCommand<TEntity>(connection, (IFilterNode?)null);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.DeleteCommand(connection, predicate);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this MySqlConnection connection, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.DeleteCommand<TEntity>(connection, filterNode);
        }

        public static DbCommandInfo UpsertCommand<TEntity>(this MySqlConnection connection, TEntity entity) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpsertCommand(connection, entity);
        }

        public static IReadOnlyList<DbCommandInfo> GetByIdRangeCommands<TEntity>(this MySqlConnection connection, IEnumerable ids, int batchSize = 500) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetByIdRangeCommands<TEntity>(connection, ids, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(this MySqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpdateRangeCommands(connection, entities, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> InsertRangeCommands<TEntity>(this MySqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.InsertRangeCommands(connection, entities, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(this MySqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.DeleteRangeCommands(connection, entities, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(this MySqlConnection connection, IEnumerable ids, int batchSize = 500) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.DeleteRangeCommands<TEntity>(connection, ids, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(this MySqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpsertRangeCommands(connection, entities, batchSize, 0);
        }

        public static DbCommandInfo ExistsCommand<TEntity>(this MySqlConnection connection) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.ExistsCommand<TEntity>(connection, (IFilterNode?)null);
        }

        public static DbCommandInfo ExistsCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.ExistsCommand(connection, predicate);
        }

        public static DbCommandInfo ExistsCommand<TEntity>(this MySqlConnection connection, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.ExistsCommand<TEntity>(connection, filterNode);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection connection) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand<TEntity>(connection, (string?)null, (IFilterNode?)null);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, (string?)null, predicate);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection connection, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand<TEntity>(connection, (string?)null, filterNode);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, object?>> selector) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, selector, (IFilterNode?)null);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, selector, predicate);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, selector, filterNode);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection connection, string propertyName) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand<TEntity>(connection, propertyName, (IFilterNode?)null);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, propertyName, predicate);
        }

        public static DbCommandInfo CountCommand<TEntity>(this MySqlConnection connection, string propertyName, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand<TEntity>(connection, propertyName, filterNode);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.AvgCommand(connection, selector, (IFilterNode?)null);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.AvgCommand(connection, selector, predicate);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.AvgCommand(connection, selector, filterNode);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this MySqlConnection connection, string propertyName) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.AvgCommand<TEntity>(connection, propertyName, (IFilterNode?)null);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this MySqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.AvgCommand(connection, propertyName, predicate);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this MySqlConnection connection, string propertyName, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.AvgCommand<TEntity>(connection, propertyName, filterNode);
        }

        public static DbCommandInfo SumCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.SumCommand(connection, selector, (IFilterNode?)null);
        }

        public static DbCommandInfo SumCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.SumCommand(connection, selector, predicate);
        }

        public static DbCommandInfo SumCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.SumCommand(connection, selector, filterNode);
        }

        public static DbCommandInfo SumCommand<TEntity>(this MySqlConnection connection, string propertyName) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.SumCommand<TEntity>(connection, propertyName, (IFilterNode?)null);
        }

        public static DbCommandInfo SumCommand<TEntity>(this MySqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.SumCommand(connection, propertyName, predicate);
        }

        public static DbCommandInfo SumCommand<TEntity>(this MySqlConnection connection, string propertyName, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.SumCommand<TEntity>(connection, propertyName, filterNode);
        }

        public static DbCommandInfo MinCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MinCommand(connection, selector, (IFilterNode?)null);
        }

        public static DbCommandInfo MinCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MinCommand(connection, selector, predicate);
        }

        public static DbCommandInfo MinCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MinCommand(connection, selector, filterNode);
        }

        public static DbCommandInfo MinCommand<TEntity>(this MySqlConnection connection, string propertyName) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MinCommand<TEntity>(connection, propertyName, (IFilterNode?)null);
        }

        public static DbCommandInfo MinCommand<TEntity>(this MySqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MinCommand(connection, propertyName, predicate);
        }

        public static DbCommandInfo MinCommand<TEntity>(this MySqlConnection connection, string propertyName, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MinCommand<TEntity>(connection, propertyName, filterNode);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MaxCommand(connection, selector, (IFilterNode?)null);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MaxCommand(connection, selector, predicate);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this MySqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MaxCommand(connection, selector, filterNode);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this MySqlConnection connection, string propertyName) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MaxCommand<TEntity>(connection, propertyName, (IFilterNode?)null);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this MySqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MaxCommand(connection, propertyName, predicate);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this MySqlConnection connection, string propertyName, IFilterNode filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MaxCommand<TEntity>(connection, propertyName, filterNode);
        }
    }
}
