using System;
using System.Collections.Generic;
using System.Text;

namespace SeranfuenLogging
{
    public enum LoggingEventType
    {
        Debug,
        Info,
        Exception
    }

    public interface ILogOutput
    {
        void LogApplicationEvent(string method, string eventMessage, LoggingEventType type);
        void LogException(Exception exception, LoggingEventType type = LoggingEventType.Exception);
    }
}
