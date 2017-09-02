using SeranfuenMirrorSyncLib.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Data
{
    public interface ISchedule
    {
        bool IsScheduled { get; }
        DateTime? LastTimeRun { get; }
        ITimeProvider TimeProvider { get; set; }
        void SetSyncRun();
        string Name { get; }
        List<string> SourcePaths { get; }
        string DestinationPath { get; }
    }
}
