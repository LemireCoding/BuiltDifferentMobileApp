using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.Services.Converters {
    internal class BoolToSetAccountStatusButtonConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (bool)value ? "Unsuspend user" : "Suspend user";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return (bool)value ? "Unsuspend user" : "Suspend user";
        }
    }
}
