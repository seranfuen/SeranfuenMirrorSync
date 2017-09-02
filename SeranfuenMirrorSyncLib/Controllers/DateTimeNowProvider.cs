using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeranfuenMirrorSyncLib.Data;

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

        #region ' Members '

        public DateTime CurrentTime
        {
            get
            {
                return DateTime.Now;
            }
        }

        public DaysOfWeekFlag DayOfWeek
        {
            get
            {
                return GetDayFlag(CurrentTime.DayOfWeek);
            }
        }

        public static DaysOfWeekFlag GetDayFlag(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case System.DayOfWeek.Monday:
                    return DaysOfWeekFlag.Monday;
                case System.DayOfWeek.Tuesday:
                    return DaysOfWeekFlag.Tuesday;
                case System.DayOfWeek.Wednesday:
                    return DaysOfWeekFlag.Wednesday;
                case System.DayOfWeek.Thursday:
                    return DaysOfWeekFlag.Thursday;
                case System.DayOfWeek.Friday:
                    return DaysOfWeekFlag.Friday;
                case System.DayOfWeek.Saturday:
                    return DaysOfWeekFlag.Saturday;
                case System.DayOfWeek.Sunday:
                    return DaysOfWeekFlag.Sunday;
                default:
                    throw new ArgumentException();
            }
        }

        #endregion
    }
}
