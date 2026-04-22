using Dapper.Forge.Core.Abstractions.Strategies;


namespace Dapper.Forge.Oracle.Strategies
{
    internal class SqlDialectStrategy : BaseSqlDialectStrategy
    {
        public static SqlDialectStrategy Instance { get; } = new();

        public int MaxInValueCount { get; }
        
        private SqlDialectStrategy()
        {
            MaxInValueCount = 1000;
        }

        public override string Terminator { get; } = Environment.NewLine;

        public override string RenderParameter(string name)
        {
            return ":" + name;
        }
    }
}
