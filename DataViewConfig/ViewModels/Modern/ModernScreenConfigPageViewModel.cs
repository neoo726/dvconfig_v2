using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DataViewConfig.Commands;
using DataViewConfig.Interfaces;
using DataViewConfig.Models;
using DataViewConfig.Models.Entities;
using DataViewConfig.Repositories;
using DataViewConfig.Services;

namespace DataViewConfig.ViewModels.Modern
{
    /// <summary>
    /// 现代化的画面配置页面ViewModel - 使用新架构
    /// </summary>
    public class ModernScreenConfigPageViewModel : ViewModelBase
    {
        #region 私有字段

        private readonly IScreenRepository _screenRepository;
        private string _screenInternalName;
        private string _screenCswName;
        private string _screenDesc;
        private string _searchScreenTxt;
        private string _searchScreenType;
        private ObservableCollection<ScreenModel> _screenList;

        #endregion

        #region 构造函数

        public ModernScreenConfigPageViewModel()
        {
            // 获取Repository服务
            _screenRepository = ServiceLocator.GetService<IScreenRepository>();

            // 初始化属性
            ScreenList = new ObservableCollection<ScreenModel>();

            // 初始化命令
            InitializeCommands();

            // 异步加载数据
            LoadDataAsync();
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 画面内部名称
        /// </summary>
        public string ScreenInternalName
        {
            get { return _screenInternalName; }
            set { SetProperty(ref _screenInternalName, value); }
        }

        /// <summary>
        /// CSW文件名
        /// </summary>
        public string ScreenCswName
        {
            get { return _screenCswName; }
            set { SetProperty(ref _screenCswName, value); }
        }

        /// <summary>
        /// 画面描述
        /// </summary>
        public string ScreenDesc
        {
            get { return _screenDesc; }
            set { SetProperty(ref _screenDesc, value); }
        }

        /// <summary>
        /// 搜索画面文本
        /// </summary>
        public string SearchScreenTxt
        {
            get { return _searchScreenTxt; }
            set { SetProperty(ref _searchScreenTxt, value); }
        }

        /// <summary>
        /// 搜索画面类型
        /// </summary>
        public string SearchScreenType
        {
            get { return _searchScreenType; }
            set { SetProperty(ref _searchScreenType, value); }
        }

        /// <summary>
        /// 画面列表
        /// </summary>
        public ObservableCollection<ScreenModel> ScreenList
        {
            get { return _screenList; }
            set { SetProperty(ref _screenList, value); }
        }

        #endregion

        #region 命令

        public ICommand QueryCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        public ICommand AddNewCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化命令
        /// </summary>
        private void InitializeCommands()
        {
            QueryCommand = new RelayCommand(
                execute: () => SafeExecute(QueryScreens, "QueryScreens"),
                canExecute: () => !IsBusy
            );

            ResetCommand = new RelayCommand(
                execute: () => SafeExecute(ResetQuery, "ResetQuery"),
                canExecute: () => !IsBusy
            );

            AddNewCommand = new RelayCommand(
                execute: () => SafeExecute(AddNewScreen, "AddNewScreen"),
                canExecute: () => !IsBusy && !string.IsNullOrEmpty(ScreenInternalName) && !string.IsNullOrEmpty(ScreenCswName)
            );

            EditCommand = new RelayCommand(
                execute: param => SafeExecute(() => EditScreen(param as ScreenModel), "EditScreen"),
                canExecute: param => !IsBusy && param is ScreenModel
            );

            DeleteCommand = new RelayCommand(
                execute: param => SafeExecute(() => DeleteScreen(param as ScreenModel), "DeleteScreen"),
                canExecute: param => !IsBusy && param is ScreenModel
            );

            RefreshCommand = new RelayCommand(
                execute: () => SafeExecute(RefreshScreenList, "RefreshScreenList"),
                canExecute: () => !IsBusy
            );
        }

        /// <summary>
        /// 异步加载数据
        /// </summary>
        private async void LoadDataAsync()
        {
            await Task.Run(() =>
            {
                SafeExecute(() =>
                {
                    IsLoading = true;
                    StatusMessage = "正在加载画面数据...";

                    // 加载画面列表
                    RefreshScreenList();

                    StatusMessage = "画面数据加载完成";
                }, "LoadData");
            });
        }

        /// <summary>
        /// 刷新画面列表
        /// </summary>
        private void RefreshScreenList()
        {
            try
            {
                IsLoading = true;

                // 使用Repository进行查询
                var screens = _screenRepository.Search(SearchScreenTxt, SearchScreenType);

                // 转换为ScreenModel
                var screenModels = screens.Select(s => new ScreenModel
                {
                    dv_screen_id = s.dv_screen_id,
                    dv_screen_internal_name = s.ScreenInternalName,
                    dv_screen_csw_name = s.ScreenCswName,
                    dv_screen_desc = s.ScreenDesc,
                    dv_screen_type = DetermineScreenType(s.ScreenCswName)
                }).ToList();

                // 更新UI
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ScreenList.Clear();
                    foreach (var screen in screenModels)
                    {
                        ScreenList.Add(screen);
                    }
                }));

                StatusMessage = "已加载 " + screenModels.Count + " 个画面";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// 确定画面类型
        /// </summary>
        private string DetermineScreenType(string cswName)
        {
            if (string.IsNullOrEmpty(cswName))
                return "未知";

            return cswName.Contains(",") ? "组合画面" : "单个画面";
        }

        /// <summary>
        /// 查询画面
        /// </summary>
        private void QueryScreens()
        {
            StatusMessage = "正在搜索画面...";
            RefreshScreenList();
        }

        /// <summary>
        /// 重置查询
        /// </summary>
        private void ResetQuery()
        {
            SearchScreenTxt = string.Empty;
            SearchScreenType = string.Empty;
            
            QueryScreens();
        }

        /// <summary>
        /// 添加新画面
        /// </summary>
        private void AddNewScreen()
        {
            // 验证输入
            if (string.IsNullOrEmpty(ScreenInternalName))
            {
                ErrorService.ShowWarning("请输入画面内部名称！");
                return;
            }

            if (string.IsNullOrEmpty(ScreenCswName))
            {
                ErrorService.ShowWarning("请输入CSW文件名！");
                return;
            }

            // 检查是否已存在
            if (_screenRepository.IsInternalNameExists(ScreenInternalName))
            {
                ErrorService.ShowWarning("画面内部名称已存在！");
                return;
            }

            // 创建新画面
            var newScreen = new ScreenEntity
            {
                ScreenInternalName = ScreenInternalName,
                ScreenCswName = ScreenCswName,
                ScreenDesc = ScreenDesc ?? string.Empty,
                ScreenType = DetermineScreenType(ScreenCswName)
            };

            // 保存到数据库
            if (_screenRepository.Add(newScreen))
            {
                ErrorService.ShowSuccess("画面添加成功！");
                
                // 清空输入
                ClearInputFields();
                
                // 刷新列表
                RefreshScreenList();
            }
            else
            {
                ErrorService.ShowError("画面添加失败！");
            }
        }

        /// <summary>
        /// 编辑画面
        /// </summary>
        private void EditScreen(ScreenModel screenModel)
        {
            if (screenModel == null) return;

            // TODO: 实现编辑功能，可以打开编辑弹窗
            StatusMessage = "编辑画面: " + screenModel.dv_screen_internal_name;
        }

        /// <summary>
        /// 删除画面
        /// </summary>
        private void DeleteScreen(ScreenModel screenModel)
        {
            if (screenModel == null) return;

            // 确认删除
            var result = System.Windows.MessageBox.Show(
                "确认要删除画面 '" + screenModel.dv_screen_internal_name + "' 吗？",
                "确认删除",
                System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Question);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                if (_screenRepository.DeleteById(screenModel.dv_screen_id))
                {
                    ErrorService.ShowSuccess("画面删除成功！");
                    RefreshScreenList();
                }
                else
                {
                    ErrorService.ShowError("画面删除失败！");
                }
            }
        }

        /// <summary>
        /// 清空输入字段
        /// </summary>
        private void ClearInputFields()
        {
            ScreenInternalName = string.Empty;
            ScreenCswName = string.Empty;
            ScreenDesc = string.Empty;
        }

        #endregion

        #region 重写方法

        public override void Initialize()
        {
            base.Initialize();
            StatusMessage = "画面配置页面已初始化";
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }

        #endregion
    }

    /// <summary>
    /// 画面模型 - 兼容现有代码
    /// </summary>
    public class ScreenModel
    {
        public int dv_screen_id { get; set; }
        public string dv_screen_internal_name { get; set; }
        public string dv_screen_csw_name { get; set; }
        public string dv_screen_desc { get; set; }
        public string dv_screen_type { get; set; }
    }
}
