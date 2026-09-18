using System.ComponentModel.DataAnnotations.Schema;


namespace DapperForge.Tests.PostgreSql
{
    /// <summary>
    /// PostgreSql's own copy of the shared test entity shape (see <see cref="Core.Widget"/> for why every
    /// provider gets its own copy rather than sharing one class: each provider's copy can diverge exactly where
    /// that provider's actual type support requires it, without dragging a workaround into the others).
    /// </summary>
    [Table("Widget", Schema = "dbo")]
    internal sealed class Widget
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Nickname { get; set; }
        public int? Quantity { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
    }
}
