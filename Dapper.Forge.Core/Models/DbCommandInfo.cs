namespace Dapper.Forge.Core.Models
{
    public sealed class DbCommandInfo
    {
        public string Sql { get; }
        public DynamicParameters? Parameters { get; }

        public DbCommandInfo(string sql, DynamicParameters? parameters = null)
        {
            Sql = sql;
            Parameters = parameters;
        }
    }
}
