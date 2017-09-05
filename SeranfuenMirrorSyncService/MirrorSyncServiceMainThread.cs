using SeranfuenLogging;
using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncWcfService.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncService
{
    public class MirrorSyncServiceMainThread : IDisposable
    {
        #region ' Fields '

        private const int TIMER_PERIOD_MS = 500;

        private bool _disposed = false;
        private Timer _timer;

        #endregion

        #region ' Methods '

        public void Dispose()
        {
            lock (this)
            {
                if (!_disposed)
                {
                    _disposed = true;
                    _timer.Dispose();
                }
            }
            Logger.Instance.LogApplicationEvent("MirrorSyncServiceMainThread.Dispose", "Main thread disposed", LoggingEventType.Debug);
        }

        public void StartThread()
        {
            Logger.Instance.LogApplicationEvent("StartThread", "Main thread started", LoggingEventType.Debug);
            _timer = new Timer(Timer_Callback, null, 0, Timeout.Infinite);
        }

        private void Timer_Callback(object state)
        {
            DoTimerWork();
            lock (this)
            {
                if (!_disposed)
                {
                    _timer.Change(TIMER_PERIOD_MS, Timeout.Infinite);
                }
            }
        }

        private void DoTimerWork()
        {
            try
            {
                var scheduledSyncs = ScheduleManager.Instance.GetScheduledSyncs();
                foreach (var sync in scheduledSyncs)
                {
                    var syncInfos = sync.GetSyncInfos();
                    syncInfos.ForEach(info => SyncScheduler.Instance.ScheduleSync(info));
                    sync.SetSyncRun();
                }

                if (scheduledSyncs.Any())
                {
                    ScheduleManager.Instance.PersistSchedules();
                }

                SyncScheduler.Instance.RunNextSync();
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                throw ex;
            }
        }

        #endregion
    }
}