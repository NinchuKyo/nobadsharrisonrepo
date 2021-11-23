using System;
using System.Globalization;
using System.Windows.Data;

namespace AOERandomizer.View.Converters
{
    internal class ScrollLimitConverter : IMultiValueConverter
    {
        private const string LOG_CTX = "AOERandomizer.View.Converters";

        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2
                && values[0] is double first
                && values[1] is double second)
            {
                return first == second;
            }

            return false;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}