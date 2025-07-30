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
    public partial class FanoutEditPopup
    {
       
        public FanoutEditPopup(FanoutReceiveModel o)
        {
            InitializeComponent();
            this.DataContext = new ViewModels.FanoutEditPopupViewModel(o);
            this.grid.MouseLeftButtonDown += (s, e) => { DragMove(); };
        }

        private void WriteTypeCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            System.Windows.Controls.ComboBox c = sender as System.Windows.Controls.ComboBox;
            var s = c.SelectedItem as DataViewConfig.EnumerationExtension.EnumerationMember;
            var selectedItem = s.Value.ToString();
            var curSelecedSource = Utli.ConvertToEnum<DvReceiveWriteTypeEnum>(selectedItem);
            switch (curSelecedSource)
            {
                case DvReceiveWriteTypeEnum.NOT_WRITE:
                    this.cacheTagNamePanel.Visibility = Visibility.Collapsed;
                    this.cacheConditionPanel.Visibility = Visibility.Collapsed;
                    break;
                case DvReceiveWriteTypeEnum.WRITE_BY_TAG_INTERNAL_NAME:
                    this.cacheTagNamePanel.Visibility = Visibility.Visible;
                    this.cacheConditionPanel.Visibility = Visibility.Collapsed;
                    break;
                case DvReceiveWriteTypeEnum.WRITE_BY_WRITE_CONDITION:
                    this.cacheTagNamePanel.Visibility = Visibility.Collapsed;
                    this.cacheConditionPanel.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void StoreTypeCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            System.Windows.Controls.ComboBox c = sender as System.Windows.Controls.ComboBox;
            var s = c.SelectedItem as DataViewConfig.EnumerationExtension.EnumerationMember;
            var selectedItem = s.Value.ToString();
            var curSelecedSource = Utli.ConvertToEnum<DvReceiveStoreTypeEnum>(selectedItem);
            switch (curSelecedSource)
            {
                case DvReceiveStoreTypeEnum.CACHE:
                    
                    this.fullStoreNamePanel.Visibility = Visibility.Collapsed;
                    break;
                case DvReceiveStoreTypeEnum.ONLY_WRITE:
                   
                    this.fullStoreNamePanel.Visibility = Visibility.Collapsed;
                    break;
                case DvReceiveStoreTypeEnum.CACHE_AND_FULL_STORE:
                   
                    this.fullStoreNamePanel.Visibility = Visibility.Visible;
                    break;
                case DvReceiveStoreTypeEnum.FULL_STORE:
                   
                    this.fullStoreNamePanel.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
