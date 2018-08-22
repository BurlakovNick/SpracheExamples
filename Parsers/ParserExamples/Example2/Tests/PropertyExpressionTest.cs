using FluentAssertions;
using NUnit.Framework;
using ParserExamples.Example2.DataModel;
using ParserExamples.Example2.Logic;

namespace ParserExamples.Example2.Tests
{
    [TestFixture]
    public class PropertyExpressionTest
    {
        [Test]
        public void TestEval()
        {
            var sale = new Sale { Bill = new Bill {AmountIsZero = true}};
            var expr = new PropertyExpression(new [] {"Bill.AmountIsZero"});
            var actual = expr.Eval(sale);
            actual.Should().BeTrue();
        }
    }
}