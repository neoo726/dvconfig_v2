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

namespace DataViewConfig
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //FrameworkCompatibilityPreferences.AreInactiveSelectionHighlightBrushKeysSupported = false;
            this.grid.MouseLeftButtonDown += (o, e) => { DragMove(); };
            this.DataContext = new MainViewModel(this);
           
        }
        //添加遮罩
        public static void AddMask()
        {
            var curWin = Application.Current.MainWindow;
            
            //mask
            Grid layer = new Grid() { Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)) };
            UIElement original = curWin.Content as UIElement;
            curWin.Content = null;
            Grid container = new Grid();
            container.Children.Add(original);
            container.Children.Add(layer);

            curWin.Content = container;
        }
        //移除遮罩
        public static void  RemoveMask()
        {
            var curWin = Application.Current.MainWindow;
           
            Grid grid = curWin.Content as Grid;
            UIElement layer = VisualTreeHelper.GetChild(grid, 1) as UIElement;
            grid.Children.Remove(layer);
            //curWin.Content = null;
            //curWin.Content = layer;
        }

        private void MainWIn_StateChanged(object sender, EventArgs e)
        {
            //if (this.WindowState == WindowState.Minimized)
            //{
            //    //this.Hide();
            //}
            //if (this.WindowState == WindowState.Maximized || this.WindowState == WindowState.Normal)
            //{
            //    this.Show();
            //    this.Activate();
            //}
        }
    
       
    }
}
