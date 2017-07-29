using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSync.Test.ViewModels
{
    public class TestMirrorSyncViewModel
    {
        public string SourcePath
        {
            get;
            set;
        }

        public string MirrorPath
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public List<FileSyncActionStatus> FileSyncActions
        {
            get;
            set;
        }
    }
}
