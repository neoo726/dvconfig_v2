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
    [ValueConversion(typeof(int), typeof(Visibility))]
    internal class ReceiveStoreType2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int iIndex = System.Convert.ToInt16(value);
            if(iIndex==2||iIndex==3)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
           
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    [ValueConversion(typeof(int), typeof(Visibility))]
    internal class ReceiveStoreCacheType2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int iIndex = System.Convert.ToInt16(value);
            if (iIndex == 0 || iIndex == 1 || iIndex == 3)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }

        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
