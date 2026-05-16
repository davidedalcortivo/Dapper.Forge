using System.Data.Common;


namespace Dapper.Forge.MySql.Strategies
{
    internal sealed partial class SqlDialectStrategy
    {
        protected override void InitializeImpl(DbConnection connection)
        {
            DefaultSchemaName = connection.Database;
        }
    }
}
