using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSync.ViewModels
{
    public class SyncScheduleViewModel : ViewModel
    {
        #region ' Fields '

        private string _syncName = "New Sync";
        private bool _enabled;
        private List<string> _sourceFolders;
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

        #region ' Properties '

        public override string DisplayName
        {
            get
            {
                return SyncName;
            }
        }

        [DisplayName("Synchronization Name")]
        public string SyncName
        {
            get { return _syncName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    ValidateNotNullField("SyncName", value);
                }
                _syncName = value;
                OnPropertyChanged("SyncName");
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                OnPropertyChanged("Enabled");
            }
        }

        public List<string> SourceFolders
        {
            get { return _sourceFolders; }
            set
            {
                _sourceFolders = value;
                OnPropertyChanged("SourceFolders");
            }
        }

        public string MirrorFolder
        {
            get { return _mirrorFolder; }
            set
            {
                _mirrorFolder = value;
                OnPropertyChanged("MirrorFolder");
            }
        }

        public bool Monday
        {
            get { return _monday; }
            set
            {
                _monday = value;
                OnPropertyChanged("Monday");
            }
        }

        public bool Tuesday
        {
            get { return _tuesday; }
            set
            {
                _tuesday = value;
                OnPropertyChanged("Tuesday");
            }
        }

        public bool Wednesday
        {
            get { return _wednesday; }
            set
            {
                _wednesday = value;
                OnPropertyChanged("Wednesday");
            }
        }

        public bool Thursday
        {
            get { return _thursday; }
            set
            {
                _thursday = value;
                OnPropertyChanged("Thursday");
            }
        }

        public bool Friday
        {
            get { return _friday; }
            set
            {
                _friday = value;
                OnPropertyChanged("Friday");
            }
        }

        public bool Saturday
        {
            get { return _tuesday; }
            set
            {
                _saturday = value;
                OnPropertyChanged("Saturday");
            }
        }

        public bool Sunday
        {
            get { return _sunday; }
            set
            {
                _sunday = value;
                OnPropertyChanged("Sunday");
            }
        }

        public bool Everyday
        {
            get { return _everyday; }
            set
            {
                _everyday = value;
                OnPropertyChanged("Everyday");
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

        public bool Manual
        {
            get { return _manual; }
            set
            {
                _manual = value;
                OnPropertyChanged("Manual");
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

        public DateTime Hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
                OnPropertyChanged("Hour");
            }
        }

        #endregion
    }
}