using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class SyncProgressReportedEventArgs : EventArgs
    {
        private SyncProgressReportType _statusType;
        private string _sourceRoot;
        private string _mirrorRoot;

        public SyncProgressReportedEventArgs(SyncProgressReportType statusType, string sourceRoot, string mirrorRoot)
        {
            _statusType = statusType;
            _sourceRoot = sourceRoot;
            _mirrorRoot = mirrorRoot;
        }

        public string SourceRoot
        {
            get
            {
                return _sourceRoot;
            }
        }

        public string MirrorRoot
        {
            get
            {
                return _mirrorRoot;
            }
        }

        public SyncProgressReportType StatusType
        {
            get
            {
                return _statusType;
            }
        }

        public enum SyncProgressReportType
        {
            SyncStart,
            SyncFinished
        }
    }
}