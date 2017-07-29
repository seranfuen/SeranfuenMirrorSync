using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSync.Test.ViewModels
{
    public class TestMirrorSyncViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _sourcePath;
        private string _mirrorPath;
        private string _status;
        private List<FileSyncActionStatus> _actions;

        public string SourcePath
        {
            get
            {
                return _sourcePath;
            }
            set
            {
                _sourcePath = value;
                OnPropertyChanged("SourcePath");
            }
        }

        public string MirrorPath
        {
            get
            {
                return _mirrorPath;
            }
            set
            {
                _mirrorPath = value;
                OnPropertyChanged("MirrorPath");
            }
        }

        public string Status
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

        public List<FileSyncActionStatus> FileSyncActions
        {
            get
            {
                return _actions;
            }
            set
            {
                _actions = value;
                OnPropertyChanged("FileSyncActions");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
