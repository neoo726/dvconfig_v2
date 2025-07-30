using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataViewConfig.Base
{
    public  class AttachProperty
    {
        public static string GetPlaceHolder(DependencyObject obj)
        {
            return (string)obj.GetValue(PlaceHolderProperty);
        }

        public static void SetPlaceHolder(DependencyObject obj, string value)
        {
            obj.SetValue(PlaceHolderProperty, value);
        }

        // Using a DependencyProperty as the backing store for PlaceHolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.RegisterAttached("PlaceHolder", typeof(string), typeof(AttachProperty),
                new PropertyMetadata("请输入傻傻", OnPlaceHolderChanged));

        private static void OnPlaceHolderChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {

            //var element = obj as TextBlock;
            //if (element != null)
            //{
            //    element.Text = e.NewValue.ToString();
            //}
        }
    }
}
