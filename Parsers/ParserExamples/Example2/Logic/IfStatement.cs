using ParserExamples.Example2.DataModel;

namespace ParserExamples.Example2.Logic
{
    public class IfStatement : IStatement
    {
        private readonly IExpression expression;
        private readonly IStatement statement;

        public IfStatement(IExpression expression, IStatement statement)
        {
            this.expression = expression;
            this.statement = statement;
        }

        public bool? Eval(Sale sale)
        {
            return expression.Eval(sale)
                ? statement.Eval(sale)
                : null;
        }

        public string Serialize()
        {
            return $"if({expression.Serialize()}){statement.Serialize()}";
        }
    }
}