using System.Data.Common;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    public abstract partial class BaseSqlDialectStrategy : ISqlDialectStrategy
    {
        protected virtual void InitializeImpl(DbConnection connection)
        {

        }
    }
}
