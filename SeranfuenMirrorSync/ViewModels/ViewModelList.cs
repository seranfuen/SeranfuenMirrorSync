using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSync.ViewModels
{
    public class ViewModelList<T> : ViewModel
    {
        #region ' Fields '

        private int _selectedIndex = -1;
        protected List<T> _listItems;

        #endregion

        #region ' Properties '

        public List<T> ListItems
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
