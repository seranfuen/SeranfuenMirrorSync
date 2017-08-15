using SeranfuenLogging;
using SeranfuenMirrorSyncWcfService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncService
{
    public partial class MirrorSyncService : ServiceBase
    {
        #region' Fields '

        private ServiceHost _serviceHost = null;
        private MirrorSyncServiceMainThread _thread = null;

        #endregion

        #region ' Ctor '

        public MirrorSyncService()
        {
            InitializeComponent();
        }

        #endregion

        #region ' Methods '

        internal void StartTest(string[] args)
        {
            OnStart(args);
        }

        internal void StopTest()
        {
            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            Logger.Instance.LogApplicationEvent("MirrorSyncService.OnStart", "Service starting", LoggingEventType.Info);
            _thread = new MirrorSyncServiceMainThread();
            InitializeWcfService();
            _thread.StartThread();
        }

        private void InitializeWcfService()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
            }
            _serviceHost = new ServiceHost(typeof(SyncService));
            _serviceHost.Open();
        }

        protected override void OnStop()
        {
            Logger.Instance.LogApplicationEvent("MirrorSyncService.OnStop", "Service stopping", LoggingEventType.Info);
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }
            _thread.Dispose();
        }

        #endregion
    }
}
