using CronExpressionDescriptor;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Golden.Fish.Desktop.ValueConverters
{
    class CronExpressionValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || targetType != typeof(string))
            {
                return null;
            }
            string s = ExpressionDescriptor.GetDescription(value as string);
            return s;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
