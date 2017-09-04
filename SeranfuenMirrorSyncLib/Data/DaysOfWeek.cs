using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    [DataContract]
    [Serializable]
    public class DaysOfWeek
    {
        #region ' Ctor '

        public DaysOfWeek()
        {

        }

        public DaysOfWeek(DaysOfWeekFlag value)
        {
            if (value == DaysOfWeekFlag.EveryDay)
            {
                Monday = Tuesday = Wednesday = Thursday = Friday = Saturday = Sunday = true;
            }
            else
            {
                Monday = (value & DaysOfWeekFlag.Monday) != 0;
                Tuesday = (value & DaysOfWeekFlag.Tuesday) != 0;
                Wednesday = (value & DaysOfWeekFlag.Wednesday) != 0;
                Thursday = (value & DaysOfWeekFlag.Thursday) != 0;
                Friday = (value & DaysOfWeekFlag.Friday) != 0;
                Saturday = (value & DaysOfWeekFlag.Saturday) != 0;
                Sunday = (value & DaysOfWeekFlag.Sunday) != 0;
            }
        }

        #endregion

        #region ' Properties '

        [DataMember]
        public bool Monday
        {
            get;
            set;
        }

        [DataMember]
        public bool Tuesday
        {
            get;
            set;
        }

        [DataMember]
        public bool Wednesday
        {
            get;
            set;
        }

        [DataMember]
        public bool Thursday
        {
            get;
            set;
        }

        [DataMember]
        public bool Friday
        {
            get;
            set;
        }

        [DataMember]
        public bool Saturday
        {
            get;
            set;
        }

        [DataMember]
        public bool Sunday
        {
            get;
            set;
        }

        public bool Everyday
        {
            get
            {
                return Monday && Tuesday && Wednesday && Thursday && Friday && Saturday && Sunday;
            }
        }

        #endregion

        #region ' Members '

        public DaysOfWeekFlag? ToFlag()
        {
            if (Everyday)
            {
                return DaysOfWeekFlag.EveryDay;
            } 

            DaysOfWeekFlag? flag = null;
            if (Monday)
            {
                flag = DaysOfWeekFlag.Monday;
            }
            if (Tuesday)
            {
                if (flag == null)
                {
                    flag = DaysOfWeekFlag.Tuesday;
                }
                flag |= DaysOfWeekFlag.Tuesday;
            }
            if (Wednesday)
            {
                if (flag == null)
                {
                    flag = DaysOfWeekFlag.Wednesday;
                }
                flag |= DaysOfWeekFlag.Wednesday;
            }
            if (Thursday)
            {
                if (flag == null)
                {
                    flag = DaysOfWeekFlag.Thursday;
                }
                flag |= DaysOfWeekFlag.Thursday;
            }
            if (Friday)
            {
                if (flag == null)
                {
                    flag = DaysOfWeekFlag.Friday;
                }
                flag |= DaysOfWeekFlag.Friday;
            }
            if (Saturday)
            {
                if (flag == null)
                {
                    flag = DaysOfWeekFlag.Saturday;
                }
                flag |= DaysOfWeekFlag.Saturday;
            }
            if (Sunday)
            {
                if (flag == null)
                {
                    flag = DaysOfWeekFlag.Sunday;
                }
                flag |= DaysOfWeekFlag.Sunday;
            }
            return flag;
        }

        #endregion
    }
}
