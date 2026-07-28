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


List<SortDescriptor<TestTableIdentityMYSQL>> sorts = [];
sorts.Add(new("Id", SortDirection.Ascending));


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
            < 20 => 0,
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

int?[] ids = [];
string? search = "ciao";

string?[] names =
[
    "Davide",
    "Marco",
    null
];

var filter =
new FilterGroup<TestTableIdentityMYSQL>
(
    [
        new FilterDescriptor<TestTableIdentityMYSQL>(
            x => x.BoolValue,
            true),

        new FilterDescriptor<TestTableIdentityMYSQL>(
            x => x.StringValue,
            "dav",
            ComparisonOperator.Contains,
            ignoreCase: true),

        new FilterGroup<TestTableIdentityMYSQL>
        (
            [
                new FilterDescriptor<TestTableIdentityMYSQL>(
                    x => x.IntValue,
                    18,
                    ComparisonOperator.GreaterThanOrEqual, not: true),

                new FilterDescriptor<TestTableIdentityMYSQL>(
                    x => x.IntValue,
                    65,
                    ComparisonOperator.LessThanOrEqual, not: true)
            ], not: true
        ),

        new FilterDescriptor<TestTableIdentityMYSQL>(
            x => x.Id,
            new[] { 1, 2, 3, 4, 5 },
            ComparisonOperator.In)
    ], not: true
);


var aaa = mysqlConnection.GetAllCommand<TestTableIdentityMYSQL>(filter);

var bbb = oracleConnection.GetAllCommand<TestTableIdentityORACLE>(x => x.StringValue == null);

var ccc = postgresqlConnection.GetAllCommand<TestTableIdentityPOSTGRESQL>(x => x.StringValue != null);

var ddd = sqlserverConnection.GetAllCommand<TestTableIdentitySQLSERVER>(
x =>
(
    x.BoolValue &&
    x.IntValue >= 18
)
&&
(
    names.Contains(x.StringValue)
    ||
    !!!x.StringValue!.StartsWith("Adm")
));

Console.WriteLine(aaa.Sql);
Console.WriteLine(bbb.Sql);
Console.WriteLine(ccc.Sql);
Console.WriteLine(ddd.Sql);
