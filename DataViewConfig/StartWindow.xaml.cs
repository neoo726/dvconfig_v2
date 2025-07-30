using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using static DataViewConfig.Config.ProjConfig.ProjConfigXml;
using Path = System.IO.Path;

namespace DataViewConfig
{
    /// <summary>
    /// StartWindow.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        public int ProgressValue { get; set; }
        public string XmlFilePath { get; set; }
        public string DbFilePath { get; set; }
        public string StrIp { get; set; }
        public string StrPort { get; set; }
        public string StrUser { get; set; }
        public string StrPassword { get; set; } 
        public List<string> DbSheetNameLst { get; set; }
       
        /// <summary>
        /// 1=部署调试,2=开发
        /// </summary>
        /// <param name="userLevel"></param>
        public StartWindow(bool showDbSelection)
        {
            InitializeComponent();
            this.SqlteRadioBtn.IsChecked = false;
            this.MysqlRadioBtn.IsChecked = true;
            this.IpTxtBox.Text = "10.128.254.130";
            this.PortTxtBox.Text = "3306";
            this.UserTxtBox.Text = "root";
            this.PwdTxtBox.Text = "Zpmc@3261";

            //this.ConnStrInput.Text = @"Data Source=10.128.254.130;Initial Catalog=dataview_template_dev;User ID=root;Password=Zpmc@3261;";
            //sqlite path
            var curPath = Environment.CurrentDirectory;
            var path = Directory.GetParent(curPath).FullName + @"\Screen\Program\ConfigFiles";
            if (showDbSelection)
            {
                this.SqlitePath.Text = path + @"\dataview.db";
            }
            else
            {
                if (!File.Exists(path + @"\dataview.db"))
                {
                    System.Windows.MessageBox.Show($"未找到配置数据库{path + @"\dataview.db"}"); return;
                }
                this.SqlitePath.Text = path + @"\dataview.db";
                this.SqlteRadioBtn.IsChecked = true;
                if (InitialConfigDb())
                {
                    OpenMainWindow();
                }
            }
           
           
        }
        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <returns></returns>
        private bool InitialConfigDb()
        {
            if (MysqlRadioBtn.IsChecked.Value)
            {
                //默认程序路径在DataView工程根目录下的ConfigTool
                try
                {
                    var dbConnStr = ConcatDbConnStr(IpTxtBox.Text, PortTxtBox.Text, UserTxtBox.Text, PwdTxtBox.Text, DatabaseTxtBox.Text);
                    if (DataView_Configuration.ProjectConf.InitialConfig("Mysql", dbConnStr))
                    {
                        var db = DataView_Configuration.ProjectConf.GetSqlSugarInstance();
                        DbHelper.db = db;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("初始化数据库失败！请手动选择数据库");
                    }
                }
                catch (Exception exz)
                {
                    System.Windows.MessageBox.Show(exz.ToString());
                    return false;
                }
            }
            else
            {
                //默认程序路径在DataView工程根目录下的ConfigTool
                var dbConnStr = @"Data Source=" + this.SqlitePath.Text + @";Version=3;";

                try
                {
                    if (DataView_Configuration.ProjectConf.InitialConfig("Sqlite", dbConnStr))
                    {
                        var db = DataView_Configuration.ProjectConf.GetSqlSugarInstance();
                        DbHelper.db = db;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("初始化数据库失败！请手动选择数据库");
                        return false;
                    }
                }
                catch (Exception exx)
                {
                    System.Windows.MessageBox.Show(exx.ToString());
                    return false;
                }
            }
            InitialTipsConfig();
            return true;
        }
        private void InitialTipsConfig()
        {
            //加载界面提示Tips数据
            // 获取exe所在路径
            string exePath = AppDomain.CurrentDomain.BaseDirectory;

            // 拼接tips.json文件的完整路径
            string jsonFilePath = Path.Combine(exePath, "Config", "tips.json");

            try
            {
                // 读取JSON文件内容
                string jsonContent = File.ReadAllText(jsonFilePath);

                // 将JSON内容转换为指定类型
                TipsModel[] tipsDataArray = JsonConvert.DeserializeObject<TipsModel[]>(jsonContent);
                foreach (var item in tipsDataArray)
                {
                    if (!GlobalConfig.ConfigToolTipsDict.ContainsKey(item.tips_name))
                    {
                        GlobalConfig.ConfigToolTipsDict.Add(item.tips_name, item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            
        }
        private void OpenFileDialog(object o)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择数据库文件(仅支持Sqlite)";
            dialog.Filter = "所有文件(*.*)|*.db";
            dialog.InitialDirectory= Directory.GetParent(Environment.CurrentDirectory).FullName + @"\Screen\Program\ConfigFiles";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                DbFilePath = dialog.FileName;
            }
            var dbConnStr = @"Data Source=" + DbFilePath + @";Version=3;";

            try
            {
                if (DataView_Configuration.ProjectConf.InitialConfig("Sqlite", dbConnStr))
                {
                    var db = DataView_Configuration.ProjectConf.GetSqlSugarInstance();
                    DbHelper.db = db;
                    OpenMainWindow();
                }
                else
                {
                    System.Windows.MessageBox.Show("初始化数据库失败！");
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }
        /// <summary>
        /// 选择具体的数据库文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog(sender);
        }
        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if(InitialConfigDb())
            {
                OpenMainWindow();
            }
            else
            {
                System.Windows.MessageBox.Show("初始化数据库失败！");
            }
        }
        
        private void OpenMainWindow()
        {
            MainWindow mw = new MainWindow();
           
            System.Windows.Application.Current.MainWindow = mw;
            mw.Show();
            this.Close();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                
            }
            if (this.WindowState == WindowState.Maximized || this.WindowState == WindowState.Normal)
            {
                this.Show();
                this.Activate();
            }
        }

        private void SheetsCombox_DropDownOpened(object sender, EventArgs e)
        {
            var dbConnStr = $"Server={IpTxtBox.Text};Uid={UserTxtBox.Text};Pwd={PwdTxtBox.Text};"; 

            var dbConn = new MySqlConnection(dbConnStr);
            try
            {
                dbConn.Open();
                DataTable databases = dbConn.GetSchema("Databases");
                foreach (DataRow database in databases.Rows)
                {
                    string databaseName = database.Field<string>("database_name");
                    Console.WriteLine(databaseName);
                    DbSheetNameLst.Add(databaseName);
                }
                dbConn.Close();
            }
            catch (Exception ex)
            {
                dbConn.Close();
            }
        }
        private string ConcatDbConnStr(string ip,string port,string user,string pwd,string database = "master")
        {
            return $"Data Source={ip};Port={port};Initial catalog={database};User Id={user};Password={pwd};";
        }
    }
}
