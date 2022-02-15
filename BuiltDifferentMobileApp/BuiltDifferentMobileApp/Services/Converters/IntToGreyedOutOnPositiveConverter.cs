using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.Services.Converters {
    public class IntToGreyedOutOnPositiveConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (int)value >= 0 ? "#808080" : "#FFFFFF";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return (int)value >= 0 ? "#808080" : "#FFFFFF";
        }
    }
}
