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
    /// 现代化的堆场配置ViewModel - 使用新架构
    /// </summary>
    public class ModernBlockConfigViewModel : ViewModelBase
    {
        #region 私有字段

        private readonly IBlockRepository _blockRepository;
        private string _blockName;
        private int _blockPlcId;
        private string _blockDesc;
        private int _blockMinPos;
        private int _blockMaxPos;
        private string _searchBlockNameTxt;
        private string _searchBlockIDTxt;
        private ObservableCollection<BlockModel> _blockList;
        private BlockStatistics _statistics;

        #endregion

        #region 构造函数

        public ModernBlockConfigViewModel()
        {
            // 获取Repository服务
            _blockRepository = ServiceLocator.GetService<IBlockRepository>();

            // 初始化属性
            BlockList = new ObservableCollection<BlockModel>();

            // 初始化命令
            InitializeCommands();

            // 异步加载数据
            LoadDataAsync();
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 堆场名称
        /// </summary>
        public string BlockName
        {
            get { return _blockName; }
            set { SetProperty(ref _blockName, value); }
        }

        /// <summary>
        /// 堆场PLC ID
        /// </summary>
        public int BlockPlcId
        {
            get { return _blockPlcId; }
            set { SetProperty(ref _blockPlcId, value); }
        }

        /// <summary>
        /// 堆场描述
        /// </summary>
        public string BlockDesc
        {
            get { return _blockDesc; }
            set { SetProperty(ref _blockDesc, value); }
        }

        /// <summary>
        /// 最小位置
        /// </summary>
        public int BlockMinPos
        {
            get { return _blockMinPos; }
            set { SetProperty(ref _blockMinPos, value); }
        }

        /// <summary>
        /// 最大位置
        /// </summary>
        public int BlockMaxPos
        {
            get { return _blockMaxPos; }
            set { SetProperty(ref _blockMaxPos, value); }
        }

        /// <summary>
        /// 搜索堆场名称
        /// </summary>
        public string SearchBlockNameTxt
        {
            get { return _searchBlockNameTxt; }
            set { SetProperty(ref _searchBlockNameTxt, value); }
        }

        /// <summary>
        /// 搜索堆场ID
        /// </summary>
        public string SearchBlockIDTxt
        {
            get { return _searchBlockIDTxt; }
            set { SetProperty(ref _searchBlockIDTxt, value); }
        }

        /// <summary>
        /// 堆场列表
        /// </summary>
        public ObservableCollection<BlockModel> BlockList
        {
            get { return _blockList; }
            set { SetProperty(ref _blockList, value); }
        }

        /// <summary>
        /// 统计信息
        /// </summary>
        public BlockStatistics Statistics
        {
            get { return _statistics; }
            set { SetProperty(ref _statistics, value); }
        }

        #endregion

        #region 命令

        public ICommand QueryCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        public ICommand AddNewCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand ViewStatisticsCommand { get; private set; }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化命令
        /// </summary>
        private void InitializeCommands()
        {
            QueryCommand = new RelayCommand(
                execute: () => SafeExecute(QueryBlocks, "QueryBlocks"),
                canExecute: () => !IsBusy
            );

            ResetCommand = new RelayCommand(
                execute: () => SafeExecute(ResetQuery, "ResetQuery"),
                canExecute: () => !IsBusy
            );

            AddNewCommand = new RelayCommand(
                execute: () => SafeExecute(AddNewBlock, "AddNewBlock"),
                canExecute: () => !IsBusy && !string.IsNullOrEmpty(BlockName) && BlockPlcId > 0
            );

            EditCommand = new RelayCommand(
                execute: param => SafeExecute(() => EditBlock(param as BlockModel), "EditBlock"),
                canExecute: param => !IsBusy && param is BlockModel
            );

            DeleteCommand = new RelayCommand(
                execute: param => SafeExecute(() => DeleteBlock(param as BlockModel), "DeleteBlock"),
                canExecute: param => !IsBusy && param is BlockModel
            );

            RefreshCommand = new RelayCommand(
                execute: () => SafeExecute(RefreshBlockList, "RefreshBlockList"),
                canExecute: () => !IsBusy
            );

            ViewStatisticsCommand = new RelayCommand(
                execute: () => SafeExecute(LoadStatistics, "LoadStatistics"),
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
                    StatusMessage = "正在加载堆场数据...";

                    // 加载堆场列表
                    RefreshBlockList();

                    // 加载统计信息
                    LoadStatistics();

                    StatusMessage = "堆场数据加载完成";
                }, "LoadData");
            });
        }

        /// <summary>
        /// 刷新堆场列表
        /// </summary>
        private void RefreshBlockList()
        {
            try
            {
                IsLoading = true;

                // 使用Repository进行查询
                var blocks = _blockRepository.Search(SearchBlockNameTxt, SearchBlockIDTxt);

                // 转换为BlockModel
                var blockModels = blocks.Select(b => new BlockModel
                {
                    BlockID = b.block_id,
                    BlockName = b.BlockName,
                    BlockDesc = b.BlockDesc,
                    BlockMinPos = b.BlockPosMin,
                    BlockMaxPos = b.BlockPosMax
                }).ToList();

                // 更新UI
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    BlockList.Clear();
                    foreach (var block in blockModels)
                    {
                        BlockList.Add(block);
                    }
                }));

                StatusMessage = "已加载 " + blockModels.Count + " 个堆场";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// 加载统计信息
        /// </summary>
        private void LoadStatistics()
        {
            try
            {
                Statistics = _blockRepository.GetStatistics();
                StatusMessage = "统计信息已更新";
            }
            catch (Exception ex)
            {
                LogHelper.Error("LoadStatistics failed: " + ex.Message);
                ErrorService.ShowError("加载统计信息失败: " + ex.Message);
            }
        }

        /// <summary>
        /// 查询堆场
        /// </summary>
        private void QueryBlocks()
        {
            StatusMessage = "正在搜索堆场...";
            RefreshBlockList();
        }

        /// <summary>
        /// 重置查询
        /// </summary>
        private void ResetQuery()
        {
            SearchBlockNameTxt = string.Empty;
            SearchBlockIDTxt = string.Empty;
            
            QueryBlocks();
        }

        /// <summary>
        /// 添加新堆场
        /// </summary>
        private void AddNewBlock()
        {
            // 验证输入
            if (string.IsNullOrEmpty(BlockName))
            {
                ErrorService.ShowWarning("请输入堆场名称！");
                return;
            }

            if (BlockPlcId <= 0)
            {
                ErrorService.ShowWarning("请输入有效的堆场PLC ID！");
                return;
            }

            // 检查是否已存在
            if (_blockRepository.IsNameExists(BlockName))
            {
                ErrorService.ShowWarning("堆场名称已存在！");
                return;
            }

            if (_blockRepository.IsPlcIdExists(BlockPlcId))
            {
                ErrorService.ShowWarning("堆场PLC ID已存在！");
                return;
            }

            // 验证位置范围
            if (BlockMinPos > BlockMaxPos)
            {
                ErrorService.ShowWarning("最小位置不能大于最大位置！");
                return;
            }

            // 创建新堆场
            var newBlock = new BlockEntity
            {
                block_id = BlockPlcId,
                BlockPlcId = BlockPlcId,
                BlockName = BlockName,
                BlockDesc = BlockDesc ?? string.Empty,
                BlockPosMin = BlockMinPos,
                BlockPosMax = BlockMaxPos
            };

            // 保存到数据库
            if (_blockRepository.Add(newBlock))
            {
                ErrorService.ShowSuccess("堆场添加成功！");
                
                // 清空输入
                ClearInputFields();
                
                // 刷新列表和统计
                RefreshBlockList();
                LoadStatistics();
            }
            else
            {
                ErrorService.ShowError("堆场添加失败！");
            }
        }

        /// <summary>
        /// 编辑堆场
        /// </summary>
        private void EditBlock(BlockModel blockModel)
        {
            if (blockModel == null) return;

            // TODO: 实现编辑功能，可以打开编辑弹窗
            StatusMessage = "编辑堆场: " + blockModel.BlockName;
        }

        /// <summary>
        /// 删除堆场
        /// </summary>
        private void DeleteBlock(BlockModel blockModel)
        {
            if (blockModel == null) return;

            // 确认删除
            var result = System.Windows.MessageBox.Show(
                "确认要删除堆场 '" + blockModel.BlockName + "' 吗？",
                "确认删除",
                System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Question);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                if (_blockRepository.DeleteById(blockModel.BlockID))
                {
                    ErrorService.ShowSuccess("堆场删除成功！");
                    RefreshBlockList();
                    LoadStatistics();
                }
                else
                {
                    ErrorService.ShowError("堆场删除失败！");
                }
            }
        }

        /// <summary>
        /// 清空输入字段
        /// </summary>
        private void ClearInputFields()
        {
            BlockName = string.Empty;
            BlockPlcId = 0;
            BlockDesc = string.Empty;
            BlockMinPos = 0;
            BlockMaxPos = 0;
        }

        #endregion

        #region 重写方法

        public override void Initialize()
        {
            base.Initialize();
            StatusMessage = "堆场配置页面已初始化";
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }

        #endregion
    }

    /// <summary>
    /// 堆场模型 - 兼容现有代码
    /// </summary>
    public class BlockModel
    {
        public int BlockID { get; set; }
        public string BlockName { get; set; }
        public string BlockDesc { get; set; }
        public int BlockMinPos { get; set; }
        public int BlockMaxPos { get; set; }
        public int BlockBayCount { get; set; }
    }
}
