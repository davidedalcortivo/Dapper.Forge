namespace Forget.Core.Models
{
    internal sealed class DbColumnInfo
    {
        public required string Name { get; init; }
        public int? OrdinalPosition { get; init; }
        public string? DataType { get; init; }
        public string? CastExpression { get; init; }
        public bool? IsCastable { get; init; }
    }
}
