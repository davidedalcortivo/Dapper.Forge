using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Utilities;
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
            PropertyInfo property = PropertyHelper.GetProperty(selector);
            PropertyHelper.EnsureValue<TEntity>(property, value);

            PropertyName = property.Name;
            Value = value;
            ComparisonOperator = comparisonOperator;
            Not = not;
            IgnoreCase = ignoreCase;
        }

        public FilterDescriptor(string propertyName, object? value, ComparisonOperator comparisonOperator = ComparisonOperator.Equal, bool not = false, bool ignoreCase = false)
        {
            PropertyInfo property = PropertyHelper.GetProperty<TEntity>(propertyName);
            PropertyHelper.EnsureValue<TEntity>(property, value);

            PropertyName = property.Name;
            Value = value;
            ComparisonOperator = comparisonOperator;
            Not = not;
            IgnoreCase = ignoreCase;
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
