using Dapper.Forge.Core.Abstractions.Models;


namespace Dapper.Forge.Core.Models
{
    /// <summary>
    /// Combines one or more <see cref="IFilterNode{TEntity}"/> instances with a logical operator, as a branch node
    /// in a filter tree.
    /// </summary>
    /// <typeparam name="TEntity">The entity type the filter applies to.</typeparam>
    /// <remarks>
    /// Nest <see cref="FilterGroup{TEntity}"/> instances inside one another, mixed with <see cref="FilterDescriptor{TEntity}"/>
    /// leaves, to build arbitrarily complex <c>AND</c>/<c>OR</c> conditions. A group with no nodes translates to an
    /// always-true condition.
    /// </remarks>
    public sealed class FilterGroup<TEntity> : IFilterNode<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets the child nodes combined by this group.
        /// </summary>
        /// <remarks>
        /// The returned list is mutable, so nodes can be added to or removed from the group after construction.
        /// </remarks>
        public List<IFilterNode<TEntity>> FilterNodes { get; }

        /// <summary>
        /// Gets or sets the operator used to combine <see cref="FilterNodes"/>.
        /// </summary>
        public LogicalOperator LogicalOperator { get; set; }

        /// <summary>
        /// Gets or sets whether the resulting condition is negated.
        /// </summary>
        public bool Not { get; set; }

        /// <summary>
        /// Initializes a new, empty instance of the <see cref="FilterGroup{TEntity}"/> class.
        /// </summary>
        /// <param name="logicalOperator">
        /// The operator used to combine the group's nodes. Defaults to <see cref="LogicalOperator.AndAlso"/>.
        /// </param>
        /// <param name="not">Whether to negate the resulting condition.</param>
        public FilterGroup(LogicalOperator logicalOperator = LogicalOperator.AndAlso, bool not = false)
        {
            FilterNodes = [];
            LogicalOperator = logicalOperator;
            Not = not;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterGroup{TEntity}"/> class with the specified child nodes.
        /// </summary>
        /// <param name="filterNodes">The child nodes to combine.</param>
        /// <param name="logicalOperator">
        /// The operator used to combine <paramref name="filterNodes"/>. Defaults to <see cref="LogicalOperator.AndAlso"/>.
        /// </param>
        /// <param name="not">Whether to negate the resulting condition.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filterNodes"/> is <see langword="null"/>.</exception>
        public FilterGroup(IEnumerable<IFilterNode<TEntity>> filterNodes, LogicalOperator logicalOperator = LogicalOperator.AndAlso, bool not = false)
        {
            ArgumentNullException.ThrowIfNull(filterNodes);

            FilterNodes = [.. filterNodes];
            LogicalOperator = logicalOperator;
            Not = not;
        }
    }

    /// <summary>
    /// Specifies how a <see cref="FilterGroup{TEntity}"/> combines its child nodes.
    /// </summary>
    public enum LogicalOperator
    {
        /// <summary>Combines nodes with a logical <c>AND</c>.</summary>
        AndAlso,

        /// <summary>Combines nodes with a logical <c>OR</c>.</summary>
        OrElse
    }
}
