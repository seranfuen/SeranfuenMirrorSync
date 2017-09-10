using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncWcfService.Controllers
{
    [Serializable]
    [DataContract]
    public class SyncStatusesKeyValuePair
    {
        #region ' Static '

        public static List<SyncStatusesKeyValuePair> FromDictionary(Dictionary<string, List<SourceMirrorSyncStatus>> dict)
        {
            var query =
                from key in dict.Keys
                select new SyncStatusesKeyValuePair()
                {
                    SyncName = key,
                    Statuses = dict[key]
                };

            return query.ToList();
        }

        public static Dictionary<string, List<SourceMirrorSyncStatus>> ToDictionary(List<SyncStatusesKeyValuePair> list)
        {
            var query =
                from pair in list
                group pair by pair.SyncName into g
                select g;

            return query.ToDictionary(key => key.Key, value => value.SelectMany(val => val.Statuses).ToList());
        }

        #endregion

        #region ' Properties '

        [DataMember]
        public string SyncName
        {
            get;
            set;
        }

        [DataMember]
        public List<SourceMirrorSyncStatus> Statuses
        {
            get;
            set;
        }

        #endregion
    }
}