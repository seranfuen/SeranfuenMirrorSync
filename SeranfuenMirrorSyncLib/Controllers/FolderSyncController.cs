using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class FolderSyncController
    {
        #region ' Events '

        public event EventHandler<FolderSyncEventArgs> FolderSynced;

        #endregion

        #region ' Fields '

        private string _rootFolder;
        private string _targetFolder;

        #endregion

        #region ' Properties' 

        public CancellationToken CancellationToken
        {
            get;
            set;
        }

        #endregion

        #region ' Constructor '

        public FolderSyncController(string rootFolder, string targetRootFolder)
        {
            _rootFolder = rootFolder;
            _targetFolder = targetRootFolder;
        }

        #endregion

        #region ' Methods '

        public void StartSync()
        {
            try
            {
                Task.WaitAll(new List<Task>() {
                Task.Factory.StartNew(() => SyncSourceInternal(_rootFolder)),
                Task.Factory.StartNew(() => SyncDestinationInternal(_targetFolder)) }.ToArray());
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is OperationCanceledException)
                {
                    throw ex.InnerException;
                } else
                {
                    throw ex;
                }
            }

        }

        private void SyncSourceInternal(string rootFolder)
        {
            if (CancellationToken != null) CancellationToken.ThrowIfCancellationRequested();

            var target = FileDatabaseEntry.GetLocalPath(FileDatabaseEntry.GetVirtualPath(rootFolder, _rootFolder), _targetFolder);
            var action = new FolderSyncAction()
            {
                TimeStarted = DateTime.Now,
                Path = target,
                Action = FolderSyncAction.FolderAction.Skip
            };

            var failed = true; ;

            try
            {
                var dirInfo = new DirectoryInfo(target);
                if (!dirInfo.Exists)
                {
                    action.Action = FolderSyncAction.FolderAction.Create;
                    dirInfo.Create();
                }
                failed = false;
            }
            catch (Exception ex)
            {
                failed = true;
                action.MessageFailed = ex.Message;
            }
            finally
            {
                action.Failed = failed;
                action.TimeFinished = DateTime.Now;
                OnFolderSynced(action);
            }

            if (!failed)
            {
                Parallel.ForEach(Directory.GetDirectories(rootFolder), (dir) => SyncSourceInternal(dir));
            }
        }

        private void SyncDestinationInternal(string targetFolder)
        {
            if (CancellationToken != null) CancellationToken.ThrowIfCancellationRequested();

            var sourceDir = FileDatabaseEntry.GetLocalPath(FileDatabaseEntry.GetVirtualPath(targetFolder, _targetFolder), _rootFolder);
            var action = new FolderSyncAction()
            {
                TimeStarted = DateTime.Now,
                Path = sourceDir,
                Action = FolderSyncAction.FolderAction.Skip
            };

            var failed = true;

            try
            {
                if (!Directory.Exists(sourceDir))
                {
                    action.Action = FolderSyncAction.FolderAction.Delete;
                    Directory.GetDirectories(targetFolder).ToList().ForEach(dir => SyncDestinationInternal(dir));
                    Parallel.ForEach(Directory.GetFiles(targetFolder), (file) => File.Delete(file));
                    Directory.Delete(targetFolder, true);
                }
                failed = false;
            }
            catch (Exception ex)
            {
                failed = true;
                action.MessageFailed = ex.Message;
            }
            finally
            {
                action.TimeFinished = DateTime.Now;
                action.Failed = failed;
                OnFolderSynced(action);
            }

            if (!failed && action.Action == FolderSyncAction.FolderAction.Skip)
            {
                Parallel.ForEach(Directory.GetDirectories(targetFolder), (dir) => SyncDestinationInternal(dir));
            }
        }

        protected virtual void OnFolderSynced(FolderSyncAction action)
        {
            FolderSynced?.Invoke(this, new FolderSyncEventArgs(action));
        }

        #endregion
    }
}