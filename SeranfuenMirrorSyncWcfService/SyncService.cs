using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncLib.Data;
using SeranfuenMirrorSyncWcfService.Controllers;
using System.IO;

namespace SeranfuenMirrorSyncWcfService
{
    public class SyncService : ISyncService
    {
        #region ' Members '

        public void CancelCurrentSync()
        {
            ServiceSyncController.Instance.CancelCurrentSync();
        }

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
                    status.FilterNotActiveActions();
                }
                return ObjectCompressionFactory.GetDefaultCompressor<SourceMirrorSyncStatus>().CompressObject(status);
            }
        }

        public byte[] GetHistorySyncStatus(string syncName, int count)
        {
            var statuses = SyncStatusHistory.Instance.GetStatuses(syncName);
            if (statuses != null)
            {
                statuses.OrderByDescending(status => status.Start).Take(count).OrderBy(status => status.Start);
                statuses.ForEach(status => status.FilterNotActiveActions());
                return ObjectCompressionFactory.GetDefaultCompressor<List<SourceMirrorSyncStatus>>().CompressObject(statuses);
            }
            else
            {
                return null;
            }
        }

        public List<ScheduleBase> GetSchedules()
        {
            return ScheduleManager.Instance.Schedules.Cast<ScheduleBase>().ToList();
        }

        public void RunSync(string name, string sourceRoot, string mirrorRoot, List<IFileFilter> fileFilters = null, List<IFileFilter> directoryFilters = null)
        {
            ServiceSyncController.Instance.ScheduleSync(name, sourceRoot, mirrorRoot);
        }

        public void RunSyncs(string name, List<string> sourceRoots, string mirrorRoot, List<IFileFilter> fileFilters = null, List<IFileFilter> directoryFilters = null)
        {
            var query =
                from source in sourceRoots
                group source by Path.GetFileName(source).ToLower() into g
                select g;

            foreach (var group in query)
            {
                int i = 1;
                foreach (var source in group)
                {
                    string destination = null;
                    if (i == 1)
                    {
                        destination = Path.Combine(mirrorRoot, Path.GetFileName(source));
                    }
                    else
                    {
                        destination = Path.Combine(mirrorRoot, string.Format("{0} ({1})", Path.GetFileName(source), i));
                    }

                    RunSync(name, source, destination, fileFilters, directoryFilters);
                    i++;
                }
            }
        }

        public void SetSchedules(List<ScheduleBase> schedules)
        {
            ScheduleManager.Instance.SetSchedules(schedules.Cast<ISchedule>().ToList());
            ScheduleManager.Instance.PersistSchedules();
        }

        #endregion
    }
}
