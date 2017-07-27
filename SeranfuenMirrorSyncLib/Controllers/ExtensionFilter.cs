using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class ExtensionFilter : IFileFilter
    {
        private string _extension;

        public ExtensionFilter(string filterExtension)
        {
            _extension = filterExtension.StartsWith(".") ? filterExtension : "." + filterExtension;
        }

        public bool ShouldFilter(string fullFilePath)
        {
            if (string.IsNullOrWhiteSpace(fullFilePath))
            {
                return false;
            }
            else
            {
                return Path.GetExtension(fullFilePath).Equals(_extension, StringComparison.OrdinalIgnoreCase);
            }
        }

        public override string ToString()
        {
            return string.Format("Filtering files whose extension is {0}", _extension);
        }
    }
}
