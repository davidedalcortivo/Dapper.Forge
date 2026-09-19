using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;


namespace Forget.Tests.Oracle
{
    /// <summary>
    /// A type Dapper has no built-in mapping for on Oracle (ODP.NET cannot bind a <see cref="Guid"/> directly),
    /// wrapped so the type handler registered for it stays scoped to the tests that use it instead of changing how
    /// <see cref="Guid"/> behaves for the whole test process.
    /// </summary>
    internal readonly record struct OracleToken(Guid Value);

    /// <summary>
    /// The remedy Dapper itself gives for such a type: a <see cref="SqlMapper.TypeHandler{T}"/>. Forget does not
    /// map types itself, it only has to stay compatible with whatever Dapper is configured to do.
    /// </summary>
    internal sealed class OracleTokenHandler : SqlMapper.TypeHandler<OracleToken>
    {
        public override void SetValue(IDbDataParameter parameter, OracleToken value)
        {
            // Dapper marks a handled type's parameter as DbType.Object before calling SetValue; ODP.NET rejects a
            // byte[] on that (ORA-50028), so the handler has to state the real database type itself.
            parameter.DbType = DbType.Binary;
            parameter.Value = value.Value.ToByteArray();
        }

        public override OracleToken Parse(object value)
        {
            return new OracleToken(new Guid((byte[])value));
        }
    }

    [Table("HandlerRow", Schema = "dbo")]
    internal sealed class HandlerRow
    {
        public int Id { get; set; }
        public OracleToken Token { get; set; }
        public string? Note { get; set; }
    }
}
