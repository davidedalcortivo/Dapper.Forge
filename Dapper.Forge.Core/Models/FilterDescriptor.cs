using Dapper.Forge.Core.Abstractions.Models;
using Dapper.Forge.Core.Utilities;
using System.Linq.Expressions;


namespace Dapper.Forge.Core.Models
{
    public class FilterDescriptor : IFilterNode
    {
        public string PropertyName { get; }
        public object? Value { get; }
        public ComparisonOperator ComparisonOperator { get; }
        public bool Not { get; }
        public bool IgnoreCase { get; }

        public FilterDescriptor(string propertyName, object? value, ComparisonOperator comparisonOperator, bool not = false, bool ignoreCase = false)
        {
            PropertyName = propertyName;
            Value = value;
            ComparisonOperator = comparisonOperator;
            Not = not;
            IgnoreCase = ignoreCase;
        }

        public static FilterDescriptor For<TEntity>(Expression<Func<TEntity, object?>> selector, ComparisonOperator comparisonOperator, object? value, bool not = false, bool ignoreCase = false) where TEntity : class
        {
            return new(PropertyHelper.GetPropertyName(selector), value, comparisonOperator, not, ignoreCase);
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
        EndsWith
    }
}
