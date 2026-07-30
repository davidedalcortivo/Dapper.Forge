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

List<SortDescriptor<TestTableMYSQL>> sortsa = [];
sortsa.Add(new("Id", SortDirection.Ascending));

List<SortDescriptor<TestTableIdentityMYSQL>> sortsb = [];
sortsb.Add(new("Id", SortDirection.Ascending));

List<SortDescriptor<TestTableORACLE>> sortsc = [];
sortsc.Add(new("Id", SortDirection.Ascending));

List<SortDescriptor<TestTableIdentityORACLE>> sortsd = [];
sortsd.Add(new("Id", SortDirection.Ascending));

List<SortDescriptor<TestTablePOSTGRESQL>> sortse = [];
sortse.Add(new("Id", SortDirection.Ascending));

List<SortDescriptor<TestTableIdentityPOSTGRESQL>> sortsf = [];
sortsf.Add(new("Id", SortDirection.Ascending));

List<SortDescriptor<TestTableSQLSERVER>> sortsg = [];
sortsg.Add(new("Id", SortDirection.Ascending));

List<SortDescriptor<TestTableIdentitySQLSERVER>> sortsh = [];
sortsh.Add(new("Id", SortDirection.Ascending));


var rnd = new Random();
var aa = new List<TestTableSQLSERVER>();
var bb = new List<TestTableIdentitySQLSERVER>();

#region DACHIUDERE
for (int i = 0; i < 10000; i++)
    aa.Add(new()
    {
        Id = Guid.NewGuid(),
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
            _ => rnd.Next(0, 2) == 1
        },

        TimestampValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => DateTime.UtcNow.AddSeconds(-rnd.Next(0, 1000000))
        },

        GuidValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => Guid.NewGuid()
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
            _ => rnd.Next(0, 2) == 1
        },

        TimestampValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => DateTime.UtcNow.AddSeconds(-rnd.Next(0, 1000000))
        },

        GuidValue = rnd.Next(0, 100) switch
        {
            < 20 => null,
            _ => Guid.NewGuid()
        }
    });
#endregion


FilterDescriptor<TestTableMYSQL> filtera = new(x => x.IntValue, null);
FilterDescriptor<TestTableIdentityMYSQL> filterb = new(x => x.IntValue, null);
FilterDescriptor<TestTableORACLE> filterc = new(x => x.IntValue, null);
FilterDescriptor<TestTableIdentityORACLE> filterd = new(x => x.IntValue, null);
FilterDescriptor<TestTablePOSTGRESQL> filtere = new(x => x.IntValue, null);
FilterDescriptor<TestTableIdentityPOSTGRESQL> filterf = new(x => x.IntValue, null);
FilterDescriptor<TestTableSQLSERVER> filterg = new(x => x.IntValue, null);
FilterDescriptor<TestTableIdentitySQLSERVER> filterh = new(x => x.IntValue, null);



var u_aaa = await mysqlConnection.MinAsync<TestTableMYSQL, int?>("IntValue", x => x.IntValue < 0);
var u_aaaa = await mysqlConnection.MinAsync<TestTableIdentityMYSQL, int?>(x => x.IntValue, x => x.IntValue != null);
var u_bbb = await oracleConnection.MinAsync((TestTableORACLE x) => x.BoolValue);
var u_bbbb = await oracleConnection.MinAsync<TestTableIdentityORACLE, int>("IntValue", x => x.IntValue < 50000);
var u_ccc = await postgresqlConnection.MinAsync<TestTablePOSTGRESQL, int?>(x => x.IntValue);
var u_cccc = await postgresqlConnection.MinAsync<TestTableIdentityPOSTGRESQL, int?>(x => x.IntValue);
var u_ddd = await sqlserverConnection.MinAsync<TestTableSQLSERVER, int?>(x => x.IntValue);
var u_dddd = await sqlserverConnection.MinAsync<TestTableIdentitySQLSERVER, int?>(x => x.IntValue);

Console.WriteLine("fine");
