using Dapper.Forge.Core.Utilities;
using System.Linq.Expressions;
using System.Reflection;


namespace Dapper.Forge.Core.Models
{
    public sealed class SortDescriptor<TEntity> where TEntity : class
    {
        public string PropertyName { get; }
        public SortDirection SortDirection { get; set; }

        public SortDescriptor(Expression<Func<TEntity, object?>> selector, SortDirection sortDirection = SortDirection.Ascending)
        {
            PropertyInfo property = PropertyHelper.GetProperty(selector);

            PropertyName = property.Name;
            SortDirection = sortDirection;
        }

        public SortDescriptor(string propertyName, SortDirection sortDirection = SortDirection.Ascending)
        {
            PropertyInfo property = PropertyHelper.GetProperty<TEntity>(propertyName);

            PropertyName = property.Name;
            SortDirection = sortDirection;
        }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
