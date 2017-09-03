using SeranfuenMirrorSync.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeranfuenMirrorSync.ViewModels
{
    public class SyncScheduleViewModel : ViewModel
    {
        #region ' Commands '

        private class DeleteCommand : ICommand
        {
            private SyncScheduleViewModel _viewModel;

            public event EventHandler CanExecuteChanged;

            public DeleteCommand(SyncScheduleViewModel viewModel)
            {
                _viewModel = viewModel;
            }

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                _viewModel.OnDeleteRequested();
            }

            public virtual void OnCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        #region ' Events '

        public event EventHandler DeleteRequested;

        #endregion

        #region ' Fields '

        private string _syncName = "New Sync";
        private bool _enabled = true;
        private SyncSourcesViewModel _syncSourcesViewModel;
        private ICommand _deleteCommand;
        private string _mirrorFolder;

        private bool _monday;
        private bool _tuesday;
        private bool _wednesday;
        private bool _thursday;
        private bool _friday;
        private bool _saturday;
        private bool _sunday;
        private bool _manual;
        private bool _everyday;
        private DateTime _hour;

        #endregion

        #region ' Ctor '

        public SyncScheduleViewModel()
        {
            _syncSourcesViewModel = new SyncSourcesViewModel();
            _deleteCommand = new DeleteCommand(this);
        }

        #endregion

        #region ' Properties '

        public override string DisplayName
        {
            get
            {
                return SyncName;
            }
        }

        public Image SyncImage
        {
            get
            {
                return Resources.sync_128;
            }
        }

        public ICommand RequestDeleteCommand
        {
            get
            {
                return _deleteCommand;
            }
            private set
            {
                _deleteCommand = value;
                OnPropertyChanged("DeleteCommand", false);
            }
        }

        [DisplayName("Synchronization Name")]
        public string SyncName
        {
            get { return _syncName; }
            set
            {
                if (_syncName != value)
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        ValidateNotNullField("SyncName", value);
                    }
                    _syncName = value;
                    OnPropertyChanged("SyncName");
                    OnPropertyChanged("DisplayName");
                }
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;
                    OnPropertyChanged("Enabled");
                }
            }
        }

        public SyncSourcesViewModel SyncSourcesViewModel
        {
            get { return _syncSourcesViewModel; }
            set
            {
                if (_syncSourcesViewModel != value)
                {
                    _syncSourcesViewModel = value;
                    OnPropertyChanged("SyncSourcesViewModel");
                }
            }
        }

        public string MirrorFolder
        {
            get { return _mirrorFolder; }
            set
            {
                if (_mirrorFolder != value)
                {
                    _mirrorFolder = value;
                    OnPropertyChanged("MirrorFolder");
                }
            }
        }

        public bool Monday
        {
            get { return _monday; }
            set
            {
                if (_monday != value)
                {
                    _monday = value;
                    OnPropertyChanged("Monday");
                }
            }
        }

        public bool Tuesday
        {
            get { return _tuesday; }
            set
            {
                if (_tuesday != value)
                {
                    _tuesday = value;
                    OnPropertyChanged("Tuesday");
                }
            }
        }

        public bool Wednesday
        {
            get { return _wednesday; }
            set
            {
                if (_wednesday != value)
                {
                    _wednesday = value;
                    OnPropertyChanged("Wednesday");
                }
            }
        }

        public bool Thursday
        {
            get { return _thursday; }
            set
            {
                if (_thursday != value)
                {
                    _thursday = value;
                    OnPropertyChanged("Thursday");
                }
            }
        }

        public bool Friday
        {
            get { return _friday; }
            set
            {
                if (_friday != value)
                {
                    _friday = value;
                    OnPropertyChanged("Friday");
                }
            }
        }

        public bool Saturday
        {
            get { return _saturday; }
            set
            {
                if (_saturday != value)
                {
                    _saturday = value;
                    OnPropertyChanged("Saturday");
                }
            }
        }

        public bool Sunday
        {
            get { return _sunday; }
            set
            {
                if (_sunday != value)
                {
                    _sunday = value;
                    OnPropertyChanged("Sunday");
                }
            }
        }

        public bool Everyday
        {
            get { return _everyday; }
            set
            {
                if (_everyday != value)
                {
                    _everyday = value;
                    OnPropertyChanged("Everyday");
                    OnPropertyChanged("WeekdaysEnabled");
                    if (_everyday)
                    {
                        Monday = true;
                        Tuesday = true;
                        Wednesday = true;
                        Thursday = true;
                        Friday = true;
                        Saturday = true;
                        Sunday = true;
                    }
                }
            }
        }

        public bool Manual
        {
            get { return _manual; }
            set
            {
                if (_monday != value)
                {
                    _manual = value;
                    OnPropertyChanged("Manual");
                    OnPropertyChanged("WeekdaysEnabled");
                    OnPropertyChanged("HourEnabled");
                    OnPropertyChanged("EverydayEnabled");
                }
            }
        }

        public bool WeekdaysEnabled
        {
            get
            {
                return !_manual &&
                       !_everyday;
            }
        }

        public bool HourEnabled
        {
            get
            {
                return !_manual;
            }
        }

        public bool EverydayEnabled
        {
            get
            {
                return !_manual;
            }
        }

        public DateTime Hour
        {
            get { return _hour; }
            set
            {
                if (_hour != value)
                {
                    _hour = value;
                    OnPropertyChanged("Hour");
                }
            }
        }

        #endregion

        #region ' Members '

        protected virtual void OnDeleteRequested()
        {
            DeleteRequested?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}