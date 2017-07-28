using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    [DataContract]
    [Serializable]
    public class FileSyncAction
    {
        [DataContract]
        public enum FileActionType
        {
            [EnumMember]
            [Description("Copy file from source to mirror")]
            Copy,

            [EnumMember]
            [Description("Unchanged file")]
            Skip,

            [EnumMember]
            [Description("File not found in source, delete from mirror")]
            Delete
        }

        #region ' Properties '

        [DataMember]
        public string VirtualPath
        {
            get;
            set;
        }

        [DataMember]
        public string SourcePath
        {
            get;
            set;
        }

        [DataMember]
        public string MirrorPath
        {
            get;
            set;
        }

        [DataMember]
        public FileActionType Action
        {
            get;
            set;
        }

        [DataMember]
        public long? SourceSize
        {
            get;
            set;
        }

        [DataMember]
        public long? MirrorSize
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? SourceLastModified
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? MirrorLastModified
        {
            get;
            set;
        }

        #endregion
    }
}
