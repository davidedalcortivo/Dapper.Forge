using Dapper.Forge.Core.Abstractions.Strategies;


namespace Dapper.Forge.MySql.Strategies
{
    internal class DbExecutionStrategy : BaseDbExecutionStrategy<DbCommandStrategy>
    {
        public static DbExecutionStrategy Instance { get; } = new(DbCommandStrategy.Instance);

        private DbExecutionStrategy(DbCommandStrategy strategy) : base(strategy) { }
    }
}
