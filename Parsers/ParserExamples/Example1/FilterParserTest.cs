using System;
using FluentAssertions;
using NUnit.Framework;

namespace ParserExamples.Example1
{
    [TestFixture]
    public class FilterParserTest
    {
        [Test]
        [TestCase("([([(Ижевск)])])")]
        [TestCase("( [Греция, Салоники, Родос], [Лиссабон, Порту, Португалия], Дублин)")]
        [TestCase("([([()])])")]
        [TestCase("Екатеринбург")]
        [TestCase("[Екатеринбург, Мельбурн]")]
        public void TestParse(string text)
        {
            TestParser(text, "SPRACHE", FilterParser.BuildFromText);
            TestParser(text, "NAIVE",   NaiveFilterParser.BuildFromText);
        }

        private static void TestParser(string text, string parserType, Func<string, IFilter> parse)
        {
            var filter = parse(text);
            var actual = filter.Serialize();
            actual.Should().Be(text.WithoutWhitespaces(), parserType);
        }
    }
}
