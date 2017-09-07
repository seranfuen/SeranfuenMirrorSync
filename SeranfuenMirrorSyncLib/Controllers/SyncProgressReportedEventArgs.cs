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
        private string _name;

        public SyncProgressReportedEventArgs(SyncProgressReportType statusType, string name, string sourceRoot, string mirrorRoot)
        {
            _name = Name;
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

        public string Name
        {
            get
            {
                return _name;
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