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
    /// 现代化的标签配置页面ViewModel - 使用新架构
    /// </summary>
    public class ModernTagConfigPageViewModel : ViewModelBase
    {
        #region 私有字段

        private readonly ITagRepository _tagRepository;
        private string _tagInternalName;
        private string _tagRealName;
        private string _tagDesc;
        private string _searchTagName;
        private string _searchMacro;
        private string _searchTagType;
        private string _searchTagPrefix;
        private string _relatedMacroName;
        private string _relatedTagInternalName;
        private ModernTagDataType _curTagDataType;
        private string _curTagPostFixType;
        private bool _tagNameKeepSame;
        private bool _isTagNameEditable;
        private ObservableCollection<TagModel> _tagList;

        #endregion

        #region 构造函数

        public ModernTagConfigPageViewModel()
        {
            // 获取Repository服务
            _tagRepository = ServiceLocator.GetService<ITagRepository>();

            // 初始化属性
            TagNameKeepSame = true;
            TagList = new ObservableCollection<TagModel>();

            // 初始化命令
            InitializeCommands();

            // 异步加载数据
            LoadDataAsync();
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 标签内部名称
        /// </summary>
        public string TagInternalName
        {
            get { return _tagInternalName; }
            set { SetProperty(ref _tagInternalName, value); }
        }

        /// <summary>
        /// 标签显示名称
        /// </summary>
        public string TagRealName
        {
            get { return _tagRealName; }
            set 
            { 
                if (SetProperty(ref _tagRealName, value))
                {
                    // 如果保持同步，则更新内部名称
                    if (TagNameKeepSame)
                    {
                        TagInternalName = value;
                    }
                }
            }
        }

        /// <summary>
        /// 标签描述
        /// </summary>
        public string TagDesc
        {
            get { return _tagDesc; }
            set { SetProperty(ref _tagDesc, value); }
        }

        /// <summary>
        /// 搜索标签名称
        /// </summary>
        public string SearchTagName
        {
            get { return _searchTagName; }
            set { SetProperty(ref _searchTagName, value); }
        }

        /// <summary>
        /// 搜索宏名称
        /// </summary>
        public string SearchMacro
        {
            get { return _searchMacro; }
            set { SetProperty(ref _searchMacro, value); }
        }

        /// <summary>
        /// 搜索标签类型
        /// </summary>
        public string SearchTagType
        {
            get { return _searchTagType; }
            set { SetProperty(ref _searchTagType, value); }
        }

        /// <summary>
        /// 搜索标签前缀
        /// </summary>
        public string SearchTagPrefix
        {
            get { return _searchTagPrefix; }
            set { SetProperty(ref _searchTagPrefix, value); }
        }

        /// <summary>
        /// 关联宏名称
        /// </summary>
        public string RelatedMacroName
        {
            get { return _relatedMacroName; }
            set { SetProperty(ref _relatedMacroName, value); }
        }

        /// <summary>
        /// 关联标签内部名称
        /// </summary>
        public string RelatedTagInternalName
        {
            get { return _relatedTagInternalName; }
            set { SetProperty(ref _relatedTagInternalName, value); }
        }

        /// <summary>
        /// 当前标签数据类型
        /// </summary>
        public ModernTagDataType CurTagDataType
        {
            get { return _curTagDataType; }
            set { SetProperty(ref _curTagDataType, value); }
        }

        /// <summary>
        /// 当前标签后缀类型
        /// </summary>
        public string CurTagPostFixType
        {
            get { return _curTagPostFixType; }
            set { SetProperty(ref _curTagPostFixType, value); }
        }

        /// <summary>
        /// 标签名称保持同步
        /// </summary>
        public bool TagNameKeepSame
        {
            get { return _tagNameKeepSame; }
            set { SetProperty(ref _tagNameKeepSame, value); }
        }

        /// <summary>
        /// 标签名称是否可编辑
        /// </summary>
        public bool IsTagNameEditable
        {
            get { return _isTagNameEditable; }
            set { SetProperty(ref _isTagNameEditable, value); }
        }

        /// <summary>
        /// 标签列表
        /// </summary>
        public ObservableCollection<TagModel> TagList
        {
            get { return _tagList; }
            set { SetProperty(ref _tagList, value); }
        }

        #endregion

        #region 命令

        public ICommand QueryCommand { get; private set; }
        public ICommand ResetCommand { get; private set; }
        public ICommand AddNewCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ImportDataCommand { get; private set; }
        public ICommand ExportDataCommand { get; private set; }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化命令
        /// </summary>
        private void InitializeCommands()
        {
            QueryCommand = new RelayCommand(
                execute: () => SafeExecute(QueryTags, "QueryTags"),
                canExecute: () => !IsBusy
            );

            ResetCommand = new RelayCommand(
                execute: () => SafeExecute(ResetQuery, "ResetQuery"),
                canExecute: () => !IsBusy
            );

            AddNewCommand = new RelayCommand(
                execute: () => SafeExecute(AddNewTag, "AddNewTag"),
                canExecute: () => !IsBusy && !string.IsNullOrEmpty(TagInternalName)
            );

            EditCommand = new RelayCommand(
                execute: param => SafeExecute(() => EditTag(param as TagModel), "EditTag"),
                canExecute: param => !IsBusy && param is TagModel
            );

            DeleteCommand = new RelayCommand(
                execute: param => SafeExecute(() => DeleteTag(param as TagModel), "DeleteTag"),
                canExecute: param => !IsBusy && param is TagModel
            );

            ImportDataCommand = new RelayCommand(
                execute: () => SafeExecute(ImportData, "ImportData"),
                canExecute: () => !IsBusy
            );

            ExportDataCommand = new RelayCommand(
                execute: () => SafeExecute(ExportData, "ExportData"),
                canExecute: () => !IsBusy && TagList != null && TagList.Count > 0
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
                    StatusMessage = "正在加载标签数据...";

                    // 加载标签列表
                    RefreshTagList();

                    StatusMessage = "标签数据加载完成";
                }, "LoadData");
            });
        }

        /// <summary>
        /// 刷新标签列表
        /// </summary>
        private void RefreshTagList()
        {
            try
            {
                IsLoading = true;

                // 使用Repository进行查询
                var tags = _tagRepository.GetAll();

                // 应用搜索过滤
                if (!string.IsNullOrEmpty(SearchTagName))
                {
                    tags = tags.Where(t => 
                        t.TagInternalName.ToLower().Contains(SearchTagName.ToLower()) ||
                        t.TagName.ToLower().Contains(SearchTagName.ToLower()) ||
                        t.TagDesc.ToLower().Contains(SearchTagName.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(SearchMacro))
                {
                    tags = tags.Where(t => 
                        !string.IsNullOrEmpty(t.RelatedMacroName) &&
                        t.RelatedMacroName.ToLower().Contains(SearchMacro.ToLower())).ToList();
                }

                // 转换为TagModel
                var tagModels = tags.Select(t => new TagModel
                {
                    Id = t.id,
                    TagInternalName = t.TagInternalName,
                    TagName = t.TagName,
                    Desc = t.TagDesc,
                    // DataType = (ModernTagDataType)t.DataTypeId,
                    // Postfix = (ModernTagPostfixType)t.PostfixTypeId,
                    RelatedMacroName = t.RelatedMacroName,
                    RelatedTagInternalName = t.RelatedTagInternalName,
                    TagNameEditable = t.IsInternalNameEditable,
                    RelatedSystemIds = t.RelatedSystemId
                }).ToList();

                // 更新UI
                App.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    TagList.Clear();
                    foreach (var tag in tagModels)
                    {
                        TagList.Add(tag);
                    }
                }));

                StatusMessage = "已加载 " + tagModels.Count + " 个标签";
            }
            finally
            {
                IsLoading = false;
            }
        }

        /// <summary>
        /// 查询标签
        /// </summary>
        private void QueryTags()
        {
            StatusMessage = "正在搜索标签...";
            RefreshTagList();
        }

        /// <summary>
        /// 重置查询
        /// </summary>
        private void ResetQuery()
        {
            SearchTagName = string.Empty;
            SearchMacro = string.Empty;
            SearchTagType = string.Empty;
            SearchTagPrefix = string.Empty;
            
            QueryTags();
        }

        /// <summary>
        /// 添加新标签
        /// </summary>
        private void AddNewTag()
        {
            // 验证输入
            if (string.IsNullOrEmpty(TagInternalName))
            {
                ErrorService.ShowWarning("请输入标签内部名称！");
                return;
            }

            // 检查是否已存在
            if (_tagRepository.IsInternalNameExists(TagInternalName))
            {
                ErrorService.ShowWarning("标签内部名称已存在！");
                return;
            }

            // 创建新标签
            var newTag = new TagEntity
            {
                TagInternalName = TagInternalName,
                TagName = TagRealName ?? TagInternalName,
                TagDesc = TagDesc ?? string.Empty,
                DataTypeId = (int)CurTagDataType,
                PostfixTypeId = 1, // 默认值，需要根据实际情况调整
                RelatedMacroName = RelatedMacroName,
                RelatedTagInternalName = RelatedTagInternalName,
                IsInternalNameEditable = IsTagNameEditable
            };

            // 保存到数据库
            if (_tagRepository.Add(newTag))
            {
                ErrorService.ShowSuccess("标签添加成功！");
                
                // 清空输入
                ClearInputFields();
                
                // 刷新列表
                RefreshTagList();
            }
            else
            {
                ErrorService.ShowError("标签添加失败！");
            }
        }

        /// <summary>
        /// 编辑标签
        /// </summary>
        private void EditTag(TagModel tagModel)
        {
            if (tagModel == null) return;

            // TODO: 实现编辑功能，可以打开编辑弹窗
            StatusMessage = "编辑标签: " + tagModel.TagName;
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        private void DeleteTag(TagModel tagModel)
        {
            if (tagModel == null) return;

            // 确认删除
            var result = System.Windows.MessageBox.Show(
                "确认要删除标签 '" + tagModel.TagName + "' 吗？",
                "确认删除",
                System.Windows.MessageBoxButton.YesNo,
                System.Windows.MessageBoxImage.Question);

            if (result == System.Windows.MessageBoxResult.Yes)
            {
                if (_tagRepository.DeleteById(tagModel.Id))
                {
                    ErrorService.ShowSuccess("标签删除成功！");
                    RefreshTagList();
                }
                else
                {
                    ErrorService.ShowError("标签删除失败！");
                }
            }
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        private void ImportData()
        {
            // TODO: 实现导入功能
            StatusMessage = "导入功能开发中...";
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        private void ExportData()
        {
            // TODO: 实现导出功能
            StatusMessage = "导出功能开发中...";
        }

        /// <summary>
        /// 清空输入字段
        /// </summary>
        private void ClearInputFields()
        {
            TagInternalName = string.Empty;
            TagRealName = string.Empty;
            TagDesc = string.Empty;
            RelatedMacroName = string.Empty;
            RelatedTagInternalName = string.Empty;
        }

        #endregion

        #region 重写方法

        public override void Initialize()
        {
            base.Initialize();
            StatusMessage = "标签配置页面已初始化";
        }

        public override void Cleanup()
        {
            base.Cleanup();
        }

        #endregion
    }
}
