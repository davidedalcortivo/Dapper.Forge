using Dapper.Forge.Core.Utilities;
using System.Data.Common;


namespace Dapper.Forge.MySql.Strategies
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
