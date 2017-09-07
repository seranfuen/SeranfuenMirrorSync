using SeranfuenMirrorSyncLib.Data;
using SeranfuenMirrorSyncWcfService.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
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
            LoadHistory();
        }

        #endregion

        #region ' Fields '

        private Dictionary<string, List<SourceMirrorSyncStatus>> _statuses;

        #endregion

        #region ' Members '

        public void PutStatus(SourceMirrorSyncStatus status)
        {
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
            } else
            {
                return null;
            }
        }

        public void PersistHistory()
        {

        }

        private void TrimHistory(SourceMirrorSyncStatus status)
        {
            var name = status.Name.ToLower();
            _statuses[name] = _statuses[name].OrderByDescending(sts => sts.End).Take(Settings.Default.MaxHistoryItems).OrderBy(sts => sts.End).ToList();
        }

        private void LoadHistory()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
