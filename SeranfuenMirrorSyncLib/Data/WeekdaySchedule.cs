using SeranfuenMirrorSyncLib.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    [Serializable]
    public class WeekdaySchedule : ISchedule
    {
        private DaysOfWeekFlag _dayOfWeek;
        private Time _time;

        #region ' Constructor '

        public WeekdaySchedule(DaysOfWeekFlag dayOfWeek, Time time)
        {
            _dayOfWeek = dayOfWeek;
            _time = time;
            TimeProvider = DateTimeNowProvider.Instance;
        }

        #endregion

        #region ' Properties '

        public DateTime? LastTimeRun
        {
            get;
            private set;
        }
        public ITimeProvider TimeProvider
        {
            get;
            set;
        }

        public bool IsScheduled
        {
            get
            {
                if (!IsDayScheduled(TimeProvider.DayOfWeek))
                {
                    return false;
                }
                var currentTime = TimeProvider.CurrentTime;
                var todayDate = currentTime.Date;
                if (!LastTimeRun.HasValue || todayDate > LastTimeRun.Value.Date)
                {
                    var scheduledTime = new DateTime(todayDate.Year, todayDate.Month, todayDate.Day, _time.Hour, _time.Minute, 0);
                    var minutesDiff = (currentTime - scheduledTime).TotalMinutes;
                    return minutesDiff >= 0 && minutesDiff < 60;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region ' Members '

        public void SetSyncRun()
        {
            LastTimeRun = TimeProvider.CurrentTime;
        }

        private bool IsDayScheduled(DaysOfWeekFlag dayOfWeek)
        {
            return (dayOfWeek & _dayOfWeek) != 0;
        }

        #endregion
    }
}
