using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeranfuenMirrorSyncLib.Data;
using System.IO;
using SeranfuenMirrorSyncLib.Utils.Clone;
using System.Threading;
using SeranfuenLogging;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class SourceMirrorSynchronizationController : ISyncActionController
    {
        public class SyncCancelledException : Exception
        {

        }

        private SourceMirrorSyncStatus _status;
        private CancellationTokenSource _cancellationToken;

        public SourceMirrorSynchronizationController(string name, string sourceRoot, string mirrorRoot)
        {
            Name = name;
            SourceRoot = sourceRoot;
            MirrorRoot = mirrorRoot;
        }

        #region ' Properties '

        public string SourceRoot
        {
            get;
            private set;
        }

        public string MirrorRoot
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public List<IFileFilter> FileFilters
        {
            get;
            set;
        }

        public List<IFileFilter> DirectoryFilters
        {
            get;
            set;
        }

        public int MaxParallelActions
        {
            get;
            set;
        }

        #endregion

        #region ' Methods '

        public void RunSynchronization()
        {
            lock(this)
            {
                _cancellationToken = new CancellationTokenSource();
            }
            try
            {
                RunSyncInternal();
            }
            catch (OperationCanceledException)
            {
                SetCancelledStatus();
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                SetFaultedStatus(ex.UnrollMessage());
            }
        }

        private void SetFaultedStatus(string faultMessage)
        {
            _status.End = DateTime.Now;
            _status.CurrentStatus = SourceMirrorSyncStatus.SyncStatus.Faulted;
            _status.FaultMessage = faultMessage;
        }

        private void SetCancelledStatus()
        {
            _status.End = DateTime.Now;
            _status.CurrentStatus = SourceMirrorSyncStatus.SyncStatus.Cancelled;
        }

        private void RunSyncInternal()
        {
            ReportLoadingData();

            var directorySyncController = new FolderSyncController(SourceRoot, MirrorRoot);
            directorySyncController.CancellationToken = _cancellationToken.Token;
            directorySyncController.StartSync();

            _cancellationToken.Token.ThrowIfCancellationRequested();

            var fileComparisonController = InitializeFileComparisonController();
            var actions = fileComparisonController.CalculateSyncActions();

            _cancellationToken.Token.ThrowIfCancellationRequested();

            RunSkippedActions(actions);
            _cancellationToken.Token.ThrowIfCancellationRequested();
            RunActions(actions);
            ReportSyncFinished();
        }

        public SourceMirrorSyncStatus GetStatus()
        {
            lock (_status)
            {
                return DeepCloneFactory<SourceMirrorSyncStatus>.DefaultCloner.DeepClone(_status);
            }
        }

        private void RunActions(List<FileSyncAction> actions)
        {
            ReportRunningActions();
            var actualActions = actions.Where(action => action.Action != FileSyncAction.FileActionType.Skip);
            UpdatePendingActions(actualActions);

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = MaxParallelActions,
                CancellationToken = _cancellationToken.Token
            };

            Parallel.ForEach(actualActions, options, (action) =>
            {
                options.CancellationToken.ThrowIfCancellationRequested();
                _status.IncrementThreads();
                ReportActionStarted(action);
                PerformAction(action);
                _status.IncrementFilesCopied();
                _status.IncrementFilesProcessed();
                _status.DecrementThreads();
            });
        }

        private void ReportRunningActions()
        {
            _status.CurrentStatus = SourceMirrorSyncStatus.SyncStatus.Synchronizing;
        }

        private void ReportActionStarted(FileSyncAction action)
        {
            _status.SetActionStarted(action);
        }

        private void UpdatePendingActions(IEnumerable<FileSyncAction> actualActions)
        {
            _status.LoadPendingActions(actualActions.ToList());
        }

        private void PerformAction(FileSyncAction action)
        {
            if (action.Action == FileSyncAction.FileActionType.Copy)
            {
                _cancellationToken.Token.ThrowIfCancellationRequested();
                PerformCopyAction(action);
            }
            else if (action.Action == FileSyncAction.FileActionType.Delete)
            {
                _cancellationToken.Token.ThrowIfCancellationRequested();
                PerformDeleteAction(action);
            }
            else
            {
                throw new InvalidOperationException(string.Format("Trying to perform invalid action: {0}", action.Action));
            }
        }

        private void ReportSyncFinished()
        {
            _status.SetFinished();
        }

        private void ReportLoadingData()
        {
            _status = new SourceMirrorSyncStatus(Name, SourceRoot, MirrorRoot);
            _status.SetStarted();
        }

        private void PerformCopyAction(FileSyncAction action)
        {
            var copyController = new ProgressCopyController(action.SourcePath, action.MirrorPath);
            copyController.CancellationToken = _cancellationToken.Token;
            var progressCopyEvent = new EventHandler<ProgressCopyEventArgs>(delegate (Object o, ProgressCopyEventArgs arg)
            {
                _status.UpdateFileCopyProgress(action, arg.Status);
            });

            copyController.ProgressCopyEvent += progressCopyEvent;
            copyController.OverwriteFile = true;
            copyController.UpdateTimestamps = true;
            try
            {
                copyController.StartCopy();
                ReportActionCompleted(action);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                ReportActionFailed(action, ex);
            }
            finally
            {
                copyController.ProgressCopyEvent -= progressCopyEvent;
            }
        }

        private void ReportActionFailed(FileSyncAction action, Exception ex)
        {
            _status.SetActionFailed(action, ex);
        }

        private void ReportActionCompleted(FileSyncAction action)
        {
            _status.SetActionFinished(action);
        }

        private void PerformDeleteAction(FileSyncAction action)
        {
            try
            {
                File.Delete(action.MirrorPath);
                ReportActionCompleted(action);
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(ex);
                ReportActionFailed(action, ex);
            }
        }

        private void RunSkippedActions(List<Data.FileSyncAction> actions)
        {
            var skipActions = actions.Where(action => action.Action == Data.FileSyncAction.FileActionType.Skip);
            var skippedActionsCount = skipActions.Count();
            _status.IncrementFilesProcessed(skippedActionsCount);
            _status.IncrementFilesSkipped(skippedActionsCount);
        }

        private FileDatabaseComparisonController InitializeFileComparisonController()
        {
            var controller = new FileDatabaseComparisonController(SourceRoot, MirrorRoot)
            {
                FileFilters = FileFilters,
                DirectoryFilters = DirectoryFilters
            };
            controller.LoadDatabases();
            return controller;
        }

        public void Cancel()
        {
            _cancellationToken.Cancel();
        }

        #endregion
    }
}