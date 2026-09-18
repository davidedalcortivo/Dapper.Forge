namespace Dapper.Forge.Tests.Core
{
    internal static class DialectStrategies
    {
        public static TheoryData<string, object> All()
        {
            return new()
            {
                { "SqlServer", Forge.SqlServer.Strategies.SqlDialectStrategy.Instance },
                { "MySql", Forge.MySql.Strategies.SqlDialectStrategy.Instance },
                { "PostgreSql", Forge.PostgreSql.Strategies.SqlDialectStrategy.Instance },
                { "Oracle", Forge.Oracle.Strategies.SqlDialectStrategy.Instance }
            };
        }
    }
}
