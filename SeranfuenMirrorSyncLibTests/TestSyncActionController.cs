using SeranfuenMirrorSyncLib.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeranfuenMirrorSyncLib.Data;
using System.Threading;

namespace SeranfuenMirrorSyncLibTests
{
    public class TestSyncActionController : ISyncActionController
    {
        public List<IFileFilter> DirectoryFilters
        {
            get;
            set;
        }

        public List<IFileFilter> FileFilters
        {
            get;
            set;
        }

        private int _sleepTimeoutMs = 500;

        public TestSyncActionController(string sourcePath, string mirrorPath)
        {
            SourceRoot = sourcePath;
            MirrorRoot = mirrorPath;
        }

        public string MirrorRoot
        {
            get;
            private set;
        }

        public string SourceRoot
        {
            get;
            private set;
        }

        public int MaxParallelActions => throw new NotImplementedException();

        public SourceMirrorSyncStatus GetStatus()
        {
            throw new NotImplementedException();
        }

        public void RunSynchronization()
        {
            Thread.Sleep(_sleepTimeoutMs);
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }
    }
}
