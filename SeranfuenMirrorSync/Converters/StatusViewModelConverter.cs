using SeranfuenMirrorSync.ViewModels;
using SeranfuenMirrorSyncLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSync.Converters
{
    public static class StatusViewModelConverter
    {
        public static StatusViewModel ToViewModel(this SourceMirrorSyncStatus status)
        {
            var viewModel = new StatusViewModel()
            {
                SourceFolder = status.SourceRoot,
                MirrorFolder = status.MirrorRoot,
                Duration = status.Duration,
                Status = status.CurrentStatus,
                Start = status.Start,
                FaultMessage = status.FaultMessage,
                Guid = status.Guid
            };
            return viewModel;
        }
    }
}
