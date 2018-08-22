using ParserExamples.Example2.DataModel;

namespace ParserExamples.Example2.Logic
{
    public interface IExpression
    {
        bool Eval(Sale sale);
        string Serialize();
    }
}