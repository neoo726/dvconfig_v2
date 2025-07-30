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
    [ValueConversion(typeof(MQRPCQueueTypeEnum), typeof(bool))]
    internal class MqRpcOnlyOne2BoolConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rpcType = (MQRPCQueueTypeEnum)value;
            if(rpcType!= MQRPCQueueTypeEnum.ONLY_ONE_RPC)
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
            var bVal = System.Convert.ToBoolean(value);
            if (bVal)
                return Enum.Parse(targetType, "1");
            else
                return DependencyProperty.UnsetValue;
        }
    }
    [ValueConversion(typeof(MQRPCQueueTypeEnum), typeof(bool))]
    internal class MqRpcTypeMultiCid2BoolConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rpcType = (MQRPCQueueTypeEnum)value;
            if (rpcType != MQRPCQueueTypeEnum.MULTI_RPC_QUEUE_BY_CRANE_ID)
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
            var bVal = System.Convert.ToBoolean(value);
            if (bVal)
                return Enum.Parse(targetType, "2");
            else
                return DependencyProperty.UnsetValue;
        }
    }
    [ValueConversion(typeof(MQRPCQueueTypeEnum), typeof(bool))]
    internal class MqRpcTypeMultiBlockId2BoolConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rpcType = (MQRPCQueueTypeEnum)value;
            if (rpcType != MQRPCQueueTypeEnum.MULTI_RPC_QUEUE_BY_BLOCK_ID)
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
            var bVal = System.Convert.ToBoolean(value);
            if (bVal)
                return Enum.Parse(targetType, "3");
            else
                return DependencyProperty.UnsetValue;
        }
    }
    [ValueConversion(typeof(MQRPCQueueTypeEnum), typeof(Visibility))]
    internal class MqRpcTypeMultiCid2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rpcType = (MQRPCQueueTypeEnum)value;
            if (rpcType != MQRPCQueueTypeEnum.MULTI_RPC_QUEUE_BY_CRANE_ID)
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
            var bVal = (Visibility)value;
            if (bVal== Visibility.Visible)
                return MQRPCQueueTypeEnum.MULTI_RPC_QUEUE_BY_CRANE_ID;
            else
                return MQRPCQueueTypeEnum.ONLY_ONE_RPC;
        }
    }
   
    [ValueConversion(typeof(MQRPCQueueTypeEnum), typeof(Visibility))]
    internal class MqRpcTypeMultiBlockId2VisibilityConverter : IValueConverter
    {
        //源属性传给目标属性时，调用此方法ConvertBack
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rpcType = (MQRPCQueueTypeEnum)value;
            if (rpcType != MQRPCQueueTypeEnum.MULTI_RPC_QUEUE_BY_BLOCK_ID)
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
            var bVal = (Visibility)value;
            if (bVal == Visibility.Visible)
                return MQRPCQueueTypeEnum.MULTI_RPC_QUEUE_BY_BLOCK_ID;
            else
                return MQRPCQueueTypeEnum.ONLY_ONE_RPC;
        }
    }
}
