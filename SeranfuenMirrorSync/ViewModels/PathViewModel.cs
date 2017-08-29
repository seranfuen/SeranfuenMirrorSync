using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSync.ViewModels
{
    public class PathViewModel : ViewModel
    {
        #region ' Fields '

        private string _path;

        #endregion

        #region ' Properties '

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }

        #endregion

        #region ' Members '

        public override string ToString()
        {
            return Path;
        }

        #endregion
    }
}
