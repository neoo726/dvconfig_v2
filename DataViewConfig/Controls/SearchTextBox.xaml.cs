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

namespace DataViewConfig.Controls
{
    /// <summary>
    /// SearchTextBox.xaml 的交互逻辑
    /// </summary>
    public partial class SearchTextBox : UserControl
    {
        public SearchTextBox()
        {
            InitializeComponent();
         
        }
        public ICommand BtnCommand
        {
            get { return (ICommand)GetValue(BtnCommandProperty); }
            set { SetValue(BtnCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BtnCommandProperty =
            DependencyProperty.Register("BtnCommand", typeof(ICommand), typeof(SearchTextBox), new PropertyMetadata(default));



        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(SearchTextBox), new PropertyMetadata(default));



        public string PreviewTxt
        {
            get { return (string)GetValue(PreviewTxtProperty); }
            set { SetValue(PreviewTxtProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PreviewTxt.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PreviewTxtProperty =
            DependencyProperty.Register("PreviewTxt", typeof(string), typeof(SearchTextBox), new PropertyMetadata("输入"));

       
        public event EventHandler<SearchEventArgs> OnSearch;
        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.BtnCommand != null)
            {
                this.BtnCommand.Execute(TbxInput.Text);
            }
            ExeccuteSearch();
        }

        private void TbxInput_OnKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key== Key.Enter)
            {
                if (this.BtnCommand != null)
                {
                    this.BtnCommand.Execute(TbxInput.Text);
                }
                ExeccuteSearch();
            }
        }

        private void ExeccuteSearch()
        {
            if (OnSearch != null)
            {
                var args = new SearchEventArgs();
                args.SearchText = TbxInput.Text;
                OnSearch(this, args);
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            TbxInput.Text = string.Empty;
            this.BtnDelete.Visibility = Visibility.Collapsed;
        }

        private void Uc_TbxContent_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tx= (TextBox)sender;
            if (tx != null)
            {
                this.BtnDelete.Visibility = Visibility.Visible;
            }
        }
    }
    public class SearchEventArgs : EventArgs
    {
        public string SearchText { get; set; }
    }
}
