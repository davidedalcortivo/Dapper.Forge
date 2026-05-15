namespace Dapper.Forge.Core.Models
{
    public sealed class DbColumnInfo
    {
        public required string Name { get; init; }
        public int OrdinalPosition { get; init; }
    }
}
