﻿using SeranfuenMirrorSyncLib.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    public class PendingSyncInfo
    {
        public PendingSyncInfo(string sourceRoot, string mirrorRoot, List<IFileFilter> fileFilter = null, List<IFileFilter> directoryFilter = null)
        {
            SourceRoot = sourceRoot;
            MirrorRoot = mirrorRoot;
            FileFilter = fileFilter;
            DirectoryFilter = directoryFilter;
        }

        public string SourceRoot
        {
            get;
            private set;
        }

        public string MirrorRoot
        {
            get;
            private set;
        }

        public List<IFileFilter> FileFilter
        {
            get;
            private set;
        }

        public List<IFileFilter> DirectoryFilter
        {
            get;
            private set;
        }
    }
}
