using SeranfuenMirrorSyncLib.Controllers;
using System;
using SeranfuenMirrorSyncLib.Data;

namespace SeranfuenMirrorSyncLibTests
{
    internal class IncreasingDayProvider : ITimeProvider
    {
        #region ' Fields '

        private DateTime _currentDate;

        #endregion

        #region ' Ctor '

        public IncreasingDayProvider(DateTime startDate)
        {
            _currentDate = startDate;
        }

        #endregion

        #region ' Properties '

        public DateTime CurrentTime
        {
            get
            {
                return _currentDate;
            }
        }

        public DaysOfWeekFlag DayOfWeek
        {
            get
            {
                return DateTimeNowProvider.GetDayFlag(_currentDate.DayOfWeek);
            }
        }

        #endregion

        #region ' Members '

        public void Step()
        {
            _currentDate = _currentDate.AddDays(1);
        }

        #endregion
    }
}