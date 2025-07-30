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
    internal class ReceiveWrite_None2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int iIndex = System.Convert.ToInt16(value);
            if(iIndex==0)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
           
        }

        //目标属性传给源属性时，调用此方法ConvertBack
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    [ValueConversion(typeof(int), typeof(bool))]
    internal class ReceiveWrite_Normal2BoolConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataView_Configuration.DvReceiveWriteTypeEnum ecsComm = (DataView_Configuration.DvReceiveWriteTypeEnum)Enum.Parse(typeof(DataView_Configuration.DvReceiveWriteTypeEnum), value.ToString());
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
            if (bVal == true)
            {
                return DataView_Configuration.DvReceiveWriteTypeEnum.WRITE_BY_TAG_INTERNAL_NAME;
            }
            else
            {
                return DataView_Configuration.DvReceiveWriteTypeEnum.WRITE_BY_WRITE_CONDITION;
            }
        }
    }
    [ValueConversion(typeof(int), typeof(bool))]
    internal class ReceiveWrite_Condition2BoolConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataView_Configuration.DvReceiveWriteTypeEnum ecsComm = (DataView_Configuration.DvReceiveWriteTypeEnum)Enum.Parse(typeof(DataView_Configuration.DvReceiveWriteTypeEnum), value.ToString());
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
            if (bVal == true)
            {
                return DataView_Configuration.DvReceiveWriteTypeEnum.WRITE_BY_WRITE_CONDITION;
            }
            else
            {
                return DataView_Configuration.DvReceiveWriteTypeEnum.WRITE_BY_TAG_INTERNAL_NAME;
            }
        }
    }
    [ValueConversion(typeof(int), typeof(Visibility))]
    internal class ReceiveWrite_Normal2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataView_Configuration.DvReceiveWriteTypeEnum ecsComm = (DataView_Configuration.DvReceiveWriteTypeEnum)Enum.Parse(typeof(DataView_Configuration.DvReceiveWriteTypeEnum), value.ToString());
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
            return (DataView_Configuration.DvReceiveWriteTypeEnum)Enum.Parse(typeof(DataView_Configuration.DvReceiveWriteTypeEnum), value.ToString()); ;
        }
    }
    [ValueConversion(typeof(int), typeof(Visibility))]
    internal class ReceiveWrite_Condition2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataView_Configuration.DvReceiveWriteTypeEnum ecsComm = (DataView_Configuration.DvReceiveWriteTypeEnum)Enum.Parse(typeof(DataView_Configuration.DvReceiveWriteTypeEnum), value.ToString());
            int iIndex = System.Convert.ToInt16(value);
            if (iIndex == 2)
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
