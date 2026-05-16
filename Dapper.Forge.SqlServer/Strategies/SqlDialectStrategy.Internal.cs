using System.Data.Common;


namespace Dapper.Forge.SqlServer.Strategies
{
    internal sealed partial class SqlDialectStrategy
    {
        protected override void InitializeImpl(DbConnection connection)
        {
            DefaultSchemaName = "dbo";
        }
    }
}
