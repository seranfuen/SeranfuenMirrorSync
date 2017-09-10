using SeranfuenMirrorSyncLib.Controllers;
using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncWcfClient
{
    public interface IServiceProxy
    {
        void RunSync(string name, string sourceRoot, string mirrorRoot, List<IFileFilter> fileFilters = null, List<IFileFilter> directoryFilters = null);
        void RunSyncs(string name, List<string> sourceRoots, string mirrorRoot, List<IFileFilter> fileFilters = null, List<IFileFilter> directoryFilters = null);
        SourceMirrorSyncStatus GetCurrentSyncStatus(bool filterCompletedActions);
        List<SourceMirrorSyncStatus> GetHistorySyncStatus(string syncName, int count);
        void CancelCurrentSync();
        void SetSchedules(List<ScheduleBase> schedules);
        List<ScheduleBase> GetSchedules();
    }
}
