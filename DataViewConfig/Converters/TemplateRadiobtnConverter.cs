using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DataViewConfig.Converters
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [ValueConversion(typeof(bool), typeof(bool))]
    internal class TemplateRadiobtnConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string checkVal = value.ToString();
            string targetVal = parameter.ToString();
            bool r = checkVal.Equals(targetVal);
            return r;
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return parameter.ToString();
            }
            return null;
        }
    }
}
