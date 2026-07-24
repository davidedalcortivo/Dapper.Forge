using Dapper.Forge.Core.Abstractions.Strategies;
using System.Text;


namespace Dapper.Forge.Core.Models
{
    internal sealed class SqlTranslationContext
    {
        private readonly Stack<StringBuilder> _stack;

        public ISqlDialectStrategy SqlDialectStrategy { get; }
        public int ParamIndex { get; set; }
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

            string name = $"__p{ParamIndex}";
            Parameters.Add(name, value);
            ParamIndex++;

            return SqlDialectStrategy.RenderParameter(name);
        }
    }
}
