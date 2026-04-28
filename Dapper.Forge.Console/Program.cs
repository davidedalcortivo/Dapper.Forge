using Dapper.Forge.Console;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Dapper.Forge.PostgreSql.Extensions;
using Npgsql;
using System.Collections.Immutable;
using System.Reflection;
using System.Xml.Linq;


NpgsqlConnection connection = new("");

List<SortDescriptor> sorts = [];
sorts.Add(new("Id", "desc"));
sorts.Add(new("StringValue", SortDirection.Descending));
sorts.Add(new("GuidValue", "descending"));
sorts.Add(SortDescriptor.For<TestTable>(x => x.GuidValue, "descending"));


PropertyInfo[] propertyInfos = ParamPropertyCache.GetProperties(new { Id = 3, stringa = "ciao" });
propertyInfos = ParamPropertyCache.GetProperties(new { Id = 3, stringa = "ciao" });
propertyInfos = ParamPropertyCache.GetProperties(new {});
propertyInfos = ParamPropertyCache.GetProperties(new { Id = 3, stringa = "hola" });
propertyInfos = ParamPropertyCache.GetProperties(new { Id = 4 });

ImmutableDictionary<string, Func<object, object?>> paramGetterCache = ParamGetterCache.GetGetters(new { Id = 3, stringa = "ciao" });
paramGetterCache = ParamGetterCache.GetGetters(new { Id = 3, stringa = "ciao" });
paramGetterCache = ParamGetterCache.GetGetters(new { });
paramGetterCache = ParamGetterCache.GetGetters(new { Id = 3, stringa = "hola" });


List<FilterDescriptor> filters = [];
filters.Add(new("StringValue", "ciao", ComparisonOperator.Equal));

FilterGroup group = new(filters, LogicalOperator.AndAlso);

TestTable testTable = new()
{
    Id = new("b0ce8453-f109-452a-850d-717a33947e10"),
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

List<TestTable> lista = [];
lista.Add(testTable);
lista.Add(testTable);


var a = connection.GetByIdRange<TestTable>(lista.Select(x => x.Id));
var b = connection.GetByIdRange<TestTable>(lista.Select(x => x.Id));
var result = connection.Upsert(testTableIdentity);

Console.WriteLine("");
