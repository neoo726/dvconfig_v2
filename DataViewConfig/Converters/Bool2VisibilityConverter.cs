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
    /// bool转visibility
    /// </summary>
    /// 
    [ValueConversion(typeof(bool), typeof(Visibility))]
    internal class Bool2VisibilityConverter:IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool c = System.Convert.ToBoolean(value);
             
            if (value == null)
                throw new ArgumentNullException("value can not be null");
            if (c)
                return Visibility.Visible;
            else 
                return Visibility.Collapsed;
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    /// <summary>
    /// bool转Collapsed
    /// </summary>
    /// 
    [ValueConversion(typeof(bool), typeof(Visibility))]
    internal class Bool2HiddenConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool c = System.Convert.ToBoolean(value);

            if (value == null)
                throw new ArgumentNullException("value can not be null");
            if (c)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    [ValueConversion(typeof(bool), typeof(Visibility))]
    internal class Bool2ReverseVisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool c = System.Convert.ToBoolean(value);

            if (value == null)
                throw new ArgumentNullException("value can not be null");
            if (c)
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    [ValueConversion(typeof(bool), typeof(string))]
    internal class Bool2DisplayContentConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool c = System.Convert.ToBoolean(value);

            if (value == null)
                throw new ArgumentNullException("value can not be null");
            if (c)
                return "Yes";
            else
                return "No";
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "Yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
