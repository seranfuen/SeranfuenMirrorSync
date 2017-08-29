using SeranfuenMirrorSync.StringResources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeranfuenMirrorSync.ViewModels
{
    public class SyncSourcesViewModel : ViewModelList<PathViewModel>
    {
        #region ' Command Definitions '

        private class AddSyncSourceCommand : ICommand
        {
            #region ' Events '

            public event EventHandler CanExecuteChanged;

            #endregion

            #region ' Fields '

            private SyncSourcesViewModel _parent;

            #endregion

            #region ' Ctor '

            internal AddSyncSourceCommand(SyncSourcesViewModel parent)
            {
                _parent = parent;
                _parent.PropertyChanged += _parent_PropertyChanged;
            }

            private void _parent_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "IsReadOnly")
                {
                    OnCanExecuteChanged();
                }
            }

            #endregion

            #region ' Members '

            public bool CanExecute(object parameter)
            {
                return !_parent.IsReadOnly;
            }

            public void Execute(object parameter)
            {
                var path = parameter as string;
                if (parameter == null || string.IsNullOrWhiteSpace(path))
                {
                    _parent.ShowUserMessage(AppStrings.SyncSourcesViewModel_EmptyPath_Message, AppStrings.SyncSourcesViewModel_EmptyPath_Title, ShowMessageRequestedEventArgs.MessageType.Warning);
                }
                else if (_parent.ListItems.Any(pth => path.Equals(pth.Path, StringComparison.OrdinalIgnoreCase)))
                {
                    _parent.ShowUserMessage(string.Format(AppStrings.SyncSourcesViewModel_AlreadyExistingPath_Message, path), (AppStrings.SyncSourcesViewModel_AlreadyExistingPath_Title), ShowMessageRequestedEventArgs.MessageType.Warning);
                }
                else
                {
                    var shouldAdd = true;
                    if (!Directory.Exists(path))
                    {
                        shouldAdd = _parent.RequestUserConfirmation(string.Format(AppStrings.SyncSourcesViewModel_PathNotExists_Message, path), AppStrings.SyncSourcesViewModel_PathNotExists_Title);
                    }
                    if (shouldAdd)
                    {
                        _parent._listItems.Add(new PathViewModel()
                        {
                            Path = path
                        });
                        _parent.OnRequestedClose();
                    }
                }
            }

            protected virtual void OnCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }

            #endregion
        }

        #endregion

        #region ' Fields '

        private ICommand _addSyncSourceCommand;

        #endregion

        #region ' Ctor '

        public SyncSourcesViewModel()
        {
            _addSyncSourceCommand = new AddSyncSourceCommand(this);
        }

        #endregion

        #region ' Properties '

        public override ICommand AddNewItem
        {
            get
            {
                return _addSyncSourceCommand;
            }
        }

        #endregion
    }
}