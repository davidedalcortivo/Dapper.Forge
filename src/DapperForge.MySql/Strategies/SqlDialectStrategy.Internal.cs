using DapperForge.Core.Utilities;
using System.Data.Common;


namespace DapperForge.MySql.Strategies
{
    internal sealed partial class SqlDialectStrategy
    {
        protected override void InitializeImpl(DbConnection connection)
        {
            DefaultSchemaName = connection.Database;

            if (!IdentifierHelper.CharsetRegex().IsMatch(DefaultSchemaName))
                throw new InvalidOperationException($"The default schema name '{DefaultSchemaName}' contains invalid characters.");
        }
    }
}
