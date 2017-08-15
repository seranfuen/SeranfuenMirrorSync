using SeranfuenLogging;
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
            //
        }

        #endregion
    }
}