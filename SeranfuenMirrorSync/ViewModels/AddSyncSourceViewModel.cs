using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeranfuenMirrorSync.ViewModels
{
    public class AddSyncSourceViewModel : ViewModel
    {
        #region ' Fields '

        private string _sourcePath;
        private SyncSourcesViewModel _syncSourcesViewModel;

        #endregion

        #region ' Ctor '

        public AddSyncSourceViewModel()
        {
            _syncSourcesViewModel = new SyncSourcesViewModel();
        }

        public AddSyncSourceViewModel(SyncSourcesViewModel syncSourcesViewModel)
        {
            _syncSourcesViewModel = syncSourcesViewModel;
        }

        #endregion

        #region ' Properties '

        public override string DisplayName
        {
            get
            {
                return "Add Sync Source";
            }
        }

        public string SourcePath
        {
            get { return _sourcePath; }
            set
            {
                _sourcePath = value;
                OnPropertyChanged("SourcePath");
            }
        }

        public ICommand AddSourceCommand
        {
            get
            {
                return _syncSourcesViewModel.AddNewItem;
            }
        }

        #endregion
    }
}
