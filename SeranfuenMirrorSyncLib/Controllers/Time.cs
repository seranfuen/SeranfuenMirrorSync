using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    [Serializable]
    public class Time
    {

        #region ' Ctor '
        public Time(int hour, int minute)
        {
            if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
            {
                throw new ArgumentException(string.Format("{0}:{1} has out of range values", hour, minute));
            }
            Hour = hour;
            Minute = minute;
        }

        #endregion

        #region ' Properties '

        public int Hour { get; private set; }
        public int Minute { get; private set; }

        #endregion

        #region ' Members '

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            if (obj == this) return true;
            var typedObj = (Time)obj;
            return typedObj.Hour == Hour && typedObj.Minute == Minute;
        }

        public override int GetHashCode()
        {
            return Minute * 17 + Hour * 31;
        }

        public override string ToString()
        {
            return string.Format("{0:00}:{1:00}", Hour, Minute);
        }

        #endregion
    }
}