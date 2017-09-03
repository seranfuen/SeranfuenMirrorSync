using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SeranfuenMirrorSyncLib.Utils.Serialization
{
    public class XmlSerialization<T> : ISerialization<T> where T : class
    {
        public T Deserialize(string path)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            string xmlString = xmlDocument.OuterXml;

            using (StringReader read = new StringReader(xmlString))
            {
                T objOut = null;
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (XmlReader reader = new XmlTextReader(read))
                {
                    objOut = (T)serializer.Deserialize(reader);
                    reader.Close();
                }

                read.Close();
                return objOut;
            }
        }

        public void Serialize(T obj, string path)
        {
            var xmlDocument = new XmlDocument();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                serializer.Serialize(stream, obj);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(path);
                stream.Close();
            }
        }
    }
}
