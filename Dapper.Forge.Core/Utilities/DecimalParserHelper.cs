using System.Globalization;
using System.Text;


namespace Dapper.Forge.Core.Utilities
{
    internal static class DecimalParserHelper
    {
        private static readonly string _decimalMinIntegerPart;
        private static readonly string _decimalMinFractionalPart;
        private static readonly string _decimalMinEffectiveFractionalPart;
        private static readonly string _decimalMaxIntegerPart;
        private static readonly string _decimalMaxFractionalPart;
        private static readonly string _decimalMaxEffectiveFractionalPart;
        private static readonly int _maxDecimalDigits;
        private static readonly bool _isDecimalMinNegative;

        static DecimalParserHelper()
        {
            string[] parts = ExpandScientificNotation(decimal.MinValue.ToString(CultureInfo.InvariantCulture)).Split('.');
            _decimalMinIntegerPart = parts[0];
            _decimalMinFractionalPart = parts.Length > 1 ? parts[1] : string.Empty;
            _decimalMinEffectiveFractionalPart = _decimalMinFractionalPart.TrimEnd('0');

            parts = ExpandScientificNotation(decimal.MaxValue.ToString(CultureInfo.InvariantCulture)).Split('.');
            _decimalMaxIntegerPart = parts[0];
            _decimalMaxFractionalPart = parts.Length > 1 ? parts[1] : string.Empty;
            _decimalMaxEffectiveFractionalPart = _decimalMaxFractionalPart.TrimEnd('0');

            if (_decimalMinIntegerPart.StartsWith('-'))
            {
                _decimalMinIntegerPart = _decimalMinIntegerPart[1..];
                _isDecimalMinNegative = true;
            }

            _maxDecimalDigits = Math.Max(_decimalMinIntegerPart.Length + _decimalMinFractionalPart.Length,
                _decimalMaxIntegerPart.Length + _decimalMaxFractionalPart.Length);
        }

        private static void EnsureDecimalIntegerPartOverflow(string integerPart, bool isNegative)
        {
            if (isNegative && !_isDecimalMinNegative ||
                isNegative && (integerPart.Length > _decimalMinIntegerPart.Length || integerPart.Length == _decimalMinIntegerPart.Length && string.CompareOrdinal(integerPart, _decimalMinIntegerPart) > 0) ||
                !isNegative && (integerPart.Length > _decimalMaxIntegerPart.Length || integerPart.Length == _decimalMaxIntegerPart.Length && string.CompareOrdinal(integerPart, _decimalMaxIntegerPart) > 0))
                throw new OverflowException();
        }

        private static void EnsureDecimalFractionalPartOverflow(string integerPart, string fractionalPart, bool isNegative)
        {
            if (isNegative && integerPart.Length == _decimalMinIntegerPart.Length && string.CompareOrdinal(integerPart, _decimalMinIntegerPart) == 0)
            {
                if (fractionalPart.Length > _decimalMinEffectiveFractionalPart.Length)
                    throw new OverflowException();

                StringBuilder fractionalBuffer = new(_decimalMinFractionalPart.Length);
                fractionalBuffer.Append(fractionalPart);

                while (fractionalBuffer.Length < _decimalMinFractionalPart.Length)
                    fractionalBuffer.Append('0');

                if (string.CompareOrdinal(fractionalBuffer.ToString(), _decimalMinFractionalPart) > 0)
                    throw new OverflowException();
            }

            if (integerPart.Length == _decimalMaxIntegerPart.Length && string.CompareOrdinal(integerPart, _decimalMaxIntegerPart) == 0)
            {
                if (fractionalPart.Length > _decimalMaxEffectiveFractionalPart.Length)
                    throw new OverflowException();

                StringBuilder fractionalBuffer = new(_decimalMaxFractionalPart.Length);
                fractionalBuffer.Append(fractionalPart);

                while (fractionalBuffer.Length < _decimalMaxFractionalPart.Length)
                    fractionalBuffer.Append('0');

                if (string.CompareOrdinal(fractionalBuffer.ToString(), _decimalMaxFractionalPart) > 0)
                    throw new OverflowException();
            }
        }

        private static string ExpandScientificNotation(string value)
        {
            int eIndex = value.IndexOfAny(['E', 'e']);

            if (eIndex < 0)
                return value;

            string mantissa = value[..eIndex];
            int exponent = int.Parse(value[(eIndex + 1)..], CultureInfo.InvariantCulture);

            bool isNegative = mantissa.StartsWith('-');

            if (isNegative)
                mantissa = mantissa[1..];

            string[] parts = mantissa.Split('.');

            string integerPart = parts[0];
            string fractionalPart = parts.Length > 1 ? parts[1] : string.Empty;

            string digits = integerPart + fractionalPart;
            int decimalIndex = integerPart.Length;

            int newDecimalIndex = decimalIndex + exponent;

            string result;

            if (newDecimalIndex <= 0)
            {
                result = "0." + new string('0', -newDecimalIndex) + digits;
            }
            else if (newDecimalIndex >= digits.Length)
            {
                result = digits + new string('0', newDecimalIndex - digits.Length);
            }
            else
            {
                result = digits.Insert(newDecimalIndex, ".");
            }

            result = result.TrimStart('0');

            if (result.StartsWith('.'))
                result = "0" + result;

            if (string.IsNullOrEmpty(result))
                result = "0";

            return isNegative ? "-" + result : result;
        }

        public static decimal? ParseDecimal(this string? s)
        {
            if (s is null)
                return null;

            s = ExpandScientificNotation(s);

            bool isNegative = s.StartsWith('-');

            if (isNegative)
                s = s[1..];

            string[] parts = s.Split(".");
            string integerPart = parts[0].TrimStart('0');
            string fractionalPart = parts.Length > 1 ? parts[1] : string.Empty;

            if (integerPart.Length == 0)
                integerPart = "0";

            EnsureDecimalIntegerPartOverflow(integerPart, isNegative);
            int allowedDecimalDigits = Math.Max(0, _maxDecimalDigits - integerPart.Length);

            if (fractionalPart.Length > allowedDecimalDigits)
            {
                char[] fractionalArray = fractionalPart[..allowedDecimalDigits].ToCharArray();

                if (fractionalPart[allowedDecimalDigits] >= '5')
                {
                    int i = allowedDecimalDigits - 1;

                    while (i >= 0)
                    {
                        if (fractionalArray[i] < '9')
                        {
                            fractionalArray[i]++;
                            break;
                        }

                        fractionalArray[i] = '0';
                        i--;
                    }

                    if (i < 0)
                    {
                        char[] interaChars = integerPart.ToCharArray();
                        int j = interaChars.Length - 1;

                        while (j >= 0)
                        {
                            if (interaChars[j] < '9')
                            {
                                interaChars[j]++;
                                break;
                            }

                            interaChars[j] = '0';
                            j--;
                        }

                        if (j < 0)
                            integerPart = "1" + new string(interaChars);
                        else
                            integerPart = new string(interaChars);
                    }
                }

                fractionalPart = new string(fractionalArray).TrimEnd('0');

                EnsureDecimalIntegerPartOverflow(integerPart, isNegative);
                EnsureDecimalFractionalPartOverflow(integerPart, fractionalPart, isNegative);

                s = fractionalPart.Length > 0 ? $"{integerPart}.{fractionalPart}" : integerPart;
            }
            else
            {
                s = string.Concat(integerPart, fractionalPart.Length > 0 ? $".{fractionalPart}" : string.Empty);
            }

            if (isNegative)
                s = "-" + s;

            return decimal.Parse(s, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
        }
    }
}
