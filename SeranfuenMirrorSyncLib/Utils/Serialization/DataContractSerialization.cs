using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SeranfuenMirrorSyncLib.Utils.Serialization
{
    public class DataContractSerialization<T> : ISerialization<T> where T : class
    {
        public T Deserialize(string path)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var reader = new XmlTextReader(path))
            {
                var obj = serializer.ReadObject(reader);
                return (T)obj;
            }
        }

        public void Serialize(T obj, string path)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var writer = new XmlTextWriter(path, Encoding.Default))
            {
                writer.Formatting = Formatting.Indented;
                serializer.WriteObject(writer, obj);
                writer.Flush();
            }
        }
    }
}
