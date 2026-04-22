using Dapper.Forge.Core.Abstractions.Strategies;


namespace Dapper.Forge.SqlServer.Strategies
{
    internal class SqlDialectStrategy : BaseSqlDialectStrategy
    {
        public static SqlDialectStrategy Instance { get; } = new();

        public int MaxParameterCount { get; }
        public int MaxInsertRowCount { get; }

        private SqlDialectStrategy()
        {
            MaxParameterCount = 2100;
            MaxInsertRowCount = 1000;
        }

        public override string RenderIdentifier(string name)
        {
            return "[" + name + "]";
        }

        public override string Concat(params string[] parts)
        {
            return string.Join(" + ", parts);
        }
    }
}
