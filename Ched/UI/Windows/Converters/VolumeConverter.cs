using System;
using System.Globalization;
using System.Windows.Data;

namespace Ched.UI.Windows.Converters
{
    public class VolumeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double volume)) return 0;
            return (int)(volume * 100);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double volume)) return 0;
            return volume / 100.0;
        }
    }
}
