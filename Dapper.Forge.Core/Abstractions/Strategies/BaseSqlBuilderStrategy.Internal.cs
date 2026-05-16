using Dapper.Forge.Core.Models;
using Dapper.Forge.Core.Utilities;
using System.Text;


namespace Dapper.Forge.Core.Abstractions.Strategies
{
    internal abstract partial class BaseSqlBuilderStrategy<TStrategy> : ISqlBuilderStrategy where TStrategy : ISqlDialectStrategy
    {
        protected virtual SqlTemplate BuildAggregateSql<TEntity>(string aggregateName) where TEntity : class
        {
            StringBuilder sqlBuffer = new();

            sqlBuffer.AppendLine("SELECT");
            sqlBuffer.Append("    ");
            sqlBuffer.Append(aggregateName);
            sqlBuffer.Append("({})");
            sqlBuffer.AppendFromTable<TEntity>(SqlDialectStrategy, string.Empty, null);
            sqlBuffer.Append("{}");
            sqlBuffer.Append(SqlDialectStrategy.Terminator);

            return new(sqlBuffer.ToString(), SqlDialectStrategy.Terminator);
        }
    }
}
