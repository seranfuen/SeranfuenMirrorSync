using SeranfuenMirrorSyncLib.Utils.Clone;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    [Serializable]
    [DataContract]
    public class SourceMirrorSyncStatus
    {
        [DataContract]
        public enum SyncStatus
        {
            [EnumMember]
            [Description("Pending to start")]
            NotStarted,

            [EnumMember]
            [Description("Calculating actions")]
            CalculatingActions,

            [EnumMember]
            [Description("Synchronizing files")]
            Synchronizing,

            [EnumMember]
            [Description("Completed")]
            Completed,

            [EnumMember]
            [Description("Faulted")]
            Faulted,

            [EnumMember]
            [Description("Cancelled")]
            Cancelled
        }

        private int _threads;
        private int _filesProcessed;
        private int _filesSkipped;
        private int _filesCopied;
        private int _filesDeletedMirror;
        private int _filesFailed;
        private DateTime _start;
        private DateTime? _end;
        private SyncStatus _syncStatus = SyncStatus.NotStarted;

        private List<FileSyncActionStatus> _actionStatuses;
        private Dictionary<string, FileSyncActionStatus> _vPathToFileSync;

        public SourceMirrorSyncStatus() { }

        public SourceMirrorSyncStatus(string sourceRoot, string mirrorRoot)
        {
            SourceRoot = sourceRoot;
            MirrorRoot = mirrorRoot;
        }

        #region ' Properties '

        [DataMember]
        public string SourceRoot
        {
            get;
            set;
        }

        [DataMember]
        public string MirrorRoot
        {
            get;
            set;
        }

        [DataMember]
        public Guid Id
        {
            get;
            set;
        }

        [DataMember]
        public int Threads
        {
            get
            {
                lock (this)
                {
                    return _threads;
                }
            }
            set
            {
                lock (this)
                {
                    _threads = value;
                }
            }
        }

        [DataMember]
        public int FilesProcessed
        {
            get
            {
                lock (this)
                {
                    return _filesProcessed;
                }
            }
            set
            {
                lock (this)
                {
                    _filesProcessed = value;
                }
            }
        }

        [DataMember]
        public int FilesSkipped
        {
            get
            {
                lock (this)
                {
                    return _filesSkipped;
                }
            }
            set
            {
                lock (this)
                {
                    _filesSkipped = value;
                }
            }
        }

        [DataMember]
        public int FilesCopied
        {
            get
            {
                lock (this)
                {
                    return _filesCopied;
                }
            }
            set
            {
                lock (this)
                {
                    _filesCopied = value;
                }
            }
        }

        [DataMember]
        public int FilesDeletedMirror
        {
            get
            {
                lock (this)
                {
                    return _filesDeletedMirror;
                }
            }
            set
            {
                lock (this)
                {
                    _filesDeletedMirror = value;
                }
            }
        }

        [DataMember]
        public int FilesFailed
        {
            get
            {
                lock (this)
                {
                    return _filesFailed;
                }
            }
            set
            {
                lock (this)
                {
                    _filesFailed = value;
                }
            }
        }

        [DataMember]
        public DateTime Start
        {
            get
            {
                lock (this)
                {
                    return _start;
                }
            }
            set
            {
                lock (this)
                {
                    _start = value;
                }
            }
        }

        [DataMember]
        public DateTime? End
        {
            get
            {
                lock (this)
                {
                    return _end;
                }
            }
            set
            {
                lock (this)
                {
                    _end = value;
                }
            }
        }

        public bool HasFinished
        {
            get
            {
                return End.HasValue;
            }
        }

        public TimeSpan Duration
        {
            get
            {
                if (End.HasValue)
                {
                    return End.Value - Start;
                }
                else
                {
                    return DateTime.Now - Start;
                }
            }
        }

        [DataMember]
        public List<FileSyncActionStatus> FileSyncActionStatuses
        {
            get
            {
                lock (this)
                {
                    if (_actionStatuses != null)
                    {
                        return _actionStatuses.ToList();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            set
            {
                lock (this)
                {
                    _actionStatuses = value ?? new List<FileSyncActionStatus>();
                    _vPathToFileSync = _actionStatuses.ToDictionary(key => key.VirtualPath, val => val);
                }
            }
        }


        public SyncStatus CurrentStatus
        {
            get
            {
                lock(this)
                {
                    return _syncStatus;
                }
            }
            set
            {
                lock (this)
                {
                    _syncStatus = value;
                }
            }
        }

        [DataMember]
        public string FaultMessage
        {
            get;
            set;
        }

        #endregion

        #region ' Methods '

        public void IncrementThreads()
        {
            lock (this)
            {
                _threads++;
            }
        }

        public void DecrementThreads()
        {
            lock (this)
            {
                _threads--;
            }
        }

        public void IncrementFilesProcessed()
        {
            lock (this)
            {
                _filesProcessed++;
            }
        }

        public void IncrementFilesProcessed(int value)
        {
            lock (this)
            {
                _filesProcessed += value;
            }
        }

        public void IncrementFilesSkipped()
        {
            lock (this)
            {
                _filesSkipped++;
            }
        }

        public void IncrementFilesSkipped(int value)
        {
            lock(this)
            {
                _filesSkipped += value;
            }
        }

        public void IncrementFilesCopied()
        {
            lock (this)
            {
                _filesCopied++;
            }
        }

        public void IncrementFilesDeletedMirror()
        {
            lock (this)
            {
                _filesDeletedMirror++;
            }
        }

        public void IncrementFilesFailed()
        {
            lock (this)
            {
                _filesFailed++;
            }
        }

        public void SetStarted()
        {
            lock (this)
            {
                _start = DateTime.Now;
                _syncStatus = SyncStatus.CalculatingActions;
            }
        }

        public void SetFinished()
        {
            lock (this)
            {
                _end = DateTime.Now;
                _syncStatus = SyncStatus.Completed;
            }
        }


        public void FilterNotActiveActions()
        {
            if (FileSyncActionStatuses != null)
            {
                FileSyncActionStatuses = FileSyncActionStatuses.Where(status => status.CurrentStatus == FileSyncActionStatus.ActionStatus.Copying).ToList();
            }
        }

        public SourceMirrorSyncStatus Clone()
        {
            lock (this)
            {
                return DeepCloneFactory<SourceMirrorSyncStatus>.DefaultCloner.DeepClone(this);
            }
        }

        public void SetActionStarted(FileSyncAction action)
        {
            lock (this)
            {
                var actionStatus = _vPathToFileSync[action.VirtualPath];
                actionStatus.CurrentStatus = FileSyncActionStatus.ActionStatus.Started;
                actionStatus.Start = DateTime.Now;
            }
        }

        public void SetActionFinished(FileSyncAction action)
        {
            lock (this)
            {
                var actionStatus = _vPathToFileSync[action.VirtualPath];
                actionStatus.CurrentStatus = FileSyncActionStatus.ActionStatus.Finished;
                actionStatus.End = DateTime.Now;
            }
        }

        public void SetActionFailed(FileSyncAction action, Exception ex)
        {
            lock (this)
            {
                var actionStatus = _vPathToFileSync[action.VirtualPath];
                actionStatus.CurrentStatus = FileSyncActionStatus.ActionStatus.Failed;
                actionStatus.Exception = ex;
                actionStatus.End = DateTime.Now;
            }
        }

        public void LoadPendingActions(List<FileSyncAction> actions)
        {
            lock (this)
            {
                _actionStatuses = actions.Select(action => new FileSyncActionStatus(action)).ToList();
                _vPathToFileSync = _actionStatuses.ToDictionary(key => key.VirtualPath, value => value);
            }
        }


        public void UpdateFileCopyProgress(FileSyncAction action, ProgressCopyStatus status)
        {
            lock (this)
            {
                var actionStatus = _vPathToFileSync[action.VirtualPath];
                actionStatus.CurrentStatus = GetSyncActionStatus(status);
                actionStatus.DataCopied = status.CopiedSize;
                actionStatus.AverageBandwidth = status.AverageSpeedBps;
            }
        }

        private FileSyncActionStatus.ActionStatus GetSyncActionStatus(ProgressCopyStatus status)
        {
            if (status.Failed)
            {
                return FileSyncActionStatus.ActionStatus.Failed;
            }
            else if (status.Finished)
            {
                return FileSyncActionStatus.ActionStatus.Finished;
            }else
            {
                return FileSyncActionStatus.ActionStatus.Copying;
            }
        }

        #endregion
    }
}
