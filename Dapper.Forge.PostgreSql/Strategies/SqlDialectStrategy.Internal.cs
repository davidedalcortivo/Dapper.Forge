using Dapper.Forge.Core.Utilities;
using System.Data.Common;


namespace Dapper.Forge.PostgreSql.Strategies
{
    internal sealed partial class SqlDialectStrategy
    {
        protected override void InitializeImpl(DbConnection connection)
        {
            DefaultSchemaName = "public";

            if (!IdentifierHelper.CharsetRegex().IsMatch(DefaultSchemaName))
                throw new InvalidOperationException($"The default schema name '{DefaultSchemaName}' contains invalid characters.");
        }
    }
}
