using Forget.Oracle.Extensions;
using Oracle.ManagedDataAccess.Client;
using Testcontainers.Oracle;


namespace Forget.Tests.Oracle
{
    /// <summary>
    /// Starts one real Oracle container (via the official <c>Testcontainers.Oracle</c> module) for the lifetime
    /// of a test class. Creates a <c>"dbo"</c> user/schema (quoted and lowercase, matching <see cref="Widget"/>'s
    /// <c>[Table(Schema = "dbo")]</c> exactly — Oracle folds unquoted identifiers to uppercase, which would not
    /// match) owning the <c>"Widget"</c> table, then calls the real <c>LoadDbCacheAsync</c> so the column-cast
    /// PL/SQL block
    /// (<see cref="SqlBuilderStrategyTests.GetColumns_EmitsPlSqlBlockThatDiscoversColumnCastExpressions"/>) runs
    /// against an actual schema instead of hand-seeded metadata like in <see cref="InsertRangeCommandsTests"/>.
    /// </summary>
    public sealed class OracleFixture : IAsyncLifetime
    {
        private const string Password = "TestPwd_1";

        private OracleContainer _container = null!;

        public OracleConnection Connection { get; private set; } = null!;

        public async ValueTask InitializeAsync()
        {
            _container = new OracleBuilder("gvenzl/oracle-xe:21.3.0-slim-faststart")
                .WithPassword(Password)
                .Build();

            await _container.StartAsync();

            // The container's own connection string logs in as "oracle" - an application user with no DBA
            // privileges, so it can't CREATE USER. Swap in "system" (whose password is the same ORACLE_PASSWORD),
            // and stay connected as it throughout rather than authenticating as "dbo" directly: Oracle's login
            // negotiation folds an unquoted User Id to uppercase, which would not match the case-sensitive
            // lowercase "dbo" user created below, and getting a quoted User Id right in a connection string is its
            // own can of worms. ALTER SESSION SET CURRENT_SCHEMA lets a DBA-privileged connection create objects
            // owned by another schema without ever logging in as that user.
            string connectionString = _container.GetConnectionString().Replace("User Id=oracle;", "User Id=system;");
            Connection = new OracleConnection(connectionString);
            await Connection.OpenAsync();

            await ExecuteAsync(Connection, $"CREATE USER \"dbo\" IDENTIFIED BY \"{Password}\"");
            await ExecuteAsync(Connection, "GRANT CONNECT, RESOURCE, UNLIMITED TABLESPACE TO \"dbo\"");
            await ExecuteAsync(Connection, "ALTER SESSION SET CURRENT_SCHEMA = \"dbo\"");

            // IsActive is NUMBER(1), populated from Widget.IsActive (int, not bool - see Widget for why): on the
            // Oracle XE 21.3 image this module defaults to, binding a C# bool against a NUMBER column throws
            // ORA-00932 ("NUMBER expected, got BOOLEAN"), a limitation absent on Oracle 23c.
            await ExecuteAsync(Connection, """
                CREATE TABLE "Widget" (
                    "Id" NUMBER(10) PRIMARY KEY,
                    "Name" VARCHAR2(255) NOT NULL,
                    "Nickname" VARCHAR2(255) NULL,
                    "Quantity" NUMBER(10) NULL,
                    "IsActive" NUMBER(1) NOT NULL,
                    "Price" NUMBER(18,2) NOT NULL
                )
                """);

            await Connection.LoadDbCacheAsync<Widget>();
        }

        private static async Task ExecuteAsync(OracleConnection connection, string sql)
        {
            await using OracleCommand command = connection.CreateCommand();
            command.CommandText = sql;
            await command.ExecuteNonQueryAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await Connection.DisposeAsync();
            await _container.DisposeAsync();
        }
    }
}
