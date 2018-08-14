using System.Linq;

namespace ParserExamples
{
    public static class StringExtentions
    {
        public static string WithoutWhitespaces(this string str) => new string(str.Where(x => !char.IsWhiteSpace(x)).ToArray());
    }
}