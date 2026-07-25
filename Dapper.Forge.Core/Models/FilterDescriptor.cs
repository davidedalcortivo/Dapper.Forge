using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Utilities;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;


namespace Dapper.Forge.Core.Models
{
    public sealed class FilterDescriptor<TEntity> : IFilterNode<TEntity> where TEntity : class
    {
        public string PropertyName { get; }
        public object? Value { get; }
        public ComparisonOperator ComparisonOperator { get; set; }
        public bool Not { get; set; }
        public bool IgnoreCase { get; set; }

        public FilterDescriptor(Expression<Func<TEntity, object?>> selector, object? value, ComparisonOperator comparisonOperator = ComparisonOperator.Equal, bool not = false, bool ignoreCase = false)
        {
            string propertyName = PropertyHelper.GetPropertyName(selector);
            PropertyInfo property = ValidatePropertyAndValue(propertyName, value);

            PropertyName = property.Name;
            Value = value;
            ComparisonOperator = comparisonOperator;
            Not = not;
            IgnoreCase = ignoreCase;
        }

        public FilterDescriptor(string propertyName, object? value, ComparisonOperator comparisonOperator = ComparisonOperator.Equal, bool not = false, bool ignoreCase = false)
        {
            PropertyInfo property = ValidatePropertyAndValue(propertyName, value);

            PropertyName = property.Name;
            Value = value;
            ComparisonOperator = comparisonOperator;
            Not = not;
            IgnoreCase = ignoreCase;
        }

        private static PropertyInfo ValidatePropertyAndValue(string propertyName, object? value)
        {
            ImmutableDictionary<string, PropertyInfo> propertiesByPropertyName = EntityInfoCache<TEntity>.PropertiesByPropertyName;

            if (!propertiesByPropertyName.TryGetValue(propertyName, out PropertyInfo? property))
                throw new ArgumentException($"The property '{propertyName}' does not exist on entity '{typeof(TEntity).Name}'.");

            Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            Type? valueType = value?.GetType();

            if (valueType is not null && !propertyType.IsAssignableFrom(valueType))
                throw new ArgumentException($"The type of the provided value '{valueType}' does not match the type of the property '{propertyType}' for the entity '{typeof(TEntity).Name}'.");

            return property;
        }
    }

    public enum ComparisonOperator
    {
        Equal,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        Contains,
        StartsWith,
        EndsWith,
        In
    }
}
