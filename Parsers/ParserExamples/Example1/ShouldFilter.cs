using System.Collections.Generic;
using System.Linq;

namespace ParserExamples.Example1
{
    public class ShouldFilter : IFilter
    {
        private readonly List<IFilter> filters;

        public ShouldFilter(List<IFilter> filters) => this.filters = filters ?? new List<IFilter>();

        public bool Match(string text) => filters.Any(f => f.Match(text));

        public string Serialize() => $"[{string.Join(",", filters.Select(x => x.Serialize()))}]";
    }
}