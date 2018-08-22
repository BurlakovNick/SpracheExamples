using System;
using FluentAssertions;
using NUnit.Framework;
using ParserExamples.Example2.Parsers;
using Sprache;

namespace ParserExamples.Example2.Tests
{
    [TestFixture]
    public class RuleParserTest
    {
        [TestCase("Bill.AmountIsZero")]
        [TestCase("Bill.AmountIsZero||Order.HasCloudCert")]
        [TestCase("History.IsOfferScheme&&Order.HasCloudCert")]
        [TestCase("History.IsOfferScheme&&Order.HasCloudCert||Bill.AmountIsZero||Bill.IsPostpay&&History.HasActiveContract")]
        public void TestParseExpression(string expr)
        {
            var actual = RuleParser.Expression.Parse(expr);
            actual.Serialize().Should().Be(expr);
        }

        [TestCase("returnBill.AmountIsZero||Order.HasCloudCert;")]
        [TestCase("if(History.IsOfferScheme)returnBill.AmountIsZero||Order.HasCloudCert;")]
        public void TestParseStatement(string expr)
        {
            var actual = RuleParser.Statement.Parse(expr);
            actual.Serialize().Should().Be(expr);
        }

        [Test]
        public void TestParseRule()
        {
            var rule =
                @"ContractRule: {
                    Product = Diadoc,
                    Logic = {
                        if (History.IsOfferScheme)
                            return false;

                        if (Bill.IsPostpay)
                            return false;

                        if (History.HasContract)
                            return false;

                        return Order.HasCloudCert || Order.HasSubscribeOrExtra;
                    }
                }";

            var actual = RuleParser.Rule.Parse(rule.WithoutWhitespaces());
            Console.WriteLine(actual.Serialize());
        }
    }
}