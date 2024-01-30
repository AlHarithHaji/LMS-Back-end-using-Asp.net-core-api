using System.Text.RegularExpressions;

namespace LMSWebAPI.Utility
{
    public static class StringExtensions
    {
        public static string ToSlug(this string value)
        {
            return Regex.Replace(value, "[^a-zA-Z0-9-]", "-").ToLower();
        }
        public static string FromSlug(this string slug)
        {
            return Regex.Replace(slug, "-", " ");
        }
        public static string RemoveSpecialCharacters(this string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_\\s.]+", "", RegexOptions.Compiled);
        }

    }
}
