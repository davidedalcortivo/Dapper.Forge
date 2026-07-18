using Dapper.Forge.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Dapper.Forge.Console
{
    [Table("test_table_identity")]
    public class TestTableIdentityMYSQL
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [UpsertKey]
        [Column("int_value")]
        public int? IntValue { get; set; }

        [UpsertKey]
        [Column("decimal_value")]
        public decimal? DecimalValue { get; set; }

        [Column("string_value")]
        public string? StringValue { get; set; }

        [Column("bool_value")]
        public bool? BoolValue { get; set; }

        [Column("timestamp_value")]
        public DateTime? TimestampValue { get; set; }

        [Column("guid_value")]
        public Guid? GuidValue { get; set; }
    }

    [Table("TEST_TABLE_IDENTITY")]
    public class TestTableIdentityORACLE
    {
        [Column("ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [UpsertKey]
        [Column("INT_VALUE")]
        public int? IntValue { get; set; }

        [UpsertKey]
        [Column("DECIMAL_VALUE")]
        public decimal? DecimalValue { get; set; }

        [Column("STRING_VALUE")]
        public string? StringValue { get; set; }

        [Column("BOOL_VALUE")]
        public int? BoolValue { get; set; }

        [Column("TIMESTAMP_VALUE")]
        public DateTime? TimestampValue { get; set; }

        [Column("GUID_VALUE")]
        public string? GuidValue { get; set; }
    }

    [Table("test_table_identity")]
    public class TestTableIdentityPOSTGRESQL
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [UpsertKey]
        [Column("int_value")]
        public int? IntValue { get; set; }

        [UpsertKey]
        [Column("decimal_value")]
        public decimal? DecimalValue { get; set; }

        [Column("string_value")]
        public string? StringValue { get; set; }

        [Column("bool_value")]
        public bool? BoolValue { get; set; }

        [Column("timestamp_value")]
        public DateTime? TimestampValue { get; set; }

        [Column("guid_value")]
        public Guid? GuidValue { get; set; }
    }

    [Table("TestTableIdentity")]
    public class TestTableIdentitySQLSERVER
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [UpsertKey]
        public int? IntValue { get; set; }

        [UpsertKey]
        public decimal? DecimalValue { get; set; }

        public string? StringValue { get; set; }

        public bool? BoolValue { get; set; }

        public DateTime? TimestampValue { get; set; }

        public Guid? GuidValue { get; set; }
    }
}
