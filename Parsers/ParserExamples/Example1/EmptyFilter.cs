namespace ParserExamples.Example1
{
    public class EmptyFilter : IFilter
    {
        public bool Match(string text) => true;

        public string Serialize() => string.Empty;
    }
}