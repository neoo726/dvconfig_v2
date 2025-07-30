using DataViewConfig.ViewModels;
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

namespace DataViewConfig.Pages.Popups
{
    /// <summary>
    /// ParamSelectedPopup.xaml 的交互逻辑
    /// </summary>
    public partial class ScreenCswSelectPopup
    {
        
        public ScreenCswSelectPopup(string selectedCswNames="")
        {
            InitializeComponent();
            this.DataContext = new ViewModels.ScreenCswSelectPopupViewModel(selectedCswNames);
           
        }

        private void FileTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(e.NewValue is FileNode selectedNode)
            {
                var vm = this.DataContext as ViewModels.ScreenCswSelectPopupViewModel;
                vm.SelectedNode = selectedNode;
                vm.SelectedRightCswName = string.Empty;
            }
        }

        private void FileRightTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is string selectedCswName)
            {
                var vm = this.DataContext as ViewModels.ScreenCswSelectPopupViewModel;
                vm.SelectedRightCswName = selectedCswName;
            }
        }
    }
  
}
