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
sorts.Add(new("Id", "asc"));

List<FilterDescriptor> filters = [];
filters.Add(new("IntValue", 3, ComparisonOperator.Equal));

FilterGroup group = new(filters, LogicalOperator.AndAlso);


var rnd = new Random();
var aa = new List<TestTableORACLE>();
var bb = new List<TestTableIdentityORACLE>();

#region DACHIUDERE
for (int i = 0; i < 10000; i++)
    aa.Add(new()
    {
        Id = Guid.NewGuid().ToString(),
        IntValue = rnd.Next(0, 100000) switch
        {
            < 20000 => null,
            _ => rnd.Next()
        },

        DecimalValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => (decimal)(rnd.NextDouble() * 10000)
        },

        StringValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => "ciao"
        },

        BoolValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => rnd.Next(0, 2) == 1 ? 1 : 0
        },

        TimestampValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => DateTime.UtcNow.AddSeconds(-rnd.Next(0, 1000000))
        },

        GuidValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => Guid.NewGuid().ToString()
        }
    });

for (int i = 0; i < 10000; i++)
    bb.Add(new()
    {
        IntValue = rnd.Next(0, 100000) switch
        {
            < 20000 => null,
            _ => rnd.Next()
        },

        DecimalValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => (decimal)(rnd.NextDouble() * 10000)
        },

        StringValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => "ciao"
        },

        BoolValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => rnd.Next(0, 2) == 1 ? 1 : 0
        },

        TimestampValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => DateTime.UtcNow.AddSeconds(-rnd.Next(0, 1000000))
        },

        GuidValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => Guid.NewGuid().ToString()
        }
    });
#endregion

var aaa = mysqlConnection.GetAll<TestTableIdentityMYSQL>(sorts);
var aaaa = mysqlConnection.GetAll<TestTableMYSQL>(sorts);
var bbb = oracleConnection.GetAll<TestTableIdentityORACLE>(sorts);
var bbbb = oracleConnection.GetAll<TestTableORACLE>(sorts);
var ccc = postgresqlConnection.GetAll<TestTableIdentityPOSTGRESQL>(sorts);
var cccc = postgresqlConnection.GetAll<TestTablePOSTGRESQL>(sorts);
var ddd = sqlserverConnection.GetAll<TestTableIdentitySQLSERVER>(sorts);
var dddd = sqlserverConnection.GetAll<TestTableSQLSERVER>(sorts);

foreach (var x in aaa)
    x.TimestampValue = null;

foreach (var x in aaaa)
    x.TimestampValue = null;

foreach (var x in bbb)
    x.TimestampValue = null;

foreach (var x in bbbb)
    x.TimestampValue = null;

foreach (var x in ccc)
    x.TimestampValue = null;

foreach (var x in cccc)
    x.TimestampValue = null;

foreach (var x in ddd)
    x.TimestampValue = null;

foreach (var x in dddd)
    x.TimestampValue = null;

await sqlserverConnection.LoadDbCacheAsync<TestTableIdentitySQLSERVER>();
await mysqlConnection.LoadDbCacheAsync<TestTableIdentityMYSQL>();


var _bbb = oracleConnection.UpsertRange<TestTableIdentityORACLE>(bbb);
var _bbbb = oracleConnection.UpsertRange<TestTableORACLE>(bbbb);


Console.WriteLine(string.Empty);
