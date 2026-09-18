using System.ComponentModel.DataAnnotations.Schema;


namespace Forget.Tests.Oracle
{
    /// <summary>
    /// Oracle's own copy of the shared test entity shape (see <see cref="Core.Widget"/> for why every
    /// provider gets its own copy rather than sharing one class). <c>IsActive</c> is <c>int</c> (0/1) here, not
    /// <c>bool</c> like every other provider's copy: binding a C# <c>bool</c> against a <c>NUMBER(1)</c> column
    /// throws <c>ORA-00932</c> on Oracle versions older than 23c (see <see cref="OracleFixture"/>), and modeling
    /// boolean flags as a plain <c>NUMBER</c> is also the conventional, version-safe way to do it on Oracle in the
    /// first place.
    /// </summary>
    [Table("Widget", Schema = "dbo")]
    internal sealed class Widget
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Nickname { get; set; }
        public int? Quantity { get; set; }
        public int IsActive { get; set; }
        public decimal Price { get; set; }
    }
}
