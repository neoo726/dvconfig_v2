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
    internal class EcsComm_MQ2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int iIndex = System.Convert.ToInt16(value);
            if(iIndex==0)
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
    internal class EcsComm_OPC2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataView_Configuration.ECSCommType ecsComm = (DataView_Configuration.ECSCommType)Enum.Parse(typeof(DataView_Configuration.ECSCommType), value.ToString());
            int iIndex = System.Convert.ToInt16(value);
            if (iIndex ==1)
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
    public class EcsComm_MQ2BoolConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int iIndex = System.Convert.ToInt16(value);
            if (iIndex == 0)
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
            var bVal = System.Convert.ToBoolean(value);
            if (bVal)
                return Enum.Parse(targetType,"0");
            else
                return Enum.Parse(targetType, "1");
        }
    }
    [ValueConversion(typeof(int), typeof(bool))]
    internal class EcsComm_OPC2BoolConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataView_Configuration.ECSCommType ecsComm = (DataView_Configuration.ECSCommType)Enum.Parse(typeof(DataView_Configuration.ECSCommType), value.ToString());
            int iIndex = System.Convert.ToInt16(value);
            if (iIndex == 1)
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
            var bVal = System.Convert.ToBoolean(value);
            if (bVal)
                return 1;
            else
                return 0;
        }
    }
    [ValueConversion(typeof(int), typeof(bool))]
    internal class EcsComm_Rest2BoolConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataView_Configuration.ECSCommType ecsComm = (DataView_Configuration.ECSCommType)Enum.Parse(typeof(DataView_Configuration.ECSCommType), value.ToString());
            int iIndex = System.Convert.ToInt16(value);
            if (iIndex == 2)
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
            var bVal = System.Convert.ToBoolean(value);
            if (bVal)
                return Enum.Parse(targetType, "2");
            else
                return Enum.Parse(targetType, "1");
        }
    }
    [ValueConversion(typeof(int), typeof(bool))]
    internal class EcsComm_Redis2BoolConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataView_Configuration.ECSCommType ecsComm = (DataView_Configuration.ECSCommType)Enum.Parse(typeof(DataView_Configuration.ECSCommType), value.ToString());
            int iIndex = System.Convert.ToInt16(value);
            if (iIndex == 3)
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
            var bVal = System.Convert.ToBoolean(value);
            if (bVal)
                return Enum.Parse(targetType, "3");
            else
                return Enum.Parse(targetType, "1");
        }
    }
    [ValueConversion(typeof(UmsTypeEnum), typeof(Visibility))]
    internal class UmsType2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataView_Configuration.UmsTypeEnum umsType = (DataView_Configuration.UmsTypeEnum)Enum.Parse(typeof(DataView_Configuration.UmsTypeEnum), value.ToString());
           
            if (umsType ==  UmsTypeEnum.UMS_LOCAL)
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
    [ValueConversion(typeof(int), typeof(string))]
    internal class EcsComm_Int2FanoutTitleStringConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataView_Configuration.ECSCommType ecsComm = (DataView_Configuration.ECSCommType)Enum.Parse(typeof(DataView_Configuration.ECSCommType), value.ToString());
            int iIndex = System.Convert.ToInt16(value);
            switch (iIndex)
            {
                case 0:
                    return "Fanout广播接口(点击展开）";
                case 1:
                    return "Fanout广播接口(点击展开）";
                case 2:
                    return "Rest轮询接口(获取状态)(点击展开）";
                //case 3:
                //    return "Redis轮询数据库状态接口(点击展开）";
                default:
                    return "";
            }
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
