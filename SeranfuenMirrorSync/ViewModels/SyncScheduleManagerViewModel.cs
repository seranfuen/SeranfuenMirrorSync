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
        #region ' Fields '

        private const string DefaultName = "SYNC #";

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
        }

        #endregion

        #region ' Commands '

        public ICommand NewSyncCommand
        {
            get
            {
                return null;
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

        #region ' Members '

        private void CreateNewSynchronization()
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
            _listItems.Add(syncSchedule);
        }

        private bool HasSyncName(string nextSyncName)
        {
            return _listItems.Any(schedule => schedule.SyncName.Equals(nextSyncName, StringComparison.OrdinalIgnoreCase));
        }

        #endregion
    }
}