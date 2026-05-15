using Dapper.Forge.Core.Abstractions.Strategies;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;


namespace Dapper.Forge.Oracle.Strategies
{
    internal partial class SqlDialectStrategy : BaseSqlDialectStrategy
    {
        public static SqlDialectStrategy Instance { get; } = new();

        public int MaxInValueCount { get; }
        
        private SqlDialectStrategy()
        {
            MaxInValueCount = 1000;
        }

        public override string Terminator { get; } = Environment.NewLine;

        public override string RenderParameter(string name)
        {
            return ":" + name;
        }

        public override string GetConnectionId(DbConnection connection)
        {
            OracleConnectionStringBuilder builder = new(connection.ConnectionString);
            return "oracle://" + builder.DataSource + "/" + builder.UserID;
        }
    }
}
