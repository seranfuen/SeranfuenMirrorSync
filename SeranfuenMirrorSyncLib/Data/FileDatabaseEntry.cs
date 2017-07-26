using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    [DataContract]
    public class FileDatabaseEntry
    {
        #region ' Static Functions '

        private const string BACKSLASH = "\\";

        public static string GetVirtualPath(string localPath, string rootFolder)
        {
            localPath = StripTrailingSlash(localPath);
            rootFolder = StripTrailingSlash(rootFolder);
            
            if (localPath.Equals(rootFolder, StringComparison.OrdinalIgnoreCase))
            {
                return string.Empty;
            }
            else
            {
                if (!localPath.StartsWith(rootFolder, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException(string.Format("The local path '{0}' is not located in root folder '{1}'", localPath, rootFolder));
                } else
                {
                    rootFolder = rootFolder + BACKSLASH;
                    return localPath.Substring(rootFolder.Length);
                }
            }
        }

        public static string GetLocalPath(string virtualPath, string rootFolder)
        {
            return Path.Combine(rootFolder, virtualPath);
        }

        private static string StripTrailingSlash(string path)
        {
            if (path.EndsWith(BACKSLASH))
            {
                return path.Substring(0, path.Length - 1);
            }
            else
            {
                return path;
            }
        }

        public static FileDatabaseEntry NonExistentFile
        {
            get
            {
                return new FileDatabaseEntry();
            }
        }

        #endregion

        public FileDatabaseEntry()
        {
        }

        public FileDatabaseEntry(FileInfo fileInfo)
        {
            LastModificationDate = fileInfo.LastWriteTime;
            Size = fileInfo.Length;
            LocalPath = fileInfo.FullName;
        }

        [DataMember]
        public string LocalPath
        {
            get;
            set;
        }

        [DataMember]
        public string VirtualPath
        {
            get;
            set;
        }
        
        [DataMember]
        public DateTime LastModificationDate
        {
            get;
            set;
        }

        [DataMember]
        public long Size
        {
            get;
            set;
        }

        public bool Exists
        {
            get
            {
                return !string.IsNullOrWhiteSpace(VirtualPath);
            }
        }
    }
}
