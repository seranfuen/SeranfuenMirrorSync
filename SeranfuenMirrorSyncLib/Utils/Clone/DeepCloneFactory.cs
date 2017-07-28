using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Utils.Clone
{
    public static class DeepCloneFactory<T>
    {
        public static IDeepClone<T> DefaultCloner
        {
            get
            {
                return new BinarySerializerDeepClone<T>();
            }
        }
    }
}
