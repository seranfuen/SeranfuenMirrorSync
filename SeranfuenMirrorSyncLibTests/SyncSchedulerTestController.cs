using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLibTests
{
    public class SyncSchedulerTestController
    {
        private ManualResetEvent syncFinished = new ManualResetEvent(false);
        private List<SyncProgressReportedEventArgs> _orderedEventArgs = new List<SyncProgressReportedEventArgs>();

        public List<SyncProgressReportedEventArgs> OrderedEventArgs
        {
            get
            {
                return _orderedEventArgs;
            }
        }

        /// <summary>
        /// Expected order: a-b, c-d, e-f
        /// </summary>
        public void RunThreeManualSyncsTest()
        {
            var syncInfo1 = new PendingSyncInfo("test", "a", "b");
            var syncInfo2 = new PendingSyncInfo("test", "c", "d");
            var syncInfo3 = new PendingSyncInfo("test", "e", "f");

            var syncScheduler = SyncScheduler.Instance;
            syncScheduler.ScheduleSync(syncInfo1);
            syncScheduler.ScheduleSync(syncInfo2);
            syncScheduler.ScheduleSync(syncInfo3);
            syncScheduler.SyncActionFactory = TestSyncActionFactory.Instance;
            syncScheduler.SyncProgressReported += SyncScheduler_SyncProgressReported;

            while (syncScheduler.PendingSyncsCount > 0 || syncScheduler.IsSyncing)
            {
                syncScheduler.RunNextSync();
                syncScheduler.RunNextSync(); // only run first sync in queue, this one should do nothing if we are running it!
                syncFinished.WaitOne();
            }

        }

        private void SyncScheduler_SyncProgressReported(object sender, SyncProgressReportedEventArgs e)
        {
            lock (this)
            {
                _orderedEventArgs.Add(e);
            }

            if (e.StatusType == SyncProgressReportedEventArgs.SyncProgressReportType.SyncFinished)
            {
                syncFinished.Set();
            }
        }
    }
}