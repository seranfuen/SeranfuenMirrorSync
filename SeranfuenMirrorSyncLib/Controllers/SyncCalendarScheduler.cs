using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeranfuenMirrorSyncLib.Data;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class SyncCalendarScheduler
    {
        #region ' Constructor '

        public SyncCalendarScheduler()
        {
            TimeProvider = DateTimeNowProvider.Instance;
        }

        #endregion

        #region ' Properties '

        public ITimeProvider TimeProvider
        {
            get;
            set;
        }

        #endregion

        #region ' Members '

        public void PutSchedule(ISchedule schedule)
        {
            throw new NotImplementedException();
        }

        public bool CheckScheduled(ISchedule schedule)
        {
            throw new NotImplementedException();
        }

        public void NotifyDone(ISchedule schedule)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
