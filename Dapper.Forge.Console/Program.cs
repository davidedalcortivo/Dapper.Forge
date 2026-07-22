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
using System.Xml.Linq;


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


var aaa = mysqlConnection.GetAllCommand<TestTableIdentityMYSQL>(x => !x.BoolValue && x.OtherBoolValue);
var bbb = oracleConnection.GetAllCommand<TestTableIdentityORACLE>(x => x.BoolValue == 1);
var ccc = postgresqlConnection.GetAllCommand<TestTableIdentityPOSTGRESQL>(x => !x.BoolValue && !x.BoolValue);
var ddd = sqlserverConnection.GetAllCommand<TestTableIdentitySQLSERVER>(x => !(x.BoolValue && x.Id > 10));

Console.WriteLine(aaa.Sql);
Console.WriteLine(bbb.Sql);
Console.WriteLine(ccc.Sql);
Console.WriteLine(ddd.Sql);
