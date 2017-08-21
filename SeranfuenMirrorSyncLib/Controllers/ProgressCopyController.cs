using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class ProgressCopyController
    {
        private string _source;
        private string _destination;
        private ProgressCopyStatus _status;

        public event EventHandler<ProgressCopyEventArgs> ProgressCopyEvent;

        public ProgressCopyController(string sourcePath, string destinationPath)
        {
            _source = sourcePath;
            _destination = destinationPath;
            _status = new ProgressCopyStatus(sourcePath, destinationPath);
        }

        #region ' Properties '

        public bool UpdateTimestamps
        {
            get;
            set;
        }
        public bool OverwriteFile
        {
            get;
            set;
        }

        public CancellationToken CancellationToken
        {
            get;
            set;
        }

        #endregion

        #region ' Functions '

        public void StartCopy()
        {
            try
            {
                var fileInfo = new FileInfo(_source);
                _status.SetStarted(fileInfo.Length);
                OnStatusUpdated();
                CopyInternal();
            }
            catch (OperationCanceledException)
            {
                try
                {
                    File.Delete(_destination);
                    
                } 
                finally
                {
                    _status.SetCanceled();
                }
            }
        }

        protected virtual void OnStatusUpdated()
        {
            ProgressCopyEvent?.Invoke(this, new ProgressCopyEventArgs(_status));
        }

        private void CopyInternal()
        {
            byte[] buffer = new byte[1024 * 1024];
            try
            {
                if (OverwriteFile)
                {
                    File.Delete(_destination);
                }

                using (FileStream source = new FileStream(_source, FileMode.Open, FileAccess.Read))
                {
                    long fileLength = source.Length;
                    using (FileStream dest = new FileStream(_destination, FileMode.CreateNew, FileAccess.Write))
                    {
                        long totalBytes = 0;
                        int currentBlockSize = 0;

                        while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            if (CancellationToken != null) CancellationToken.ThrowIfCancellationRequested();
                            dest.Write(buffer, 0, currentBlockSize);

                            totalBytes += currentBlockSize;
                            _status.UpdateProgress(totalBytes);
                            OnStatusUpdated();

                            if (_status.Canceled)
                            {
                                break;
                            }
                        }
                    }
                }

                if (UpdateTimestamps)
                {
                    var info = new FileInfo(_source);
                    File.SetLastWriteTime(_destination, info.LastWriteTime);
                    File.SetCreationTime(_destination, info.CreationTime);
                }

                _status.SetCompleted();
                OnStatusUpdated();
            }
            catch (Exception ex)
            {
                _status.SetFailed(ex);
                OnStatusUpdated();
            }
        }

        #endregion
    }
}