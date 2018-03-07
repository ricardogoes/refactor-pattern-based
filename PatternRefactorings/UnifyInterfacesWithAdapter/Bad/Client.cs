using System;
using System.Linq;
using System.Text;
using System.IO;

namespace PatternRefactorings.UnifyInterfacesWithAdapter.Bad
{
    class Client
    {
        private const string MESSAGE = "Hello, Pluralsight!";
        public void PrintHelloTo(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine(MESSAGE);
        }
        public void PrintHelloTo(TextWriter textWriter)
        {
            textWriter.WriteLine(MESSAGE);
        }
    }
}
