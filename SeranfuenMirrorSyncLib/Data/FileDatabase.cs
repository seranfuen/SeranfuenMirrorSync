using SeranfuenMirrorSyncLib.Controllers;
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
    public class FileDatabase
    {
        private List<FileDatabaseEntry> _listDatabaseEntries;
        private Dictionary<string, FileDatabaseEntry> _vPathEntryDict;

        public FileDatabase(List<FileDatabaseEntry> listDatabaseEntries)
        {
            DatabaseEntries = listDatabaseEntries;
            _vPathEntryDict = DatabaseEntries.ToDictionary(key => key.VirtualPath, value => value);
        }

        [DataMember]
        public List<FileDatabaseEntry> DatabaseEntries
        {
            get { return _listDatabaseEntries; }
            set { _listDatabaseEntries = value; }
        }

        public int Count
        {
            get
            {
                return _listDatabaseEntries.Count;
            }
        }

        public FileDatabaseEntry GetEntryByVirtualPath(string vPath)
        {
            if (_vPathEntryDict.ContainsKey(vPath))
            {
                return _vPathEntryDict[vPath];
            }
            else
            {
                return null;
            }
        }

        public bool ContainsPath(string vPath)
        {
            return _vPathEntryDict.ContainsKey(vPath);
        }
    }
}
