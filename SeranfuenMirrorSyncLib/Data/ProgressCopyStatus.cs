using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    public class ProgressCopyStatus
    {

        public ProgressCopyStatus(string source, string destination)
        {
            SourcePath = source;
            DestinationPath = destination;
        }

        #region ' Properties '

        public string SourcePath
        {
            get;
            private set;
        }

        public string DestinationPath
        {
            get;
            private set;
        }

        public bool Started
        {
            get
            {
                return Start.HasValue;
            }
        }

        public bool Canceled
        {
            get;
            private set;
        }

        public bool Failed
        {
            get;
            private set;
        }

        public bool Finished
        {
            get
            {
                return End.HasValue;
            }
        }

        public Exception Exception
        {
            get;
            private set;
        }

        public DateTime? Start
        {
            get;
            private set;
        }

        public DateTime? End
        {
            get;
            private set;
        }

        public long TotalSize
        {
            get;
            private set;
        }

        public long CopiedSize
        {
            get;
            private set;
        }

        public long AverageSpeedBps
        {
            get
            {
                if (Started)
                {
                    var endTime = Finished ? End.Value : DateTime.Now;
                    var totalSeconds = Math.Max(1, (long)(endTime - Start.Value).TotalSeconds);
                    return CopiedSize / (long)totalSeconds;
                }
                else
                {
                    return 0;
                }
            }
        }

        public decimal Progress
        {
            get
            {
                if (!Started)
                {
                    return 0;
                }
                else if (TotalSize == 0)
                {
                    return 0;
                }
                else
                {
                    return CopiedSize / TotalSize;
                }
            }
        }

        #endregion

        #region ' Functions '

        public void SetStarted(long totalSize)
        {
            Start = DateTime.Now;
            TotalSize = totalSize;
        }

        public void SetCompleted()
        {
            End = DateTime.Now;
        }

        public void SetFailed(Exception exception)
        {
            SetCompleted();
            Failed = true;
            Exception = exception;
        }

        public void SetCanceled()
        {
            SetCompleted();
            Canceled = true;
        }

        public void UpdateProgress(long bytesCopied)
        {
            CopiedSize = bytesCopied;
        }

        public override string ToString()
        {
            return string.Format("'{0}' to '{1}' {2}/{3} bytes transferred", SourcePath, DestinationPath, CopiedSize, TotalSize);
        }

        #endregion
    }
}