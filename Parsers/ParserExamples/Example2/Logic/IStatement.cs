using ParserExamples.Example2.DataModel;

namespace ParserExamples.Example2.Logic
{
    public interface IStatement
    {
        bool? Eval(Sale sale);
        string Serialize();
    }
}