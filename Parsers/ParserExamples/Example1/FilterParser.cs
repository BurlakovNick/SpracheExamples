using System;
using System.Collections.Generic;
using System.Linq;
using Sprache;

namespace ParserExamples.Example1
{
    public static class FilterParser
    {
        public static IFilter BuildFromText(string text)
        {
            var normalizedText = new string(text.Where(c => !char.IsWhiteSpace(c)).ToArray());
            var input = new Input(normalizedText);
            var parser = Expr;
            var parsed = parser(input);
            if (!parsed.WasSuccessful)
            {
                throw new Exception($"Message: {parsed.Message}, Offset: {parsed.Remainder}");
            }
            return parsed.Value;
        }

        private static Parser<IFilter> Word =>
            Parse
                .LetterOrDigit
                .AtLeastOnce()
                .Text()
                .Select(word => new WordFilter(word));

        private static Parser<IEnumerable<IFilter>> List =>
            Parse.Ref(() => Expr)
                .DelimitedBy(Parse.Chars(',', ';'));

        private static Parser<IFilter> Should =>
            from left in Parse.Char('[')
            from expr in List.Optional()
            from right in Parse.Char(']')
            select new ShouldFilter(expr.GetOrDefault()?.ToList());

        private static Parser<IFilter> Must =>
            from left in Parse.Char('(')
            from expr in List.Optional()
            from right in Parse.Char(')')
            select new MustFilter(expr.GetOrDefault()?.ToList());

        private static Parser<IFilter> Expr => Should.XOr(Must).XOr(Word);
    }
}
