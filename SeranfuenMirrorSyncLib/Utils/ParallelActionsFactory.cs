using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Utils
{
    public static class ParallelOptionsFactory
    {
        public static ParallelOptions GetMaxParlallelOptions(int maxActions)
        {
            return new ParallelOptions()
            {
                MaxDegreeOfParallelism = maxActions
            };
        }
    }
}
