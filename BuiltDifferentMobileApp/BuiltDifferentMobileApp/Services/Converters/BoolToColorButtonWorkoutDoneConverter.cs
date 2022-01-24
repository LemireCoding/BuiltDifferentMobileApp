using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BuiltDifferentMobileApp.Services.Converters
{
    public class BoolToColorButtonWorkoutDoneConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "#525557" : "#BA181B";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? "#525557" : "#BA181B";
        }
    }
}
