namespace Forget.Tests.Core
{
    internal static class DialectStrategies
    {
        public static TheoryData<string, object> All()
        {
            return new()
            {
                { "SqlServer", Forget.SqlServer.Strategies.SqlDialectStrategy.Instance },
                { "MySql", Forget.MySql.Strategies.SqlDialectStrategy.Instance },
                { "PostgreSql", Forget.PostgreSql.Strategies.SqlDialectStrategy.Instance },
                { "Oracle", Forget.Oracle.Strategies.SqlDialectStrategy.Instance }
            };
        }
    }
}
