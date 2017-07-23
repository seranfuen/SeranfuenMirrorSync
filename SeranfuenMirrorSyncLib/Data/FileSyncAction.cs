using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    public class FileSyncAction
    {
        public enum FileActionType
        {
            Copy,
            Update,
            NotChanged,
            Delete
        }

        public string VirtualPath
        {
            get;
            set;
        }

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

        public FileActionType ActionType
        {
            get;
            set;
        }

        public long? SourceSize
        {
            get;
            set;
        }

        public long? MirrorSize
        {
            get;
            set;
        }

        public DateTime? SourceLastModified
        {
            get;
            set;
        }

        public DateTime? MirrorLastModified
        {
            get;
            set;
        }

        public string SourceHash
        {
            get;
            set;
        }

        public string MirrorHash
        {
            get;
            set;
        }
    }
}
