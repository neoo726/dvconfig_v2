using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DataViewConfig.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string imagePath = value as string;
            if (string.IsNullOrEmpty(imagePath))
                return null;

            //string imagePath = string.Format("/Images/{0}.png", imageName);

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath = Path.Combine(baseDirectory, "Images\\", imagePath);
            
            if (File.Exists(fullPath))
            {
                return new BitmapImage(new Uri(fullPath));
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TipsTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            int curType = (int)value;
            if (curType == 1)
            {
                return "文字";
            }
            else
            {
                return "图片";
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "文字")
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}
