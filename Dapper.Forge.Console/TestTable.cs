using System.ComponentModel.DataAnnotations.Schema;


namespace Dapper.Forge.Console
{
    [Table("test_table", Schema = "public")]
    public class TestTable
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("int_value")]
        public int? IntValue { get; set; }

        [Column("decimal_value")]
        public decimal? DecimalValue { get; set; }

        [Column("string_value")]
        public string? StringValue { get; set; }

        [Column("bool_value")]
        public bool? BoolValue { get; set; }

        [Column("date_value")]
        public DateOnly? DateValue { get; set; }

        [Column("time_value")]
        public TimeOnly? TimeValue { get; set; }

        [Column("timestamp_value")]
        public DateTime? TimestampValue { get; set; }

        [Column("timestamptz_value")]
        public DateTime? TimestamptzValue { get; set; }

        [Column("guid_value")]
        public Guid? GuidValue { get; set; }
    }
}
