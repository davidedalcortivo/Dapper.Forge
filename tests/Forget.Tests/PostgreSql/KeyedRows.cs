using Forget.Tests.Core;
using System.ComponentModel.DataAnnotations.Schema;


namespace Forget.Tests.PostgreSql
{
    /// <summary>
    /// The shape of a reference table keyed by an enum. <c>GetByIdRange</c>/<c>DeleteRange</c> bind their ids as one
    /// array (<c>= ANY(@ids)</c> on PostgreSql), so the key's CLR type decides what array Npgsql is asked to write.
    /// </summary>
    [Table("EnumKeyed", Schema = "dbo")]
    internal sealed class EnumKeyed
    {
        public TypeMatrixKind Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    /// <summary>The same id-range paths with a <see cref="Guid"/> key, the most common non-integer key.</summary>
    [Table("GuidKeyed", Schema = "dbo")]
    internal sealed class GuidKeyed
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
