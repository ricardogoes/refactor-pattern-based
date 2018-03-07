using System;
using System.Linq;
using System.Text;
using System.IO;

namespace PatternRefactorings.UnifyInterfacesWithAdapter.Better1
{
    // Step One: Choose preferred interface and extract to common interface
    public interface IAppender
    {
        void AppendLine(string message);
    }

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
