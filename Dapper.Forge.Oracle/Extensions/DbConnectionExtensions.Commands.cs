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
        public static void LoadRuntimeCache<TEntity>(this OracleConnection connection) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
        }

        public static DbCommandInfo GetAllCommand<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetAllCommand(connection, (IFilterNode<TEntity>?)null, sortDescriptors);
        }

        public static DbCommandInfo GetAllCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetAllCommand(connection, predicate, sortDescriptors);
        }

        public static DbCommandInfo GetAllCommand<TEntity>(this OracleConnection connection, IFilterNode<TEntity>? filterNode, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetAllCommand(connection, filterNode, sortDescriptors);
        }

        public static DbCommandInfo GetFirstCommand<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetFirstCommand(connection, (IFilterNode<TEntity>?)null, sortDescriptors);
        }

        public static DbCommandInfo GetFirstCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetFirstCommand(connection, predicate, sortDescriptors);
        }

        public static DbCommandInfo GetFirstCommand<TEntity>(this OracleConnection connection, IFilterNode<TEntity>? filterNode, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetFirstCommand(connection, filterNode, sortDescriptors);
        }

        public static DbCommandInfo GetFirstOrDefaultCommand<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetFirstOrDefaultCommand(connection, (IFilterNode<TEntity>?)null, sortDescriptors);
        }

        public static DbCommandInfo GetFirstOrDefaultCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetFirstOrDefaultCommand(connection, predicate, sortDescriptors);
        }

        public static DbCommandInfo GetFirstOrDefaultCommand<TEntity>(this OracleConnection connection, IFilterNode<TEntity>? filterNode, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetFirstOrDefaultCommand(connection, filterNode, sortDescriptors);
        }

        public static DbCommandInfo GetSingleCommand<TEntity>(this OracleConnection connection) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetSingleCommand(connection, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo GetSingleCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetSingleCommand(connection, predicate);
        }

        public static DbCommandInfo GetSingleCommand<TEntity>(this OracleConnection connection, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetSingleCommand(connection, filterNode);
        }

        public static DbCommandInfo GetSingleOrDefaultCommand<TEntity>(this OracleConnection connection) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetSingleOrDefaultCommand(connection, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo GetSingleOrDefaultCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetSingleOrDefaultCommand(connection, predicate);
        }

        public static DbCommandInfo GetSingleOrDefaultCommand<TEntity>(this OracleConnection connection, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetSingleOrDefaultCommand(connection, filterNode);
        }

        public static DbCommandInfo GetByIdCommand<TEntity>(this OracleConnection connection, object id) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetByIdCommand<TEntity>(connection, id);
        }

        public static DbCommandInfo GetPageCommand<TEntity>(this OracleConnection connection, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, int? skip = null, int? take = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetPageCommand(connection, (IFilterNode<TEntity>?)null, sortDescriptors, skip, take);
        }

        public static DbCommandInfo GetPageCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, int? skip = null, int? take = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetPageCommand(connection, predicate, sortDescriptors, skip, take);
        }

        public static DbCommandInfo GetPageCommand<TEntity>(this OracleConnection connection, IFilterNode<TEntity>? filterNode, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors = null, int? skip = null, int? take = null) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetPageCommand(connection, filterNode, sortDescriptors, skip, take);
        }

        public static DbCommandInfo UpdateCommand<TEntity>(this OracleConnection connection, TEntity entity) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpdateCommand(connection, entity);
        }

        public static DbCommandInfo UpdateCommand<TEntity>(this OracleConnection connection, object values) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpdateCommand(connection, values, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo UpdateCommand<TEntity>(this OracleConnection connection, object values, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpdateCommand(connection, values, predicate);
        }

        public static DbCommandInfo UpdateCommand<TEntity>(this OracleConnection connection, object values, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpdateCommand(connection, values, filterNode);
        }

        public static DbCommandInfo InsertCommand<TEntity>(this OracleConnection connection, TEntity entity) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.InsertCommand(connection, entity);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this OracleConnection connection, TEntity entity) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.DeleteCommand(connection, entity);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this OracleConnection connection, object id) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.DeleteCommand<TEntity>(connection, id);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this OracleConnection connection) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.DeleteCommand(connection, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.DeleteCommand(connection, predicate);
        }

        public static DbCommandInfo DeleteCommand<TEntity>(this OracleConnection connection, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.DeleteCommand(connection, filterNode);
        }

        public static DbCommandInfo UpsertCommand<TEntity>(this OracleConnection connection, TEntity entity) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpsertCommand(connection, entity);
        }

        public static IReadOnlyList<DbCommandInfo> GetByIdRangeCommands<TEntity>(this OracleConnection connection, IEnumerable ids, int batchSize = 500) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.GetByIdRangeCommands<TEntity>(connection, ids, batchSize, SqlDialectStrategy.Instance.MaxInValueCount);
        }

        public static IReadOnlyList<DbCommandInfo> UpdateRangeCommands<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpdateRangeCommands(connection, entities, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> InsertRangeCommands<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.InsertRangeCommands(connection, entities, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            batchSize = batchSize > 0 ? Math.Min(batchSize, SqlDialectStrategy.Instance.MaxInValueCount) : SqlDialectStrategy.Instance.MaxInValueCount;
            return DbCommandStrategy.Instance.DeleteRangeCommands(connection, entities, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> DeleteRangeCommands<TEntity>(this OracleConnection connection, IEnumerable ids, int batchSize = 500) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            batchSize = batchSize > 0 ? Math.Min(batchSize, SqlDialectStrategy.Instance.MaxInValueCount) : SqlDialectStrategy.Instance.MaxInValueCount;
            return DbCommandStrategy.Instance.DeleteRangeCommands<TEntity>(connection, ids, batchSize, 0);
        }

        public static IReadOnlyList<DbCommandInfo> UpsertRangeCommands<TEntity>(this OracleConnection connection, IEnumerable<TEntity> entities, int batchSize = 500) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.UpsertRangeCommands(connection, entities, batchSize, 0);
        }

        public static DbCommandInfo ExistsCommand<TEntity>(this OracleConnection connection) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.ExistsCommand(connection, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo ExistsCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.ExistsCommand(connection, predicate);
        }

        public static DbCommandInfo ExistsCommand<TEntity>(this OracleConnection connection, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.ExistsCommand(connection, filterNode);
        }

        public static DbCommandInfo CountCommand<TEntity>(this OracleConnection connection) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo CountCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, predicate);
        }

        public static DbCommandInfo CountCommand<TEntity>(this OracleConnection connection, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, filterNode);
        }

        public static DbCommandInfo CountCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, selector, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo CountCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, selector, predicate);
        }

        public static DbCommandInfo CountCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, selector, filterNode);
        }

        public static DbCommandInfo CountCommand<TEntity>(this OracleConnection connection, string propertyName) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, propertyName, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo CountCommand<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, propertyName, predicate);
        }

        public static DbCommandInfo CountCommand<TEntity>(this OracleConnection connection, string propertyName, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.CountCommand(connection, propertyName, filterNode);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.AvgCommand(connection, selector, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.AvgCommand(connection, selector, predicate);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.AvgCommand(connection, selector, filterNode);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this OracleConnection connection, string propertyName) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.AvgCommand(connection, propertyName, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.AvgCommand(connection, propertyName, predicate);
        }

        public static DbCommandInfo AvgCommand<TEntity>(this OracleConnection connection, string propertyName, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.AvgCommand(connection, propertyName, filterNode);
        }

        public static DbCommandInfo SumCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.SumCommand(connection, selector, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo SumCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.SumCommand(connection, selector, predicate);
        }

        public static DbCommandInfo SumCommand<TEntity>(this OracleConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.SumCommand(connection, selector, filterNode);
        }

        public static DbCommandInfo SumCommand<TEntity>(this OracleConnection connection, string propertyName) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.SumCommand(connection, propertyName, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo SumCommand<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.SumCommand(connection, propertyName, predicate);
        }

        public static DbCommandInfo SumCommand<TEntity>(this OracleConnection connection, string propertyName, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.SumCommand(connection, propertyName, filterNode);
        }

        public static DbCommandInfo MinCommand<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MinCommand(connection, selector, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo MinCommand<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MinCommand(connection, selector, predicate);
        }

        public static DbCommandInfo MinCommand<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MinCommand(connection, selector, filterNode);
        }

        public static DbCommandInfo MinCommand<TEntity>(this OracleConnection connection, string propertyName) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MinCommand(connection, propertyName, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo MinCommand<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MinCommand(connection, propertyName, predicate);
        }

        public static DbCommandInfo MinCommand<TEntity>(this OracleConnection connection, string propertyName, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MinCommand(connection, propertyName, filterNode);
        }

        public static DbCommandInfo MaxCommand<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MaxCommand(connection, selector, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo MaxCommand<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MaxCommand(connection, selector, predicate);
        }

        public static DbCommandInfo MaxCommand<TEntity, TProperty>(this OracleConnection connection, Expression<Func<TEntity, TProperty?>> selector, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MaxCommand(connection, selector, filterNode);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this OracleConnection connection, string propertyName) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MaxCommand(connection, propertyName, (IFilterNode<TEntity>?)null);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this OracleConnection connection, string propertyName, Expression<Func<TEntity, bool>>? predicate) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MaxCommand(connection, propertyName, predicate);
        }

        public static DbCommandInfo MaxCommand<TEntity>(this OracleConnection connection, string propertyName, IFilterNode<TEntity>? filterNode) where TEntity : class
        {
            DbCommandStrategy.Instance.LoadRuntimeCache<TEntity>(connection);
            return DbCommandStrategy.Instance.MaxCommand(connection, propertyName, filterNode);
        }
    }
}
