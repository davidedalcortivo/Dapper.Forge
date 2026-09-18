using DapperForge.Core.Utilities;
using System.Data.Common;


namespace DapperForge.SqlServer.Strategies
{
    internal sealed partial class SqlDialectStrategy
    {
        protected override void InitializeImpl(DbConnection connection)
        {
            DefaultSchemaName = "dbo";

            if (!IdentifierHelper.CharsetRegex().IsMatch(DefaultSchemaName))
                throw new InvalidOperationException($"The default schema name '{DefaultSchemaName}' contains invalid characters.");
        }
    }
}
