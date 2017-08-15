using SeranfuenMirrorSyncLib.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLibTests
{
    public class TestSyncActionFactory : ISyncActionControllerFactory
    {
        #region ' Singleton '

        private static TestSyncActionFactory _instance;

        public static TestSyncActionFactory Instance
        {
            get
            {
                if (_instance == null) _instance = new TestSyncActionFactory();
                return _instance;
            }
        }

        private TestSyncActionFactory()
        {

        }

        #endregion

        public ISyncActionController GetDefaultController(string sourcePath, string mirrorPath)
        {
            return new TestSyncActionController(sourcePath, mirrorPath);
        }
    }
}
