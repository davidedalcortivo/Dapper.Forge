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
