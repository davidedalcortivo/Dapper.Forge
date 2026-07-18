using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
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
            _ = await GetColumnsImplAsync<TEntity>(connection, sync, commandTimeout, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetAllCommand(connection, predicate, sortDescriptors);
            return await QueryImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetAllCommand<TEntity>(connection, filterNode, sortDescriptors);
            return await QueryImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity> GetFirstImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstCommand(connection, predicate, sortDescriptors);
            return await QueryFirstImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity> GetFirstImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstCommand<TEntity>(connection, filterNode, sortDescriptors);
            return await QueryFirstImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity?> GetFirstOrDefaultImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstOrDefaultCommand(connection, predicate, sortDescriptors);
            return await QueryFirstOrDefaultImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity?> GetFirstOrDefaultImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstOrDefaultCommand<TEntity>(connection, filterNode, sortDescriptors);
            return await QueryFirstOrDefaultImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity> GetSingleImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleCommand(connection, predicate);
            return await QuerySingleImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity> GetSingleImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleCommand<TEntity>(connection, filterNode);
            return await QuerySingleImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity?> GetSingleOrDefaultImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleOrDefaultCommand(connection, predicate);
            return await QuerySingleOrDefaultImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity?> GetSingleOrDefaultImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleOrDefaultCommand<TEntity>(connection, filterNode);
            return await QuerySingleOrDefaultImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<TEntity?> GetByIdImplAsync<TEntity>(DbConnection connection, bool sync, object id, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetByIdCommand<TEntity>(connection, id);
            return await QueryFirstOrDefaultImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetPageImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetPageCommand(connection, predicate, sortDescriptors, skip, take);
            return await QueryImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetPageImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetPageCommand<TEntity>(connection, filterNode, sortDescriptors, skip, take);
            return await QueryImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);
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

        public virtual async Task<int> UpdateImplAsync<TEntity>(DbConnection connection, bool sync, object param, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand<TEntity>(connection, param, filterNode);
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

        public virtual async Task<int> DeleteImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand<TEntity>(connection, filterNode);
            return await ExecuteImplAsync(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> UpsertImplAsync<TEntity>(DbConnection connection, bool sync, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpsertCommand(connection, entity);
            return await ExecuteImplAsync(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<TEntity?>> GetByIdRangeImplAsync<TEntity>(DbConnection connection, bool sync, IEnumerable ids, bool preserveDuplicates, bool preserveNulls, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
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
                IEnumerable<TEntity> entities = await QueryImplAsync<TEntity>(connection, sync, command, transaction, commandTimeout, cancellationToken);

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

        public virtual async Task<bool> ExistsImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.ExistsCommand<TEntity>(connection, filterNode);
            return await ExecuteScalarImplAsync<bool>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> CountImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, object?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(connection, selector, predicate);
            return await ExecuteScalarImplAsync<int>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> CountImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, object?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(connection, selector, filterNode);
            return await ExecuteScalarImplAsync<int>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> CountImplAsync<TEntity>(DbConnection connection, bool sync, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(connection, propertyName, predicate);
            return await ExecuteScalarImplAsync<int>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> CountImplAsync<TEntity>(DbConnection connection, bool sync, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand<TEntity>(connection, propertyName, filterNode);
            return await ExecuteScalarImplAsync<int>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> AvgImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(connection, selector, predicate);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> AvgImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(connection, selector, filterNode);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> AvgImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(connection, propertyName, predicate);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> AvgImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand<TEntity>(connection, propertyName, filterNode);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> SumImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(connection, selector, predicate);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> SumImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(connection, selector, filterNode);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> SumImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(connection, propertyName, predicate);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> SumImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand<TEntity>(connection, propertyName, filterNode);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> MinImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(connection, selector, predicate);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> MinImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(connection, selector, filterNode);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> MinImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(connection, propertyName, predicate);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> MinImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand<TEntity>(connection, propertyName, filterNode);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> MaxImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(connection, selector, predicate);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> MaxImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(connection, selector, filterNode);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> MaxImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(connection, propertyName, predicate);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<decimal?> MaxImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand<TEntity>(connection, propertyName, filterNode);
            return await ExecuteScalarImplAsync<decimal?>(connection, sync, command, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<IReadOnlyList<DbColumnInfo>> GetColumnsImplAsync<TEntity>(DbConnection connection, bool sync, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            string connectionId = SqlDialectStrategy.GetConnectionId(connection);
            IReadOnlyList<DbColumnInfo>? columns = DbColumnInfoCache<TEntity>.GetValueOrDefault(connectionId);

            if (columns is null)
            {
                SemaphoreSlim semaphore = DbColumnInfoCache<TEntity>.GetSemaphore(connectionId);

                if (sync)
                    semaphore.Wait(cancellationToken);
                else
                    await semaphore.WaitAsync(cancellationToken);

                try
                {
                    columns = DbColumnInfoCache<TEntity>.GetValueOrDefault(connectionId);

                    if (columns is null)
                    {
                        DbCommandInfo command = dbCommandStrategy.GetColumnsCommand<TEntity>(connection);
                        columns = await QueryImplAsync<DbColumnInfo>(connection, sync, command, null, commandTimeout, cancellationToken);

                        _ = DbColumnInfoCache<TEntity>.TryAdd(connectionId, columns);
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            
            return columns;
        }
    }
}
