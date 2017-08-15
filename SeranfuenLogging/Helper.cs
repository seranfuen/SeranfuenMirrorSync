using System;
using System.Collections.Generic;
using System.Text;

namespace SeranfuenLogging
{
    public static class Helper
    {
        public static Exception UnrollException(this Exception exception)
        {
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            return exception;
        }
    }
}
