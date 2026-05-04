using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.SqlServer.Strategies;
using Microsoft.Data.SqlClient;
using System.Collections;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;


namespace Dapper.Forge.PostgreSql.Extensions
{
    public static partial class DbConnectionExtensions
    {
        public static IReadOnlyList<TEntity> GetAll<TEntity>(this SqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetAllImplAsync<TEntity>(connection, true, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetAllImplAsync(connection, true, predicate, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetAll<TEntity>(this SqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetAllImplAsync<TEntity>(connection, true, filterNode, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetFirst<TEntity>(this SqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstImplAsync<TEntity>(connection, true, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetFirst<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstImplAsync(connection, true, predicate, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetFirst<TEntity>(this SqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstImplAsync<TEntity>(connection, true, filterNode, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this SqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync<TEntity>(connection, true, (IFilterNode?)null, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync(connection, true, predicate, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetFirstOrDefault<TEntity>(this SqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetFirstOrDefaultImplAsync<TEntity>(connection, true, filterNode, sortDescriptors, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetSingle<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleImplAsync<TEntity>(connection, true, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetSingle<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleImplAsync(connection, true, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity GetSingle<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleImplAsync<TEntity>(connection, true, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync<TEntity>(connection, true, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync(connection, true, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetSingleOrDefault<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetSingleOrDefaultImplAsync<TEntity>(connection, true, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static TEntity? GetById<TEntity>(this SqlConnection connection, object id, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetByIdImplAsync<TEntity>(connection, true, id, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this SqlConnection connection, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetPageImplAsync<TEntity>(connection, true, (IFilterNode?)null, sortDescriptors, skip, take, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetPageImplAsync(connection, true, predicate, sortDescriptors, skip, take, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity> GetPage<TEntity>(this SqlConnection connection, IFilterNode filterNode, IEnumerable<SortDescriptor>? sortDescriptors = null, int? skip = null, int? take = null, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.GetPageImplAsync<TEntity>(connection, true, filterNode, sortDescriptors, skip, take, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Update<TEntity>(this SqlConnection connection, TEntity entity, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.UpdateImplAsync(connection, true, entity, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Update<TEntity>(this SqlConnection connection, object param, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.UpdateImplAsync<TEntity>(connection, true, param, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Update<TEntity>(this SqlConnection connection, object param, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.UpdateImplAsync(connection, true, param, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Update<TEntity>(this SqlConnection connection, object param, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.UpdateImplAsync<TEntity>(connection, true, param, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Insert<TEntity>(this SqlConnection connection, TEntity entity, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.InsertImplAsync(connection, true, entity, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this SqlConnection connection, TEntity entity, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.DeleteImplAsync(connection, true, entity, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this SqlConnection connection, object id, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.DeleteImplAsync<TEntity>(connection, true, id, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.DeleteImplAsync<TEntity>(connection, true, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.DeleteImplAsync(connection, true, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Delete<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.DeleteImplAsync<TEntity>(connection, true, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Upsert<TEntity>(this SqlConnection connection, TEntity entity, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.UpsertImplAsync(connection, true, entity, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static IReadOnlyList<TEntity?> GetByIdRange<TEntity>(this SqlConnection connection, IEnumerable ids, bool preserveDuplicates = false, bool preserveNulls = false, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            batchSize = Math.Min(batchSize, SqlDialectStrategy.Instance.MaxParameterCount);
            return DbExecutionStrategy.Instance.GetByIdRangeImplAsync<TEntity>(connection, true, ids, preserveDuplicates, preserveNulls, batchSize, 0, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int UpdateRange<TEntity>(this SqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            int chunkSize = SqlDialectStrategy.Instance.MaxParameterCount / propertyInfos.Length;

            return DbExecutionStrategy.Instance.UpdateRangeImplAsync(connection, true, entities, batchSize, chunkSize, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int InsertRange<TEntity>(this SqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            ImmutableArray<PropertyInfo> insertPropertyInfos = EntityInfoCache<TEntity>.InsertPropertyInfos;
            int chunkSize = Math.Min(SqlDialectStrategy.Instance.MaxParameterCount / insertPropertyInfos.Length, SqlDialectStrategy.Instance.MaxInsertRowCount);

            return DbExecutionStrategy.Instance.InsertRangeImplAsync(connection, true, entities, batchSize, chunkSize, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int DeleteRange<TEntity>(this SqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.DeleteRangeImplAsync(connection, true, entities, batchSize, SqlDialectStrategy.Instance.MaxParameterCount, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int DeleteRange<TEntity>(this SqlConnection connection, IEnumerable ids, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.DeleteRangeImplAsync<TEntity>(connection, true, ids, batchSize, SqlDialectStrategy.Instance.MaxParameterCount, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int UpsertRange<TEntity>(this SqlConnection connection, IEnumerable<TEntity> entities, int batchSize = 500, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            ImmutableArray<PropertyInfo> propertyInfos = EntityInfoCache<TEntity>.PropertyInfos;
            int chunkSize = SqlDialectStrategy.Instance.MaxParameterCount / propertyInfos.Length;

            return DbExecutionStrategy.Instance.UpsertRangeImplAsync(connection, true, entities, batchSize, chunkSize, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static bool Exists<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.ExistsImplAsync<TEntity>(connection, true, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static bool Exists<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.ExistsImplAsync(connection, true, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static bool Exists<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.ExistsImplAsync<TEntity>(connection, true, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Count<TEntity>(this SqlConnection connection, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.CountImplAsync<TEntity>(connection, true, (string?)null, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Count<TEntity>(this SqlConnection connection, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, (string?)null, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Count<TEntity>(this SqlConnection connection, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.CountImplAsync<TEntity>(connection, true, (string?)null, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Count<TEntity>(this SqlConnection connection, Expression<Func<TEntity, object?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, selector, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Count<TEntity>(this SqlConnection connection, Expression<Func<TEntity, object?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Count<TEntity>(this SqlConnection connection, Expression<Func<TEntity, object?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Count<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.CountImplAsync<TEntity>(connection, true, propertyName, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Count<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.CountImplAsync(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static int Count<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.CountImplAsync<TEntity>(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, selector, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.AvgImplAsync<TEntity>(connection, true, propertyName, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.AvgImplAsync(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Avg<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.AvgImplAsync<TEntity>(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, selector, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.SumImplAsync<TEntity>(connection, true, propertyName, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.SumImplAsync(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Sum<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.SumImplAsync<TEntity>(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Min<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.MinImplAsync(connection, true, selector, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Min<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.MinImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Min<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.MinImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Min<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.MinImplAsync<TEntity>(connection, true, propertyName, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Min<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.MinImplAsync(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Min<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.MinImplAsync<TEntity>(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Max<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.MaxImplAsync(connection, true, selector, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Max<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.MaxImplAsync(connection, true, selector, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Max<TEntity>(this SqlConnection connection, Expression<Func<TEntity, decimal?>> selector, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.MaxImplAsync(connection, true, selector, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Max<TEntity>(this SqlConnection connection, string propertyName, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.MaxImplAsync<TEntity>(connection, true, propertyName, (IFilterNode?)null, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Max<TEntity>(this SqlConnection connection, string propertyName, Expression<Func<TEntity, bool>> predicate, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.MaxImplAsync(connection, true, propertyName, predicate, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }

        public static decimal? Max<TEntity>(this SqlConnection connection, string propertyName, IFilterNode filterNode, SqlTransaction? transaction = null, int? commandTimeout = null) where TEntity : class
        {
            return DbExecutionStrategy.Instance.MaxImplAsync<TEntity>(connection, true, propertyName, filterNode, transaction, commandTimeout, CancellationToken.None).GetAwaiter().GetResult();
        }
    }
}
