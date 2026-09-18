using DapperForge.Core.Abstractions.Strategies;


namespace DapperForge.Tests.Core
{
    /// <summary>
    /// Locks the literal SQL syntax each provider's dialect strategy renders. These are the primitives every other
    /// translator/builder test composes with, so a regression here would otherwise surface as confusing failures
    /// everywhere else instead of here.
    /// </summary>
    public class DialectPrimitivesTests
    {
        [Fact]
        public void SqlServer_UsesBracketIdentifiersAndAtParameters()
        {
            DapperForge.SqlServer.Strategies.SqlDialectStrategy dialect = DapperForge.SqlServer.Strategies.SqlDialectStrategy.Instance;

            Assert.Equal("[Name]", dialect.RenderIdentifier("Name"));
            Assert.Equal("@p0", dialect.RenderParameter("p0"));
            Assert.Equal("a + b", dialect.Concat("a", "b"));
            Assert.Equal("[Id] IN @p0", dialect.In("[Id]", "@p0", false));
            Assert.Equal("[Id] IN @p0", dialect.In("[Id]", "@p0", true));
            Assert.Equal("[Active] = 1", dialect.IsTrue("[Active]"));
            Assert.Equal("CAST(x AS NVARCHAR(MAX))", dialect.CastAsString("x"));
        }

        [Fact]
        public void MySql_UsesBacktickIdentifiersAndConcatFunction()
        {
            DapperForge.MySql.Strategies.SqlDialectStrategy dialect = DapperForge.MySql.Strategies.SqlDialectStrategy.Instance;

            Assert.Equal("`Name`", dialect.RenderIdentifier("Name"));
            Assert.Equal("@p0", dialect.RenderParameter("p0"));
            Assert.Equal("CONCAT(a, b)", dialect.Concat("a", "b"));
            Assert.Equal("`Id` IN @p0", dialect.In("`Id`", "@p0", false));
            Assert.Equal("`Id` IN @p0", dialect.In("`Id`", "@p0", true));
            Assert.Equal("`Active` = 1", dialect.IsTrue("`Active`"));
            Assert.Equal("CAST(x AS CHAR)", dialect.CastAsString("x"));
        }

        [Fact]
        public void PostgreSql_UsesAnyForInAndTrueForBooleans()
        {
            DapperForge.PostgreSql.Strategies.SqlDialectStrategy dialect = DapperForge.PostgreSql.Strategies.SqlDialectStrategy.Instance;

            Assert.Equal("\"Name\"", dialect.RenderIdentifier("Name"));
            Assert.Equal("@p0", dialect.RenderParameter("p0"));
            Assert.Equal("a || b", dialect.Concat("a", "b"));
            Assert.Equal("\"Id\" = ANY(@p0)", dialect.In("\"Id\"", "@p0", false));
            Assert.Equal("\"Id\" IN @p0", dialect.In("\"Id\"", "@p0", true));
            Assert.Equal("\"Active\" = TRUE", dialect.IsTrue("\"Active\""));
            Assert.Equal("x::text", dialect.CastAsString("x"));
        }

        [Fact]
        public void Oracle_UsesColonParametersAndToCharCast()
        {
            DapperForge.Oracle.Strategies.SqlDialectStrategy dialect = DapperForge.Oracle.Strategies.SqlDialectStrategy.Instance;

            Assert.Equal("\"Name\"", dialect.RenderIdentifier("Name"));
            Assert.Equal(":p0", dialect.RenderParameter("p0"));
            Assert.Equal("a || b", dialect.Concat("a", "b"));
            Assert.Equal("\"Id\" IN :p0", dialect.In("\"Id\"", ":p0", false));
            Assert.Equal("\"Id\" IN :p0", dialect.In("\"Id\"", ":p0", true));
            Assert.Equal("\"Active\" = 1", dialect.IsTrue("\"Active\""));
            Assert.Equal("TO_CHAR(x)", dialect.CastAsString("x"));
        }

        [Theory]
        [MemberData(nameof(DialectStrategies.All), MemberType = typeof(DialectStrategies))]
        public void EscapeLike_EscapesWildcardsButLeavesQuotesAlone(string dialectName, object dialectObject)
        {
            _ = dialectName;
            ISqlDialectStrategy dialect = (ISqlDialectStrategy)dialectObject;

            // The quote is intentionally left untouched: EscapeLike's result is always bound as a query parameter,
            // never inlined as SQL text, so it needs no SQL-string-literal escaping - only the LIKE
            // metacharacters (\, %, _) need protecting from being interpreted as wildcards.
            Assert.Equal("5\\%\\_'", dialect.EscapeLike("5%_'"));
            Assert.Equal("hello", dialect.EscapeLike("hello"));
            Assert.Equal(string.Empty, dialect.EscapeLike(string.Empty));
        }

        [Fact]
        public void MySql_Like_DoublesTheEscapeBackslashForItsOwnStringLiteralParsing()
        {
            DapperForge.MySql.Strategies.SqlDialectStrategy dialect = DapperForge.MySql.Strategies.SqlDialectStrategy.Instance;

            // MySQL applies C-style backslash escaping inside string literals by default, so the ESCAPE clause's
            // literal must contain two backslash characters for MySQL's own parser to collapse them into the one
            // literal backslash actually meant as the escape character - unlike the other three providers, whose
            // string literals don't treat backslash specially.
            Assert.Equal("col LIKE pat ESCAPE '\\\\'", dialect.Like("col", "pat"));
        }

        [Theory]
        [MemberData(nameof(DialectStrategies.All), MemberType = typeof(DialectStrategies))]
        public void ToLowerAndToUpper_WrapInStandardFunctions(string dialectName, object dialectObject)
        {
            _ = dialectName;
            ISqlDialectStrategy dialect = (ISqlDialectStrategy)dialectObject;

            Assert.Equal("LOWER(x)", dialect.ToLower("x"));
            Assert.Equal("UPPER(x)", dialect.ToUpper("x"));
        }

        [Theory]
        [InlineData("SqlServer")]
        [InlineData("Oracle")]
        public void Pagination_SqlServerAndOracle_UseOffsetFetchNext(string dialectName)
        {
            ISqlDialectStrategy dialect = dialectName == "SqlServer"
                ? DapperForge.SqlServer.Strategies.SqlDialectStrategy.Instance
                : DapperForge.Oracle.Strategies.SqlDialectStrategy.Instance;

            string expected = string.Join(Environment.NewLine,
                "",
                "OFFSET",
                "    <<SKIP>> ROWS",
                "FETCH NEXT",
                "    <<TAKE>> ROWS ONLY");

            Assert.Equal(expected, dialect.Pagination("<<SKIP>>", "<<TAKE>>"));
        }

        [Theory]
        [InlineData("MySql")]
        [InlineData("PostgreSql")]
        public void Pagination_MySqlAndPostgreSql_UseLimitOffset(string dialectName)
        {
            ISqlDialectStrategy dialect = dialectName == "MySql"
                ? DapperForge.MySql.Strategies.SqlDialectStrategy.Instance
                : DapperForge.PostgreSql.Strategies.SqlDialectStrategy.Instance;

            string expected = string.Join(Environment.NewLine,
                "",
                "LIMIT",
                "    <<TAKE>>",
                "OFFSET",
                "    <<SKIP>>");

            Assert.Equal(expected, dialect.Pagination("<<SKIP>>", "<<TAKE>>"));
        }
    }
}
