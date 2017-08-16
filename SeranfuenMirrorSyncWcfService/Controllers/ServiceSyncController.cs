﻿using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncLib.Data;
using SeranfuenMirrorSyncWcfService.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncWcfService.Controllers
{
    public class ServiceSyncController : IDisposable
    {
        #region ' Singleton '

        private static ServiceSyncController _instance;

        public static ServiceSyncController Instance
        {
            get
            {
                if (_instance == null) _instance = new ServiceSyncController();
                return _instance;
            }
        }

        private ServiceSyncController()
        {
            _scheduler = SyncScheduler.Instance;
            _timer = new Timer(Timer_Callback, null, Settings.Default.ProcessQueueEveryMs, Timeout.Infinite);
        }

        private void Timer_Callback(object state)
        {
            DoTimerWork();
            lock (this)
            {
                if (!_disposed)
                {
                    _timer.Change(Settings.Default.ProcessQueueEveryMs, Timeout.Infinite);
                }
            }
        }

        #endregion

        #region ' Fields '

        private SyncScheduler _scheduler;
        private Timer _timer;
        private bool _disposed = false;

        #endregion

        #region ' Methods '

        public void ScheduleSync(string sourcePath, string mirrorPath)
        {
            _scheduler.ScheduleSync(new SeranfuenMirrorSyncLib.Data.PendingSyncInfo(sourcePath, mirrorPath));
        }

        public SourceMirrorSyncStatus GetCurrentStatus()
        {
            return _scheduler.GetCurrentStatus();
        }

        public void Dispose()
        {
            lock (this)
            {
                _disposed = true;
                _timer.Dispose();
            }
        }

        private void DoTimerWork()
        {
            _scheduler.RunNextSync();
            lock (this)
            {
                if (!_disposed)
                {
                    _timer.Change(Settings.Default.ProcessQueueEveryMs, Timeout.Infinite);
                }
            }
        }

        #endregion
    }
}