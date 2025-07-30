using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// TipsLabel.xaml 的交互逻辑
    /// </summary>
    public partial class TipsLabel : UserControl
    {
        public TipsLabel()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(TipsLabel), new FrameworkPropertyMetadata(typeof(TipsLabel)));

            InitializeComponent();
        }
        private static void TextChanged(DependencyObject d,DependencyPropertyChangedEventArgs e)
        {
            TipsLabel t= d as TipsLabel;
            if (t != null)
            {
                string oldTxt = e.OldValue as string;
                string newTxt = e.NewValue as string;
                t.UpdateTipsContent(newTxt);
            }
        }
        private void UpdateTipsContent(string newValue)
        {
            this.TipsTxtBlock.Text  = newValue;
        }
        [Bindable(true)]
        public string TipContent
        {
            get { return (string)GetValue(TipContentProperty); }
            set { SetValue(TipContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TipContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipContentProperty =
            DependencyProperty.Register("TipContent", typeof(string), typeof(TipsLabel), new PropertyMetadata((string)null,new PropertyChangedCallback(TextChanged)));


        private static void ShowImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TipsLabel t = d as TipsLabel;
            if (t != null)
            {
                bool old = (bool)e.OldValue;
                bool newB = (bool)e.NewValue;
                t.UpdateTipsShowImage(newB);
            }
        }
        private void UpdateTipsShowImage(bool newValue)
        {
            if (newValue)
            {
                this.TipsImage.Visibility = Visibility.Visible;
                this.TipsTxtBlock.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.TipsImage.Visibility = Visibility.Collapsed;
                this.TipsTxtBlock.Visibility = Visibility.Visible;
            }
        }
        [Bindable(true)]
        public bool ShowImage
        {
            get { return (bool)GetValue(ShowImageProperty); }
            set { SetValue(ShowImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowImageProperty =
            DependencyProperty.Register("ShowImage", typeof(bool), typeof(TipsLabel), new PropertyMetadata(false,new PropertyChangedCallback(ShowImageChanged)));


        private static void ImageUrlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TipsLabel t = d as TipsLabel;
            if (t != null)
            {
                string oldTxt = e.OldValue as string;
                string newTxt = e.NewValue as string;
                t.UpdateTipsImageUrl(newTxt);
            }
        }
        private void UpdateTipsImageUrl(string newValue)
        {
            try
            {
                this.TipsImage.Source = new BitmapImage(new Uri($"pack://siteoforigin:,,,/Libs/img/{newValue}"));
            }
            catch (Exception ex)
            {
                ShowImage = false;
                TipContent = "未找到提示图片:" + newValue;
            }
        }
        [Bindable(true)]
        public string ImageUrl
        {
            get { return (string)GetValue(ImageUrlProperty); }
            set { SetValue(ImageUrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageUrl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageUrlProperty =
            DependencyProperty.Register("ImageUrl", typeof(string), typeof(TipsLabel), new PropertyMetadata((string)null, new PropertyChangedCallback(ImageUrlChanged)));


        private static void TipsNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TipsLabel t = d as TipsLabel;
            if (t != null)
            {
                string oldTxt = e.OldValue as string;
                string newTxt = e.NewValue as string;
                t.UpdateTipsName(newTxt);
            }
        }
        private void UpdateTipsName(string newValue)
        {
            //
            if (!GlobalConfig.ConfigToolTipsDict.ContainsKey(newValue))
            {
                this.ShowImage = false;
                this.UpdateTipsContent($"未找到提示内容！请检查提示名称：{newValue}是否存在？");
            }
            else
            {
                var tipsObj = GlobalConfig.ConfigToolTipsDict[newValue];
                if(tipsObj != null)
                {
                    if (tipsObj.tips_type == 1)
                    {
                        ShowImage = false;
                        TipContent = tipsObj.tips_zh;
                    }
                    else
                    {
                        ShowImage = true;
                        ImageUrl = tipsObj.tips_img_url;
                        //TipsImage.Source = new BitmapImage(new Uri($"pack://application:,,,/Images/{ImageUrl}"));
                    }
                }
                //this.TipsTxtBlock.Text = newValue;
            }
            
        }
        [Bindable(true)]
        public string TipName
        {
            get { return (string)GetValue(TipNameProperty); }
            set { SetValue(TipNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TipContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TipNameProperty =
            DependencyProperty.Register("TipName", typeof(string), typeof(TipsLabel), new PropertyMetadata((string)null, new PropertyChangedCallback(TipsNameChanged)));

    }
}
