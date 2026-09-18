using Forget.Core.Utilities;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;


namespace Forget.Oracle.Strategies
{
    internal sealed partial class SqlDialectStrategy
    {
        protected override void InitializeImpl(DbConnection connection)
        {
            OracleConnectionStringBuilder builder = new(connection.ConnectionString);
            DefaultSchemaName = builder.UserID;

            if (!IdentifierHelper.CharsetRegex().IsMatch(DefaultSchemaName))
                throw new InvalidOperationException($"The default schema name '{DefaultSchemaName}' contains invalid characters.");
        }
    }
}
