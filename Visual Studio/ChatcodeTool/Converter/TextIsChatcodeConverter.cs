using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ChatcodeTool.Converter
{
    class TextIsChatcodeConverter : IValueConverter
    {
        //This is for the Error-Text
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == string.Empty) return Visibility.Hidden;

            if(value is string s)
            {
                try
                {
                    string trimmed = s.Trim(new char[] { '[', ' ', ']', '&' });
                    //Guildwars2 Chatcodes always have the exact Length of 8
                    if (trimmed.Length != 8) return Visibility.Visible;
                    System.Convert.FromBase64String(trimmed);
                    return Visibility.Hidden;
                } catch
                {
                }
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
