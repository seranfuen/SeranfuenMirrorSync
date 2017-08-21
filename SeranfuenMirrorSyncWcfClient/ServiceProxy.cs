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
        private const string HttpBindingName = "BasicHttpBinding_ISyncService";

        public SourceMirrorSyncStatus GetCurrentSyncStatus(bool filterCompletedActions)
        {
            using (var proxy = GetProxyInstance())
            {
                var statusArray = proxy.GetCurrentSyncStatus(filterCompletedActions);
                if (statusArray == null) return null;
                return ObjectCompressionFactory.GetDefaultCompressor<SourceMirrorSyncStatus>().DecompressObjecT(statusArray);
            }
        }

        public void RunSync(string sourceRoot, string mirrorRoot, List<IFileFilter> fileFilters = null, List<IFileFilter> directoryFilters = null)
        {
            using (var proxy = GetProxyInstance())
            {
                proxy.RunSync(sourceRoot, mirrorRoot, fileFilters?.ToArray(), directoryFilters?.ToArray());
            }
        }

        public void CancelCurrentSync()
        {
            using (var proxy = GetProxyInstance())
            {
                proxy.CancelCurrentSync();
            }
        }

        private static SyncServiceClient GetProxyInstance()
        {
            return new ServiceReference.SyncServiceClient(HttpBindingName);
        }
    }
}
