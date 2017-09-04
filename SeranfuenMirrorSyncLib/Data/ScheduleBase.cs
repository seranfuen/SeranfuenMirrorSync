using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SeranfuenMirrorSyncLib.Controllers;
using System.Xml.Serialization;

namespace SeranfuenMirrorSyncLib.Data
{
    [KnownType(typeof(WeekdaySchedule))]
    [KnownType(typeof(ManualSchedule))]
    [XmlInclude(typeof(WeekdaySchedule))]
    [XmlInclude(typeof(ManualSchedule))]
    [DataContract]
    public abstract class ScheduleBase : ISchedule
    {
        public abstract bool IsScheduled { get; }

        [DataMember]
        public abstract DateTime? LastTimeRun { get; set; }

        [XmlIgnore]
        public abstract ITimeProvider TimeProvider { get; set; }

        [DataMember]
        public abstract string Name { get; set; }

        [DataMember]
        public abstract List<string> SourcePaths { get; set; }

        [DataMember]
        public abstract string DestinationPath { get; set;  }

        public abstract void SetSyncRun();
    }
}
