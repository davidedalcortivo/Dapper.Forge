using System.Text.RegularExpressions;


namespace DapperForge.Core.Utilities
{
    internal static partial class IdentifierHelper
    {
        [GeneratedRegex("^[A-Za-z][A-Za-z0-9_]*$", RegexOptions.Compiled)]
        public static partial Regex CharsetRegex();
    }
}
