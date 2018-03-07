using System;
using System.Linq;
using System.Text;
using System.IO;

namespace PatternRefactorings.UnifyInterfacesWithAdapter.Good
{
    // In this case, we need an adapter for StringBuilder, too, since we don't own that type
    public interface IAppender
    {
        void AppendLine(string message);
    }

    public class TextWriterAppenderAdapter : IAppender
    {
        private readonly TextWriter _writer;

        public TextWriterAppenderAdapter(TextWriter writer)
        {
            this._writer = writer;
        }

        public void AppendLine(string message)
        {
            _writer.WriteLine(message);
        }
    }

    public class StringBuilderAppenderAdapter : IAppender
    {
        private readonly StringBuilder _builder;

        public StringBuilderAppenderAdapter(StringBuilder builder)
        {
            this._builder = builder;
        }

        public void AppendLine(string message)
        {
            _builder.AppendLine(message);
        }
    }

    class Client
    {
        private const string MESSAGE = "Hello, Pluralsight!";

        // Can be removed provided clients are able to pass a StringBuilderAppenderAdapter
        //public void PrintHelloTo(StringBuilder stringBuilder)
        //{
        //    stringBuilder.AppendLine(MESSAGE);
        //}

        // now a single method is used polymorphically
        public void PrintHelloTo(IAppender appender)
        {
            appender.AppendLine(MESSAGE);
        }
    }
}
