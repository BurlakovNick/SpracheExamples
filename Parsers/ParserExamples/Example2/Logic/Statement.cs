using System;
using ParserExamples.Example2.DataModel;

namespace ParserExamples.Example2.Logic
{
    public class ReturnStatement : IStatement
    {
        private readonly IExpression expression;

        public ReturnStatement(IExpression expression)
        {
            this.expression = expression;
        }

        public bool? Eval(Sale sale)
        {
            return expression.Eval(sale);
        }

        public string Serialize()
        {
            return $"return{expression.Serialize()};";
        }
    }
}