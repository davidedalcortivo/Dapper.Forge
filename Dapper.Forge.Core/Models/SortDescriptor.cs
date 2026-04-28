using Dapper.Forge.Core.Utilities;
using System.Linq.Expressions;


namespace Dapper.Forge.Core.Models
{
    public sealed class SortDescriptor
    {
        public string PropertyName { get; }
        public SortDirection SortDirection { get; }

        public SortDescriptor(string propertyName)
        {
            PropertyName = propertyName;
            SortDirection = SortDirection.Ascending;
        }

        public SortDescriptor(string propertyName, SortDirection sortDirection)
        {
            PropertyName = propertyName;
            SortDirection = sortDirection;
        }

        public SortDescriptor(string propertyName, string sortDirection)
        {
            PropertyName = propertyName;
            SortDirection = sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase) ||
                sortDirection.Equals("descending", StringComparison.OrdinalIgnoreCase) ? SortDirection.Descending : SortDirection.Ascending;
        }

        public static SortDescriptor For<TEntity>(Expression<Func<TEntity, object?>> selector, SortDirection sortDirection) where TEntity : class
        {
            return new(PropertyHelper.GetPropertyName(selector), sortDirection);
        }

        public static SortDescriptor For<TEntity>(Expression<Func<TEntity, object?>> selector, string sortDirection) where TEntity : class
        {
            return new(PropertyHelper.GetPropertyName(selector), sortDirection);
        }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
