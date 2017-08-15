using System;
using System.Collections.Generic;
using System.Text;

namespace SeranfuenLogging
{
    public class Logger
    {
        #region ' Singleton '

        private static Logger _instance;

        public static Logger Instance
        {
            get
            {
                if (_instance == null) _instance = new Logger();
                return _instance;
            }
        }

        private Logger()
        {
            InitializerLoggers();
        }

        #endregion

        #region ' Fields '

        private List<ILogOutput> _outputs = new List<ILogOutput>();

        #endregion

        #region ' Methods '

        public void LogApplicationEvent(string method, string eventMessage, LoggingEventType type)
        {
            _outputs.ForEach(output => output.LogApplicationEvent(method, eventMessage, type));
        }

        public void LogException(Exception exception, LoggingEventType type = LoggingEventType.Exception)
        {
            _outputs.ForEach(output => output.LogException(exception, type));
        }

        private void InitializerLoggers()
        {
            _outputs.Add(new ConsoleLogOutput());
        }

        #endregion
    }
}
