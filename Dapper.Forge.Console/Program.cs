using Dapper;
using Dapper.Forge.Console;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.PostgreSql.Extensions;
using Npgsql;
using System.Collections.Immutable;
using System.Reflection;


NpgsqlConnection connection = new("");

List<SortDescriptor> sorts = [];
sorts.Add(new("Id", "desc"));
sorts.Add(new("StringValue", SortDirection.Descending));
sorts.Add(new("GuidValue", "descending"));
sorts.Add(SortDescriptor.For<TestTable>(x => x.GuidValue, "descending"));

List<FilterDescriptor> filters = [];
filters.Add(new("StringValue", "ciao", ComparisonOperator.Equal));

FilterGroup group = new(filters, LogicalOperator.AndAlso);

TestTable testTable = new()
{
    Id = new("c0ce8453-f109-452a-850d-717a33947e10"),
    IntValue = 1,
    DecimalValue = (decimal?)3.4,
    StringValue = "ciao",
    BoolValue = true,
    DateValue = null,
    TimeValue = null,
    TimestampValue = DateTime.Now,
    TimestamptzValue = DateTime.Now,
    GuidValue = Guid.NewGuid()
};

TestTableIdentity testTableIdentity = new()
{
    IntValue = 1,
    DecimalValue = (decimal?)3.4,
    StringValue = "hola",
    DateValue = null,
    TimeValue = null,
    TimestampValue = DateTime.Now,
    GuidValue = Guid.NewGuid()
};

var aa = await connection.GetAllAsync<TestTable>();

foreach (var a in aa)
    a.TimestamptzValue = DateTime.Now;

var lista = connection.Update<TestTable>(new { BoolValue  = (bool?)null });
var lista2 = await connection.UpdateRangeAsync<TestTable>(aa);


var result = connection.MaxCommand<TestTable>("IntValue");
var result2 = connection.MaxCommand<TestTable>(x => x.IntValue);

Console.WriteLine("");
