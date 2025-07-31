using System.Windows.Controls;
using DataViewConfig.ViewModels.Modern;

namespace DataViewConfig.Pages.Modern
{
    /// <summary>
    /// ModernArchitectureTestPage.xaml 的交互逻辑
    /// </summary>
    public partial class ModernArchitectureTestPage : UserControl
    {
        public ModernArchitectureTestPage()
        {
            InitializeComponent();
            DataContext = new ModernArchitectureTestViewModel();
        }
    }
}
