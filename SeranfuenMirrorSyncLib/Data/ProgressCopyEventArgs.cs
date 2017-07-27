using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    public class ProgressCopyEventArgs : EventArgs
    {
        public ProgressCopyEventArgs(ProgressCopyStatus status)
        {
            Status = status;
        }

        public ProgressCopyStatus Status
        {
            get;
            private set;
        }
    }
}
