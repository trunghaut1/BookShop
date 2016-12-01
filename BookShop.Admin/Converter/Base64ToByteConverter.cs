using System;
using System.Globalization;
using System.Windows.Data;

namespace BookShop.Admin.Converter
{
    public class Base64ToByteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if(value != null)
                {
                    return System.Convert.FromBase64String(value.ToString());
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
