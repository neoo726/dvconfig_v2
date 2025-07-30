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
    public partial class ParamSelectedPopup : Window
    {
        public ParamSelectedPopup(short[] paramIdArray)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.ParamSelectedPopupViewModel(paramIdArray);
            this.grid.MouseLeftButtonDown += (s, e) => { DragMove(); };
        }
    }
}
