using SeranfuenMirrorSyncLib.Data;
using SeranfuenMirrorSyncLib.Utils.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncWcfService.Controllers
{
    public class ScheduleManager
    {
        #region ' Singleton '

        private static ScheduleManager _manager;
        
        public static ScheduleManager Instance
        {
            get
            {
                if (_manager == null) _manager = new ScheduleManager();
                return _manager;
            }
        }

        private ScheduleManager()
        {
            _schedules = new List<ISchedule>();
            var currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _filepath = Path.Combine(currentPath, PERSIST_FILE_NAME);
            LoadSchedules();
        }

        #endregion

        #region ' Constants '

        private const string PERSIST_FILE_NAME = "data\\schedules.xml";

        #endregion

        #region ' Fields '

        private List<ISchedule> _schedules;
        private string _filepath;

        #endregion

        #region ' Properties '

        public List<ISchedule> Schedules
        {
            get
            {
                return _schedules.ToList();
            }
        }

        public void SetSchedules(List<ISchedule> schedules)
        {
            _schedules = schedules;
        }

        public void PersistSchedules()
        {
            var dir = Path.GetDirectoryName(_filepath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var serialization = new XmlSerialization<List<ScheduleBase>>();
            serialization.Serialize(_schedules.Cast<ScheduleBase>().ToList(), _filepath);
        }

        public void LoadSchedules()
        {
            if (File.Exists(_filepath))
            {
                var serialization = new XmlSerialization<List<ScheduleBase>>();
                _schedules = serialization.Deserialize(_filepath).Cast<ISchedule>().ToList();
            }
        }
        #endregion
    }
}
