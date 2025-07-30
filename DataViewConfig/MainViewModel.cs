using CMSCore;
using DataView_Configuration;
using DataViewConfig.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DataViewConfig
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        #region 主菜单
        public const string NaviConfigMenu = "main_menu_debug";
        public const string BaseConfigMenu = "main_menu_base_info";
        public const string BaseFuncMenu = "main_menu_func_config";
        public const string exSystemName = "main_menu_func_interactive_config";
        public const string AssistConfigMenu = "main_menu_func_config";
        public const string ExSystemConfigMenu = "main_menu_func_interactive_config";
        #endregion

        public event PropertyChangedEventHandler  PropertyChanged;
        public Dictionary<string, UIElement> PageDict { get; set; } = new Dictionary<string, UIElement>();
        public Dictionary<string, UIElement> ExSystemPageDict { get; set; } = new Dictionary<string, UIElement>();
        public ObservableCollection<DvSystemModel> ExSystemLst { get; set; }
        public List<Controls.MenuItem> SideMenuItemLst { get; set; }
        public UIElement MainContent { get; set; }
        public Command OpenPageCommand { get; set; }
        public Command OpenExSystemConfigPageCommand { get; set; }
        public Command MinimizeCommand { get; set; }
        public Command CloseCommand { get; set; }
        public Command CreateNewInteractiveSystemCommand { get; set; }
        public Command RemoveExSystemConfigCommand { get; set; }
        //根据当前用户角色判断是否显示该元素
        public bool IsShowItem { get; set; }
        public bool IsShowDataViewConfig { get; set; }
        public string CurrentProjectPath { get; set; }
        public string CurrentVersion { get; set; }
        public string ConfigDbVersion { get; set; }
        private UmsTypeEnum curLoginType;
        private MainWindow _mainWindow;
        public MainViewModel(MainWindow curMainWindow)
        {
            _mainWindow = curMainWindow;
            IsShowDataViewConfig = GlobalConfig.IsDataViewConfig;
            CurrentProjectPath = "当前工程路径：   " + Directory.GetParent(Environment.CurrentDirectory).FullName;
            CurrentVersion = "v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            IsShowItem = true;
            DataView_Configuration.DvSysmtemConfig.InitialConfig();
            curLoginType = DvLoginConfig.LoginType;
            // 加载语言配置
            LoadLanguageFromDatabase();
            RefreshExSystemLst();
            RefreshSideMenu();
            OpenPageCommand = new Command(OpenPage);
            OpenExSystemConfigPageCommand = new Command(ExSystemConfigPage);
            MinimizeCommand = new Command((o) =>
            {
                SystemCommands.MinimizeWindow((o as Window));
            });
            CloseCommand = new Command((o) =>
            {
                Application.Current.Shutdown();
            });
            CreateNewInteractiveSystemCommand = new Command((o) =>
            { 
                ExSystemConfigPage(null);
            });
            RemoveExSystemConfigCommand = new Command((o) =>
            {
                if(MessageBox.Show("确定要删除该交互对象吗？注意：删除后，该交互对象相关的接口、控件填充默认值等配置均会删除！","提示",MessageBoxButton.YesNo)== MessageBoxResult.No)
                {
                    return;
                }
                var systemModel = o as DvSystemModel;
                if (systemModel == null) return;
                if (systemModel.IsPermanent)
                {
                    MessageBox.Show("该交互对象为常驻对象，无法强制删除！");return;
                }
                try
                {
                    DbHelper.db.BeginTran();
                    DbHelper.db.Deleteable<DbModels.dv_system>()
              .Where(x => x.system_id == systemModel.SystemId).ExecuteCommand();
                    DbHelper.db.Deleteable<DbModels.dv_request_interface>()
                   .Where(x => x.system_id == systemModel.SystemId).ExecuteCommand();
                    DbHelper.db.Deleteable<DbModels.dv_receive_fanout>()
                   .Where(x => x.system_id == systemModel.SystemId).ExecuteCommand();
                    DbHelper.db.Deleteable<DbModels.dv_control_default_value>()
                  .Where(x => x.system_id == systemModel.SystemId).ExecuteCommand();
                    DbHelper.db.CommitTran();
                    RefreshExSystemLst();
                }
                catch (Exception ex)
                {
                    DbHelper.db.RollbackTran();
                    MessageBox.Show("删除失败！异常信息提示：" + ex.ToString(), "提示");
                }
            });
            
           
            EventBus.Instance.EventChangeEventHandler += Instance_EventChangeEventHandler;
            EventBus.Instance.LoginTypeEventHandler += Instance_LoginTypeEventHandler;
            EventBus.Instance.EditConfigPageOpenEventHandler += Instance_EditConfigPageOpenEventHandler;
            EventBus.Instance.DeleteExSystemEventHandler += Instance_DeleteExSystemEventHandler;
            EventBus.Instance.ExSystemEnableChangeEventHandler += Instance_ExSystemEnableChangeEventHandler;
            //EventBus.Instance.LanguageChangeEventHandler += Instance_LanguageChangeEventHandler;
        }
        private void LoadLanguageFromDatabase()
        {
            try
            {
                var curProjectConf = DbHelper.db.Queryable<DbModels.dv_project_conf>().ToList()[0];
                var language = curProjectConf.language.ToLower();
                if (language == "en-us")
                {
                    GlobalConfig.CurLanguage = GlobalLanguage.enUS;
                }
                else
                {
                    GlobalConfig.CurLanguage = GlobalLanguage.zhCN;
                }
                Utli.ChangeLanguage();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载语言设置失败！异常信息：{ex.Message}");
            }
        }
        /// <summary>
        /// 监听exSystem交互对象Enable变更
        /// </summary>
        /// <param name="exSystemName"></param>
        /// <param name="CommEnable"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Instance_ExSystemEnableChangeEventHandler(string exSystemName, bool CommEnable)
        {
            try
            {
                Task.Factory.StartNew(() =>
                {
                    var dbExSystem = DbHelper.db.Queryable<DbModels.dv_system>().Where(x => x.system_name == exSystemName).First();
                    if (dbExSystem == null) return;
                    dbExSystem.comm_enable = CommEnable;
                    var iRet = DbHelper.db.Updateable<DbModels.dv_system>(dbExSystem).
                            Where(x => x.system_id == dbExSystem.system_id).ExecuteCommand();
                });
                
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.ToString());
            }
        }

        private void Instance_DeleteExSystemEventHandler(string DeletedSystemName)
        {
            if (MessageBox.Show("确定要删除该交互对象吗？注意：删除后，该交互对象相关的接口、控件填充默认值等配置均会删除！", "提示", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            var systemModel = ExSystemLst.Where(x=>x.SystemName==DeletedSystemName).FirstOrDefault();
            if (systemModel == null) return;
            if (systemModel.IsPermanent)
            {
                MessageBox.Show("该交互对象为常驻对象，无法强制删除！"); return;
            }
            try
            {
                DbHelper.db.BeginTran();
                DbHelper.db.Deleteable<DbModels.dv_system>()
          .Where(x => x.system_id == systemModel.SystemId).ExecuteCommand();
                DbHelper.db.Deleteable<DbModels.dv_request_interface>()
               .Where(x => x.system_id == systemModel.SystemId).ExecuteCommand();
                DbHelper.db.Deleteable<DbModels.dv_receive_fanout>()
               .Where(x => x.system_id == systemModel.SystemId).ExecuteCommand();
                DbHelper.db.Deleteable<DbModels.dv_control_default_value>()
              .Where(x => x.system_id == systemModel.SystemId).ExecuteCommand();
                DbHelper.db.CommitTran();
                RefreshExSystemLst();
                RefreshSideMenu();
            }
            catch (Exception ex)
            {
                DbHelper.db.RollbackTran();
                MessageBox.Show("删除失败！异常信息提示：" + ex.ToString(), "提示");
            }
        }
        private void RefreshConfigDb()
        {
            
        }
        /// <summary>
        /// 刷新菜单栏
        /// </summary>
        private void RefreshSideMenu()
        {
            //侧边菜单栏内容
            SideMenuItemLst = new List<Controls.MenuItem>();
            SideMenuItemLst.Add(new Controls.MenuItem(new Controls.MenuModel(GetResourceString("main_menu_debug"), new List<Controls.MenuItemModel>()
                {
                    new Controls.MenuItemModel(){ Name=GetResourceString("main_menu_config_view"),RelatedConfigPageName="NaviConfigPage"},
                }), _mainWindow));
            var baseMenuItemList = new List<Controls.MenuItemModel>()
            {
                new Controls.MenuItemModel(){ Name=GetResourceString("main_menu_base_crane"),RelatedConfigPageName="CraneConfigPage"},
                new Controls.MenuItemModel(){ Name=GetResourceString("main_menu_base_ros"),RelatedConfigPageName="RcsConfigPage"},
            };
            var curProjectType = DbHelper.db.Queryable<DbModels.dv_project_conf>().First();
            if (curProjectType.project_type.ToLower() != "sts")
            {
                baseMenuItemList.Add(new Controls.MenuItemModel() { Name = GetResourceString("main_menu_base_block"), RelatedConfigPageName = "BlockConfigPage" });
            }
            SideMenuItemLst.Add(
                new Controls.MenuItem(new Controls.MenuModel(GetResourceString("main_menu_base_info"), baseMenuItemList), _mainWindow)
            );
           
           
            if (IsShowItem)
                {
                    SideMenuItemLst.Add(
                   new Controls.MenuItem(new Controls.MenuModel(GetResourceString("main_menu_project_config"), new List<Controls.MenuItemModel>()
                   {
                        new Controls.MenuItemModel(){ Name=GetResourceString("main_menu_project_tag"),RelatedConfigPageName="TagConfigPage"},
                        new Controls.MenuItemModel(){ Name=GetResourceString("main_menu_project_screen"),RelatedConfigPageName="ScreenConfigPage"},
                        new Controls.MenuItemModel(){ Name=GetResourceString("main_menu_project_control"),RelatedConfigPageName="ControlConfigPage"},
                   }), _mainWindow)
               );
            }
            SideMenuItemLst.Add(new Controls.MenuItem(new Controls.MenuModel(GetResourceString("main_menu_func_config"), new List<Controls.MenuItemModel>()
            {
                new Controls.MenuItemModel(){ Name=GetResourceString("main_menu_func_login"),RelatedConfigPageName="LoginConfigPage"},
                new Controls.MenuItemModel(){ Name=GetResourceString("main_menu_func_switch_screen"),RelatedConfigPageName="ScreenSwitchConfigPage"},
                new Controls.MenuItemModel(){ Name=GetResourceString("main_menu_func_tips_window"),RelatedConfigPageName="DvTipsConfigPage"},
            }), _mainWindow));
            var newExsystemMenuItemLst = new List<Controls.MenuItemModel>();
            //交互功能-常驻-参数列表
            newExsystemMenuItemLst.Add(new MenuItemModel()
            {
                Name = GetResourceString("main_menu_func_interactive_parameter_list"),
                RelatedConfigPageName = "InteractiveParameterConfigPage"
            });
            foreach (var item in ExSystemLst)
            {
                var newItemModel = new Controls.MenuItemModel()
                {
                    Name = item.SystemName,
                    IsDynamicCreated = true,
                    IsEnable = item.CommEnable,
                    ExSystemModel = item,
                };
                //设计角色&权限才显示删除功能
                if (!item.IsPermanent&&IsShowItem)
                {
                    newItemModel.IsDeleteEnable = true;
                }
                newItemModel.SubEventHandler();
                newExsystemMenuItemLst.Add(newItemModel);
            }
           
         
            if (IsShowItem)
            {
                SideMenuItemLst.Add(new Controls.MenuItem(new Controls.MenuModel(GetResourceString(ExSystemConfigMenu), newExsystemMenuItemLst), _mainWindow
                , true, new Action(() =>
                {
                    ExSystemConfigPage(null); 
                }))
                );
                //SideMenuItemLst.Add(new Controls.MenuItem(new Controls.MenuModel("辅助功能", new List<Controls.MenuItemModel>()
                //{
                //    new Controls.MenuItemModel(){ Name="组件提示",RelatedConfigPageName="TipsConfigPage"},
                //    //new Controls.MenuItemModel(){ Name="模板切换",RelatedConfigPageName="TemplateConfigPage"},
                //    //new Controls.MenuItemModel(){ Name="帮助手册",RelatedConfigPageName="HelpLinkConfigPage"},
                //}), _mainWindow));
            }
            else
            {
                SideMenuItemLst.Add(new Controls.MenuItem(new Controls.MenuModel(ExSystemConfigMenu, newExsystemMenuItemLst), _mainWindow
               , false, null)
               );
            }
            _mainWindow.Menu.Children.Clear();
            foreach (var item in SideMenuItemLst)
            {
                _mainWindow.Menu.Children.Add(item);
            }
        }
        private void Instance_EditConfigPageOpenEventHandler(EventBus.EventBusType eventType, string pageName)
        {
            switch (pageName)
            {
                case "crane":
                    List<ListViewItem> itemLst = Utli.FindVisualChild<ListViewItem>(_mainWindow.Menu);
                    foreach (ListViewItem item in itemLst)
                    {
                        var menuModel = item.DataContext as MenuItemModel;
                        if (menuModel.Name == "设备") 
                        {
                            item.Focus();
                            item.IsSelected = true;
                        }
                    }
                    OpenPage("CraneConfigPage");
                    break;
                case "rcs":
                    List<ListViewItem> itemLst1 = Utli.FindVisualChild<ListViewItem>(_mainWindow.Menu);
                    foreach (ListViewItem item in itemLst1)
                    {
                        var menuModel = item.DataContext as MenuItemModel;
                        if (menuModel.Name == "操作台")
                        {
                            item.Focus();
                            item.IsSelected = true;
                        }
                    }
                    OpenPage("RcsConfigPage");
                    break;
                case "projType":
                    break;
                default:
                    var systemInfo = ExSystemLst.Where(x => x.SystemName == pageName).FirstOrDefault();
                    if (systemInfo == null) return;
                    List<ListViewItem> itemLst12 = Utli.FindVisualChild<ListViewItem>(_mainWindow.Menu);
                    foreach (ListViewItem item in itemLst12)
                    {
                        var menuModel = item.DataContext as MenuItemModel;
                        if (menuModel.Name == systemInfo.SystemName)
                        {
                            item.Focus();
                            item.IsSelected = true;
                        }
                    }
                    ExSystemConfigPage(systemInfo);
                    break;
            }
          
        }

        private void Instance_LoginTypeEventHandler(UmsTypeEnum loginType)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                curLoginType = loginType;
                RefreshExSystemLst();
                RefreshSideMenu();
            }));
        }

        private void Instance_EventChangeEventHandler(EventBus.EventBusType eventType,string exSystemName=null,bool CommEnbale=false)
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                RefreshExSystemLst();
                RefreshSideMenu();
            }));
        }
        private void RefreshExSystemLst()
        {
            DataView_Configuration.DvSysmtemConfig.InitialConfig();
            ExSystemLst = new ObservableCollection<DvSystemModel>();
            var allSystem = DataView_Configuration.DvSysmtemConfig.dvSystemCommDict.Values.ToList();
            foreach (var item in allSystem)
            {
                switch (curLoginType)
                {
                    case UmsTypeEnum.UMS_LOCAL:
                        if (item.SystemName.ToLower() == "ums_dv" || item.SystemName.ToLower() == "ums_ecs")
                        {
                            continue;
                        }
                        break;
                    case UmsTypeEnum.UMS_DV:
                        if (item.SystemName.ToLower() == "ums_ecs")
                        {
                            continue;
                        }
                        break;
                    case UmsTypeEnum.UMS_ECS:
                        if (item.SystemName.ToLower() == "ums_dv")
                        {
                            continue;
                        }
                        break;
                    case UmsTypeEnum.UMS_OTHERR:
                        if (item.SystemName.ToLower() == "ums_dv" || item.SystemName.ToLower() == "ums_ecs")
                        {
                            continue;
                        }
                        break;
                }
                //if (item.IsPermanent)
                //{
                //    switch (curLoginType)
                //    {
                //        case UmsTypeEnum.UMS_LOCAL:
                //            if (item.SystemName.ToLower() == "ums_dv"|| item.SystemName.ToLower() == "ums_ecs")
                //            {
                //                continue;
                //            }
                //            break;
                //        case UmsTypeEnum.UMS_DV:
                //            if (item.SystemName.ToLower() == "ums_ecs")
                //            {
                //                continue;
                //            }
                //            break;
                //        case UmsTypeEnum.UMS_ECS:
                //            if (item.SystemName.ToLower() == "ums_dv")
                //            {
                //                continue;
                //            }
                //            break;
                //    }
                //}
                ExSystemLst.Add(item);
            }
            
        }
        public void ExSystemConfigPage(object o)
        {
            try
            {
                var dvSystem = o as DataView_Configuration.DvSystemModel;

                //反射创建
                Type type = Assembly.GetExecutingAssembly().GetType("DataViewConfig.Pages.InteractiveSystemConfigPage");
                if (o == null)
                {
                    MainContent = (UIElement)Activator.CreateInstance(type, dvSystem);

                }
                else
                {
                    //避免重复创建UIElement实例
                    var instance = Activator.CreateInstance(type, dvSystem);
                    if (!ExSystemPageDict.ContainsKey(o.ToString()))
                    {
                        ExSystemPageDict.Add(o.ToString(), (UIElement)instance);
                    }
                    else
                    {
                        ExSystemPageDict[o.ToString()] = (UIElement)instance;
                    }
                    MainContent = ExSystemPageDict[o.ToString()];
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MainContent"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载页面失败！错误信息：{ex.ToString()}");
            }
        }
        public void OpenPage(object o)
        {
            try
            {
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    //反射创建
                    Type type = Assembly.GetExecutingAssembly().GetType("DataViewConfig.Pages." + o.ToString());
                    //避免重复创建UIElement实例
                    if (!PageDict.ContainsKey(o.ToString()))
                    {
                        PageDict.Add(o.ToString(), (UIElement)Activator.CreateInstance(type));
                    }
                    MainContent = PageDict[o.ToString()];
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MainContent"));
                }));

            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载页面失败！错误信息：{ex.ToString()}");
            }
        }
        private string GetResourceString(string key)
        {
            try
            {
                var resource = Application.Current.Resources[key];
                if (resource != null)
                {
                    return resource.ToString();
                }
                return key;
            }
            catch (Exception ex)
            {
                LogHelper.Error($"获取资源字符串失败: {ex.Message}");
                return key;
            }
        }
        private void Instance_LanguageChangeEventHandler()
        {
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                RefreshSideMenu();
            }));
        }
    }
}
