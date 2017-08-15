using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class DateTimeNowProvider : ITimeProvider
    {
        #region ' Singleton '

        private static DateTimeNowProvider _instance;

        public static DateTimeNowProvider Instance
        {
            get
            {
                if (_instance == null) _instance = new DateTimeNowProvider();
                return _instance;
            }
        }

        private DateTimeNowProvider() { }

        #endregion

        public DateTime CurrentTime
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
