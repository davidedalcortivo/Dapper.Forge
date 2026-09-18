using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;


namespace Dapper.Forge.Tests.SqlServer
{
    /// <summary>
    /// Starts one real SqlServer container (via Testcontainers) for the lifetime of a test class, and creates the
    /// <c>dbo.Widget</c> table the integration tests run against. No explicit schema creation is needed: every
    /// SqlServer database already has a <c>dbo</c> schema by default, matching
    /// <see cref="Widget"/>'s <c>[Table(Schema = "dbo")]</c>.
    /// </summary>
    public sealed class SqlServerFixture : IAsyncLifetime
    {
        private MsSqlContainer _container = null!;

        public SqlConnection Connection { get; private set; } = null!;

        public async ValueTask InitializeAsync()
        {
            _container = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04")
                .Build();

            await _container.StartAsync();

            Connection = new SqlConnection(_container.GetConnectionString());
            await Connection.OpenAsync();

            await using SqlCommand command = Connection.CreateCommand();
            command.CommandText = """
                CREATE TABLE dbo.Widget (
                    Id INT PRIMARY KEY,
                    Name NVARCHAR(255) NOT NULL,
                    Nickname NVARCHAR(255) NULL,
                    Quantity INT NULL,
                    IsActive BIT NOT NULL,
                    Price DECIMAL(18,2) NOT NULL
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
