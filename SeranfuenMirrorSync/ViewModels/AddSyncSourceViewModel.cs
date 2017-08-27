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
            
        }

        #endregion

        #region ' Properties '

        public SyncSourcesViewModel SyncSourcesViewModel
        {
            get
            {
                return _syncSourcesViewModel;
            }
            set
            {
                _syncSourcesViewModel = value;
                _syncSourcesViewModel.ShowMessageRequested += _syncSourcesViewModel_ShowMessageRequested;
                _syncSourcesViewModel.UserConfirmationRequested += _syncSourcesViewModel_UserConfirmationRequested;
                OnPropertyChanged("SyncSourcesViewModel");
                OnPropertyChanged("AddSourceCommand");
            }
        }

        private void _syncSourcesViewModel_UserConfirmationRequested(object sender, UserConfirmationRequestedEventArgs e)
        {
            e.Cancel = !RequestUserConfirmation(e.Message, e.Title);
        }

        private void _syncSourcesViewModel_ShowMessageRequested(object sender, ShowMessageRequestedEventArgs e)
        {
            ShowUserMessage(e.Message, e.Title, e.Type);
        }

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
                if (_syncSourcesViewModel == null)
                {
                    return null;
                }
                return _syncSourcesViewModel.AddNewItem;
            }
        }

        #endregion
    }
}
