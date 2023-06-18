using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace RestaurantDesk.Converters
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string row = value as string;
            if (row == "M")
                return "Male";
            else
                return "Female";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string row = value as string;
            if (row != null && row == "Female")
                return "M";
            else
                return "F";
        }
    }
}
