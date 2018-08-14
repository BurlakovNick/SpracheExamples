namespace ParserExamples.Example1
{
    public interface IFilter
    {
        bool Match(string text);
        string Serialize();
    }
}