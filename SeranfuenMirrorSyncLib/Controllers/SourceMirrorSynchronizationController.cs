using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeranfuenMirrorSyncLib.Data;
using System.IO;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class SourceMirrorSynchronizationController
    {
        private Guid _currentGuid;
        private SourceMirrorSyncStatus _status;

        public SourceMirrorSynchronizationController(string sourceRoot, string mirrorRoot)
        {
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

        #endregion

        #region ' Methods '

        public void RunSynchronization()
        {
            _currentGuid = Guid.NewGuid();
            ReportLoadingData();
            var fileComparisonController = InitializeFileComparisonController();
            var actions = fileComparisonController.CalculateSyncActions();
            RunSkippedActions(actions);
            RunActions(actions);
            ReportSyncFinished();
        }

        private void RunActions(List<FileSyncAction> actions)
        {
            var actualActions = actions.Where(action => action.Action != FileSyncAction.FileActionType.Skip);
            UpdatePendingActions(actualActions);

            Parallel.ForEach(actualActions, (action) =>
            {
                _status.IncrementThreads();
                ReportActionStarted(action);
                PerformAction(action);
                _status.DecrementThreads();
            });
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
                PerformCopyAction(action);
            }
            else if (action.Action == FileSyncAction.FileActionType.Delete)
            {
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
            _status = new SourceMirrorSyncStatus(SourceRoot, MirrorRoot);
            _status.SetStarted();
        }

        private void PerformCopyAction(FileSyncAction action)
        {
            var copyController = new ProgressCopyController(action.SourcePath, action.MirrorPath);
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

        #endregion
    }
}