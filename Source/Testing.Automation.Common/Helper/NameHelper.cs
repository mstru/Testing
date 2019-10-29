using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Testing.Automation.Common.Helper
{
    
    /// <summary>
    /// Trieda obsahuje užitočné činnosti pri práci s testovacími dátami.
    /// </summary>
    public static class NameHelper
    {
        /// <summary>
        /// Vytvorí náhodný názov.
        /// </summary>
        /// <param name="length">Dĺžka.</param>
        /// <returns></returns>
        public static string RandomName(int length)
        {
            const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var randomString = new StringBuilder();
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                randomString.Append(Chars[random.Next(Chars.Length)]);
            }
            return randomString.ToString();
        }

        /// <summary>
        /// Skráti názov súboru odstránením výskytov daného vzoru až do dĺžky priečinka + názov súboru sa skráti ako maximálna dĺžka.
        /// </summary>
        /// <param name="folder">Zložka.</param>
        /// <param name="fileName">Názov súboru.</param>
        /// <param name="pattern">Vzor regulárneho výrazu, ktorý sa má zhodovať.</param>
        /// <param name="maxLength">Max dĺžka</param>
        /// <returns>Reťazec.</returns>
        public static string ShortenFileName(string folder, string fileName, string pattern, int maxLength)
        {
            while (((folder + fileName).Length > maxLength) && fileName.Contains(pattern))
            {
                Regex rgx = new Regex(pattern);
                fileName = rgx.Replace(fileName, string.Empty, 1);
            }

            if ((folder + fileName).Length > 255)
            {
                //..
            }
            return fileName;
        }
    }
}
