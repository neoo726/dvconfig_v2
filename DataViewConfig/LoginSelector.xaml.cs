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
using System.Windows.Shapes;

namespace DataViewConfig
{
    /// <summary>
    /// LoginSelector.xaml 的交互逻辑
    /// </summary>
    public partial class LoginSelector : Window
    {
        public LoginSelector()
        {
            InitializeComponent();
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if(RCMSGUI_RadioBtn.IsChecked == true)
            {
                GlobalConfig.IsDataViewConfig = false;
            }
            else
            {
                GlobalConfig.IsDataViewConfig= true;
            }
            //StartWindow s = new StartWindow(Config.ProjConfig.curProjConfig.showDbSelectionWindow.showDbSelectionWindowEnable);
            //s.Show();
            this.Close();
        }
    }
}
