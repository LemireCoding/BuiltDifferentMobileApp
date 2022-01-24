using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.Services.Converters
{
    public class BoolToTextButtonMealEatenConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Unmark" : "Mark as Eaten";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "Unmark" : "Mark as Eaten" ;
        }
    }
}
