using SeranfuenMirrorSyncLib.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    [Serializable]
    [DataContract]
    public class WeekdaySchedule : ISchedule
    {
        private DaysOfWeekFlag _dayOfWeek;
        private Time _time;

        #region ' Constructor '

        public WeekdaySchedule(DaysOfWeekFlag dayOfWeek, Time time, List<string> sourcePaths, string destinatioPath)
        {
            _dayOfWeek = dayOfWeek;
            _time = time;
            TimeProvider = DateTimeNowProvider.Instance;
            SourcePaths = sourcePaths;
            DestinationPath = destinatioPath;
        }

        public WeekdaySchedule()
        {
            TimeProvider = DateTimeNowProvider.Instance;
        }

        #endregion

        #region ' Properties '

        [DataMember]
        public DateTime? LastTimeRun
        {
            get;
            set;
        }

        public ITimeProvider TimeProvider
        {
            get;
            set;
        }

        [DataMember]
        public Time Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
            }
        }

        [DataMember]
        public DaysOfWeekFlag DaysOfWeekFlag
        {
            get
            {
                return _dayOfWeek;
            }
            set
            {
                _dayOfWeek = value;
            }
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

        [DataMember]
        public string Name
        {
            get;
            set;
        }

        [DataMember]
        public List<string> SourcePaths
        {
            get;
            set;
        }

        [DataMember]
        public string DestinationPath
        {
            get;
            set;
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
