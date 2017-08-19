using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSync.ViewModels
{
    public class ManualSyncViewModel
    {

#if DEBUG

        private string _sourcePath = @"F:\Test\Software";
        private string _destinationPath = @"D:\Backup";

#else
        private string _sourcePath;
        private string _destinationPath;
#endif


        public string SourcePath
        {
            get { return _sourcePath; }
            set { _sourcePath = value; }
        }

        public string MirrorPath
        {
            get { return _destinationPath; }
            set { _destinationPath = value; }
        }

        public string FinalMirrorPath
        {
            get
            {
                if (UseSourceFolderName)
                {
                    MirrorPath = Path.Combine(MirrorPath, Path.GetFileName(SourcePath));
                }

                if (!CreateDateFolder)
                {
                    return MirrorPath;
                }
                else
                {
                    return string.Format("{0} {1:d}", MirrorPath, DateTime.Now).Replace('/', '-');
                }
            }
        }

        [DefaultValue(true)]
        public bool UseSourceFolderName
        {
            get;
            set;
        }

        public bool CreateDateFolder
        {
            get;
            set;
        }
    }
}
