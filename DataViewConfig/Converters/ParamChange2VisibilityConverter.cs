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
    /// bool转visibility
    /// </summary>
    /// 
    [ValueConversion(typeof(int), typeof(Visibility))]
    internal class ParamSelectOther2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           ParamChangeType ecsComm = (ParamChangeType)Enum.Parse(typeof(ParamChangeType), value.ToString());

            int iIndex = System.Convert.ToInt16(ecsComm);
            if(iIndex==1)
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
    internal class ParamAddNew2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ParamChangeType ecsComm = (ParamChangeType)Enum.Parse(typeof(ParamChangeType), value.ToString());
            int iIndex = System.Convert.ToInt16(ecsComm);
            if (iIndex ==2)
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
    [ValueConversion(typeof(int), typeof(bool))]
    public class ParamEditExist2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ParamChangeType ecsComm = (ParamChangeType)Enum.Parse(typeof(ParamChangeType), value.ToString());
            int iIndex = System.Convert.ToInt16(ecsComm);
            if (iIndex == 3)
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
            var bVal = System.Convert.ToBoolean(value);
            if (bVal)
                return Enum.Parse(targetType,"0");
            else
                return Enum.Parse(targetType, "1");
        }
    }
    [ValueConversion(typeof(int), typeof(Visibility))]
    internal class ParamAddOrEidt2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ParamChangeType ecsComm = (ParamChangeType)Enum.Parse(typeof(ParamChangeType), value.ToString());
            int iIndex = System.Convert.ToInt16(ecsComm);
            if (iIndex == 2|| iIndex == 3)
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
    [ValueConversion(typeof(int), typeof(bool))]
    internal class ParamEidt2ReverseBooleanConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ParamChangeType ecsComm = (ParamChangeType)Enum.Parse(typeof(ParamChangeType), value.ToString());
            int iIndex = System.Convert.ToInt16(ecsComm);
            if (iIndex == 3)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
