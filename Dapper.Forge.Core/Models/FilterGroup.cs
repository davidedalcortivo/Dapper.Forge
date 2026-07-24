using Dapper.Forge.Core.Abstractions.Models;


namespace Dapper.Forge.Core.Models
{
    public sealed class FilterGroup<TEntity> : IFilterNode<TEntity> where TEntity : class
    {
        public List<IFilterNode<TEntity>> FilterNodes { get; }
        public LogicalOperator LogicalOperator { get; }
        public bool Not { get; }

        public FilterGroup(IEnumerable<IFilterNode<TEntity>> filterNodes, LogicalOperator logicalOperator, bool not = false)
        {
            FilterNodes = [];
            FilterNodes.AddRange(filterNodes);
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
