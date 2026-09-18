namespace DapperForge.Tests.Core
{
    internal static class DialectStrategies
    {
        public static TheoryData<string, object> All()
        {
            return new()
            {
                { "SqlServer", DapperForge.SqlServer.Strategies.SqlDialectStrategy.Instance },
                { "MySql", DapperForge.MySql.Strategies.SqlDialectStrategy.Instance },
                { "PostgreSql", DapperForge.PostgreSql.Strategies.SqlDialectStrategy.Instance },
                { "Oracle", DapperForge.Oracle.Strategies.SqlDialectStrategy.Instance }
            };
        }
    }
}
