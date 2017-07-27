using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSync.Controllers
{
    public static class ByteStringConverter
    {
        public static string ToSizeString(this long bytes)
        {
            string[] suf = { "b", "kb", "mb", "gb", "tb" };
            if (bytes == 0)
            {
                return "0" + suf[0];
            }
            else
            {
                int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
                double num = Math.Round(bytes / Math.Pow(1024, place), 1);
                return num + " " + suf[place];
            }
        }
    }
}
