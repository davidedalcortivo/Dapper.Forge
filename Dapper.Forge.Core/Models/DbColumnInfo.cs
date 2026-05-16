namespace Dapper.Forge.Core.Models
{
    internal sealed class DbColumnInfo
    {
        public required string Name { get; init; }
        public int OrdinalPosition { get; init; }
    }
}
