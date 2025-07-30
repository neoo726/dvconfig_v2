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
    public partial class InterfaceEditPopup
    {
       
        public InterfaceEditPopup(RequestInterfaceModel o)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.InterfaceEditPopupViewModel(o);
            this.grid.MouseLeftButtonDown += (s, e) => { DragMove(); };
        }

        private void CommTypeCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox c = sender as System.Windows.Controls.ComboBox;
            var s = c.SelectedItem as DataViewConfig.EnumerationExtension.EnumerationMember;
            var selectedItem = s.Value.ToString();
            var curSelecedSource = Utli.ConvertToEnum<ECSCommType>(selectedItem);
            switch (curSelecedSource)
            {
                case ECSCommType.MQ:
                    this.msgTypePanel.Visibility = Visibility.Visible;
                    this.paramSepartorPanel.Visibility = Visibility.Collapsed;
                    this.argsTagPanel.Visibility = Visibility.Collapsed;
                    this.eventTagPanel.Visibility = Visibility.Collapsed;
                    this.returnTagPanel.Visibility = Visibility.Collapsed;
                    this.returnTagTypePanel.Visibility = Visibility.Collapsed;
                    break;
                case ECSCommType.OPC:
                    this.msgTypePanel.Visibility = Visibility.Collapsed;
                    this.paramSepartorPanel.Visibility = Visibility.Visible;
                    this.argsTagPanel.Visibility = Visibility.Visible;
                    this.eventTagPanel.Visibility = Visibility.Visible;
                    this.returnTagPanel.Visibility = Visibility.Visible;
                    this.returnTagTypePanel.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void tagFeedbackOpctypeRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            this.tagFeedbackOpctypeRadioBtn.IsChecked = true;
            this.tagArgsEventOpcTypeRadioBtn.IsChecked = false;
            this.tagCommandIdIndexValOpcTypeRadioBtn.IsChecked = false;
        }

        private void tagArgsEventOpcTypeRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            this.tagFeedbackOpctypeRadioBtn.IsChecked = false;
            this.tagArgsEventOpcTypeRadioBtn.IsChecked = true;
            this.tagCommandIdIndexValOpcTypeRadioBtn.IsChecked = false;
        }

        private void tagCommandIdIndexValOpctypeRadioBtn_Checked(object sender, RoutedEventArgs e)
        {
            this.tagFeedbackOpctypeRadioBtn.IsChecked = false;
            this.tagArgsEventOpcTypeRadioBtn.IsChecked = false;
            this.tagCommandIdIndexValOpcTypeRadioBtn.IsChecked = true;
        }
    }
}
