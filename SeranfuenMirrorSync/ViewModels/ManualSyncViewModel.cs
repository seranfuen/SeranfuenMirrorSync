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
        public string SourcePath
        {
            get;
            set;
        }

        public string MirrorPath
        {
            get;
            set;
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
