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

        #endregion

        public ISyncActionController GetDefaultController(string sourcePath, string mirrorPath)
        {
            return new SourceMirrorSynchronizationController(sourcePath, mirrorPath);
        }
    }
}
