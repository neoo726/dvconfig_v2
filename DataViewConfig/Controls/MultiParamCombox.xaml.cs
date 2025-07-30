using System;
using System.Collections.Generic;
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
    /// <summary>
    /// MultiParamCombox.xaml 的交互逻辑
    /// </summary>
    /// <summary>
    /// MultiCombox.xaml 的交互逻辑
    /// </summary>
    public partial class MultiParamCombox : UserControl
    {
        public MultiParamCombox()
        {
            InitializeComponent();
        }
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MultiParamCombox), new PropertyMetadata(null, OnCommandChanged));


        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(MultiParamCombox), new PropertyMetadata(null, OnCommandParameterCallback));

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        private static void OnCommandParameterCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        #region Dependency Properties

        public IEnumerable<RpcParamMultipleCheckboxModel> ItemsSource
        {
            get { return (IEnumerable<RpcParamMultipleCheckboxModel>)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
                SetText();
            }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(object), typeof(MultiParamCombox), new FrameworkPropertyMetadata(null, ItemsSourcePropertyChangedCallback));

        private static void ItemsSourcePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var multioleCheckbox = dependencyObject as MultiParamCombox;
            if (multioleCheckbox == null) return;
            multioleCheckbox.CheckableCombo.ItemsSource = multioleCheckbox.ItemsSource;

            if (multioleCheckbox.ItemsSource.Count() > 0)
            {
                multioleCheckbox.Text = string.Empty;
                foreach (var item in multioleCheckbox.ItemsSource)
                {
                    if (item.IsSelected)
                    {
                        multioleCheckbox.Text += item.ParamName + ",";
                    }
                }
                multioleCheckbox.Text = string.IsNullOrEmpty(multioleCheckbox.Text) ? "" : multioleCheckbox.Text.TrimEnd(new[] { ',' });
            }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);

            }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MultiParamCombox), new FrameworkPropertyMetadata("", TextPropertyChangedCallback));


        private static void TextPropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var multioleCheckbox = dependencyObject as MultiParamCombox;
            if (multioleCheckbox == null) return;
        }
        public string ParamIdStr
        {
            get { return (string)GetValue(ParamIdStrProperty); }
            set
            {
                SetValue(ParamIdStrProperty, value);

            }
        }

        public static readonly DependencyProperty ParamIdStrProperty =
            DependencyProperty.Register("ParamIdStr", typeof(string), typeof(MultiParamCombox), new FrameworkPropertyMetadata("", TextPropertyChangedCallback));

        public string DefaultText
        {
            get { return (string)GetValue(DefaultTextProperty); }
            set { SetValue(DefaultTextProperty, value); }
        }

        public static readonly DependencyProperty DefaultTextProperty =
            DependencyProperty.Register("DefaultText", typeof(string), typeof(MultiParamCombox), new UIPropertyMetadata(string.Empty));

        #endregion
        #region Event

        private void Checkbox_OnClick(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            if (checkbox == null) return;
            if (checkbox.Content is string && (string)checkbox.Content == "All")
            {
                Text = "";
                if (checkbox.IsChecked != null && checkbox.IsChecked.Value)
                {
                    foreach (var item in ItemsSource)
                    {
                        item.IsSelected = true;
                        Text = "All";
                    }
                }
                else
                {
                    foreach (var item in ItemsSource)
                    {
                        item.IsSelected = false;
                        Text = "None";
                    }
                }
            }
            else
            {
                SetText();
            }
            Command.Execute(CommandParameter);
        }

        #endregion
        #region Private Method

        private void SetText()
        {
            Text = "";
            ParamIdStr = "";
            var all = ItemsSource.FirstOrDefault(x => x.ParamName == "All");
            foreach (var item in ItemsSource)
            {


                if (item.IsSelected && item.ParamName != "All")
                {
                    ParamIdStr += item.ParamID + ",";
                    Text += item.ParamName + ",";
                }
                else if (all != null)
                {
                    if (all.IsSelected)
                        all.IsSelected = false;
                }
            }
            ParamIdStr = string.IsNullOrEmpty(ParamIdStr) ? "1" : ParamIdStr.TrimEnd(new[] { ',' });
            Text = string.IsNullOrEmpty(Text) ? DefaultText : Text.TrimEnd(new[] { ',' });
        }

        #endregion
    }
    public class RpcParamMultipleCheckboxModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public int ParamID { get; set; }

        private string paramName;
        public string ParamName
        {
            get { return paramName; }
            set
            {
                paramName = value;
                OnPropertyChanged("ParamName");
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private string _paramDesc;

        public string ParamDesc
        {
            get { return _paramDesc; }
            set
            {
                _paramDesc = value;
                OnPropertyChanged("ParamDesc");
            }
        }
    }
}
