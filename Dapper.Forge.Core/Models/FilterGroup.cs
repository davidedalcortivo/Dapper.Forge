using Dapper.Forge.Core.Abstractions.Models;


namespace Dapper.Forge.Core.Models
{
    public sealed class FilterGroup : IFilterNode
    {
        public List<IFilterNode> FilterNodes { get; }
        public LogicalOperator LogicalOperator { get; }
        public bool Not { get; }

        public FilterGroup(IEnumerable<IFilterNode> filterNodes, LogicalOperator logicalOperator, bool not = false)
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
