using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeranfuenMirrorSync.ViewModels
{
    public class SyncSourcesViewModel : ViewModelList<string>
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
                if (parameter != null && !(parameter is string))
                {
                    throw new ArgumentException("Parameber to Add New Item Command must be a string");
                }
                var itemToAdd = parameter as string;
                _parent._listItems.Add(itemToAdd);
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