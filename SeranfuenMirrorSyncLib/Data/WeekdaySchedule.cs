using SeranfuenMirrorSyncLib.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    public class WeekdaySchedule : ISchedule
    {
        private DayOfWeek _dayOfWeek;
        private Time _time;

        public WeekdaySchedule(DayOfWeek dayOfWeek, Time time)
        {
            _dayOfWeek = dayOfWeek;
            _time = time;
        }
    }
}
