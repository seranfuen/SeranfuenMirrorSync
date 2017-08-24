using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeranfuenMirrorSyncLib.Controllers
{
    public static class ExtensionMethods
    {
        public static string Capitalize(this string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }
            else if (str.Trim().Length == 0)
            {
                return str;
            }
            else
            {
                var sb = new StringBuilder();
                sb.Append(str[0].ToString().ToUpper());
                if (str.Length > 1)
                {
                    sb.Append(str.Substring(1));
                }
                return sb.ToString();
            }
        }
    }
}
