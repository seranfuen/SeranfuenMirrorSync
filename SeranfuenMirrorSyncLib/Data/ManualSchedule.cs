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
    [Serializable]
    [DataContract]
    public class ManualSchedule : ScheduleBase
    {
        #region ' Ctor '

        public ManualSchedule(List<string> sourcePaths, string destinationPath)
        {
            SourcePaths = sourcePaths;
            DestinationPath = destinationPath;
        }

        public ManualSchedule()
        {
            TimeProvider = DateTimeNowProvider.Instance;
        }

        #endregion

        #region ' Properties '

        public override bool IsScheduled
        {
            get
            {
                return false;
            }
        }

        [DataMember]
        public override DateTime? LastTimeRun
        {
            get;
            set;
        }

        [XmlIgnore]
        public override ITimeProvider TimeProvider
        {
            get
            {
                return DateTimeNowProvider.Instance;
            }
            set
            {

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

        #endregion
    }
}
