using DapperForge.Core.Utilities;
using System.Linq.Expressions;
using System.Reflection;


namespace DapperForge.Core.Models
{
    /// <summary>
    /// Specifies how results should be ordered by a single property.
    /// </summary>
    /// <typeparam name="TEntity">The entity type being sorted.</typeparam>
    /// <remarks>
    /// Pass one or more instances, in the desired order of precedence, to any query method that accepts a sequence
    /// of <see cref="SortDescriptor{TEntity}"/> to build a multi-column <c>ORDER BY</c> clause.
    /// </remarks>
    public sealed class SortDescriptor<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets the name of the entity property to sort by.
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        public SortDirection SortDirection { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortDescriptor{TEntity}"/> class using a strongly-typed
        /// property selector.
        /// </summary>
        /// <param name="selector">An expression selecting the property to sort by, for example <c>x =&gt; x.Name</c>.</param>
        /// <param name="sortDirection">The sort direction. Defaults to <see cref="SortDirection.Ascending"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="selector"/> does not select a simple property.</exception>
        public SortDescriptor(Expression<Func<TEntity, object?>> selector, SortDirection sortDirection = SortDirection.Ascending)
        {
            PropertyInfo property = PropertyHelper.GetProperty(selector);

            PropertyName = property.Name;
            SortDirection = sortDirection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SortDescriptor{TEntity}"/> class using a property name.
        /// </summary>
        /// <param name="propertyName">The name of the property to sort by.</param>
        /// <param name="sortDirection">The sort direction. Defaults to <see cref="SortDirection.Ascending"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="propertyName"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="propertyName"/> does not match a property on <typeparamref name="TEntity"/>.
        /// </exception>
        public SortDescriptor(string propertyName, SortDirection sortDirection = SortDirection.Ascending)
        {
            PropertyInfo property = PropertyHelper.GetProperty<TEntity>(propertyName);

            PropertyName = property.Name;
            SortDirection = sortDirection;
        }
    }

    /// <summary>
    /// Specifies the direction of a sort applied by a <see cref="SortDescriptor{TEntity}"/>.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>Sorts from lowest to highest.</summary>
        Ascending,

        /// <summary>Sorts from highest to lowest.</summary>
        Descending
    }
}
