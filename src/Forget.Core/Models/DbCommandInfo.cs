using Dapper;


namespace Forget.Core.Models
{
    /// <summary>
    /// Represents a ready-to-execute SQL command, pairing the generated SQL text with its bound parameters.
    /// </summary>
    /// <remarks>
    /// Instances of this type are produced by the library's command-building pipeline. They are either executed
    /// internally against the database, or returned directly by the <c>*Command</c> extension methods (for example
    /// <c>GetAllCommand</c>, <c>InsertCommand</c>, <c>UpsertRangeCommands</c>) for inspection, logging, or manual
    /// execution with Dapper.
    /// </remarks>
    public sealed class DbCommandInfo
    {
        /// <summary>
        /// Gets the generated SQL text for this command.
        /// </summary>
        public string Sql { get; }

        /// <summary>
        /// Gets the parameters bound to <see cref="Sql"/>, or <see langword="null"/> if the command requires none.
        /// </summary>
        public DynamicParameters? Parameters { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DbCommandInfo"/> class.
        /// </summary>
        /// <param name="sql">The SQL text to execute.</param>
        /// <param name="parameters">The parameters bound to <paramref name="sql"/>, if any.</param>
        public DbCommandInfo(string sql, DynamicParameters? parameters = null)
        {
            Sql = sql;
            Parameters = parameters;
        }
    }
}
