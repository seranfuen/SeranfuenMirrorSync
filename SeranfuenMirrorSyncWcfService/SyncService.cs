using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncLib.Data;
using SeranfuenMirrorSyncWcfService.Controllers;

namespace SeranfuenMirrorSyncWcfService
{
    public class SyncService : ISyncService
    {
        public byte[] GetCurrentSyncStatus(bool filterCompletedActions)
        {
            var status = ServiceSyncController.Instance.GetCurrentStatus();
            if (status == null)
            {
                return null;
            }
            else
            {
                if (filterCompletedActions)
                {
                    status.FilterCompletedActions();
                }
                return ObjectCompressionFactory.GetDefaultCompressor<SourceMirrorSyncStatus>().CompressObject(status);
            }
        }

        public List<SourceMirrorSyncStatus> GetHistorySyncStatus(string sourceRoot, string mirrorRoot, int count)
        {
            // use a status controller to find the history for source and mirror
            throw new NotImplementedException();
        }

        public void RunSync(string sourceRoot, string mirrorRoot, List<IFileFilter> fileFilters = null, List<IFileFilter> directoryFilters = null)
        {
            ServiceSyncController.Instance.ScheduleSync(sourceRoot, mirrorRoot);
        }
    }
}
