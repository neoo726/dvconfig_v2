using DataView_Configuration;
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
    public partial class ControlDefaultValEditPopup
    {
       
        public ControlDefaultValEditPopup(DbModels.dv_control_default_value o)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.NewControlDefaultValEditPopupViewModel(o);
            this.grid.MouseLeftButtonDown += (s, e) => { DragMove(); };
        }

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var combobox = sender as ComboBox;
            if (!combobox.IsKeyboardFocusWithin) return;
            switch (combobox.Name)
            {
                case "tagSelectionCombox1":
                    this.tagSelectionCombox1.IsDropDownOpen = true;
                    break;
                case "tagSelectionCombox2":
                    this.tagSelectionCombox2.IsDropDownOpen = true;
                    break;
            }
        }
    }
}
