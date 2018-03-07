using System;
using System.Linq;
using System.Text;
using System.IO;

namespace PatternRefactorings.UnifyInterfacesWithAdapter.Better3
{
    // Step Three: Extract methods from the client and move them to the adapter
    public interface IAppender
    {
        void AppendLine(string message);
    }

    public class TextWriterAppenderAdapter
    {

        public TextWriterAppenderAdapter(TextWriter writer)
        {
            this.Writer = writer;
        }

        public TextWriter Writer { get; private set; }

        // Extracted from Client
        public void AppendLine(string message)
        {
            Writer.WriteLine(message);
        }
    }

    class Client
    {
        private const string MESSAGE = "Hello, Pluralsight!";
        public void PrintHelloTo(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine(MESSAGE);
        }
        public void PrintHelloTo(TextWriterAppenderAdapter textWriter)
        {
            textWriter.AppendLine(MESSAGE);
        }
    }
}
