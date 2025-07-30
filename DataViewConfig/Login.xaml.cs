using CMSCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace DataViewConfig
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public  Login()
        {
            InitializeComponent();
           
            this.grid.MouseLeftButtonDown += (o, e) => { DragMove(); };
        }

        /// <summary>
        /// 登录
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(this.LoginNameTxtBox.Text))
            {
                MessageBox.Show("用户名不能为空！");
                return;
            }
            var pwd = this.LoginPwdTxtBox.Password;

            if (!Config.ProjConfig.curProjConfig.UserList.Exists(x => x.userName == this.LoginNameTxtBox.Text && x.userPwd == pwd))
            {
                MessageBox.Show("账号或密码错误！");
                return;
            }
            var user= Config.ProjConfig.curProjConfig.UserList.Where(x=>x.userName == this.LoginNameTxtBox.Text).ToList()[0];
            LogHelper.Info($"user:{user.userName} login success!");
            GlobalConfig.CurUserLevel = UserLevelType.Administrator;//Utli.ConvertToEnum<UserLevelType>(user.userLevel.ToString());
            StartWindow s = new StartWindow(false);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //避免重复打开
            System.Diagnostics.Process[] myProcesses = System.Diagnostics.Process.GetProcessesByName("DataViewConfig");
            if (myProcesses.Length > 1)
            {
                MessageBox.Show("程序已经启动，请勿重复打开！");
                Application.Current.Shutdown();
            }
            //加载xml配置
            var xmlPath = Directory.GetCurrentDirectory()+ @"\projConfig.xml";
            if (!File.Exists(xmlPath))
            {
                System.Windows.MessageBox.Show($"未找到配置文件(projConfig.xml)"); return;
            }
            var curProjConfig = new Config.ProjConfig(xmlPath);
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current?.Shutdown();
        }
    }
}
