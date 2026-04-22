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
            StringBuilder sqlBuilder = new();

            sqlBuilder.AppendLine();
            sqlBuilder.AppendLine("LIMIT");
            sqlBuilder.Append("    ");
            sqlBuilder.AppendLine(takeParameter);
            sqlBuilder.AppendLine("OFFSET");
            sqlBuilder.Append("    ");
            sqlBuilder.Append(skipParameter);

            return sqlBuilder.ToString();
        }
    }
}
