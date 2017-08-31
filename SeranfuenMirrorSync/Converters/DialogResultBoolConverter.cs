using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SeranfuenMirrorSync.Converters
{
    public static class DialogResultBoolConverter
    {
        public static bool? ToBool(this MessageBoxResult result)
        {
            if (result == MessageBoxResult.OK || result == MessageBoxResult.Yes)
            {
                return true;
            }
            else if (result == MessageBoxResult.No)
            {
                return false;
            }
            else
            {
                return null;
            }
        }
    }
}
