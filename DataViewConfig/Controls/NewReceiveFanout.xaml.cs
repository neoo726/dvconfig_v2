﻿using System;
using System.Collections.Generic;
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

namespace DataViewConfig.Controls
{
    /// <summary>
    /// NewRequestInterface.xaml 的交互逻辑
    /// </summary>
    public partial class NewReceiveFanout : UserControl
    {
        public NewReceiveFanout()
        {
            InitializeComponent();
            this.DataContext = new NewReceiveFanoutViewModel();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
