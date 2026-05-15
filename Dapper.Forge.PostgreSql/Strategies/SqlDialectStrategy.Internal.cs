using System.Data.Common;


namespace Dapper.Forge.PostgreSql.Strategies
{
    internal partial class SqlDialectStrategy
    {
        protected override void InitializeImpl(DbConnection connection)
        {
            DefaultSchemaName = "public";
        }
    }
}
