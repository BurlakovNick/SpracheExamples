using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ParserExamples.Example2.DataModel;

namespace ParserExamples.Example2.Logic
{
    public class PropertyExpression : IExpression
    {
        private readonly IEnumerable<string> propertyPath;

        public PropertyExpression(
            IEnumerable<string> propertyPath
        )
        {
            this.propertyPath = propertyPath;
        }

        public bool Eval(Sale sale)
        {
            var type = typeof(Sale);
            object val = sale;

            PropertyInfo property;
            foreach (var prop in propertyPath)
            {
                var properties = type.GetProperties();
                property = properties.FirstOrDefault(p => p.Name == prop);
                if (property == null)
                {
                    throw new InvalidOperationException($"Property {prop} is not found, full path = {string.Join(".", propertyPath)}");
                }

                val = property.GetValue(val);
                type = val.GetType();
            }

            return (bool) val;
        }

        public string Serialize()
        {
            return string.Join(".", propertyPath);
        }
    }
}