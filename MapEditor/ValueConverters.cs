using System;
using System.Globalization;
using System.Windows.Data;

namespace MapEditor
{
    class SampleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class CanvasLeftConverter : IMultiValueConverter
    {
        private CanvasLeftConverter() { }
        public static CanvasLeftConverter Instance { get; } = new CanvasLeftConverter();

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

    class CanvasTopConverter : IMultiValueConverter
    {
        private CanvasTopConverter() { }
        public static CanvasTopConverter Instance { get; } = new CanvasTopConverter();

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

    class CanvasRightConverter : IMultiValueConverter
    {
        private CanvasRightConverter() { }
        public static CanvasRightConverter Instance { get; } = new CanvasRightConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double r = 0;
            if (values[0] is int && values[1] is int)
                r = ((int)values[0] + 1) * (int)values[1];
            return r;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class CanvasBottomConverter : IMultiValueConverter
    {
        private CanvasBottomConverter() { }
        public static CanvasBottomConverter Instance { get; } = new CanvasBottomConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double r = 0;
            if (values[0] is int && values[1] is int)
                r = ((int)values[0] + 1) * (int)values[1];
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
