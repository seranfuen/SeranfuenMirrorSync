using System.Collections.Generic;
using SeranfuenMirrorSyncLib.Data;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public interface ISyncActionController
    {
        List<IFileFilter> DirectoryFilters { get; set; }
        List<IFileFilter> FileFilters { get; set; }
        string MirrorRoot { get; }
        string SourceRoot { get; }
        int MaxParallelActions { get; }

        SourceMirrorSyncStatus GetStatus();
        void RunSynchronization();
    }
}