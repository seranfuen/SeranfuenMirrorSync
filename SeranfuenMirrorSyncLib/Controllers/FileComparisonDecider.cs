using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeranfuenMirrorSyncLib.Data;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class FileComparisonDecider
    {
        private string _sourceRoot;
        private string _mirrorRoot;

        public FileComparisonDecider(string sourceRoot, string mirrorRoot)
        {
            _sourceRoot = sourceRoot;
            _mirrorRoot = mirrorRoot;
        }

        public FileSyncAction DecideAction(FileDatabaseEntry fileSource, FileDatabaseEntry fileMirror)
        {
            var syncAction = InitializeSyncAction(fileSource, fileMirror);

            if (!fileSource.Exists)
            {
                syncAction.Action = FileSyncAction.FileActionType.Delete;
            }

            else if (!fileMirror.Exists)
            {
                syncAction.Action = FileSyncAction.FileActionType.Copy;
                syncAction.MirrorPath = FileDatabaseEntry.GetLocalPath(fileSource.VirtualPath, _mirrorRoot);
            }
            else if (fileSource.Size != fileMirror.Size || fileSource.LastModificationDate != fileMirror.LastModificationDate)
            {
                syncAction.Action = FileSyncAction.FileActionType.Copy;
            }
            else
            {
                syncAction.Action = FileSyncAction.FileActionType.Skip;
            }

            return syncAction;
        }

        private FileSyncAction InitializeSyncAction(FileDatabaseEntry fileSource, FileDatabaseEntry fileMirror)
        {
            if (!fileSource.Exists && !fileMirror.Exists)
            {
                throw new InvalidOperationException("Cannot compare two files that do not exist");
            }

            var syncAction = new FileSyncAction()
            {
                MirrorPath = fileMirror.LocalPath,
                SourcePath = fileSource.LocalPath,
                VirtualPath = GetVirtualPath(fileSource, fileMirror)
            };

            if (fileSource.Exists)
            {
                syncAction.SourceSize = fileSource.Size;
                syncAction.SourceLastModified = fileSource.LastModificationDate;
            }

            if (fileMirror.Exists)
            {
                syncAction.MirrorSize = fileMirror.Size;
                syncAction.MirrorLastModified = fileMirror.LastModificationDate;
            }
            return syncAction;
        }

        private string GetVirtualPath(FileDatabaseEntry fileSource, FileDatabaseEntry fileMirror)
        {
            if (fileSource.Exists)
            {
                return fileSource.VirtualPath;
            }
            else
            {
                return fileMirror.VirtualPath;
            }
        }
    }
}
