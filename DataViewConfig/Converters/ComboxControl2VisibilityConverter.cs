using DataView_Configuration;
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
    /// DbModels.dv_control_conf   Combox 转Visibility
    /// </summary>
    /// 
    [ValueConversion(typeof(DbModels.dv_control_conf), typeof(Visibility))]
    internal class ComboxControl2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value ==null) return Visibility.Collapsed;
            var contrl = (DbModels.dv_control_conf)value;
            if (contrl.dv_control_type_id == (int)ControlType.DrawCombox)
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
    /// <summary>
    /// Combox转bool
    /// </summary>
    /// 
    [ValueConversion(typeof(int), typeof(bool))]
    internal class ComboxSelectIndex2BoolConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var contrl = (int)value;
            if (contrl==1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value?1:2;
        }
    }
    [ValueConversion(typeof(int), typeof(bool))]
    internal class ComboxSelectText2BoolConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var contrl = (int)value;
            if (contrl == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? 2 : 1;
        }
    }
}
