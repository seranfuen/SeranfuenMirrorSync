using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public static class ObjectCompressionFactory
    {
        public static IDataCompression<T> GetDefaultCompressor<T>()
        {
            return new GZipDataCompression<T>();
        }
    }
}
