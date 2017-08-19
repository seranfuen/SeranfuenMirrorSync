using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncLib.Data;
using SeranfuenMirrorSyncWcfClient.ServiceReference;

namespace SeranfuenMirrorSyncWcfClient
{
    public class ServiceProxy : IServiceProxy
    {
        public SourceMirrorSyncStatus GetCurrentSyncStatus(bool filterCompletedActions)
        {
            using (var proxy = new ServiceReference.SyncServiceClient("BasicHttpBinding_ISyncService"))
            {
                var statusArray = proxy.GetCurrentSyncStatus(filterCompletedActions);
                if (statusArray == null) return null;
                return ObjectCompressionFactory.GetDefaultCompressor<SourceMirrorSyncStatus>().DecompressObjecT(statusArray);
            }
        }

        public void RunSync(string sourceRoot, string mirrorRoot, List<IFileFilter> fileFilters = null, List<IFileFilter> directoryFilters = null)
        {
            using (var proxy = new ServiceReference.SyncServiceClient("BasicHttpBinding_ISyncService"))
            {
                proxy.RunSync(sourceRoot, mirrorRoot, fileFilters?.ToArray(), directoryFilters?.ToArray());
            }
        }
    }
}
