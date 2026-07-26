using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Collections;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    internal abstract partial class BaseDbExecutionStrategy<TStrategy> : IDbExecutionStrategy where TStrategy : IDbCommandStrategy
    {
        public ISqlDialectStrategy SqlDialectStrategy { get; }

        protected readonly TStrategy dbCommandStrategy;

        protected BaseDbExecutionStrategy(TStrategy dbCommandStrategy)
        {
            this.dbCommandStrategy = dbCommandStrategy;
            SqlDialectStrategy = this.dbCommandStrategy.SqlDialectStrategy;
        }

        public virtual async Task LoadDbCacheImplAsync<TEntity>(DbConnection connection, bool sync, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            await GetColumnsImplAsync<TEntity>(connection, sync, commandTimeout, cancellationToken);
        }

        public abstract Task GetColumnsImplAsync<TEntity>(DbConnection connection, bool sync, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class;

        public virtual async Task<IReadOnlyList<TEntity>> GetAllImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetAllCommand(connection, predicate, sortDescriptors);
            return await QueryImplAsync<TEntity>(connection, sync, command, null, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode<TEntity>? filterNode, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetAllCommand(connection, filterNode, sortDescriptors);
            return await QueryImplAsync<TEntity>(connection, sync, command, null, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity> GetFirstImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstCommand(connection, predicate, sortDescriptors);
            return await QueryFirstImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity> GetFirstImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode<TEntity>? filterNode, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstCommand(connection, filterNode, sortDescriptors);
            return await QueryFirstImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity?> GetFirstOrDefaultImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstOrDefaultCommand(connection, predicate, sortDescriptors);
            return await QueryFirstOrDefaultImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity?> GetFirstOrDefaultImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode<TEntity>? filterNode, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstOrDefaultCommand(connection, filterNode, sortDescriptors);
            return await QueryFirstOrDefaultImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity> GetSingleImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleCommand(connection, predicate);
            return await QuerySingleImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity> GetSingleImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleCommand(connection, filterNode);
            return await QuerySingleImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity?> GetSingleOrDefaultImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleOrDefaultCommand(connection, predicate);
            return await QuerySingleOrDefaultImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity?> GetSingleOrDefaultImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleOrDefaultCommand(connection, filterNode);
            return await QuerySingleOrDefaultImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity?> GetByIdImplAsync<TEntity>(DbConnection connection, bool sync, object id, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetByIdCommand<TEntity>(connection, id);
            return await QueryFirstOrDefaultImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetPageImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetPageCommand(connection, predicate, sortDescriptors, skip, take);
            return await QueryImplAsync<TEntity>(connection, sync, command, take, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetPageImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode<TEntity>? filterNode, IEnumerable<SortDescriptor<TEntity>>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetPageCommand(connection, filterNode, sortDescriptors, skip, take);
            return await QueryImplAsync<TEntity>(connection, sync, command, take, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> UpdateImplAsync<TEntity>(DbConnection connection, bool sync, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand(connection, entity);
            return await ExecuteImplAsync(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> UpdateImplAsync<TEntity>(DbConnection connection, bool sync, object param, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand(connection, param, predicate);
            return await ExecuteImplAsync(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> UpdateImplAsync<TEntity>(DbConnection connection, bool sync, object param, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand(connection, param, filterNode);
            return await ExecuteImplAsync(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> InsertImplAsync<TEntity>(DbConnection connection, bool sync, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.InsertCommand(connection, entity);
            return await ExecuteImplAsync(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> DeleteImplAsync<TEntity>(DbConnection connection, bool sync, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand(connection, entity);
            return await ExecuteImplAsync(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> DeleteImplAsync<TEntity>(DbConnection connection, bool sync, object id, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand<TEntity>(connection, id);
            return await ExecuteImplAsync(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> DeleteImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand(connection, predicate);
            return await ExecuteImplAsync(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> DeleteImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand(connection, filterNode);
            return await ExecuteImplAsync(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> UpsertImplAsync<TEntity>(DbConnection connection, bool sync, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpsertCommand(connection, entity);
            return await ExecuteImplAsync(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity?>> GetByIdRangeImplAsync<TEntity>(DbConnection connection, bool sync, IEnumerable ids, bool preserveDuplicates, bool preserveNulls, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            ArgumentNullException.ThrowIfNull(ids);

            List<object?> idList = [];
            HashSet<object?> idSet = [];

            if (preserveDuplicates)
            {
                foreach (object? id in ids)
                {
                    idList.Add(id);
                    idSet.Add(id);
                }
            }
            else
            {
                foreach (object? id in ids)
                {
                    if (idSet.Add(id))
                        idList.Add(id);
                }
            }

            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.GetByIdRangeCommands<TEntity>(connection, idSet, batchSize, chunkSize);
            List<TEntity> entityList = [];

            if (commands.Count == 0)
                return entityList;

            PropertyInfo idProperty = EntityInfoCache<TEntity>.IdProperty;
            Func<TEntity, object?> propertyGetter = EntityInfoCache<TEntity>.PropertyGettersByPropertyName[idProperty.Name];
            Dictionary<object, List<int>> indexesById = new(idSet.Count);
            TEntity?[] entityArray = new TEntity?[idList.Count];

            for (int i = 0; i < idList.Count; i++)
            {
                if (!indexesById.TryGetValue(idList[i]!, out List<int>? indexes))
                    indexesById[idList[i]!] = indexes = [];

                indexes.Add(i);
            }

            foreach (DbCommandInfo command in commands)
            {
                IEnumerable<TEntity> entities = await QueryImplAsync<TEntity>(connection, sync, command, null, transaction, commandTimeout, cancellationToken);

                foreach (TEntity entity in entities)
                {
                    object id = propertyGetter(entity)!;
                    List<int> indexes = indexesById[id];

                    foreach (int index in indexes)
                        entityArray[index] = entity;
                }
            }

            if (preserveNulls)
                return entityArray;

            foreach (TEntity? entity in entityArray)
            {
                if (entity is not null)
                    entityList.Add(entity);
            }

            return entityList;
        }

        public virtual async Task<int> UpdateRangeImplAsync<TEntity>(DbConnection connection, bool sync, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.UpdateRangeCommands(connection, entities, batchSize, chunkSize);
            return await ExecuteRangeImplAsync(connection, sync, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> InsertRangeImplAsync<TEntity>(DbConnection connection, bool sync, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.InsertRangeCommands(connection, entities, batchSize, chunkSize);
            return await ExecuteRangeImplAsync(connection, sync, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> DeleteRangeImplAsync<TEntity>(DbConnection connection, bool sync, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.DeleteRangeCommands(connection, entities, batchSize, chunkSize);
            return await ExecuteRangeImplAsync(connection, sync, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> DeleteRangeImplAsync<TEntity>(DbConnection connection, bool sync, IEnumerable ids, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.DeleteRangeCommands<TEntity>(connection, ids, batchSize, chunkSize);
            return await ExecuteRangeImplAsync(connection, sync, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> UpsertRangeImplAsync<TEntity>(DbConnection connection, bool sync, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.UpsertRangeCommands(connection, entities, batchSize, chunkSize);
            return await ExecuteRangeImplAsync(connection, sync, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<bool> ExistsImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.ExistsCommand(connection, predicate);
            return await ExecuteScalarImplAsync<bool>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<bool> ExistsImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.ExistsCommand(connection, filterNode);
            return await ExecuteScalarImplAsync<bool>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<long> CountImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(connection, predicate);
            return await ExecuteScalarImplAsync<long>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<long> CountImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(connection, filterNode);
            return await ExecuteScalarImplAsync<long>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<long> CountImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(connection, selector, predicate);
            return await ExecuteScalarImplAsync<long>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<long> CountImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, object?>> selector, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(connection, selector, filterNode);
            return await ExecuteScalarImplAsync<long>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<long> CountImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(connection, propertyName, predicate);
            return await ExecuteScalarImplAsync<long>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<long> CountImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(connection, propertyName, filterNode);
            return await ExecuteScalarImplAsync<long>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> AvgImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(connection, selector, predicate);
            return (await ExecuteScalarImplAsync<string?>(connection, sync, command, transaction, commandTimeout, cancellationToken)).ParseDecimal();
        }

        public virtual async Task<decimal?> AvgImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(connection, selector, filterNode);
            return (await ExecuteScalarImplAsync<string?>(connection, sync, command, transaction, commandTimeout, cancellationToken)).ParseDecimal();
        }

        public virtual async Task<decimal?> AvgImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(connection, propertyName, predicate);
            return (await ExecuteScalarImplAsync<string?>(connection, sync, command, transaction, commandTimeout, cancellationToken)).ParseDecimal();
        }

        public virtual async Task<decimal?> AvgImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(connection, propertyName, filterNode);
            return (await ExecuteScalarImplAsync<string?>(connection, sync, command, transaction, commandTimeout, cancellationToken)).ParseDecimal();
        }

        public virtual async Task<decimal?> SumImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(connection, selector, predicate);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> SumImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(connection, selector, filterNode);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> SumImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(connection, propertyName, predicate);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> SumImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(connection, propertyName, filterNode);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TProperty?> MinImplAsync<TEntity, TProperty>(DbConnection connection, bool sync, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(connection, selector, predicate);
            return await ExecuteScalarImplAsync<TProperty?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TProperty?> MinImplAsync<TEntity, TProperty>(DbConnection connection, bool sync, Expression<Func<TEntity, TProperty?>> selector, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(connection, selector, filterNode);
            return await ExecuteScalarImplAsync<TProperty?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TProperty?> MinImplAsync<TEntity, TProperty>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(connection, propertyName, predicate);
            return await ExecuteScalarImplAsync<TProperty?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TProperty?> MinImplAsync<TEntity, TProperty>(DbConnection connection, bool sync, string propertyName, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(connection, propertyName, filterNode);
            return await ExecuteScalarImplAsync<TProperty?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TProperty?> MaxImplAsync<TEntity, TProperty>(DbConnection connection, bool sync, Expression<Func<TEntity, TProperty?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(connection, selector, predicate);
            return await ExecuteScalarImplAsync<TProperty?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TProperty?> MaxImplAsync<TEntity, TProperty>(DbConnection connection, bool sync, Expression<Func<TEntity, TProperty?>> selector, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(connection, selector, filterNode);
            return await ExecuteScalarImplAsync<TProperty?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TProperty?> MaxImplAsync<TEntity, TProperty>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(connection, propertyName, predicate);
            return await ExecuteScalarImplAsync<TProperty?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TProperty?> MaxImplAsync<TEntity, TProperty>(DbConnection connection, bool sync, string propertyName, IFilterNode<TEntity>? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(connection, propertyName, filterNode);
            return await ExecuteScalarImplAsync<TProperty?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }
    }
}
