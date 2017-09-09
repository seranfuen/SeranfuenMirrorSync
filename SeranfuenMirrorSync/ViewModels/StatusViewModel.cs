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
        private SourceMirrorSyncStatus.SyncStatus _status;

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

        #endregion
    }
}