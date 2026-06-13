using Dapper.Forge.Console;
using Dapper.Forge.Core.Models;
using Dapper.Forge.MySql.Extensions;
using Dapper.Forge.Oracle.Extensions;
using Dapper.Forge.PostgreSql.Extensions;
using Dapper.Forge.SqlServer.Extensions;
using Microsoft.Data.SqlClient;
using MySqlConnector;
using Npgsql;
using Oracle.ManagedDataAccess.Client;

List<SortDescriptor> sorts = [];
sorts.Add(new("Id", "desc"));
sorts.Add(new("StringValue", SortDirection.Ascending));
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


MySqlConnection mysqlConnection = new();
OracleConnection oracleConnection = new();
SqlConnection sqlserverConnection = new();

var aa = new List<TestTable>();

for (int i = 0; i < 10000; i++)
    aa.Add(new TestTable() { Id = Guid.NewGuid() });

var bb = new List<TestTableIdentity>();

for (int i = 0; i < 10000; i++)
    bb.Add(new TestTableIdentity() { Id = 1 });

//postgresqlConnection.LoadDbCache<TestTable>();


var mysqlCommands = mysqlConnection.GetPageCommand<TestTable>(null, 10, 4);

var mysqlCommand = mysqlConnection.MaxCommand<TestTable>(x => x.IntValue);
var oracleCommand = oracleConnection.MaxCommand<TestTable>(x => x.IntValue);
var postgresqlCommand = postgresqlConnection.MaxCommand<TestTable>(x => x.IntValue);
var sqlserverCommand = sqlserverConnection.MaxCommand<TestTable>(x => x.IntValue);

mysqlCommand = mysqlConnection.MaxCommand<TestTable>(x => x.IntValue, x => x.StringValue == "ciao");
oracleCommand = oracleConnection.MaxCommand<TestTable>(x => x.IntValue, x => x.StringValue == "ciao");
postgresqlCommand = postgresqlConnection.MaxCommand<TestTable>(x => x.IntValue, x => x.StringValue == "ciao");
sqlserverCommand = sqlserverConnection.MaxCommand<TestTable>(x => x.IntValue, x => x.StringValue == "ciao");

mysqlCommand = mysqlConnection.MaxCommand<TestTable>(x => x.IntValue, group);
oracleCommand = oracleConnection.MaxCommand<TestTable>(x => x.IntValue, group);
postgresqlCommand = postgresqlConnection.MaxCommand<TestTable>(x => x.IntValue, group);
sqlserverCommand = sqlserverConnection.MaxCommand<TestTable>(x => x.IntValue, group);

mysqlCommand = mysqlConnection.MaxCommand<TestTable>("IntValue");
oracleCommand = oracleConnection.MaxCommand<TestTable>("IntValue");
postgresqlCommand = postgresqlConnection.MaxCommand<TestTable>("IntValue");
sqlserverCommand = sqlserverConnection.MaxCommand<TestTable>("IntValue");

mysqlCommand = mysqlConnection.MaxCommand<TestTable>("IntValue", x => x.StringValue == "ciao");
oracleCommand = oracleConnection.MaxCommand<TestTable>("IntValue", x => x.StringValue == "ciao");
postgresqlCommand = postgresqlConnection.MaxCommand<TestTable>("IntValue", x => x.StringValue == "ciao");
sqlserverCommand = sqlserverConnection.MaxCommand<TestTable>("IntValue", x => x.StringValue == "ciao");

mysqlCommand = mysqlConnection.MaxCommand<TestTable>("IntValue", group);
oracleCommand = oracleConnection.MaxCommand<TestTable>("IntValue", group);
postgresqlCommand = postgresqlConnection.MaxCommand<TestTable>("IntValue", group);
sqlserverCommand = sqlserverConnection.MaxCommand<TestTable>("IntValue", group);

Console.WriteLine(string.Empty);
