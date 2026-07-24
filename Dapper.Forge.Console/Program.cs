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

string[] ids = ["1", null, "2", "3"];
string? search = "ciao";

var aaa = mysqlConnection.GetAllCommand<TestTableIdentityMYSQL>(x => x.StringValue == null);
var bbb = oracleConnection.GetAllCommand<TestTableIdentityORACLE>(x => x.StringValue!
    .ToLower()
    .ToUpper()
    .Contains("MAR"));
var ccc = postgresqlConnection.GetAllCommand<TestTableIdentityPOSTGRESQL>(x =>
    ids.Contains(
        x.GuidValue!
         .ToString()
         .ToUpper()));
var ddd = sqlserverConnection.GetAllCommand<TestTableIdentitySQLSERVER>(x => !x.Id.ToString().Contains("1"));

Console.WriteLine(aaa.Sql);
Console.WriteLine(bbb.Sql);
Console.WriteLine(ccc.Sql);
Console.WriteLine(ddd.Sql);
