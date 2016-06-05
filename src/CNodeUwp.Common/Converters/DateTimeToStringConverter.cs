using System;
using Windows.UI.Xaml.Data;

namespace CNodeUwp.Common.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var dt = value as DateTime?;
            return dt?.ToString("yyyy.MM.dd HH:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
