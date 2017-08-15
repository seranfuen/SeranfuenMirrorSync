using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SeranfuenMirrorSyncLib.Controllers.SyncProgressReportedEventArgs;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class SyncScheduler
    {
        #region ' Singleton '

        private static SyncScheduler _instance;

        public static SyncScheduler Instance
        {
            get
            {
                if (_instance == null) _instance = new SyncScheduler();
                return _instance;
            }
        }

        private SyncScheduler()
        {
            SyncActionFactory = SyncActionControllerFactory.Instance;
            _pendingSyncs = new Queue<PendingSyncInfo>();
        }

        #endregion

        public event EventHandler<SyncProgressReportedEventArgs> SyncProgressReported;

        #region ' Fields '

        private Queue<PendingSyncInfo> _pendingSyncs;
        private bool _isRunningSync;
        private ISyncActionController _currentSync;

        #endregion

        #region ' Properties '

        public ISyncActionControllerFactory SyncActionFactory
        {
            get;
            set;
        }

        public int PendingSyncsCount
        {
            get
            {
                lock (this)
                {
                    return _pendingSyncs.Count;
                }
            }
        }

        public bool IsSyncing
        {
            get
            {
                lock (this)
                {
                    return _isRunningSync;
                }
            }
        }

        #endregion

        #region ' Methods '

        public void ScheduleSync(PendingSyncInfo syncInfo)
        {
            lock (this)
            {
                _pendingSyncs.Enqueue(syncInfo);
            }
        }

        public void RunNextSync()
        {
            PendingSyncInfo nextSync = null;

            lock (this)
            {
                if (!_isRunningSync && PendingSyncsCount > 0)
                {
                    nextSync = _pendingSyncs.Dequeue();
                    _isRunningSync = true;
                }
            }

            if (nextSync != null)
            {
                Task.Factory.StartNew(() => RunSyncInternal(nextSync));
            }
        }

        public SourceMirrorSyncStatus GetCurrentStatus()
        {
            if (_currentSync == null) return null;
            return _currentSync.GetStatus();
        }

        protected virtual void OnSyncProgressReported(SyncProgressReportType statusType, PendingSyncInfo nextSync)
        {
            SyncProgressReported?.Invoke(this, new SyncProgressReportedEventArgs(statusType, nextSync.SourceRoot, nextSync.MirrorRoot));
        }

        private void RunSyncInternal(PendingSyncInfo nextSync)
        {
            _currentSync = SyncActionFactory.GetDefaultController(nextSync.SourceRoot, nextSync.MirrorRoot);
            _currentSync.FileFilters = nextSync.FileFilter;
            _currentSync.DirectoryFilters = nextSync.DirectoryFilter;
            OnSyncProgressReported(SyncProgressReportType.SyncStart, nextSync);
            _currentSync.RunSynchronization();

            lock (this)
            {
                _isRunningSync = false;
            }

            OnSyncProgressReported(SyncProgressReportType.SyncFinished, nextSync);
        }

        #endregion
    }
}