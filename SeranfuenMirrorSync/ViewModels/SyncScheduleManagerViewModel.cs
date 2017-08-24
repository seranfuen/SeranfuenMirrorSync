using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeranfuenMirrorSync.ViewModels
{
    public class SyncScheduleManagerViewModel : ViewModelList<SyncScheduleViewModel>
    {
        #region ' Commands '

        private class SyncScheduleManagerCreateSyncCommand : ICommand
        {
            public event EventHandler CanExecuteChanged;

            #region ' Fields '

            private SyncScheduleManagerViewModel _parent;

            #endregion

            #region ' Ctor '

            internal SyncScheduleManagerCreateSyncCommand(SyncScheduleManagerViewModel parent)
            {
                _parent = parent;
            }

            #endregion

            #region ' Methods '

            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                var syncSchedule = new SyncScheduleViewModel();
                for (int i = 1; i < int.MaxValue; i++)
                {
                    var nextSyncName = DefaultName + i;
                    if (!HasSyncName(nextSyncName))
                    {
                        syncSchedule.SyncName = nextSyncName;
                        break;
                    }
                }
                _parent._listItems.Add(syncSchedule);
                _parent.SelectLast();
                _parent.OnPropertyChanged("ListItems");
            }

            private bool HasSyncName(string nextSyncName)
            {
                return _parent._listItems.Any(schedule => schedule.SyncName.Equals(nextSyncName, StringComparison.OrdinalIgnoreCase));
            }

            #endregion
        }

        #endregion

        #region ' Fields '

        private const string DefaultName = "SYNC #";
        private ICommand _createSyncCommand;

        #endregion

        #region ' Properties '

        public override string DisplayName
        {
            get
            {
                return "Synchronization Schedule Manager";
            }
        }

        #endregion

        #region ' Ctor '

        public SyncScheduleManagerViewModel()
        {
            _listItems = new List<SyncScheduleViewModel>();
            _createSyncCommand = new SyncScheduleManagerCreateSyncCommand(this);
        }

        #endregion

        #region ' Commands '

        public ICommand NewSyncCommand
        {
            get
            {
                return _createSyncCommand;
            }
        }

        public ICommand RemoveCurrentSyncCommand
        {
            get
            {
                return null;
            }
        }

        public ICommand PersistSyncSchedulesCommand
        {
            get
            {
                return null;
            }
        }

        #endregion
    }
}