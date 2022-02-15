using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.Services.Converters {
    public class DateToVisibleBooleanConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((DateTime)value).Date == DateTime.Today.Date ? "True" : "False";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return ((DateTime)value).Date == DateTime.Today.Date ? "True" : "False";
        }
    }
}
