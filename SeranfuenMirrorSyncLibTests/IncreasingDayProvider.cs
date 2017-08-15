using SeranfuenMirrorSyncLib.Controllers;
using System;

namespace SeranfuenMirrorSyncLibTests
{
    internal class IncreasingDayProvider : ITimeProvider
    {
        private DateTime _currentDate;

        public IncreasingDayProvider(DateTime startDate)
        {
            _currentDate = startDate;
        }

        public DateTime CurrentTime
        {
            get
            {
                return _currentDate;
            }
        }

        public void Step()
        {
            _currentDate = _currentDate.AddDays(1);
        }
    }
}