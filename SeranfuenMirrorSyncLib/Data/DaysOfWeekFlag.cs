using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    [DataContract]
    public enum DaysOfWeekFlag
    {
        [EnumMember]
        Monday = 1,
        [EnumMember]
        Tuesday = 2,
        [EnumMember]
        Wednesday = 4,
        [EnumMember]
        Thursday = 8,
        [EnumMember]
        Friday = 16,
        [EnumMember]
        Saturday = 32,
        [EnumMember]
        Sunday = 64,
        [EnumMember]
        EveryDay = 127
    }
}
