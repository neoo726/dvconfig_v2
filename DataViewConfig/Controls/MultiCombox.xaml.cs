using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace DataViewConfig.Controls
{

    public partial class MultiSelectComboBox : UserControl, INotifyPropertyChanged
    {
        private ObservableCollection<MultiSelectComboBoxItem> items = new ObservableCollection<MultiSelectComboBoxItem>();
        public ObservableCollection<MultiSelectComboBoxItem> Items
        {
            get { return items; }
            set
            {
                items = value;
                OnPropertyChanged("Items");
            }
        }

        private bool isPopupOpen = false;
        public bool IsPopupOpen
        {
            get { return isPopupOpen; }
            set
            {
                isPopupOpen = value;
                OnPropertyChanged("IsPopupOpen");
            }
        }

        private string selectedItemsDisplayText = "Please select items";
        public string SelectedItemsDisplayText
        {
            get { return selectedItemsDisplayText; }
            set
            {
                selectedItemsDisplayText = value;
                OnPropertyChanged("SelectedItemsDisplayText");
            }
        }

        public MultiSelectComboBox()
        {
            InitializeComponent();
            DependencyPropertyDescriptor
                .FromProperty(ItemsSourceProperty, typeof(MultiSelectComboBox))
                .AddValueChanged(this, (s, e) => RefreshItems());

            // Bind DataContext
            DataContext = this;
        }
        private void RefreshItems()
        {
            // 清空 ListBox 和 TextBox 中的内容
            listBox.Items.Clear();
            textBox.Text = "";

            // 如果没有绑定数据源，则不需要更新控件
            if (ItemsSource == null)
                return;

            // 将数据源中的项添加到 ListBox 中
            foreach (var item in ItemsSource)
            {
                var checkBox = new CheckBox { Content = item };
                checkBox.Checked += CheckBox_Checked;
                checkBox.Unchecked += CheckBox_Unchecked;
                listBox.Items.Add(checkBox);
            }

            // 将选中的项显示在 TextBox 中
            var selectedItems = GetSelectedItems(listBox);
            if (selectedItems.Any())
                textBox.Text = string.Join(",", selectedItems);
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            // 将选中的项显示在 TextBox 中
            textBox.Text = string.Join(",", GetSelectedItems(listBox));
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            // 将取消选中的项从 TextBox 中删除
            textBox.Text = string.Join(",", GetSelectedItems(listBox));
        }
        public List<object> GetSelectedItems(ListBox listBox)
        {
            List<object> selectedItems = new List<object>();
            foreach (ListBoxItem listBoxItem in listBox.SelectedItems)
            {
                selectedItems.Add(listBoxItem.DataContext);
            }
            return selectedItems;
        }

        private void OnTextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            // Open the popup when the text box gets focus
            IsPopupOpen = true;
        }

        private void OnListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the selected items display text
            SelectedItemsDisplayText = string.Join(", ", items.Where(x => x.IsSelected).Select(x => x.DisplayText));

            // Close the popup when the selection changes
            IsPopupOpen = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static readonly DependencyProperty ItemsSourceProperty =
    DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(MultiSelectComboBox), new PropertyMetadata(null));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

    }

    public class MultiSelectComboBoxItem
    {
        public string DisplayText { get; set; }
        public bool IsSelected { get; set; }
    }

}
