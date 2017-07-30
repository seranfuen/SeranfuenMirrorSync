using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class SyncScheduler
    {
        #region ' Singleton '

        private static SyncScheduler _instance;

        private static SyncScheduler Instance
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

        #region ' Fields '

        private Queue<PendingSyncInfo> _pendingSyncs;

        #endregion

        #region ' Properties '

        public ISyncActionControllerFactory SyncActionFactory
        {
            get;
            set;
        }

        #endregion
    }
}