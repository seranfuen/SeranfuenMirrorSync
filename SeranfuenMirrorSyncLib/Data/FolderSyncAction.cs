﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    [DataContract]
    public class FolderSyncAction
    {
        [DataContract]
        public enum FolderAction
        {
            [EnumMember]
            Delete,
            [EnumMember]
            Create,
            [EnumMember]
            Skip
        }

        [DataMember]
        public FolderAction Action
        {
            get;
            set;
        }

        [DataMember]
        public string Path
        {
            get;
            set;
        }

        [DataMember]
        public DateTime TimeStarted
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? TimeFinished
        {
            get;
            set;
        }

        [DataMember]
        public bool Failed
        {
            get;
            set;
        }

        [DataMember]
        public string MessageFailed
        {
            get;
            set;
        }

    }
}
