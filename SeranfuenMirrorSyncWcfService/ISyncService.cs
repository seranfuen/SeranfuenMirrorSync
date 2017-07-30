using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SeranfuenMirrorSyncWcfService
{
    [ServiceContract]
    public interface ISyncService
    {
        [OperationContract]
        void RunSync(string sourceRoot, string mirrorRoot, List<IFileFilter> fileFilters = null, List<IFileFilter> directoryFilters = null);

        [OperationContract]
        SourceMirrorSyncStatus GetCurrentSyncStatus();

        [OperationContract]
        List<SourceMirrorSyncStatus> GetHistorySyncStatus(string sourceRoot, string mirrorRoot, int count);

        // TODO: scheduling operations (set new schedule, change schedule, retrieve schedules)
    }
}
