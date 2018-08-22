using System.Linq;

namespace ParserExamples.Example2.Logic
{
    public class Rule
    {
        public string Name { get; set; }
        public string Product { get; set; }
        public IStatement[] Statements { get; set; }

        public string Serialize()
        {
            return
                $"{Name}: {{\n" +
                $"\tProduct = {Product},\n" +
                $"\tLogic = {{\n" +
                string.Join("\n", Statements.Select(x => $"\t\t{x.Serialize()}")) +
                $"}}";
        }
    }
}