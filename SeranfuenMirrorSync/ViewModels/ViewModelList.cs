using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSync.ViewModels
{
    public class ViewModelList<T> : ViewModel where T : class
    {
        #region ' Fields '

        private int _selectedIndex = -1;
        protected ObservableCollection<T> _listItems = new ObservableCollection<T>();

        #endregion

        #region ' Properties '

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
                OnPropertyChanged("SelectedIndex");
                OnPropertyChanged("Current");
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
