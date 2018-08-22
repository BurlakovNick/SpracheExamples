namespace ParserExamples.Example2.DataModel
{
    public class Sale
    {
        public Order Order { get; set; }
        public Bill Bill { get; set; }
        public History History { get; set; }
    }
}