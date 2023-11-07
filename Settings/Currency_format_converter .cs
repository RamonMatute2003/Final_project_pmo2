﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_project.Settings {
    internal class Currency_format_converter:IValueConverter{
        public object Convert(object value,Type targetType,object parameter,CultureInfo culture) {
            if(value is double doubleValue) {
                return doubleValue.ToString("C",CultureInfo.CreateSpecificCulture("es-HN"));
            }
            return value.ToString();
        }

        public object ConvertBack(object value,Type targetType,object parameter,CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
