using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class FolderSyncEventArgs : EventArgs
    {
        public FolderSyncEventArgs(FolderSyncAction folderSyncAction)
        {
            SyncAction = folderSyncAction;
        }

        public FolderSyncAction SyncAction
        {
            get;
            private set;
        }
    }
}
