using Npgsql;
using Testcontainers.PostgreSql;


namespace Dapper.Forge.Tests.PostgreSql
{
    /// <summary>
    /// Starts one real PostgreSql container (via Testcontainers) for the lifetime of a test class, and creates the
    /// <c>dbo."Widget"</c> table the integration tests run against. Shared across every test in the class rather
    /// than started per test, since spinning up a container is the expensive part.
    /// </summary>
    public sealed class PostgreSqlFixture : IAsyncLifetime
    {
        private PostgreSqlContainer _container = null!;

        public NpgsqlConnection Connection { get; private set; } = null!;

        public async ValueTask InitializeAsync()
        {
            _container = new PostgreSqlBuilder("postgres:16-alpine")
                .Build();

            await _container.StartAsync();

            Connection = new NpgsqlConnection(_container.GetConnectionString());
            await Connection.OpenAsync();

            await using NpgsqlCommand command = Connection.CreateCommand();
            command.CommandText = """
                CREATE SCHEMA IF NOT EXISTS dbo;
                CREATE TABLE dbo."Widget" (
                    "Id" INTEGER PRIMARY KEY,
                    "Name" TEXT NOT NULL,
                    "Nickname" TEXT NULL,
                    "Quantity" INTEGER NULL,
                    "IsActive" BOOLEAN NOT NULL,
                    "Price" NUMERIC(18,2) NOT NULL
                );
                """;
            await command.ExecuteNonQueryAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await Connection.DisposeAsync();
            await _container.DisposeAsync();
        }
    }
}
