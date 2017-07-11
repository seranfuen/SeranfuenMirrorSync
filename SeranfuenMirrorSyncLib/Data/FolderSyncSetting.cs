using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    [DataContract]
    [Serializable]
    public class FolderSyncSetting
    {
        [DataMember]
        public string SourceRootFolder
        {
            get;
            set;
        }

        [DataMember]
        public string MirrorRootFolder
        {
            get;
            set;
        }

        [DataMember]
        public bool Enabled
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? SyncTime1
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? SyncTime2
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            if (this == obj) return true;
            var typedObject = (FolderSyncSetting)obj;
            return typedObject.SourceRootFolder.Equals(SourceRootFolder, StringComparison.OrdinalIgnoreCase) && 
                typedObject.MirrorRootFolder.Equals(MirrorRootFolder, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return 13 * SourceRootFolder.GetHashCode() + 17 * MirrorRootFolder.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("Source: '{0}' - Mirror: '{1}'", SourceRootFolder, MirrorRootFolder);
        }
    }
}