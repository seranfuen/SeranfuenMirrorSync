using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class FolderSyncController
    {
        public event EventHandler<FolderSyncEventArgs> FolderSynced;

        private string _rootFolder;
        private string _targetFolder;

        public FolderSyncController(string rootFolder, string targetRootFolder)
        {
            _rootFolder = rootFolder;
            _targetFolder = targetRootFolder;
        }

        public void StartSync()
        {
            Task.WaitAll(new List<Task>() {
                Task.Factory.StartNew(() => SyncSourceInternal(_rootFolder)),
                Task.Factory.StartNew(() => SyncDestinationInternal(_targetFolder))
            }.ToArray());
            
        }

        private void SyncSourceInternal(string rootFolder)
        {
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
            var sourceDir = FileDatabaseEntry.GetLocalPath(FileDatabaseEntry.GetVirtualPath(targetFolder, _targetFolder), _rootFolder);
            var action = new FolderSyncAction()
            {
                TimeStarted = DateTime.Now,
                Path = sourceDir,
                Action = FolderSyncAction.FolderAction.Skip
            };

            var failed = true; ;

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
        }

        protected virtual void OnFolderSynced(FolderSyncAction action)
        {
            FolderSynced?.Invoke(this, new FolderSyncEventArgs(action));
        }
    }
}