namespace Dapper.Forge.Core.Models
{
    public class SortDescriptor
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
    }

    public enum SortDirection
    {
        Ascending,
        Descending
    }
}
