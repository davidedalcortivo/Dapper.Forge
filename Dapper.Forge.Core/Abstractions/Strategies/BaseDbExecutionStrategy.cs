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

        public virtual IReadOnlyList<TEntity> GetAll<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetAllCommand(predicate, sortDescriptors);
            return connection.Query<TEntity>(command.Sql, command.Parameters, transaction, true, commandTimeout).AsList();
        }

        public virtual IReadOnlyList<TEntity> GetAll<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetAllCommand<TEntity>(filterNode, sortDescriptors);
            return connection.Query<TEntity>(command.Sql, command.Parameters, transaction, true, commandTimeout).AsList();
        }

        public virtual TEntity GetFirst<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstCommand(predicate, sortDescriptors);
            return connection.QueryFirst<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual TEntity GetFirst<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstCommand<TEntity>(filterNode, sortDescriptors);
            return connection.QueryFirst<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual TEntity? GetFirstOrDefault<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstOrDefaultCommand(predicate, sortDescriptors);
            return connection.QueryFirstOrDefault<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual TEntity? GetFirstOrDefault<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetFirstOrDefaultCommand<TEntity>(filterNode, sortDescriptors);
            return connection.QueryFirstOrDefault<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual TEntity GetSingle<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleCommand(predicate);
            return connection.QuerySingle<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual TEntity GetSingle<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleCommand<TEntity>(filterNode);
            return connection.QuerySingle<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual TEntity? GetSingleOrDefault<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleOrDefaultCommand(predicate);
            return connection.QuerySingleOrDefault<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual TEntity? GetSingleOrDefault<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetSingleOrDefaultCommand<TEntity>(filterNode);
            return connection.QuerySingleOrDefault<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual TEntity? GetById<TEntity>(DbConnection connection, object id, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetByIdCommand<TEntity>(id);
            return connection.QueryFirstOrDefault<TEntity>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual IReadOnlyList<TEntity> GetPage<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetPageCommand(predicate, sortDescriptors, skip, take);
            return connection.Query<TEntity>(command.Sql, command.Parameters, transaction, true, commandTimeout).AsList();
        }

        public virtual IReadOnlyList<TEntity> GetPage<TEntity>(DbConnection connection, IFilterNode? filterNode, IEnumerable<SortDescriptor>? sortDescriptors, int? skip, int? take, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.GetPageCommand<TEntity>(filterNode, sortDescriptors, skip, take);
            return connection.Query<TEntity>(command.Sql, command.Parameters, transaction, true, commandTimeout).AsList();
        }

        public virtual int Update<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand(entity);
            return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual int Update<TEntity>(DbConnection connection, object param, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand(param, predicate);
            return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual int Update<TEntity>(DbConnection connection, object param, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpdateCommand<TEntity>(param, filterNode);
            return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual int Insert<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.InsertCommand(entity);
            return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual int Delete<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand(entity);
            return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual int Delete<TEntity>(DbConnection connection, object id, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand<TEntity>(id);
            return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual int Delete<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand(predicate);
            return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual int Delete<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.DeleteCommand<TEntity>(filterNode);
            return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual int Upsert<TEntity>(DbConnection connection, TEntity entity, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.UpsertCommand(entity);
            return connection.Execute(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual IReadOnlyList<TEntity?> GetByIdRange<TEntity>(DbConnection connection, IEnumerable ids, bool preserveDuplicates, bool preserveNulls, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout) where TEntity : class
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
                IEnumerable<TEntity> entities = connection.Query<TEntity>(command.Sql, command.Parameters, transaction, true, commandTimeout);

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

        public virtual int UpdateRange<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.UpdateRangeCommands(entities, batchSize, chunkSize);
            return ExecuteRange(connection, commands, transaction, commandTimeout);
        }

        public virtual int InsertRange<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.InsertRangeCommands(entities, batchSize, chunkSize);
            return ExecuteRange(connection, commands, transaction, commandTimeout);
        }

        public virtual int DeleteRange<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.DeleteRangeCommands(entities, batchSize, chunkSize);
            return ExecuteRange(connection, commands, transaction, commandTimeout);
        }

        public virtual int DeleteRange<TEntity>(DbConnection connection, IEnumerable ids, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.DeleteRangeCommands<TEntity>(ids, batchSize, chunkSize);
            return ExecuteRange(connection, commands, transaction, commandTimeout);
        }

        public virtual int UpsertRange<TEntity>(DbConnection connection, IEnumerable<TEntity> entities, int batchSize, int chunkSize, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            IReadOnlyList<DbCommandInfo> commands = dbCommandStrategy.UpsertRangeCommands(entities, batchSize, chunkSize);
            return ExecuteRange(connection, commands, transaction, commandTimeout);
        }

        public virtual bool Exists<TEntity>(DbConnection connection, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.ExistsCommand(predicate);
            return connection.ExecuteScalar<bool>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual bool Exists<TEntity>(DbConnection connection, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.ExistsCommand<TEntity>(filterNode);
            return connection.ExecuteScalar<bool>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual int Count<TEntity>(DbConnection connection, Expression<Func<TEntity, object?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(selector, predicate);
            return connection.ExecuteScalar<int>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual int Count<TEntity>(DbConnection connection, Expression<Func<TEntity, object?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(selector, filterNode);
            return connection.ExecuteScalar<int>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual int Count<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand(propertyName, predicate);
            return connection.ExecuteScalar<int>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual int Count<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.CountCommand<TEntity>(propertyName, filterNode);
            return connection.ExecuteScalar<int>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Avg<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(selector, predicate);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Avg<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(selector, filterNode);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Avg<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand(propertyName, predicate);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Avg<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.AvgCommand<TEntity>(propertyName, filterNode);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Sum<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(selector, predicate);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Sum<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(selector, filterNode);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Sum<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand(propertyName, predicate);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Sum<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.SumCommand<TEntity>(propertyName, filterNode);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Min<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(selector, predicate);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Min<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(selector, filterNode);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Min<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand(propertyName, predicate);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Min<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MinCommand<TEntity>(propertyName, filterNode);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Max<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(selector, predicate);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Max<TEntity>(DbConnection connection, Expression<Func<TEntity, decimal?>>? selector, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(selector, filterNode);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Max<TEntity>(DbConnection connection, string? propertyName, Expression<Func<TEntity, bool>>? predicate, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand(propertyName, predicate);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }

        public virtual decimal? Max<TEntity>(DbConnection connection, string? propertyName, IFilterNode? filterNode, DbTransaction? transaction, int? commandTimeout) where TEntity : class
        {
            DbCommandInfo command = dbCommandStrategy.MaxCommand<TEntity>(propertyName, filterNode);
            return connection.ExecuteScalar<decimal?>(command.Sql, command.Parameters, transaction, commandTimeout);
        }
    }
}
