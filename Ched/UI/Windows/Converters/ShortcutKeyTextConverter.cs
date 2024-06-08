﻿using System;
using System.Globalization;
using System.Windows.Data;

using Ched.UI.Shortcuts;

namespace Ched.UI.Windows.Converters
{
    public class ShortcutKeyTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            var key = (System.Windows.Forms.Keys)value;
            return key.ToShortcutChar();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
