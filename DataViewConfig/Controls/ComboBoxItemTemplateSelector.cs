﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DataViewConfig.Controls
{
    public class ComboBoxItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SelectedTemplate { get; set; }
        public DataTemplate DropDownTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ComboBoxItem comboBoxItem = container.GetVisualParent<ComboBoxItem>();
            if (comboBoxItem == null)
            {
                return SelectedTemplate;
            }
            return DropDownTemplate;
        }
    }
    public static class DependencyObjectExtensions
    {
        public static T GetVisualParent<T>(this DependencyObject child) where T : Visual
        {
            while ((child != null) && !(child is T))
            {
                child = VisualTreeHelper.GetParent(child);
            }
            return child as T;
        }
    }
}
