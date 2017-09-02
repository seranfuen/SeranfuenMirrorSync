using System;
using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncLib.Data;

namespace SeranfuenMirrorSyncLibTests
{
    internal class StaticTimeProvider : ITimeProvider
    {
        #region ' Ctor '

        public StaticTimeProvider(DateTime staticTime)
        {
            CurrentTime = staticTime;
        }

        #endregion

        #region ' Properties '

        public DateTime CurrentTime
        {
            get;
            private set;
        }

        public DaysOfWeekFlag DayOfWeek
        {
            get
            {
                return DateTimeNowProvider.GetDayFlag(CurrentTime.DayOfWeek);
            }
        }

        #endregion
    }
}