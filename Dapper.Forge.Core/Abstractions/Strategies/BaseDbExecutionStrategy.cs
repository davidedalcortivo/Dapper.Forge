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
        public ISqlDialectStrategy SqlDialectStrategy { get; }

        protected readonly TStrategy dbCommandStrategy;

        protected BaseDbExecutionStrategy(TStrategy dbCommandStrategy)
        {
            this.dbCommandStrategy = dbCommandStrategy;
            SqlDialectStrategy = this.dbCommandStrategy.SqlDialectStrategy;
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetAllCommand(predicate, sortDescriptors);

            if (sync)
                return connection.Query<TEntity>(command.Sql, command.Parameters, transaction, true, commandTimeout).AsList();

            return (await connection.QueryAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken))).AsList();
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetAllImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetAllCommand<TEntity>(filterNode, sortDescriptors);

            if (sync)
                return connection.Query<TEntity>(command.Sql, command.Parameters, transaction, true, commandTimeout).AsList();

            return (await connection.QueryAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken))).AsList();
        }

        public virtual async Task<TEntity> GetFirstImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstCommand(predicate, sortDescriptors);

            if (sync)
                return connection.QueryFirst<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.QueryFirstAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity> GetFirstImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstCommand<TEntity>(filterNode, sortDescriptors);

            if (sync)
                return connection.QueryFirst<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.QueryFirstAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity?> GetFirstOrDefaultImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstOrDefaultCommand(predicate, sortDescriptors);

            if (sync)
                return connection.QueryFirstOrDefault<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.QueryFirstOrDefaultAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity?> GetFirstOrDefaultImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstOrDefaultCommand<TEntity>(filterNode, sortDescriptors);

            if (sync)
                return connection.QueryFirstOrDefault<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.QueryFirstOrDefaultAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity> GetSingleImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleCommand(predicate);

            if (sync)
                return connection.QuerySingle<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.QuerySingleAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity> GetSingleImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleCommand<TEntity>(filterNode);

            if (sync)
                return connection.QuerySingle<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.QuerySingleAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity?> GetSingleOrDefaultImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleOrDefaultCommand<TEntity>(predicate);

            if (sync)
                return connection.QuerySingleOrDefault<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.QuerySingleOrDefaultAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity?> GetSingleOrDefaultImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleOrDefaultCommand<TEntity>(filterNode);

            if (sync)
                return connection.QuerySingleOrDefault<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.QuerySingleOrDefaultAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<TEntity?> GetByIdImplAsync<TEntity>(DbConnection connection, bool sync, object id, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetByIdCommand<TEntity>(id);

            if (sync)
                return connection.QueryFirstOrDefault<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.QueryFirstOrDefaultAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetPageImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetPageCommand(predicate, sortDescriptors, skip, take);

            if (sync)
                return connection.Query<TEntity>(command.Sql, command.Parameters, transaction, true, commandTimeout).AsList();

            return (await connection.QueryAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken))).AsList();
        }

        public virtual async Task<IReadOnlyList<TEntity>> GetPageImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetPageCommand<TEntity>(filterNode, sortDescriptors, skip, take);

            if (sync)
                return connection.Query<TEntity>(command.Sql, command.Parameters, transaction, true, commandTimeout).AsList();

            return (await connection.QueryAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken))).AsList();
        }

        public virtual async Task<int> UpdateImplAsync<TEntity>(DbConnection connection, bool sync, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand(entity);

            if (sync)
                return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> UpdateImplAsync<TEntity>(DbConnection connection, bool sync, object param, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand(param, predicate);

            if (sync)
                return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> UpdateImplAsync<TEntity>(DbConnection connection, bool sync, object param, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand<TEntity>(param, filterNode);

            if (sync)
                return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> InsertImplAsync<TEntity>(DbConnection connection, bool sync, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.InsertCommand(entity);

            if (sync)
                return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> DeleteImplAsync<TEntity>(DbConnection connection, bool sync, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand(entity);

            if (sync)
                return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> DeleteImplAsync<TEntity>(DbConnection connection, bool sync, object id, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand<TEntity>(id);

            if (sync)
                return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> DeleteImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand(predicate);

            if (sync)
                return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> DeleteImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand<TEntity>(filterNode);

            if (sync)
                return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> UpsertImplAsync<TEntity>(DbConnection connection, bool sync, TEntity entity, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpsertCommand(entity);

            if (sync)
                return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteAsync(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<IReadOnlyList<TEntity?>> GetByIdRangeImplAsync<TEntity>(DbConnection connection, bool sync, IEnumerable ids, bool preserveDuplicates, bool preserveNulls, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
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

            if (commands.Count == 0)
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
                IEnumerable<TEntity> entities;

                if (sync)
                    entities = connection.Query<TEntity>(command.Sql, command.Parameters, transaction, true, commandTimeout);
                else
                    entities = await connection.QueryAsync<TEntity>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));

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
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.UpdateRangeCommands(entities, batchSize, chunkSize);
            return await ExecuteRangeImplAsync(connection, sync, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> InsertRangeImplAsync<TEntity>(DbConnection connection, bool sync, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.InsertRangeCommands(entities, batchSize, chunkSize);
            return await ExecuteRangeImplAsync(connection, sync, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> DeleteRangeImplAsync<TEntity>(DbConnection connection, bool sync, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.DeleteRangeCommands(entities, batchSize, chunkSize);
            return await ExecuteRangeImplAsync(connection, sync, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> DeleteRangeImplAsync<TEntity>(DbConnection connection, bool sync, IEnumerable ids, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.DeleteRangeCommands<TEntity>(ids, batchSize, chunkSize);
            return await ExecuteRangeImplAsync(connection, sync, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<int> UpsertRangeImplAsync<TEntity>(DbConnection connection, bool sync, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.UpsertRangeCommands(entities, batchSize, chunkSize);
            return await ExecuteRangeImplAsync(connection, sync, commands, transaction, commandTimeout, cancellationToken);
        }

        public virtual async Task<bool> ExistsImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.ExistsCommand(predicate);

            if (sync)
                return connection.ExecuteScalar<bool>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<bool> ExistsImplAsync<TEntity>(DbConnection connection, bool sync, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.ExistsCommand<TEntity>(filterNode);

            if (sync)
                return connection.ExecuteScalar<bool>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> CountImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, object?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(selector, predicate);

            if (sync)
                return connection.ExecuteScalar<int>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<int>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> CountImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, object?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(selector, filterNode);

            if (sync)
                return connection.ExecuteScalar<int>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<int>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> CountImplAsync<TEntity>(DbConnection connection, bool sync, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(propertyName, predicate);

            if (sync)
                return connection.ExecuteScalar<int>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<int>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<int> CountImplAsync<TEntity>(DbConnection connection, bool sync, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand<TEntity>(propertyName, filterNode);

            if (sync)
                return connection.ExecuteScalar<int>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<int>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> AvgImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(selector, predicate);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> AvgImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(selector, filterNode);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> AvgImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(propertyName, predicate);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> AvgImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand<TEntity>(propertyName, filterNode);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> SumImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(selector, predicate);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> SumImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(selector, filterNode);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> SumImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(propertyName, predicate);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> SumImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand<TEntity>(propertyName, filterNode);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MinImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(selector, predicate);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MinImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(selector, filterNode);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MinImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(propertyName, predicate);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MinImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand<TEntity>(propertyName, filterNode);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MaxImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(selector, predicate);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);

            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MaxImplAsync<TEntity>(DbConnection connection, bool sync, Expression<Func<TEntity, decimal?>> selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(selector, filterNode);

            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
            
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MaxImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(propertyName, predicate);
            
            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
            
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }

        public virtual async Task<decimal?> MaxImplAsync<TEntity>(DbConnection connection, bool sync, string propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand<TEntity>(propertyName, filterNode);
            
            if (sync)
                return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
            
            return await connection.ExecuteScalarAsync<decimal?>(new CommandDefinition(command.Sql, command.Parameters, transaction, commandTimeout, cancellationToken: cancellationToken));
        }
    }
}
