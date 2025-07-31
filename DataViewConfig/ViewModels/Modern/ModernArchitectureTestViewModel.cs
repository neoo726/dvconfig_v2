using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using DataViewConfig.Commands;
using DataViewConfig.Interfaces;
using DataViewConfig.Models;
using DataViewConfig.Repositories;
using DataViewConfig.Services;

namespace DataViewConfig.ViewModels.Modern
{
    /// <summary>
    /// 现代化架构测试ViewModel
    /// </summary>
    public class ModernArchitectureTestViewModel : ViewModelBase
    {
        #region 私有字段

        private readonly ITagRepository _tagRepository;
        private readonly IScreenRepository _screenRepository;
        private readonly IBlockRepository _blockRepository;
        
        private int _tagCount;
        private int _screenCount;
        private int _blockCount;
        private ObservableCollection<string> _logMessages;
        private ObservableCollection<TagModel> _tagSampleData;
        private ObservableCollection<ScreenModel> _screenSampleData;
        private ObservableCollection<BlockModel> _blockSampleData;

        #endregion

        #region 构造函数

        public ModernArchitectureTestViewModel()
        {
            // 获取Repository服务
            _tagRepository = ServiceLocator.GetService<ITagRepository>();
            _screenRepository = ServiceLocator.GetService<IScreenRepository>();
            _blockRepository = ServiceLocator.GetService<IBlockRepository>();

            // 初始化集合
            LogMessages = new ObservableCollection<string>();
            TagSampleData = new ObservableCollection<TagModel>();
            ScreenSampleData = new ObservableCollection<ScreenModel>();
            BlockSampleData = new ObservableCollection<BlockModel>();

            // 初始化命令
            InitializeCommands();

            // 添加欢迎消息
            AddLogMessage("🚀 现代化架构测试页面已初始化");
            AddLogMessage("✅ Repository服务已注入");
            
            StatusMessage = "准备就绪 - 可以开始测试新架构";
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 标签数量
        /// </summary>
        public int TagCount
        {
            get { return _tagCount; }
            set { SetProperty(ref _tagCount, value); }
        }

        /// <summary>
        /// 画面数量
        /// </summary>
        public int ScreenCount
        {
            get { return _screenCount; }
            set { SetProperty(ref _screenCount, value); }
        }

        /// <summary>
        /// 堆场数量
        /// </summary>
        public int BlockCount
        {
            get { return _blockCount; }
            set { SetProperty(ref _blockCount, value); }
        }

        /// <summary>
        /// 日志消息
        /// </summary>
        public ObservableCollection<string> LogMessages
        {
            get { return _logMessages; }
            set { SetProperty(ref _logMessages, value); }
        }

        /// <summary>
        /// 标签示例数据
        /// </summary>
        public ObservableCollection<TagModel> TagSampleData
        {
            get { return _tagSampleData; }
            set { SetProperty(ref _tagSampleData, value); }
        }

        /// <summary>
        /// 画面示例数据
        /// </summary>
        public ObservableCollection<ScreenModel> ScreenSampleData
        {
            get { return _screenSampleData; }
            set { SetProperty(ref _screenSampleData, value); }
        }

        /// <summary>
        /// 堆场示例数据
        /// </summary>
        public ObservableCollection<BlockModel> BlockSampleData
        {
            get { return _blockSampleData; }
            set { SetProperty(ref _blockSampleData, value); }
        }

        /// <summary>
        /// 状态颜色
        /// </summary>
        public Brush StatusColor
        {
            get
            {
                if (IsLoading) return Brushes.Orange;
                if (HasError) return Brushes.Red;
                return Brushes.Green;
            }
        }

        #endregion

        #region 命令

        public ICommand TestRepositoryCommand { get; private set; }
        public ICommand LoadTagDataCommand { get; private set; }
        public ICommand LoadScreenDataCommand { get; private set; }
        public ICommand LoadBlockDataCommand { get; private set; }
        public ICommand RefreshAllCommand { get; private set; }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化命令
        /// </summary>
        private void InitializeCommands()
        {
            TestRepositoryCommand = new RelayCommand(
                execute: () => SafeExecute(TestRepository, "TestRepository"),
                canExecute: () => !IsBusy
            );

            LoadTagDataCommand = new RelayCommand(
                execute: () => SafeExecute(LoadTagData, "LoadTagData"),
                canExecute: () => !IsBusy
            );

            LoadScreenDataCommand = new RelayCommand(
                execute: () => SafeExecute(LoadScreenData, "LoadScreenData"),
                canExecute: () => !IsBusy
            );

            LoadBlockDataCommand = new RelayCommand(
                execute: () => SafeExecute(LoadBlockData, "LoadBlockData"),
                canExecute: () => !IsBusy
            );

            RefreshAllCommand = new RelayCommand(
                execute: () => SafeExecute(RefreshAll, "RefreshAll"),
                canExecute: () => !IsBusy
            );
        }

        /// <summary>
        /// 测试Repository功能
        /// </summary>
        private async void TestRepository()
        {
            AddLogMessage("🧪 开始测试Repository功能...");
            
            await Task.Run(() =>
            {
                // 测试所有Repository
                var testResult = RepositoryTestService.TestAllRepositories();
                
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (testResult)
                    {
                        AddLogMessage("✅ Repository测试通过！");
                        StatusMessage = "Repository功能正常";
                    }
                    else
                    {
                        AddLogMessage("❌ Repository测试失败！");
                        StatusMessage = "Repository功能异常";
                        HasError = true;
                    }
                }));
            });
        }

        /// <summary>
        /// 加载标签数据
        /// </summary>
        private async void LoadTagData()
        {
            AddLogMessage("📊 开始加载标签数据...");
            
            await Task.Run(() =>
            {
                try
                {
                    var tags = _tagRepository.GetAll();
                    var sampleTags = tags.Take(10).Select(t => new TagModel
                    {
                        Id = t.id,
                        TagInternalName = t.TagInternalName,
                        TagName = t.TagName,
                        Desc = t.TagDesc
                    }).ToList();

                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        TagSampleData.Clear();
                        foreach (var tag in sampleTags)
                        {
                            TagSampleData.Add(tag);
                        }
                        
                        TagCount = tags.Count;
                        AddLogMessage($"✅ 已加载 {tags.Count} 个标签，显示前 {sampleTags.Count} 个");
                        StatusMessage = $"标签数据加载完成 ({tags.Count} 个)";
                    }));
                }
                catch (Exception ex)
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        AddLogMessage($"❌ 加载标签数据失败: {ex.Message}");
                        StatusMessage = "标签数据加载失败";
                        HasError = true;
                    }));
                }
            });
        }

        /// <summary>
        /// 加载画面数据
        /// </summary>
        private async void LoadScreenData()
        {
            AddLogMessage("🖼️ 开始加载画面数据...");
            
            await Task.Run(() =>
            {
                try
                {
                    var screens = _screenRepository.GetAll();
                    var sampleScreens = screens.Take(10).Select(s => new ScreenModel
                    {
                        dv_screen_id = s.dv_screen_id,
                        dv_screen_internal_name = s.ScreenInternalName,
                        dv_screen_csw_name = s.ScreenCswName,
                        dv_screen_desc = s.ScreenDesc,
                        dv_screen_type = s.ScreenCswName?.Contains(",") == true ? "组合画面" : "单个画面"
                    }).ToList();

                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ScreenSampleData.Clear();
                        foreach (var screen in sampleScreens)
                        {
                            ScreenSampleData.Add(screen);
                        }
                        
                        ScreenCount = screens.Count;
                        AddLogMessage($"✅ 已加载 {screens.Count} 个画面，显示前 {sampleScreens.Count} 个");
                        StatusMessage = $"画面数据加载完成 ({screens.Count} 个)";
                    }));
                }
                catch (Exception ex)
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        AddLogMessage($"❌ 加载画面数据失败: {ex.Message}");
                        StatusMessage = "画面数据加载失败";
                        HasError = true;
                    }));
                }
            });
        }

        /// <summary>
        /// 加载堆场数据
        /// </summary>
        private async void LoadBlockData()
        {
            AddLogMessage("🏗️ 开始加载堆场数据...");
            
            await Task.Run(() =>
            {
                try
                {
                    var blocks = _blockRepository.GetAll();
                    var sampleBlocks = blocks.Take(10).Select(b => new BlockModel
                    {
                        BlockID = b.block_id,
                        BlockName = b.BlockName,
                        BlockDesc = b.BlockDesc,
                        BlockMinPos = b.BlockPosMin,
                        BlockMaxPos = b.BlockPosMax
                    }).ToList();

                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        BlockSampleData.Clear();
                        foreach (var block in sampleBlocks)
                        {
                            BlockSampleData.Add(block);
                        }
                        
                        BlockCount = blocks.Count;
                        AddLogMessage($"✅ 已加载 {blocks.Count} 个堆场，显示前 {sampleBlocks.Count} 个");
                        StatusMessage = $"堆场数据加载完成 ({blocks.Count} 个)";
                    }));
                }
                catch (Exception ex)
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        AddLogMessage($"❌ 加载堆场数据失败: {ex.Message}");
                        StatusMessage = "堆场数据加载失败";
                        HasError = true;
                    }));
                }
            });
        }

        /// <summary>
        /// 刷新全部数据
        /// </summary>
        private async void RefreshAll()
        {
            AddLogMessage("🔄 开始刷新全部数据...");
            
            // 重置错误状态
            HasError = false;
            
            // 依次加载所有数据
            LoadTagData();
            await Task.Delay(500);
            
            LoadScreenData();
            await Task.Delay(500);
            
            LoadBlockData();
            
            AddLogMessage("✅ 全部数据刷新完成");
            StatusMessage = "全部数据已刷新";
        }

        /// <summary>
        /// 添加日志消息
        /// </summary>
        private void AddLogMessage(string message)
        {
            var timestamp = DateTime.Now.ToString("HH:mm:ss");
            var logEntry = $"[{timestamp}] {message}";
            
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                LogMessages.Add(logEntry);
                
                // 保持日志数量在合理范围内
                while (LogMessages.Count > 100)
                {
                    LogMessages.RemoveAt(0);
                }
            }));
        }

        #endregion

        #region 重写方法

        public override void Initialize()
        {
            base.Initialize();
            AddLogMessage("🎯 测试页面初始化完成");
        }

        public override void Cleanup()
        {
            base.Cleanup();
            AddLogMessage("🧹 测试页面清理完成");
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            
            // 当状态相关属性变化时，通知状态颜色更新
            if (propertyName == nameof(IsLoading) || propertyName == nameof(HasError))
            {
                OnPropertyChanged(nameof(StatusColor));
            }
        }

        #endregion
    }
}
