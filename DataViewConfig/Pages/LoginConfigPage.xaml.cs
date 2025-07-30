using DataView_Configuration;
using System;
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

namespace DataViewConfig.Pages
{
    /// <summary>
    /// InterfaceConfigPage.xaml 的交互逻辑
    /// </summary>
    public partial class LoginConfigPage : UserControl
    {
        public LoginConfigPage()
        {
            InitializeComponent();
            this.DataContext = new ViewModels.LoginConfigViewModel();
        }

        //private void UserSystemCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var umsTypeCombox = sender as ComboBox;
        //    var selectedValue = umsTypeCombox.SelectedValue;
        //}
    }
}
