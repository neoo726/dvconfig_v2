using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// ImagePopup.xaml 的交互逻辑
    /// </summary>
    public partial class ImagePopup : Window
    {
        public ImagePopup(string imageUrl)
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(imageUrl)) return;
            var convert = new Converters.ImageConverter();
            this.MyImage.Source = (BitmapImage)convert.Convert(imageUrl, typeof(string), null, null);
        }
    }
}
