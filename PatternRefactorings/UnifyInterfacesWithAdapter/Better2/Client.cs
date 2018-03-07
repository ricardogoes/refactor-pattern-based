using System;
using System.Linq;
using System.Text;
using System.IO;

namespace PatternRefactorings.UnifyInterfacesWithAdapter.Better2
{
    // Step Two: Extract Class on Client Code
    //  Pull the non-preferred class (TextWriter) into a new adapter class
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
    }

    class Client
    {
        private const string MESSAGE = "Hello, Pluralsight!";
        public void PrintHelloTo(StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine(MESSAGE);
        }
        public void PrintHelloTo(TextWriterAppenderAdapter textWriter) // updated to use adapter type; callers will need updated as well.
        {
            textWriter.Writer.WriteLine(MESSAGE);
        }
    }
}
