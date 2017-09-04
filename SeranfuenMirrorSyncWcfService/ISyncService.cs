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
        byte[] GetCurrentSyncStatus(bool filterCompletedActions);

        [OperationContract]
        List<SourceMirrorSyncStatus> GetHistorySyncStatus(string sourceRoot, string mirrorRoot, int count);

        [OperationContract]
        void CancelCurrentSync();

        /// <summary>
        /// We need the whole list, it will replace the current schedules so adding a new schedule means sending the previous schedules plus the new one
        /// </summary>
        /// <param name="schedules"></param>
        [OperationContract]
        void SetSchedules(List<ScheduleBase> schedules);

        [OperationContract]
        List<ScheduleBase> GetSchedules();
    }
}
