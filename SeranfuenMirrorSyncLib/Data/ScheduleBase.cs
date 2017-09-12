using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SeranfuenMirrorSyncLib.Controllers;
using System.Xml.Serialization;
using System.IO;

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

        public virtual List<PendingSyncInfo> GetSyncInfos()
        {
            var listInfos = new List<PendingSyncInfo>();

            var query =
                from source in SourcePaths
                group source by Path.GetFileName(source).ToLower() into g
                select g;

            foreach (var group in query)
            {
                foreach (var source in group)
                {
                    int i = 1;
                    string destination = null;
                    if (i == 1)
                    {
                        destination = Path.Combine(DestinationPath, Path.GetFileName(source));
                    }
                    else
                    {
                        destination = Path.Combine(DestinationPath, string.Format("{0} ({1})", Path.GetFileName(source), i));
                    }

                    listInfos.Add(new PendingSyncInfo(Name, source, DestinationPath));
                    i++;
                }
            }
            return SourcePaths.Select(path => new PendingSyncInfo(Name, path, DestinationPath)).ToList();
        }
    }
}
