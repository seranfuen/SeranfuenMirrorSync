using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SeranfuenMirrorSync.Converters
{
    [ValueConversion(typeof(TimeSpan), typeof(String))]
    public class TimeSpanMinuteSecondStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var timeSpan = (TimeSpan)value;
            if (timeSpan == TimeSpan.MaxValue)
            {
                return string.Empty;
            }
            else
            {
                return string.Format("{0:#,#00}m {1:00}s", Math.Round(timeSpan.TotalMinutes, 0), timeSpan.Seconds);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.MinValue;
        }
    }
}
