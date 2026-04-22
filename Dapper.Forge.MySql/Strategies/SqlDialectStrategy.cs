using Dapper.Forge.Core.Abstractions.Strategies;
using System.Text;


namespace Dapper.Forge.MySql.Strategies
{
    internal class SqlDialectStrategy : BaseSqlDialectStrategy
    {
        public static SqlDialectStrategy Instance { get; } = new();

        public override string RenderIdentifier(string name)
        {
            return "`" + name + "`";
        }

        public override string Concat(params string[] parts)
        {
            return "CONCAT(" + string.Join(", ", parts) + ")";
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
