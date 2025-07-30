using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DataViewConfig.Base
{
    public partial class BaseWindow : Window
    {
        public BaseWindow():base()
        {
            this.Closed += Window_Closed;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.ResizeMode = ResizeMode.NoResize;
        }
        private  void Window_Closed(object sender,EventArgs e)
        {
            Grid grid = this.Owner.Content as Grid;
            UIElement original = VisualTreeHelper.GetChild(grid, 0) as UIElement;
            grid.Children.Remove(original);
            this.Owner.Content = original;
        }
        public bool? ShowDialog(Window owner)
        {
            Grid layer = new Grid() { Background = new SolidColorBrush(Color.FromArgb(128, 0, 0, 0)) };
            UIElement original = owner.Content as UIElement;
            owner.Content = null;

            Grid container = new Grid();
            container.Children.Add(original);
            container.Children.Add(layer);

            owner.Content = container;
            this.Owner = owner;
            return this.ShowDialog();
        }
    }
}
