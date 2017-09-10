using System;
using SeranfuenMirrorSyncLib.Data;

namespace SeranfuenMirrorSync.ViewModels
{
    public class StatusViewModel : ViewModel
    {
        #region ' Fields '

        private string _sourceFolder;
        private string _mirrorFolder;
        private TimeSpan _duration;
        private DateTime _start;
        private SourceMirrorSyncStatus.SyncStatus _status;
        private Guid _guid;

        #endregion

        #region ' Properties '

        public string SourceFolder
        {
            get
            {
                return _sourceFolder;
            }
            set
            {
                _sourceFolder = value;
                OnPropertyChanged("SourceFolder");
            }
        }

        public string MirrorFolder
        {
            get
            {
                return _mirrorFolder;
            }
            set
            {
                _mirrorFolder = value;
                OnPropertyChanged("MirrorFolder");
            }
        }

        public System.Windows.Media.Brush StatusBrush
        {
            get
            {
                if (Status == SourceMirrorSyncStatus.SyncStatus.Faulted)
                {
                    return System.Windows.Media.Brushes.Red;
                }
                else
                {
                    return System.Windows.Media.Brushes.Black;
                }
            }
        }

        public string FaultMessage
        {
            get;
            set;
        }

        public TimeSpan Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                OnPropertyChanged("Duration");
            }
        }

        public SourceMirrorSyncStatus.SyncStatus Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public DateTime Start
        {
            get
            {
                return _start;
            }
            set
            {
                _start = value;
                OnPropertyChanged("Start");
            }
        }

        public Guid Guid
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
                OnPropertyChanged("Guid");
            }
        }

        #endregion
    }
}