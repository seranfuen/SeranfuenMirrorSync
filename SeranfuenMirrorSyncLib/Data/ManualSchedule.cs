using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SeranfuenMirrorSyncLib.Controllers;

namespace SeranfuenMirrorSyncLib.Data
{
    [Serializable]
    [DataContract]
    public class ManualSchedule : ISchedule
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

        public bool IsScheduled
        {
            get
            {
                return false;
            }
        }

        [DataMember]
        public DateTime? LastTimeRun
        {
            get;
            set;
        }

        public ITimeProvider TimeProvider
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

        #endregion
    }
}
