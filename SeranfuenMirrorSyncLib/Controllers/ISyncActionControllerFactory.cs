using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public interface ISyncActionControllerFactory
    {
        ISyncActionController GetDefaultController(string sourcePath, string mirrorPath);
    }
}
