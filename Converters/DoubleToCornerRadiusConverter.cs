﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RestaurantDesk.Converters
{
    internal class DoubleToCornerRadiusConverter : IValueConverter
    {
        public static readonly DoubleToCornerRadiusConverter Instance = new();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => new CornerRadius(Math.Max(0, (double)value));

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
