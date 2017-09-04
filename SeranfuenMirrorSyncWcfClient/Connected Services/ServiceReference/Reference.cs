﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SeranfuenMirrorSyncWcfClient.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.ISyncService")]
    public interface ISyncService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISyncService/RunSync", ReplyAction="http://tempuri.org/ISyncService/RunSyncResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(object[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(string[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Data.SourceMirrorSyncStatus[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Data.SourceMirrorSyncStatus))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Data.FileSyncActionStatus[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Data.FileSyncActionStatus))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Data.FileSyncAction))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Data.FileSyncAction.FileActionType))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Data.FileSyncActionStatus.ActionStatus))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Data.ScheduleBase[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Data.ScheduleBase))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Data.WeekdaySchedule))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Data.DaysOfWeekFlag))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Data.ManualSchedule))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Exception))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(SeranfuenMirrorSyncLib.Controllers.Time))]
        void RunSync(string sourceRoot, string mirrorRoot, object[] fileFilters, object[] directoryFilters);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISyncService/RunSync", ReplyAction="http://tempuri.org/ISyncService/RunSyncResponse")]
        System.Threading.Tasks.Task RunSyncAsync(string sourceRoot, string mirrorRoot, object[] fileFilters, object[] directoryFilters);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISyncService/GetCurrentSyncStatus", ReplyAction="http://tempuri.org/ISyncService/GetCurrentSyncStatusResponse")]
        byte[] GetCurrentSyncStatus(bool filterCompletedActions);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISyncService/GetCurrentSyncStatus", ReplyAction="http://tempuri.org/ISyncService/GetCurrentSyncStatusResponse")]
        System.Threading.Tasks.Task<byte[]> GetCurrentSyncStatusAsync(bool filterCompletedActions);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISyncService/GetHistorySyncStatus", ReplyAction="http://tempuri.org/ISyncService/GetHistorySyncStatusResponse")]
        SeranfuenMirrorSyncLib.Data.SourceMirrorSyncStatus[] GetHistorySyncStatus(string sourceRoot, string mirrorRoot, int count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISyncService/GetHistorySyncStatus", ReplyAction="http://tempuri.org/ISyncService/GetHistorySyncStatusResponse")]
        System.Threading.Tasks.Task<SeranfuenMirrorSyncLib.Data.SourceMirrorSyncStatus[]> GetHistorySyncStatusAsync(string sourceRoot, string mirrorRoot, int count);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISyncService/CancelCurrentSync", ReplyAction="http://tempuri.org/ISyncService/CancelCurrentSyncResponse")]
        void CancelCurrentSync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISyncService/CancelCurrentSync", ReplyAction="http://tempuri.org/ISyncService/CancelCurrentSyncResponse")]
        System.Threading.Tasks.Task CancelCurrentSyncAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISyncService/SetSchedules", ReplyAction="http://tempuri.org/ISyncService/SetSchedulesResponse")]
        void SetSchedules(SeranfuenMirrorSyncLib.Data.ScheduleBase[] schedules);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISyncService/SetSchedules", ReplyAction="http://tempuri.org/ISyncService/SetSchedulesResponse")]
        System.Threading.Tasks.Task SetSchedulesAsync(SeranfuenMirrorSyncLib.Data.ScheduleBase[] schedules);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISyncService/GetSchedules", ReplyAction="http://tempuri.org/ISyncService/GetSchedulesResponse")]
        SeranfuenMirrorSyncLib.Data.ScheduleBase[] GetSchedules();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISyncService/GetSchedules", ReplyAction="http://tempuri.org/ISyncService/GetSchedulesResponse")]
        System.Threading.Tasks.Task<SeranfuenMirrorSyncLib.Data.ScheduleBase[]> GetSchedulesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISyncServiceChannel : SeranfuenMirrorSyncWcfClient.ServiceReference.ISyncService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SyncServiceClient : System.ServiceModel.ClientBase<SeranfuenMirrorSyncWcfClient.ServiceReference.ISyncService>, SeranfuenMirrorSyncWcfClient.ServiceReference.ISyncService {
        
        public SyncServiceClient() {
        }
        
        public SyncServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SyncServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SyncServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SyncServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void RunSync(string sourceRoot, string mirrorRoot, object[] fileFilters, object[] directoryFilters) {
            base.Channel.RunSync(sourceRoot, mirrorRoot, fileFilters, directoryFilters);
        }
        
        public System.Threading.Tasks.Task RunSyncAsync(string sourceRoot, string mirrorRoot, object[] fileFilters, object[] directoryFilters) {
            return base.Channel.RunSyncAsync(sourceRoot, mirrorRoot, fileFilters, directoryFilters);
        }
        
        public byte[] GetCurrentSyncStatus(bool filterCompletedActions) {
            return base.Channel.GetCurrentSyncStatus(filterCompletedActions);
        }
        
        public System.Threading.Tasks.Task<byte[]> GetCurrentSyncStatusAsync(bool filterCompletedActions) {
            return base.Channel.GetCurrentSyncStatusAsync(filterCompletedActions);
        }
        
        public SeranfuenMirrorSyncLib.Data.SourceMirrorSyncStatus[] GetHistorySyncStatus(string sourceRoot, string mirrorRoot, int count) {
            return base.Channel.GetHistorySyncStatus(sourceRoot, mirrorRoot, count);
        }
        
        public System.Threading.Tasks.Task<SeranfuenMirrorSyncLib.Data.SourceMirrorSyncStatus[]> GetHistorySyncStatusAsync(string sourceRoot, string mirrorRoot, int count) {
            return base.Channel.GetHistorySyncStatusAsync(sourceRoot, mirrorRoot, count);
        }
        
        public void CancelCurrentSync() {
            base.Channel.CancelCurrentSync();
        }
        
        public System.Threading.Tasks.Task CancelCurrentSyncAsync() {
            return base.Channel.CancelCurrentSyncAsync();
        }
        
        public void SetSchedules(SeranfuenMirrorSyncLib.Data.ScheduleBase[] schedules) {
            base.Channel.SetSchedules(schedules);
        }
        
        public System.Threading.Tasks.Task SetSchedulesAsync(SeranfuenMirrorSyncLib.Data.ScheduleBase[] schedules) {
            return base.Channel.SetSchedulesAsync(schedules);
        }
        
        public SeranfuenMirrorSyncLib.Data.ScheduleBase[] GetSchedules() {
            return base.Channel.GetSchedules();
        }
        
        public System.Threading.Tasks.Task<SeranfuenMirrorSyncLib.Data.ScheduleBase[]> GetSchedulesAsync() {
            return base.Channel.GetSchedulesAsync();
        }
    }
}
