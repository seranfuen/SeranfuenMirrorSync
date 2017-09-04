using SeranfuenMirrorSyncLib.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SeranfuenMirrorSyncLib.Data
{
    [Serializable]
    [DataContract]
    public class WeekdaySchedule : ScheduleBase
    {
        private Time _time;
        private DaysOfWeek _daysOfWeek;

        #region ' Constructor '

        public WeekdaySchedule(DaysOfWeekFlag dayOfWeek, Time time, List<string> sourcePaths, string destinatioPath)
        {
            _daysOfWeek = new DaysOfWeek(dayOfWeek);
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
        public override DateTime? LastTimeRun
        {
            get;
            set;
        }

        [XmlIgnore]
        public override ITimeProvider TimeProvider
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

        public DaysOfWeekFlag? DaysOfWeekFlag
        {
            get
            {
                return _daysOfWeek.ToFlag();
            }
            set
            {
                if (value != null)
                {
                    _daysOfWeek = new DaysOfWeek(value.Value);
                } else
                {
                    _daysOfWeek = null;
                }
            }
        }

        [DataMember]
        public DaysOfWeek DaysOfWeek
        {
            get
            {
                return _daysOfWeek;
            }
            set
            {
                _daysOfWeek = value;
            }
        }

        public override bool IsScheduled
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
        public override string Name
        {
            get;
            set;
        }

        [DataMember]
        public override List<string> SourcePaths
        {
            get;
            set;
        }

        [DataMember]
        public override string DestinationPath
        {
            get;
            set;
        }

        #endregion

        #region ' Members '

        public override void SetSyncRun()
        {
            LastTimeRun = TimeProvider.CurrentTime;
        }

        private bool IsDayScheduled(DaysOfWeekFlag dayOfWeek)
        {
            return (dayOfWeek & _daysOfWeek.ToFlag()) != 0;
        }

        #endregion
    }
}
