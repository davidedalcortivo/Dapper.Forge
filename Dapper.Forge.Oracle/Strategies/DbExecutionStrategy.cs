using Dapper.Forge.Core.Abstractions.Strategies;
using Dapper.Forge.Core.Caching;
using Dapper.Forge.Core.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Collections.Immutable;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Text.Json;


namespace Dapper.Forge.Oracle.Strategies
{
    internal sealed class DbExecutionStrategy : BaseDbExecutionStrategy<DbCommandStrategy>
    {
        public static DbExecutionStrategy Instance { get; } = new(DbCommandStrategy.Instance);

        private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

        private DbExecutionStrategy(DbCommandStrategy strategy) : base(strategy) { }

        public override async Task GetColumnsImplAsync<TEntity>(DbConnection connection, bool sync, int? commandTimeout, CancellationToken cancellationToken) where TEntity : class
        {
            string connectionId = SqlDialectStrategy.GetConnectionId(connection);
            IDictionary<string, DbColumnInfo>? columns = DbColumnInfoCache<TEntity>.GetDictValueOrDefault(connectionId);

            if (columns is null)
            {
                SemaphoreSlim semaphore = DbColumnInfoCache<TEntity>.GetSemaphore(connectionId);

                if (sync)
                    semaphore.Wait(cancellationToken);
                else
                    await semaphore.WaitAsync(cancellationToken);

                try
                {
                    columns = DbColumnInfoCache<TEntity>.GetDictValueOrDefault(connectionId);

                    if (columns is null)
                    {
                        ImmutableArray<PropertyInfo> properties = EntityInfoCache<TEntity>.Properties;
                        ImmutableDictionary<string, string> columnNamesByPropertyName = EntityInfoCache<TEntity>.ColumnNamesByPropertyName;

                        OracleConnection _connection = (OracleConnection)connection;
                        bool ownsConnection = _connection.State == ConnectionState.Closed;

                        try
                        {
                            if (ownsConnection)
                            {
                                if (sync)
                                    _connection.Open();
                                else
                                    await _connection.OpenAsync(cancellationToken);
                            }

                            DbCommandInfo command = dbCommandStrategy.GetColumnsCommand<TEntity>(_connection);
                            string schemaName = "SchemaName";
                            string tableName = "TableName";

                            using OracleCommand oracleCommand = _connection.CreateCommand();
                            using OracleParameter schemaParameter = new(schemaName, OracleDbType.Varchar2, ParameterDirection.Input);
                            using OracleParameter tableParameter = new(tableName, OracleDbType.Varchar2, ParameterDirection.Input);
                            using OracleParameter resultParameter = new("result", OracleDbType.Clob, ParameterDirection.Output);

                            oracleCommand.BindByName = true;
                            oracleCommand.CommandText = command.Sql;

                            if (commandTimeout.HasValue)
                                oracleCommand.CommandTimeout = commandTimeout.Value;

                            oracleCommand.Parameters.Add(schemaParameter).Value = command.Parameters!.Get<string>(schemaName);
                            oracleCommand.Parameters.Add(tableParameter).Value = command.Parameters!.Get<string>(tableName);
                            oracleCommand.Parameters.Add(resultParameter);

                            if (sync)
                                _ = oracleCommand.ExecuteNonQuery();
                            else
                                _ = await oracleCommand.ExecuteNonQueryAsync(cancellationToken);

                            string json = ((OracleClob)resultParameter.Value).Value;

                            columns = (JsonSerializer.Deserialize<List<DbColumnInfo>>(json, _jsonOptions) ?? [])
                                .ToDictionary(x => x.Name, x => x, StringComparer.OrdinalIgnoreCase);

                            if (properties.Length != columns.Count)
                                throw new InvalidOperationException($"Database table schema mismatch for entity '{typeof(TEntity).Name}'. Expected {properties.Length} mapped properties but found {columns.Count} database columns.");

                            foreach (PropertyInfo property in properties)
                            {
                                string columnName = columnNamesByPropertyName[property.Name];

                                if (!columns.TryGetValue(columnName, out DbColumnInfo? _))
                                    throw new InvalidOperationException($"Database column mapping mismatch for entity '{typeof(TEntity).Name}'. Database column '{columnName}' is not mapped to any entity property.");
                            }

                            _ = DbColumnInfoCache<TEntity>.TryAdd(connectionId, columns);
                        }
                        finally
                        {
                            if (ownsConnection)
                            {
                                if (sync)
                                    _connection.Close();
                                else
                                    await _connection.CloseAsync();
                            }
                        }
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
        }
    }
}
