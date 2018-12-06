using System;
using System.Globalization;
using System.Windows.Data;

namespace MapEditor
{
    class MultiplyConverter : IMultiValueConverter
    {
        private MultiplyConverter() { }
        public static MultiplyConverter Instance { get; } = new MultiplyConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double r = 0;
            if (values[0] is int && values[1] is int)
                r = (int)values[0] * (int)values[1];
            return r;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class StringToIntConverter : IValueConverter
    {
        private StringToIntConverter() { }
        public static StringToIntConverter Instance { get; } = new StringToIntConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = (string)value;
            if (int.TryParse(s, out int result))
                return result;
            else
                return -1;
        }
    }
}
