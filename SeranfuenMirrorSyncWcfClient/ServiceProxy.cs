﻿using System;
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
        #region ' Fields '

        private const string HttpBindingName = "BasicHttpBinding_ISyncService";

        #endregion

        #region ' Members '

        public SourceMirrorSyncStatus GetCurrentSyncStatus(bool filterCompletedActions)
        {
            using (var proxy = GetProxyInstance())
            {
                var statusArray = proxy.GetCurrentSyncStatus(filterCompletedActions);
                if (statusArray == null) return null;
                return ObjectCompressionFactory.GetDefaultCompressor<SourceMirrorSyncStatus>().DecompressObjecT(statusArray);
            }
        }

        public List<SourceMirrorSyncStatus> GetHistorySyncStatus(string syncName, int count)
        {
            using (var proxy = GetProxyInstance())
            {
                var statusArray = proxy.GetHistorySyncStatus(syncName, count);
                if (statusArray == null) return null;
                return ObjectCompressionFactory.GetDefaultCompressor<List<SourceMirrorSyncStatus>>().DecompressObjecT(statusArray);
            }
        }


        public void RunSync(string name, string sourceRoot, string mirrorRoot, List<IFileFilter> fileFilters = null, List<IFileFilter> directoryFilters = null)
        {
            using (var proxy = GetProxyInstance())
            {
                proxy.RunSync(name, sourceRoot, mirrorRoot, fileFilters?.ToArray(), directoryFilters?.ToArray());
            }
        }


        public void RunSyncs(string name, List<string> sourceRoots, string mirrorRoot, List<IFileFilter> fileFilters = null, List<IFileFilter> directoryFilters = null)
        {
            using (var proxy = GetProxyInstance())
            {
                proxy.RunSyncs(name, sourceRoots.ToArray(), mirrorRoot, fileFilters?.ToArray(), directoryFilters?.ToArray());
            }
        }

        public void CancelCurrentSync()
        {
            using (var proxy = GetProxyInstance())
            {
                proxy.CancelCurrentSync();
            }
        }

        public void SetSchedules(List<ScheduleBase> schedules)
        {
            using (var proxy = GetProxyInstance())
            {
                proxy.SetSchedules(schedules.ToArray());
            }
        }

        public List<ScheduleBase> GetSchedules()
        {
            using (var proxy = GetProxyInstance())
            {
                return proxy.GetSchedules().ToList();
            }
        }

        private static SyncServiceClient GetProxyInstance()
        {
            return new ServiceReference.SyncServiceClient(HttpBindingName);
        }

        #endregion
    }
}
