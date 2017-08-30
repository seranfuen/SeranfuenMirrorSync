using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeranfuenMirrorSync.ViewModels
{
    public class ViewModelList<T> : ViewModel where T : ViewModel
    {
        #region ' Fields '

        private int _selectedIndex = -1;
        private bool _isReadOnly = false;
        private ICommand _removeCurrentCmd;
        private ICommand _addNewItemCommand;
        protected ObservableCollection<T> _listItems = new ObservableCollection<T>();

        #endregion

        #region ' Command Definitions '

        private class RemoveCurrentCommand : ICommand
        {
            #region ' Events '

            public event EventHandler CanExecuteChanged;

            #endregion

            #region ' Fields '

            private ViewModelList<T> _parent;

            #endregion

            #region ' Ctor '

            internal RemoveCurrentCommand(ViewModelList<T> parent)
            {
                _parent = parent;
                _parent.ListItems.CollectionChanged += ListItems_CollectionChanged;
                _parent.PropertyChanged += _parent_PropertyChanged;
            }

            private void _parent_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "IsReadOnly")
                {
                    OnCanExecuteChanged();
                }
            }

            private void ListItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            {
                OnCanExecuteChanged();
            }

            protected virtual void OnCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }

            #endregion

            #region ' Members '

            public bool CanExecute(object parameter)
            {
                return _parent.Count > 0 && !_parent.IsReadOnly;
            }

            public void Execute(object parameter)
            {
                _parent.ListItems.Remove(_parent.Current);
            }

            #endregion
        }

        private class AddNewItemCommand : ICommand
        {
            #region ' Events '

            public event EventHandler CanExecuteChanged;

            #endregion

            #region ' Fields '

            private ViewModelList<T> _parent;

            #endregion

            #region ' Ctor '

            internal AddNewItemCommand(ViewModelList<T> parent)
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
                if (parameter != null && !(parameter is T))
                {
                    throw new ArgumentException("Parameber to Add New Item Command must be of class " + typeof(T).Name);
                }
                var itemToAdd = parameter as T;
                _parent._listItems.Add(itemToAdd);
            }

            protected virtual void OnCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }

            #endregion
        }

        #endregion

        #region ' Ctor '

        public ViewModelList()
        {
            _removeCurrentCmd = new RemoveCurrentCommand(this);
            _addNewItemCommand = new AddNewItemCommand(this);
        }

        #endregion

        #region ' Properties '

        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged("IsReadOnly");
            }
        }

        public ObservableCollection<T> ListItems
        {
            get { return _listItems; }
            set
            {
                _listItems = value;
                OnPropertyChanged("ListItems");
            }
        }

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged("SelectedIndex", false);
                OnPropertyChanged("Current", false);
            }
        }

        public T Current
        {
            get
            {
                if (Count == 0)
                {
                    return null;
                }
                return _listItems[_selectedIndex];
            }
        }

        public int Count
        {
            get
            {
                return _listItems == null ? 0 : _listItems.Count;
            }
        }

        #endregion

        #region ' Commands '

        public virtual ICommand RemoveCurrent
        {
            get
            {
                return _removeCurrentCmd;
            }
        }

        public virtual ICommand AddNewItem
        {
            get
            {
                return _addNewItemCommand;
            }
        }

        #endregion

        #region ' Methods '

        public void SelectFirst()
        {
            SelectedIndex = _listItems != null && _listItems.Count > 0 ? 0 : -1;
        }

        public void SelectLast()
        {
            SelectedIndex = _listItems != null ? _listItems.Count - 1 : -1;
        }

        #endregion
    }
}
