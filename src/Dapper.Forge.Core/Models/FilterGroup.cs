using Dapper.Forge.Core.Abstractions.Models;


namespace Dapper.Forge.Core.Models
{
    public sealed class FilterGroup<TEntity> : IFilterNode<TEntity> where TEntity : class
    {
        public List<IFilterNode<TEntity>> FilterNodes { get; }
        public LogicalOperator LogicalOperator { get; set; }
        public bool Not { get; set; }

        public FilterGroup(LogicalOperator logicalOperator = LogicalOperator.AndAlso, bool not = false)
        {
            FilterNodes = [];
            LogicalOperator = logicalOperator;
            Not = not;
        }

        public FilterGroup(IEnumerable<IFilterNode<TEntity>> filterNodes, LogicalOperator logicalOperator = LogicalOperator.AndAlso, bool not = false)
        {
            ArgumentNullException.ThrowIfNull(filterNodes);

            FilterNodes = [.. filterNodes];
            LogicalOperator = logicalOperator;
            Not = not;
        }
    }

    public enum LogicalOperator
    {
        AndAlso,
        OrElse
    }
}
