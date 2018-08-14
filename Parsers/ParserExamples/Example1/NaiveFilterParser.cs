using System;
using System.Collections.Generic;
using System.Linq;

namespace ParserExamples.Example1
{
    public static class NaiveFilterParser
    {
        public static IFilter BuildFromText(string text)
        {
            var stack = new Stack<(char, List<IFilter>)>();
            var word = new List<char>();

            stack.Push(('@', new List<IFilter>()));

            foreach (var ch in text)
            {
                if (char.IsWhiteSpace(ch))
                {
                    continue;
                }

                if (ch == ')' || ch == ']')
                {
                    Pop(ch);
                    continue;
                }

                if (ch == '(' || ch == '[')
                {
                    Push(ch);
                    continue;
                }

                if (ch == ',')
                {
                    ClearWord();
                    continue;
                }

                word.Add(ch);
            }

            ClearWord();

            return stack.Peek().Item2.First();

            void ClearWord()
            {
                if (word.Count > 0)
                {
                    stack.Peek().Item2.Add(new WordFilter(new string(word.ToArray())));
                }

                word.Clear();
            }

            void Push(char ch)
            {
                ClearWord();
                stack.Push((ch, new List<IFilter>()));
            }

            void Pop(char ch)
            {
                ClearWord();

                if (!stack.TryPop(out var top))
                {
                    throw new Exception($"stack is empty, expected = {ch}");
                }

                var expected = ch == ')' ? '(' : '[';
                if (top.Item1 != expected)
                {
                    throw new Exception($"on top of stack is {top}, expected = {ch}");
                }

                var newFilter = ch == ')'
                    ? (IFilter) new MustFilter(top.Item2)
                    : new ShouldFilter(top.Item2);

                stack.Peek().Item2.Add(newFilter);
            }
        }
    }
}