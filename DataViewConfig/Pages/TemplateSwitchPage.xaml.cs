﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataViewConfig.Pages
{
    /// <summary>
    /// InterfaceConfigPage.xaml 的交互逻辑
    /// </summary>
    public partial class TemplateSwitchPage : UserControl
    {
        public TemplateSwitchPage()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.TemplateSwitchPageViewModel();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }
    }
}
