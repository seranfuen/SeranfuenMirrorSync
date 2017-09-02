using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class ScheduleController
    {
        #region ' Singleton '

        private static ScheduleController _instance;
        private static object oLock = new object();

        public static ScheduleController Instance
        {
            get
            {
                lock (oLock)
                {
                    if (_instance == null) _instance = new ScheduleController();
                    return _instance;
                }
            }
        }

        #endregion
    }
}
