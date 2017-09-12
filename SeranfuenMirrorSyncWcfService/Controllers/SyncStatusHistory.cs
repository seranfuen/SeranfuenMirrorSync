using SeranfuenMirrorSyncLib.Data;
using SeranfuenMirrorSyncLib.Utils.Serialization;
using SeranfuenMirrorSyncWcfService.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SeranfuenMirrorSyncLib.Data.SourceMirrorSyncStatus;

namespace SeranfuenMirrorSyncWcfService.Controllers
{
    public class SyncStatusHistory
    {
        #region ' Singleton '

        private static SyncStatusHistory _instance;

        public static SyncStatusHistory Instance
        {
            get
            {
                if (_instance == null) _instance = new SyncStatusHistory();
                return _instance;
            }
        }

        private SyncStatusHistory()
        {
            _statuses = new Dictionary<string, List<SourceMirrorSyncStatus>>();
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _filepath = Path.Combine(currentPath, PERSIST_FILE_NAME);
            LoadHistory();
        }

        #endregion

        #region ' Constants '

        private const string PERSIST_FILE_NAME = "data\\history.xml";

        #endregion

        #region ' Fields '

        private string _filepath;
        private Dictionary<string, List<SourceMirrorSyncStatus>> _statuses;

        #endregion

        #region ' Members '

        public void PutStatus(SourceMirrorSyncStatus status)
        {
            // we only serialize faulted actions which can help debugging or finding problems
            status.FileSyncActionStatuses = status.FileSyncActionStatuses.Where(action => action.CurrentStatus == FileSyncActionStatus.ActionStatus.Failed).ToList();
            var name = status.Name.ToLower();
            if (!_statuses.ContainsKey(name))
            {
                _statuses.Add(name, new List<SourceMirrorSyncStatus>());
            }
            _statuses[name].Add(status);

            if (_statuses[name].Count > Settings.Default.MaxHistoryItems)
            {
                TrimHistory(status);
            }
            PersistHistory();
        }

        public List<SourceMirrorSyncStatus> GetStatuses(string name)
        {
            var lowerCapsName = name.ToLower();
            if (_statuses.ContainsKey(lowerCapsName))
            {
                return _statuses[lowerCapsName].ToList();
            }
            else
            {
                return null;
            }
        }

        public void PersistHistory()
        {
            var dir = Path.GetDirectoryName(_filepath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var serialization = new DataContractSerialization<List<SyncStatusesKeyValuePair>>();
            serialization.Serialize(SyncStatusesKeyValuePair.FromDictionary(_statuses), _filepath);
        }

        private void TrimHistory(SourceMirrorSyncStatus status)
        {
            var name = status.Name.ToLower();
            _statuses[name] = _statuses[name].OrderByDescending(sts => sts.End).Take(Settings.Default.MaxHistoryItems).OrderBy(sts => sts.End).ToList();
        }

        private void LoadHistory()
        {
            if (File.Exists(_filepath))
            {
                var serialization = new DataContractSerialization<List<SyncStatusesKeyValuePair>>();
                _statuses = SyncStatusesKeyValuePair.ToDictionary(serialization.Deserialize(_filepath).Cast<SyncStatusesKeyValuePair>().ToList());
            }
        }

        #endregion
    }
}
