using Dapper.Forge.Core.Abstractions.Strategies;
using System.Text;


namespace Dapper.Forge.PostgreSql.Strategies
{
    internal class SqlDialectStrategy : BaseSqlDialectStrategy
    {
        public static SqlDialectStrategy Instance { get; } = new();

        public override string In(string identifier, string parameter)
        {
            return identifier + " = ANY(" + parameter + ")";
        }

        public override (string, string) In(string identifier)
        {
            return (identifier + " = ANY(", ")");
        }

        public override string Pagination(string skipParameter, string takeParameter)
        {
            StringBuilder sqlBuffer = new();

            sqlBuffer.AppendLine();
            sqlBuffer.AppendLine("LIMIT");
            sqlBuffer.Append("    ");
            sqlBuffer.AppendLine(takeParameter);
            sqlBuffer.AppendLine("OFFSET");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(skipParameter);

            return sqlBuffer.ToString();
        }
    }
}
