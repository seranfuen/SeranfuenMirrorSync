using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SeranfuenLogging
{
    public class ConsoleLogOutput : ILogOutput
    {
        private const string HEADER_INDENT = " >> ";
        private const string END_OF_EVENT = "-------------";

        public void LogApplicationEvent(string method, string eventMessage, LoggingEventType type)
        {
            if (Debugger.IsAttached || type != LoggingEventType.Debug)
            {
                var ofSb = new StringBuilder();
                ofSb.Append(GetEventHeader(type));
                ofSb.Append(HEADER_INDENT);
                ofSb.Append("@ ");
                ofSb.AppendLine(method);
                ofSb.AppendLine();
                ofSb.Append(eventMessage);
                ofSb.AppendLine();
                ofSb.AppendLine(END_OF_EVENT);

                Console.WriteLine(ofSb.ToString());
            }
        }

        public void LogException(Exception exception, LoggingEventType type = LoggingEventType.Exception)
        {
            if (Debugger.IsAttached || type != LoggingEventType.Debug)
            {
                var ofSb = new StringBuilder();
                ofSb.Append(GetEventHeader(type));
                AppendExceptionDetails(exception, ofSb);

                if (exception.InnerException != null)
                {
                    var unrolledException = exception.UnrollException();
                    AppendExceptionDetails(unrolledException, ofSb);
                }

                ofSb.AppendLine();
                ofSb.AppendLine(END_OF_EVENT);
                Console.WriteLine(ofSb.ToString());
            }
        }

        private static void AppendExceptionDetails(Exception exception, StringBuilder ofSb)
        {
            ofSb.AppendLine(exception.StackTrace);
            ofSb.AppendLine();
            ofSb.AppendLine(exception.Message);
        }

        private string GetEventHeader(LoggingEventType eventType)
        {
            var ofSb = new StringBuilder();
            ofSb.Append(HEADER_INDENT);
            ofSb.Append(DateTime.Now.ToString("f"));
            ofSb.Append(HEADER_INDENT);
            ofSb.AppendLine(eventType.ToString());
            return ofSb.ToString();
        }
    }
}
