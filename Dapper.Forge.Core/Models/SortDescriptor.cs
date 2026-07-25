using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
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
            string propertyName = PropertyHelper.GetPropertyName(selector);
            PropertyInfo property = ValidateProperty(propertyName);

            PropertyName = property.Name;
            SortDirection = sortDirection;
        }

        public SortDescriptor(string propertyName, SortDirection sortDirection = SortDirection.Ascending)
        {
            PropertyInfo property = ValidateProperty(propertyName);

            PropertyName = property.Name;
            SortDirection = sortDirection;
        }

        private static PropertyInfo ValidateProperty(string propertyName)
        {
            ImmutableDictionary<string, PropertyInfo> propertiesByPropertyName = EntityInfoCache<TEntity>.PropertiesByPropertyName;

            if (!propertiesByPropertyName.TryGetValue(propertyName, out PropertyInfo? property))
                throw new ArgumentException($"The property '{propertyName}' does not exist on entity '{typeof(TEntity).Name}'.");

            return property;
        }
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
