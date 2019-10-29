using System;
using System.Globalization;
using System.Text;

namespace Testing.Automation.Web.Extensions
{
    /// <summary>
    /// Trieda, ktora odstranuje diaktriiku zo stringu
    /// </summary>
    public static class RemoveDiacritics
    {
        public static String WithText(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
