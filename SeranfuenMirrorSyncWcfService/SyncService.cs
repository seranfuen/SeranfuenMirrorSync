using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncLib.Data;

namespace SeranfuenMirrorSyncWcfService
{
    public class SyncService : ISyncService
    {
        public SourceMirrorSyncStatus GetCurrentSyncStatus()
        {
            // use sync scheduler or status controller to get status of current sync
            throw new NotImplementedException();
        }

        public List<SourceMirrorSyncStatus> GetHistorySyncStatus(string sourceRoot, string mirrorRoot, int count)
        {
            // use a status controller to find the history for source and mirror
            throw new NotImplementedException();
        }

        public void RunSync(string sourceRoot, string mirrorRoot, List<IFileFilter> fileFilters = null, List<IFileFilter> directoryFilters = null)
        {
            // put sync in scheduler
            throw new NotImplementedException();
        }
    }
}
