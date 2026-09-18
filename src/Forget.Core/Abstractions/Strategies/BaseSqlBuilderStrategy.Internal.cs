using Forget.Core.Models;
using Forget.Core.Utilities;
using System.Text;


namespace Forget.Core.Abstractions.Strategies
{
    internal abstract partial class BaseSqlBuilderStrategy<TStrategy> : ISqlBuilderStrategy where TStrategy : ISqlDialectStrategy
    {
        protected virtual SqlTemplate BuildAggregateSql<TEntity>(string aggregate) where TEntity : class
        {
            StringBuilder sqlBuffer = new();

            sqlBuffer.AppendLine("SELECT");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(aggregate);
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, string.Empty, null);
            sqlBuffer.Append(SqlDialectStrategy.Placeholder);
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator, SqlDialectStrategy.Placeholder);
        }
    }
}
