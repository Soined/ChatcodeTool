using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ChatcodeTool.Converter
{
    class QuantityIsValidConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //We don't want to show the error text if the user hasn't typed anything yet, which is the reason for the part after ||
            return (value is string s && int.TryParse(s, out int res) && res > 0 && res < 256 || (string)value == string.Empty) ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
