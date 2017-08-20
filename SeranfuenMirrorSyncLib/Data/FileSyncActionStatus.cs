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
    public class FileSyncActionStatus : FileSyncAction
    {
        [DataContract]
        public enum ActionStatus
        {
            [EnumMember]
            [Description("Action is scheduled, not yet run")]
            Pending,
            [EnumMember]
            [Description("Action has started")]
            Started,

            [EnumMember]
            [Description("In the process of copying data")]
            Copying,

            [EnumMember]
            [Description("Action ran successfully")]
            Finished,

            [EnumMember]
            [Description("Action failed")]
            Failed
        }

        public FileSyncActionStatus()
        {
            CurrentStatus = ActionStatus.Pending;
        }

        public FileSyncActionStatus(FileSyncAction prototype)
        {
            Action = prototype.Action;
            MirrorLastModified = prototype.MirrorLastModified;
            MirrorPath = prototype.MirrorPath;
            MirrorSize = prototype.MirrorSize;
            SourceLastModified = prototype.SourceLastModified;
            SourcePath = prototype.SourcePath;
            SourceSize = prototype.SourceSize;
            VirtualPath = prototype.VirtualPath;
        }

        #region ' Properties '

        [DataMember]
        public DateTime Start
        {
            get;
            set;
        }

        [DataMember]
        public DateTime? End
        {
            get;
            set;
        }

        [DataMember]
        public ActionStatus CurrentStatus
        {
            get;
            set;
        }

        [DataMember]
        public long DataCopied
        {
            get;
            set;
        }

        [DataMember]
        public Exception Exception
        {
            get;
            set;
        }

        [DataMember]
        public long AverageBandwidth
        {
            get;
            internal set;
        }

        public decimal ProgressPercent
        {
            get
            {
                if (CurrentStatus != ActionStatus.Copying)
                {
                    return CurrentStatus == ActionStatus.Finished ? 100m : decimal.Zero;
                }
                else
                {
                    return SourceSize > 0 ? (Convert.ToDecimal(DataCopied) / Convert.ToDecimal(SourceSize)) * 100m : decimal.Zero;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;
            if (obj == this) return true;
            var typedObj = (FileSyncActionStatus)obj;
            if (VirtualPath == null) return typedObj.VirtualPath == null;
            return VirtualPath.Equals(typedObj.VirtualPath, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            if (VirtualPath == null)
            {
                return 0;
            } else
            {
                return VirtualPath.ToLower().GetHashCode();
            }
        }

        #endregion
    }
}