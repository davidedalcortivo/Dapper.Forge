using Dapper;
using DapperForge.Core.Abstractions.Strategies;
using System.Text;
using System.Text.RegularExpressions;


namespace DapperForge.Core.Models
{
    internal sealed partial class SqlTranslationContext
    {
        private const string _parameterPrefix = "p";
        private readonly Stack<StringBuilder> _stack;

        public ISqlDialectStrategy SqlDialectStrategy { get; }
        public int ParameterIndex { get; set; }
        public StringBuilder SqlBuffer { get; private set; }
        public DynamicParameters? Parameters { get; private set; }

        public SqlTranslationContext(ISqlDialectStrategy sqlDialectStrategy, DynamicParameters? parameters = null)
        {
            _stack = new();
            SqlDialectStrategy = sqlDialectStrategy;
            SqlBuffer = new();
            Parameters = parameters;
        }

        public void Push()
        {
            _stack.Push(SqlBuffer);
            SqlBuffer = new();
        }

        public string Pop()
        {
            string sql = SqlBuffer.ToString();
            SqlBuffer = _stack.Pop();
            return sql;
        }

        public string AddParameter(object? value)
        {
            Parameters ??= new();

            string name = _parameterPrefix + ParameterIndex;
            Parameters.Add(name, value);
            ParameterIndex++;

            return SqlDialectStrategy.RenderParameter(name);
        }

        [GeneratedRegex($"^{_parameterPrefix}\\d+$", RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        public static partial Regex ParameterRegex();
    }
}
