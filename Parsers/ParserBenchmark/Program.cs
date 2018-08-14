using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ParserExamples.Example1;

namespace ParserBenchmark
{
    public class FilterParserBenchmark
    {
        [ParamsSource(nameof(GenerateTests))]
        public string Text { get; set; }

        public IEnumerable<string> GenerateTests()
        {
            yield return GenDeepBrackets();
            yield return GenLongList();
        }

        private static string GenDeepBrackets()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append("(");
            for (var j = 0; j < 100; j++)
            {
                if (j > 0)
                {
                    stringBuilder.Append(",");
                }
                var n = 500;
                for (var i = 0; i < n; i++)
                {
                    stringBuilder.Append(i % 2 == 0 ? '(' : '[');
                }

                stringBuilder.Append('x');

                for (var i = n - 1; i >= 0; i--)
                {
                    stringBuilder.Append(i % 2 == 0 ? ')' : ']');
                }
            }
            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }

        private static string GenLongList()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("(");

            for (var j = 0; j < 100; j++)
            {
                if (j > 0)
                {
                    stringBuilder.Append(",");
                }

                stringBuilder.Append("[");
                for (var i = 0; i < 500; i++)
                {
                    if (i > 0)
                    {
                        stringBuilder.Append(",");
                    }
                    stringBuilder.Append("x");
                }
                stringBuilder.Append("]");
            }

            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        [Benchmark]
        public void Sprache() => FilterParser.BuildFromText(Text);

        [Benchmark]
        public void Naive() => FilterParser.BuildFromText(Text);
    }

    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<FilterParserBenchmark>();
        }
    }
}