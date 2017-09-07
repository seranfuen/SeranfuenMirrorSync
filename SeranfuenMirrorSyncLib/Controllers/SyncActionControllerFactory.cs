using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class SyncActionControllerFactory : ISyncActionControllerFactory
    {
        #region ' Singleton '

        private static SyncActionControllerFactory _instance;
        private static int _defaultMaxActions = 4;

        public static SyncActionControllerFactory Instance
        {
            get
            {
                if (_instance == null) _instance = new SyncActionControllerFactory();
                return _instance;
            }
        }

        private SyncActionControllerFactory()
        {

        }

        public static int MaxParallelActions
        {
            get
            {
                return _defaultMaxActions;
            }
            set
            {
                _defaultMaxActions = value;
            }
        }

        #endregion

        public ISyncActionController GetDefaultController(string name, string sourcePath, string mirrorPath)
        {
            var controller = new SourceMirrorSynchronizationController(name, sourcePath, mirrorPath);
            controller.MaxParallelActions = _defaultMaxActions;
            return controller;
        }
    }
}
