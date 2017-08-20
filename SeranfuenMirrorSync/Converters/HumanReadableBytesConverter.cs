using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SeranfuenMirrorSync.Converters
{
    public class HumanReadableBytesConverter : IValueConverter
    {
        private static readonly string[] SizeSuffixes ={ "bytes", "KB", "MB", "GB", "TB"};

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bytes = (long)value;
            if (bytes < 0)
            {
                return string.Empty;
            }
            if (bytes == 0)
            {
                return "0 bytes";
            }

            var mag = (int)Math.Log(bytes, 1024);
            var adjustedSize = (decimal)bytes / (1L << (mag * 10));

            if (Math.Round(adjustedSize, 2) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n2} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
