using System.Data.Common;

namespace Dapper.Forge.Core.Abstractions.Strategies
{
    internal interface ISqlDialectStrategy
    {
        string DefaultSchemaName { get; }
        string NullValue { get; }
        string Terminator { get; }
        string Placeholder { get; }

        string RenderIdentifier(string name);
        string RenderParameter(string name);
        string Concat(params string[] parts);
        string ToLower(string sql);
        string ToUpper(string sql);
        string Like(string column, string pattern);
        string EscapeLike(string value);
        string In(string identifier, string parameter);
        (string, string) In(string identifier);
        string IsNull(string column);
        string IsNotNull(string column);
        string IsTrue(string column);
        string Pagination(string skipParameter, string takeParameter);
        string CastAsString(string sql);
        string GetConnectionId(DbConnection connection);
        void Initialize(DbConnection connection);
    }
}
