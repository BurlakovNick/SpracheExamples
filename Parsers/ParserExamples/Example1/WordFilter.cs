namespace ParserExamples.Example1
{
    public class WordFilter : IFilter
    {
        private readonly string word;

        public WordFilter(string word) => this.word = word;

        public bool Match(string text) => text.Contains(word);

        public string Serialize() => word;
    }
}