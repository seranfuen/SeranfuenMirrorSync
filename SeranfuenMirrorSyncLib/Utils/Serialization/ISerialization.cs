using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Utils.Serialization
{
    public interface ISerialization<T> where T : class
    {
        void Serialize(T obj, string path);
        T Deserialize(string path);
    }
}
