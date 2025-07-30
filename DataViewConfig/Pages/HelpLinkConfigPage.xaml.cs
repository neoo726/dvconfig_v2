using Markdig;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserControl = System.Windows.Controls.UserControl;

namespace DataViewConfig.Pages
{
    /// <summary>
    /// InterfaceConfigPage.xaml 的交互逻辑
    /// </summary>
    public partial class HelpLinkConfigPage : UserControl
    {
        public HelpLinkConfigPage()
        {
            InitializeComponent();
            //this.DataContext = new ViewModels.TipsConfigPageViewModel();
            // 读取Markdown文件内容
            //string markdownFilePath = AppDomain.CurrentDomain.BaseDirectory+"\\Md\\HelpLink.md";
            //string markdownContent = File.ReadAllText(markdownFilePath, Encoding.UTF8);

            //// 将Markdown文本解析成FlowDocument对象
            //var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            //FlowDocument document = new FlowDocument();
            ////document.Blocks.Add(Markdown.ToFlowDocument(markdownContent, pipeline));

            //// 将FlowDocument对象赋值给FlowDocumentScrollViewer控件的Document属性
            //markdownViewer.Document = document;
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }
        //
    }
}
