using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public class GZipDataCompression<T> : IDataCompression<T>
    {
        public byte[] CompressObject(T obj)
        {
            using (MemoryStream msCompressed = new MemoryStream())
            using (GZipStream gZipStream = new GZipStream(msCompressed, CompressionMode.Compress))
            using (MemoryStream msDecompressed = new MemoryStream())
            {
                new BinaryFormatter().Serialize(msDecompressed, obj);
                byte[] byteArray = msDecompressed.ToArray();

                gZipStream.Write(byteArray, 0, byteArray.Length);
                gZipStream.Close();
                return msCompressed.ToArray();
            }
        }

        public T DecompressObjecT(byte[] compressedObj)
        {
            var mem2 = new MemoryStream(compressedObj);
            using (var decompressor = new GZipStream(mem2, CompressionMode.Decompress))
            {
                return (T)new BinaryFormatter().Deserialize(decompressor);
            }
        }
    }
}