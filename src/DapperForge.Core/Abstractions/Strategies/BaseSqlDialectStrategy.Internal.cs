using System.Data.Common;


namespace DapperForge.Core.Abstractions.Strategies
{
    internal abstract partial class BaseSqlDialectStrategy : ISqlDialectStrategy
    {
        protected virtual void InitializeImpl(DbConnection connection)
        {

        }
    }
}
