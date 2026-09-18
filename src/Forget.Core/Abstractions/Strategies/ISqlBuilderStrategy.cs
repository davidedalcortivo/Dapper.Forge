using Forget.Core.Models;


namespace Forget.Core.Abstractions.Strategies
{
    internal interface ISqlBuilderStrategy
    {
        ISqlDialectStrategy SqlDialectStrategy { get; }

        SqlTemplate GetColumnsSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate GetAllSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate GetFirstSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate GetByIdSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate UpdateSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate InsertSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate DeleteSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate UpsertSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate GetByIdRangeSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate UpdateRangeSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate InsertRangeSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate DeleteRangeSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate UpsertRangeSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate ExistsSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate CountSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate AvgSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate SumSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate MinSqlBuilder<TEntity>() where TEntity : class;
        SqlTemplate MaxSqlBuilder<TEntity>() where TEntity : class;
    }
}
