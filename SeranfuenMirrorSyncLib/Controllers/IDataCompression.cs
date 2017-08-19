using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public interface IDataCompression<T>
    {
        byte[] CompressObject(T obj);
        T DecompressObjecT(byte[] compressedObj);
    }
}
