using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DataViewConfig.Commands;

namespace DataViewConfig.ViewModels.Examples
{
    /// <summary>
    /// 示例页面ViewModel - 展示新架构的使用方法
    /// </summary>
    public class ExamplePageViewModel : ViewModelBase
    {
        #region 私有字段

        private string _title;
        private string _description;
        private bool _isEnabled;
        private ObservableCollection<string> _items;

        #endregion

        #region 构造函数

        public ExamplePageViewModel()
        {
            // 初始化属性
            Title = "示例页面";
            Description = "这是一个展示新架构的示例页面";
            IsEnabled = true;
            Items = new ObservableCollection<string>();

            // 初始化命令
            InitializeCommands();

            // 加载数据
            LoadData();
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 页面标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// 页面描述
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }

        /// <summary>
        /// 项目列表
        /// </summary>
        public ObservableCollection<string> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        #endregion

        #region 命令

        public ICommand LoadDataCommand { get; private set; }
        public ICommand SaveDataCommand { get; private set; }
        public ICommand AddItemCommand { get; private set; }
        public ICommand RemoveItemCommand { get; private set; }
        public ICommand TestConnectionCommand { get; private set; }

        #endregion

        #region 私有方法

        private void InitializeCommands()
        {
            LoadDataCommand = new RelayCommand(
                execute: () => SafeExecute(LoadData, "LoadData"),
                canExecute: () => !IsBusy
            );

            SaveDataCommand = new RelayCommand(
                execute: () => SafeExecute(SaveData, "SaveData"),
                canExecute: () => !IsBusy && IsEnabled
            );

            AddItemCommand = new RelayCommand(
                execute: param => SafeExecute(() => AddItem(param as string), "AddItem"),
                canExecute: param => !IsBusy && IsEnabled
            );

            RemoveItemCommand = new RelayCommand(
                execute: param => SafeExecute(() => RemoveItem(param as string), "RemoveItem"),
                canExecute: param => !IsBusy && param != null
            );

            TestConnectionCommand = new RelayCommand(
                execute: () => SafeExecute(TestDatabaseConnection, "TestConnection"),
                canExecute: () => !IsBusy
            );
        }

        private void LoadData()
        {
            StatusMessage = "正在加载数据...";
            IsLoading = true;

            try
            {
                // 模拟数据加载
                Items.Clear();
                Items.Add("项目 1");
                Items.Add("项目 2");
                Items.Add("项目 3");

                // 使用应用状态服务
                var stateService = StateService;
                if (stateService != null)
                {
                    var currentLang = stateService.CurrentLanguage;
                    StatusMessage = "数据加载完成 - 当前语言: " + currentLang;
                }
                else
                {
                    StatusMessage = "数据加载完成";
                }
            }
            catch (Exception ex)
            {
                var errorService = ErrorService;
                if (errorService != null)
                {
                    errorService.HandleError(ex, "LoadData");
                }
                StatusMessage = "数据加载失败: " + ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void SaveData()
        {
            StatusMessage = "正在保存数据...";

            try
            {
                // 模拟数据保存
                System.Threading.Thread.Sleep(1000);

                var errorService = ErrorService;
                if (errorService != null)
                {
                    errorService.ShowSuccess("数据保存成功！");
                }

                StatusMessage = "数据保存完成";
            }
            catch (Exception ex)
            {
                StatusMessage = "数据保存失败: " + ex.Message;
                throw;
            }
        }

        private void AddItem(string itemName)
        {
            if (string.IsNullOrEmpty(itemName))
            {
                itemName = "新项目 " + (Items.Count + 1);
            }

            Items.Add(itemName);
            StatusMessage = "已添加项目: " + itemName;
        }

        private void RemoveItem(string itemName)
        {
            if (!string.IsNullOrEmpty(itemName) && Items.Contains(itemName))
            {
                Items.Remove(itemName);
                StatusMessage = "已移除项目: " + itemName;
            }
        }

        private void TestDatabaseConnection()
        {
            StatusMessage = "正在测试数据库连接...";

            try
            {
                var dbService = DbService;
                if (dbService != null)
                {
                    var isConnected = dbService.TestConnection();
                    var message = isConnected ? "数据库连接成功！" : "数据库连接失败！";
                    
                    var errorService = ErrorService;
                    if (errorService != null)
                    {
                        if (isConnected)
                        {
                            errorService.ShowSuccess(message);
                        }
                        else
                        {
                            errorService.ShowWarning(message);
                        }
                    }

                    StatusMessage = message;
                }
                else
                {
                    StatusMessage = "数据库服务不可用";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = "数据库连接测试失败: " + ex.Message;
                throw;
            }
        }

        #endregion

        #region 重写方法

        public override void Initialize()
        {
            base.Initialize();
            
            // 订阅应用状态变更事件
            var stateService = StateService;
            if (stateService != null)
            {
                stateService.StateChanged += OnApplicationStateChanged;
            }
        }

        public override void Cleanup()
        {
            // 取消订阅事件
            var stateService = StateService;
            if (stateService != null)
            {
                stateService.StateChanged -= OnApplicationStateChanged;
            }

            base.Cleanup();
        }

        #endregion

        #region 事件处理

        private void OnApplicationStateChanged(object sender, DataViewConfig.Interfaces.StateChangedEventArgs e)
        {
            // 响应应用状态变更
            if (e.PropertyName == "CurrentLanguage")
            {
                StatusMessage = "语言已切换到: " + e.NewValue;
            }
        }

        #endregion
    }
}
