using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Models;


namespace Dapper.Forge.Core.Caching
{
    public static class SqlBuilderCache<TEntity, TStrategy> where TEntity : class where TStrategy : ISqlBuilderStrategy
    {
        private static bool _isInitialized = false;
        private static readonly object _lock = new();

        public static SqlTemplate GetAllSql { get; private set; } = new();
        public static SqlTemplate GetFirstSql { get; private set; } = new();
        public static SqlTemplate GetByIdSql { get; private set; } = new();
        public static SqlTemplate UpdateSql { get; private set; } = new();
        public static SqlTemplate InsertSql { get; private set; } = new();
        public static SqlTemplate DeleteSql { get; private set; } = new();
        public static SqlTemplate UpsertSql { get; private set; } = new();
        public static SqlTemplate GetByIdRangeSql { get; private set; } = new();
        public static SqlTemplate UpdateRangeSql { get; private set; } = new();
        public static SqlTemplate InsertRangeSql { get; private set; } = new();
        public static SqlTemplate DeleteRangeSql { get; private set; } = new();
        public static SqlTemplate UpsertRangeSql { get; private set; } = new();
        public static SqlTemplate ExistsSql { get; private set; } = new();
        public static SqlTemplate CountSql { get; private set; } = new();
        public static SqlTemplate AvgSql { get; private set; } = new();
        public static SqlTemplate SumSql { get; private set; } = new();
        public static SqlTemplate MinSql { get; private set; } = new();
        public static SqlTemplate MaxSql { get; private set; } = new();

        public static void Initialize(TStrategy strategy)
        {
            if (!_isInitialized)
            {
                lock (_lock)
                {
                    if (!_isInitialized)
                    {
                        GetAllSql = strategy.GetAllSqlBuilder<TEntity>();
                        GetFirstSql = strategy.GetFirstSqlBuilder<TEntity>();
                        GetByIdSql = strategy.GetByIdSqlBuilder<TEntity>();
                        UpdateSql = strategy.UpdateSqlBuilder<TEntity>();
                        InsertSql = strategy.InsertSqlBuilder<TEntity>();
                        DeleteSql = strategy.DeleteSqlBuilder<TEntity>();
                        UpsertSql = strategy.UpsertSqlBuilder<TEntity>();
                        GetByIdRangeSql = strategy.GetByIdRangeSqlBuilder<TEntity>();
                        UpdateRangeSql = strategy.UpdateRangeSqlBuilder<TEntity>();
                        InsertRangeSql = strategy.InsertRangeSqlBuilder<TEntity>();
                        DeleteRangeSql = strategy.DeleteRangeSqlBuilder<TEntity>();
                        UpsertRangeSql = strategy.UpsertRangeSqlBuilder<TEntity>();
                        ExistsSql = strategy.ExistsSqlBuilder<TEntity>();
                        CountSql = strategy.CountSqlBuilder<TEntity>();
                        AvgSql = strategy.AvgSqlBuilder<TEntity>();
                        SumSql = strategy.SumSqlBuilder<TEntity>();
                        MinSql = strategy.MinSqlBuilder<TEntity>();
                        MaxSql = strategy.MaxSqlBuilder<TEntity>();

                        _isInitialized = true;
                    }
                }
            }
        }
    }
}
