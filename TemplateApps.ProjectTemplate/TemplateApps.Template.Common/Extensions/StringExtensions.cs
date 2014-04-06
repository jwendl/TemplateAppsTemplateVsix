using System;
using System.Linq;
using System.Text;

namespace $safeprojectname$.Extensions
{
    /// <summary>
    /// Some string helper classes to help with database naming conventions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Changes text that is PascalCasing to Pascal_Casing
        /// </summary>
        /// <param name="pascalCase">The pascal case.</param>
        /// <returns></returns>
        public static string ToUnderscore(this string pascalCase)
        {
            Arg.IsNotNull(() => pascalCase);

            var stringBuilder = new StringBuilder();
            char prevCharacter = pascalCase.First();
            foreach (var c in pascalCase)
            {
                if (char.IsLower(prevCharacter) && char.IsUpper(c))
                {
                    stringBuilder.Append('_');
                }

                stringBuilder.Append(c);
                prevCharacter = c;
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Uppers the case id.
        /// </summary>
        /// <param name="pascalCase">The pascal case.</param>
        /// <returns></returns>
        public static string ToUppercaseId(this string pascalCase)
        {
            Arg.IsNotNull(() => pascalCase);

            return pascalCase.Replace("Id", "ID");
        }

        /// <summary>
        /// Encodes a string to base 64.
        /// </summary>
        /// <param name="value">The string to encode.</param>
        /// <returns></returns>
        public static string ToBase64(this string value)
        {
            Arg.IsNotNull(() => value);

            var encodedBytes = new byte[value.Length];
            encodedBytes = Encoding.UTF8.GetBytes(value);

            var encodedData = Convert.ToBase64String(encodedBytes);
            return encodedData;
        }

        /// <summary>
        /// Decodes a base 64 string.
        /// </summary>
        /// <param name="value">The base 64 string.</param>
        /// <returns></returns>
        public static string FromBase64(this string value)
        {
            var encoder = new UTF8Encoding();
            var utf8Decode = encoder.GetDecoder();
            var toEncodeBytes = Convert.FromBase64String(value);

            var characterCount = utf8Decode.GetCharCount(toEncodeBytes, 0, toEncodeBytes.Length);
            var decodedCharacter = new char[characterCount];
            utf8Decode.GetChars(toEncodeBytes, 0, toEncodeBytes.Length, decodedCharacter, 0);

            var result = new String(decodedCharacter);
            return result;
        }
    }
}
