using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;
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
            return parsed.Value.Values.FirstOrDefault();
        }

        private static Parser<FilterList> Word =>
            Parse
                .LetterOrDigit
                .AtLeastOnce()
                .Text()
                .Select(word => new FilterList(new WordFilter(word)));

        private static Parser<FilterList> List =>
            Parse
                .ChainOperator(Parse.Chars(',', ';'), Parse.Ref(() => Expr), (_, left, right) => FilterList.Merge(left, right));

        private static Parser<FilterList> Should =>
            from left in Parse.Char('[')
            from expr in List.Optional()
            from right in Parse.Char(']')
            let filter = new ShouldFilter(expr.GetOrDefault()?.Values)
            select new FilterList(filter);

        private static Parser<FilterList> Must =>
            from left in Parse.Char('(')
            from expr in List.Optional()
            from right in Parse.Char(')')
            let filter = new MustFilter(expr.GetOrDefault()?.Values)
            select new FilterList(filter);

        private static Parser<FilterList> Expr => Should.XOr(Must).XOr(Word);

        private class FilterList
        {
            public FilterList(IEnumerable<IFilter> wordList) => Values = wordList.ToList();

            public FilterList(IFilter wordFilter) => Values = new List<IFilter> {wordFilter};

            public static FilterList Merge(FilterList left, FilterList right) => new FilterList(left.Values.Union(right.Values));

            public List<IFilter> Values { get; }
        }
    }
}
