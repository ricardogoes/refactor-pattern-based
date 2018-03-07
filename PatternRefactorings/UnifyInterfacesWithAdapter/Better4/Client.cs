using System;
using System.Linq;
using System.Text;
using System.IO;

namespace PatternRefactorings.UnifyInterfacesWithAdapter.Better4
{
    // Step Four: Implement the interface with the adapter class
    // Step Five: Update client so all references to adapter class use common interface
    public interface IAppender
    {
        void AppendLine(string message);
    }

    // Implement IAppender
    public class TextWriterAppenderAdapter : IAppender
    {

        public TextWriterAppenderAdapter(TextWriter writer)
        {
            this.Writer = writer;
        }

        // this can be made private or a private field
        private TextWriter Writer { get; set; }

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
        // update to use common interface
        public void PrintHelloTo(IAppender appender)
        {
            appender.AppendLine(MESSAGE);
        }
    }
}
