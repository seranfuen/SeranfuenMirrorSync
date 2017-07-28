using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Utils.Clone
{
    public interface IDeepClone<T>
    {
        T DeepClone(T source);
    }
}
