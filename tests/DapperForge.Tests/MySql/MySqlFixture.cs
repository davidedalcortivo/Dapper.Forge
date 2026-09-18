using MySqlConnector;
using Testcontainers.MySql;


namespace DapperForge.Tests.MySql
{
    /// <summary>
    /// Starts one real MySql container (via Testcontainers) for the lifetime of a test class, and creates the
    /// <c>Widget</c> table the integration tests run against. The container's database is named <c>dbo</c> so it
    /// matches <see cref="Widget"/>'s <c>[Table(Schema = "dbo")]</c> — MySql treats "schema" and
    /// "database" as the same thing.
    /// </summary>
    public sealed class MySqlFixture : IAsyncLifetime
    {
        private MySqlContainer _container = null!;

        public MySqlConnection Connection { get; private set; } = null!;

        public async ValueTask InitializeAsync()
        {
            _container = new MySqlBuilder("mysql:8.4")
                .WithDatabase("dbo")
                .Build();

            await _container.StartAsync();

            Connection = new MySqlConnection(_container.GetConnectionString());
            await Connection.OpenAsync();

            await using MySqlCommand command = Connection.CreateCommand();
            command.CommandText = """
                CREATE TABLE `Widget` (
                    `Id` INT PRIMARY KEY,
                    `Name` VARCHAR(255) NOT NULL,
                    `Nickname` VARCHAR(255) NULL,
                    `Quantity` INT NULL,
                    `IsActive` TINYINT(1) NOT NULL,
                    `Price` DECIMAL(18,2) NOT NULL
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
