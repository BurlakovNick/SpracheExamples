﻿using ParserExamples.Example2.DataModel;

namespace ParserExamples.Example2.Logic
{
    public class OrExpression : IExpression
    {
        private readonly IExpression left;
        private readonly IExpression right;

        public OrExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public bool Eval(Sale sale)
        {
            return left.Eval(sale) || right.Eval(sale);
        }

        public string Serialize()
        {
            return $"{left.Serialize()}||{right.Serialize()}";
        }
    }
}