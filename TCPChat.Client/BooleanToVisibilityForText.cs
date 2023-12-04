﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TCPChat.Client
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityForText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? Visibility.Collapsed : Visibility.Visible;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                return visibility == Visibility.Collapsed;
            }

            return false;
        }
    }
}
