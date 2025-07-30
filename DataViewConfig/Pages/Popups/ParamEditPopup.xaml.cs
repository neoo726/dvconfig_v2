using DataView_Configuration;
using DataViewConfig.Models;
using DataViewConfig.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class ParamEditPopup
    {
       
        public ParamEditPopup(ParamModel o)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.ParameterEditPopupViewModel(o);
            this.grid.MouseLeftButtonDown += (s, e) => { DragMove(); };
            this.ParamSourceType.SelectedIndex = 0;
        }


        private void ParamSourceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox c = sender as System.Windows.Controls.ComboBox;
            if (c == null || c.SelectedItem == null) return;
            var s = c.SelectedItem as DataViewConfig.EnumerationExtension.EnumerationMember;
            var selectedItem = s.Value.ToString();
            var curSelecedSource = Utli.ConvertToEnum<RequestParamSource>(selectedItem);
            switch (curSelecedSource)
            {
                case RequestParamSource.NORMAL_TAG:
                    this.tagNamePanel.Visibility = Visibility.Visible;
                    this.tagJsonPathNamePanel.Visibility = Visibility.Collapsed;
                    this.constValPanel.Visibility = Visibility.Collapsed;
                    this.controlNamePanel.Visibility = Visibility.Collapsed;
                    this.controlComboxDataSelectPanel.Visibility = Visibility.Collapsed;
                    this.macroNamePanel.Visibility = Visibility.Collapsed;
                    this.dbSheetNamePanel.Visibility = Visibility.Collapsed;
                    this.childParamPanel.Visibility = Visibility.Collapsed;
                    break;
                case RequestParamSource.JSON_TAG:
                    this.tagNamePanel.Visibility = Visibility.Visible;
                    this.tagJsonPathNamePanel.Visibility = Visibility.Visible;
                    this.childParamPanel.Visibility = Visibility.Collapsed;
                    //this.tagNamePanel.Visibility = Visibility.Collapsed;

                    this.constValPanel.Visibility = Visibility.Collapsed;
                    this.controlNamePanel.Visibility = Visibility.Collapsed;
                    this.controlComboxDataSelectPanel.Visibility = Visibility.Collapsed;
                    this.macroNamePanel.Visibility = Visibility.Collapsed;
                    this.dbSheetNamePanel.Visibility = Visibility.Collapsed;
                    this.childParamPanel.Visibility = Visibility.Collapsed;
                    break;
                case RequestParamSource.MACRO:
                    this.macroNamePanel.Visibility = Visibility.Visible;

                    this.constValPanel.Visibility = Visibility.Collapsed;
                    this.tagNamePanel.Visibility = Visibility.Collapsed;
                    this.tagJsonPathNamePanel.Visibility = Visibility.Collapsed;
                    //this.constValPanel.Visibility = Visibility.Collapsed;
                    this.controlNamePanel.Visibility = Visibility.Collapsed;
                    this.controlComboxDataSelectPanel.Visibility = Visibility.Collapsed;
                    this.dbSheetNamePanel.Visibility = Visibility.Collapsed;
                    this.childParamPanel.Visibility = Visibility.Collapsed;
                    break;
                case RequestParamSource.CONSTANT:
                    this.constValPanel.Visibility = Visibility.Visible;

                    this.tagNamePanel.Visibility = Visibility.Collapsed;
                    this.tagJsonPathNamePanel.Visibility = Visibility.Collapsed;
                    //this.constValPanel.Visibility = Visibility.Collapsed;
                    this.controlNamePanel.Visibility = Visibility.Collapsed;
                    this.controlComboxDataSelectPanel.Visibility = Visibility.Collapsed;
                    this.macroNamePanel.Visibility = Visibility.Collapsed;
                    this.dbSheetNamePanel.Visibility = Visibility.Collapsed;
                    this.childParamPanel.Visibility = Visibility.Collapsed;
                    break;
                //case RequestParamSource.DB:
                //    this.dbSheetNamePanel.Visibility = Visibility.Visible;

                //    this.constValPanel.Visibility = Visibility.Collapsed;

                //    this.tagNamePanel.Visibility = Visibility.Collapsed;
                //    this.tagJsonPathNamePanel.Visibility = Visibility.Collapsed;
                //    //this.constValPanel.Visibility = Visibility.Collapsed;
                //    this.controlNamePanel.Visibility = Visibility.Collapsed;
                //    this.macroNamePanel.Visibility = Visibility.Collapsed;

                //    break;
                case RequestParamSource.ARRAY_ONE_ITEM:
                    this.controlNamePanel.Visibility = Visibility.Collapsed;
                    this.controlComboxDataSelectPanel.Visibility= Visibility.Collapsed;
                    this.tagNamePanel.Visibility = Visibility.Collapsed;
                    this.tagJsonPathNamePanel.Visibility = Visibility.Collapsed;
                    this.constValPanel.Visibility = Visibility.Collapsed;
                    //this.controlNamePanel.Visibility = Visibility.Collapsed;
                    this.macroNamePanel.Visibility = Visibility.Collapsed;
                    this.dbSheetNamePanel.Visibility = Visibility.Collapsed;
                    this.childParamPanel.Visibility = Visibility.Visible;
                    break;
                case RequestParamSource.CONTROL:
                    this.controlNamePanel.Visibility = Visibility.Visible;
                    this.controlComboxDataSelectPanel.Visibility = Visibility.Visible;
                    this.tagNamePanel.Visibility = Visibility.Collapsed;
                    this.tagJsonPathNamePanel.Visibility = Visibility.Collapsed;
                    this.constValPanel.Visibility = Visibility.Collapsed;
                    //this.controlNamePanel.Visibility = Visibility.Collapsed;
                    this.macroNamePanel.Visibility = Visibility.Collapsed;
                    this.dbSheetNamePanel.Visibility = Visibility.Collapsed;
                    this.childParamPanel.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void ComboBox_TextInput(object sender, TextCompositionEventArgs e)
        {
            // 获取查询条件
            var query = ((ComboBox)sender).Text.Trim().ToLower();
            var vm = this.DataContext as NewRpcEditPopupViewModel;
            // 如果查询条件为空，则显示原始集合中的所有项
            if (string.IsNullOrEmpty(query))
            {
                vm.FilterAllParamLst = new ObservableCollection<ParamModel>(vm.AllParamLst);
                return;
            }
            // 筛选原始集合中匹配的项
            var filtered = vm.AllParamLst.Where(item => item.ControlInternalName.ToLower().Contains(query));
            vm.FilterAllParamLst = new ObservableCollection<ParamModel>(filtered);
        }

        private void ComboBox_KeyUp(object sender, KeyEventArgs e)
        {
            //ComboBox comboBox = sender as ComboBox;
            //var vm = this.DataContext as NewRpcEditPopupViewModel;
            //vm.UpdateParamItems(comboBox.Text);  //刷新下拉选项
            //if (vm.FilterAllParamLst.Count > 0)
            //    comboBox.IsDropDownOpen = true;
            //TextBox textBox = (TextBox)comboBox.Template.FindName("PART_EditableTextBox", comboBox);
            //textBox.SelectionStart = textBox.Text.Length;
        }

      
    }
}
