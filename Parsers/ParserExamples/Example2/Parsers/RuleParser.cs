using System.Collections.Generic;
using System.Linq;
using ParserExamples.Example2.Logic;
using Sprache;

namespace ParserExamples.Example2.Parsers
{
    public static class RuleParser
    {
        private static Parser<string> Word =>
            Parse
            .LetterOrDigit
            .AtLeastOnce()
            .Text();

        public static Parser<string> Property => Word;

        public static Parser<IEnumerable<string>> PropertyPath =>
            Property.DelimitedBy(Parse.Char('.'));

        public static Parser<IExpression> PropertyExpression =>
            from path in PropertyPath
            select new PropertyExpression(path);

        public static Parser<IExpression> AndExpression =>
            Parse
                .ChainOperator(Parse.String("&&"), PropertyExpression,
                    (_, left, right) => new AndExpression(left, right));

        public static Parser<IExpression> OrExpression =>
            Parse
                .ChainOperator(Parse.String("||"), AndExpression,
                    (_, left, right) => new OrExpression(left, right));

        public static Parser<IExpression> Expression => OrExpression;

        public static Parser<IStatement> ReturnStatement =>
            from _ in Parse.String("return")
            from expr in Expression
            from __ in Parse.String(";")
            select new ReturnStatement(expr);

        public static Parser<IStatement> IfStatement =>
            from _ in Parse.String("if(")
            from expr in Expression
            from __ in Parse.String(")")
            from statement in ReturnStatement
            select new IfStatement(expr, statement);

        public static Parser<IStatement> Statement =>
            IfStatement.Or(ReturnStatement);

        public static Parser<IEnumerable<IStatement>> StatementList =>
            Statement.Many();

        public static Parser<string> Product =>
            from _ in Parse.String("Product=")
            from product in Word
            from __ in Parse.String(",")
            select product;

        public static Parser<string> RuleName => Word;

        public static Parser<Rule> Rule =>
            from ruleName in RuleName
            from _ in Parse.String(":{")
            from product in Product
            from __ in Parse.String("Logic={")
            from statements in StatementList
            from ___ in Parse.String("}}")
            select new Rule
            {
                Name = ruleName,
                Product = product,
                Statements = statements.ToArray()
            };
    }
}