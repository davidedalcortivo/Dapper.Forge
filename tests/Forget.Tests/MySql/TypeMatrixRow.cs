using Forget.Core.Models;
using Forget.Tests.Core;
using System.ComponentModel.DataAnnotations.Schema;


namespace Forget.Tests.MySql
{
    /// <summary>
    /// One property per CLR type worth proving against a real engine (numeric widths, floating point, decimal,
    /// bool, unicode string, every date/time flavor, <see cref="Guid"/>, enum, <see cref="byte"/>[]), each with a
    /// nullable counterpart where it matters. MySql's own copy: every provider's copy diverges exactly where
    /// that engine's real type support requires it (see <see cref="Core.Widget"/>).
    /// <see cref="Identifier"/> is the natural upsert key.
    /// </summary>
    [Table("TypeMatrix", Schema = "dbo")]
    internal sealed class TypeMatrixRow
    {
        public int Id { get; set; }
        public long BigValue { get; set; }
        public short SmallValue { get; set; }
        public double DoubleValue { get; set; }
        public float SingleValue { get; set; }
        public decimal DecimalValue { get; set; }
        public bool Flag { get; set; }
        public string Label { get; set; } = string.Empty;
        public DateTime Moment { get; set; }
        public DateOnly CalendarDay { get; set; }
        public TimeOnly TimeOfDay { get; set; }

        [UpsertKey]
        public Guid Identifier { get; set; }

        public TypeMatrixKind Kind { get; set; }
        public byte[] Payload { get; set; } = [];

        public long? NullableBig { get; set; }
        public DateTime? NullableMoment { get; set; }
        public Guid? NullableGuid { get; set; }
        public TypeMatrixKind? NullableKind { get; set; }
        public string? NullableLabel { get; set; }
    }
}
