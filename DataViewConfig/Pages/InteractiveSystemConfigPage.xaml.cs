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
    public partial class InteractiveSystemConfigPage : UserControl
    {
        public InteractiveSystemConfigPage(DataView_Configuration.DvSystemModel dvSystem=null)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.InteractiveSystemConfigPageViewModel(dvSystem==null?0:dvSystem.SystemId);
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            var expander=sender as Expander;
            if(expander!=null)
            {
                switch (expander.Name)
                {
                    case "BaseInfoExpander":
                        if(this.BaseInfoExpander.IsExpanded)
                        {
                            this.RpcRequestExpander.IsExpanded = false;
                            this.FanoutExpander.IsExpanded = false;
                            this.DefaultValueExpander.IsExpanded = false;
                        }
                        break;
                    case "RpcRequestExpander":
                        if (this.RpcRequestExpander.IsExpanded)
                        {
                            this.BaseInfoExpander.IsExpanded = false;
                            this.FanoutExpander.IsExpanded = false;
                            this.DefaultValueExpander.IsExpanded = false;
                        }
                        break;
                    case "FanoutExpander":
                        if (this.FanoutExpander.IsExpanded)
                        {
                            this.RpcRequestExpander.IsExpanded = false;
                            this.BaseInfoExpander.IsExpanded = false;
                            this.DefaultValueExpander.IsExpanded = false;
                        }
                        break;
                    case "DefaultValueExpander":
                        if (this.DefaultValueExpander.IsExpanded)
                        {
                            this.BaseInfoExpander.IsExpanded = false;
                            this.FanoutExpander.IsExpanded = false;
                            this.RpcRequestExpander.IsExpanded = false;
                        }
                        break;
                }
               
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("A");
        }
    }
}
