using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using System.Collections;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    public abstract partial class BaseDbExecutionStrategy<TStrategy> : IDbExecutionStrategy where TStrategy : IDbCommandStrategy
    {
        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetAllCommand(predicate, sortDescriptors);
            return (await connection.QueryAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken))).AsList();
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetAllCommand<TEntity>(filterNode, sortDescriptors);
            return (await connection.QueryAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken))).AsList();
        }

        public virtual async Task<TEntity> GetFirstAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstCommand(predicate, sortDescriptors);
            return await connection.QueryFirstAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity> GetFirstAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstCommand<TEntity>(filterNode, sortDescriptors);
            return await connection.QueryFirstAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstOrDefaultCommand(predicate, sortDescriptors);
            return await connection.QueryFirstOrDefaultAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity?> GetFirstOrDefaultAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstOrDefaultCommand<TEntity>(filterNode, sortDescriptors);
            return await connection.QueryFirstOrDefaultAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity> GetSingleAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleCommand<TEntity>(predicate);
            return await connection.QuerySingleAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity> GetSingleAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleCommand<TEntity>(filterNode);
            return await connection.QuerySingleAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleOrDefaultCommand<TEntity>(predicate);
            return await connection.QuerySingleOrDefaultAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity?> GetSingleOrDefaultAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleOrDefaultCommand<TEntity>(filterNode);
            return await connection.QuerySingleOrDefaultAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity?> GetByIdAsync<TEntity>(DbConnection connection, object id, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetByIdCommand<TEntity>(id);
            return await connection.QueryFirstOrDefaultAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetPageCommand(predicate, sortDescriptors, skip, take);
            return (await connection.QueryAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken))).AsList();
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetPageAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetPageCommand<TEntity>(filterNode, sortDescriptors, skip, take);
            return (await connection.QueryAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken))).AsList();
        }

        public virtual async Task<int> UpdateAsync<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand(entity);
            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> UpdateAsync<TEntity>(DbConnection connection, object param, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand(param, predicate);
            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> UpdateAsync<TEntity>(DbConnection connection, object param, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand<TEntity>(param, filterNode);
            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> InsertAsync<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.InsertCommand(entity);
            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> DeleteAsync<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand<TEntity>(entity);
            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> DeleteAsync<TEntity>(DbConnection connection, object id, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand<TEntity>(id);
            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> DeleteAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand(predicate);
            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> DeleteAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand<TEntity>(filterNode);
            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> UpsertAsync<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpsertCommand(entity);
            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<IReadOnlyList<TEntity?>> GetByIdRangeAsync<TEntity>(DbConnection connection, IEnumerable ids, bool preserveDuplicates, bool preserveNulls, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            List<object?> idList = [];

            if (preserveDuplicates)
            {
                foreach (object? id in ids)
                    idList.Add(id);
            }
            else
            {
                HashSet<object?> idSet = [];

                foreach (object? id in ids)
                {
                    if (idSet.Add(id))
                        idList.Add(id);
                }
            }

            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.GetByIdRangeCommands<TEntity>(idList, batchSize, chunkSize);
            List<TEntity> entityList = [];

            if (commands.Count <= 0)
                return entityList;

            PropertyInfo idPropertyInfo = EntityInfoCache<TEntity>.IdPropertyInfo;
            Func<TEntity, object?> propertyGetter = EntityInfoCache<TEntity>.PropertyGettersByPropertyName[idPropertyInfo.Name];
            Dictionary<object, List<int>> indexesById = new(idList.Count);
            TEntity?[] entityArray = new TEntity?[idList.Count];

            for (int i = 0; i < idList.Count; i++)
            {
                if (!indexesById.TryGetValue(idList[i]!, out List<int>? indexes))
                    indexesById[idList[i]!] = indexes = [];

                indexes.Add(i);
            }

            foreach (DbCommandInfo command in commands)
            {
                IEnumerable<TEntity> entities = await connection.QueryAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));

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

        public virtual async Task<int> UpdateRangeAsync<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.UpdateRangeCommands(entities, batchSize, chunkSize);
            return await ExecuteRangeAsync(connection, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> InsertRangeAsync<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.InsertRangeCommands(entities, batchSize, chunkSize);
            return await ExecuteRangeAsync(connection, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> DeleteRangeAsync<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.DeleteRangeCommands(entities, batchSize, chunkSize);
            return await ExecuteRangeAsync(connection, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> DeleteRangeAsync<TEntity>(DbConnection connection, IEnumerable ids, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.DeleteRangeCommands<TEntity>(ids, batchSize, chunkSize);
            return await ExecuteRangeAsync(connection, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> UpsertRangeAsync<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.UpsertRangeCommands(entities, batchSize, chunkSize);
            return await ExecuteRangeAsync(connection, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<bool> ExistsAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.ExistsCommand(predicate);
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<bool> ExistsAsync<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.ExistsCommand<TEntity>(filterNode);
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> CountAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, object?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(selector, predicate);
            return await connection.ExecuteScalarAsync<int>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> CountAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, object?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand<TEntity>(selector, filterNode);
            return await connection.ExecuteScalarAsync<int>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> CountAsync<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(propertyName, predicate);
            return await connection.ExecuteScalarAsync<int>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> CountAsync<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand<TEntity>(propertyName, filterNode);
            return await connection.ExecuteScalarAsync<int>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> AvgAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(selector, predicate);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> AvgAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand<TEntity>(selector, filterNode);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> AvgAsync<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(propertyName, predicate);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> AvgAsync<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand<TEntity>(propertyName, filterNode);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> SumAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(selector, predicate);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> SumAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand<TEntity>(selector, filterNode);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> SumAsync<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(propertyName, predicate);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> SumAsync<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand<TEntity>(propertyName, filterNode);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MinAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(selector, predicate);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MinAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand<TEntity>(selector, filterNode);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MinAsync<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(propertyName, predicate);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MinAsync<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand<TEntity>(propertyName, filterNode);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MaxAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(selector, predicate);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MaxAsync<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand<TEntity>(selector, filterNode);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MaxAsync<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(propertyName, predicate);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MaxAsync<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand<TEntity>(propertyName, filterNode);
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }
    }
}
