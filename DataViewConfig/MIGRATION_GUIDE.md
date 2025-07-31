# DataView Config Tool V2 - 代码迁移指南

## 🎯 迁移目标

本指南帮助开发人员将现有的ViewModel逐步迁移到新的Repository架构模式。

## 📋 迁移步骤

### 第一步：分析现有ViewModel

在迁移之前，先分析现有ViewModel的结构：

```csharp
// 现有ViewModel示例
public class TagConfigPageViewModel : INotifyPropertyChanged
{
    // 直接数据库访问
    private void LoadTags()
    {
        var tags = DbHelper.db.Queryable<DbModels.dv_tag>().ToList();
        // ...
    }
    
    // 简单命令
    public ICommand QueryCommand => new Command(QueryTags);
}
```

### 第二步：创建现代化ViewModel

```csharp
// 新的现代化ViewModel
public class ModernTagConfigPageViewModel : ViewModelBase
{
    #region 私有字段
    private readonly ITagRepository _tagRepository;
    #endregion
    
    #region 构造函数
    public ModernTagConfigPageViewModel()
    {
        // 1. 获取Repository服务
        _tagRepository = ServiceLocator.GetService<ITagRepository>();
        
        // 2. 初始化命令
        InitializeCommands();
        
        // 3. 异步加载数据
        LoadDataAsync();
    }
    #endregion
    
    #region 命令初始化
    private void InitializeCommands()
    {
        QueryCommand = new RelayCommand(
            execute: () => SafeExecute(QueryTags, "QueryTags"),
            canExecute: () => !IsBusy
        );
    }
    #endregion
    
    #region 数据操作
    private async void LoadDataAsync()
    {
        await Task.Run(() =>
        {
            SafeExecute(() =>
            {
                IsLoading = true;
                StatusMessage = "正在加载数据...";
                
                // 使用Repository
                RefreshData();
                
                StatusMessage = "数据加载完成";
            }, "LoadData");
        });
    }
    
    private void RefreshData()
    {
        try
        {
            // Repository数据访问
            var items = _tagRepository.GetAll();
            
            // 业务逻辑处理
            var models = items.Select(ConvertToModel).ToList();
            
            // 线程安全UI更新
            App.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                ItemList.Clear();
                foreach (var model in models)
                {
                    ItemList.Add(model);
                }
            }));
        }
        finally
        {
            IsLoading = false;
        }
    }
    #endregion
}
```

### 第三步：迁移数据访问逻辑

#### 旧方式 → 新方式对照

| 操作类型 | 旧方式 | 新方式 |
|---------|--------|--------|
| **查询所有** | `DbHelper.db.Queryable<DbModels.dv_tag>().ToList()` | `_tagRepository.GetAll()` |
| **条件查询** | `DbHelper.db.Queryable<DbModels.dv_tag>().Where(x => x.TagName.Contains("test")).ToList()` | `_tagRepository.Search("test")` |
| **分页查询** | 手动实现分页逻辑 | `_tagRepository.GetPaged(1, 20)` |
| **添加数据** | `DbHelper.db.Insertable<DbModels.dv_tag>(newTag).ExecuteCommand()` | `_tagRepository.Add(newTag)` |
| **更新数据** | `DbHelper.db.Updateable<DbModels.dv_tag>(tag).ExecuteCommand()` | `_tagRepository.Update(tag)` |
| **删除数据** | `DbHelper.db.Deleteable<DbModels.dv_tag>().Where(x => x.id == id).ExecuteCommand()` | `_tagRepository.DeleteById(id)` |

### 第四步：迁移命令系统

#### 旧命令 → 新命令

```csharp
// 旧方式
public ICommand QueryCommand => new Command(QueryTags);
public ICommand AddCommand => new Command(AddTag);

// 新方式
public ICommand QueryCommand { get; private set; }
public ICommand AddCommand { get; private set; }

private void InitializeCommands()
{
    QueryCommand = new RelayCommand(
        execute: () => SafeExecute(QueryTags, "QueryTags"),
        canExecute: () => !IsBusy
    );
    
    AddCommand = new RelayCommand(
        execute: () => SafeExecute(AddTag, "AddTag"),
        canExecute: () => !IsBusy && CanAddTag()
    );
}

private bool CanAddTag()
{
    return !string.IsNullOrEmpty(TagName) && !string.IsNullOrEmpty(TagInternalName);
}
```

### 第五步：迁移错误处理

#### 旧错误处理 → 新错误处理

```csharp
// 旧方式
private void LoadTags()
{
    try
    {
        var tags = DbHelper.db.Queryable<DbModels.dv_tag>().ToList();
        // 处理数据...
    }
    catch (Exception ex)
    {
        MessageBox.Show("加载失败: " + ex.Message);
    }
}

// 新方式
private void LoadTags()
{
    SafeExecute(() =>
    {
        var tags = _tagRepository.GetAll();
        // 处理数据...
        
        ErrorService.ShowSuccess("数据加载成功");
    }, "LoadTags");
}
```

## 🔧 迁移工具和辅助方法

### 1. 数据转换辅助方法

```csharp
// 实体转模型
private TagModel ConvertToModel(TagEntity entity)
{
    return new TagModel
    {
        Id = entity.id,
        TagInternalName = entity.TagInternalName,
        TagName = entity.TagName,
        Desc = entity.TagDesc,
        // ... 其他属性
    };
}

// 模型转实体
private TagEntity ConvertToEntity(TagModel model)
{
    return new TagEntity
    {
        id = model.Id,
        TagInternalName = model.TagInternalName,
        TagName = model.TagName,
        TagDesc = model.Desc,
        // ... 其他属性
    };
}
```

### 2. 异步操作模板

```csharp
// 异步数据加载模板
private async void LoadDataAsync()
{
    await Task.Run(() =>
    {
        SafeExecute(() =>
        {
            IsLoading = true;
            StatusMessage = "正在加载数据...";
            
            // 数据处理逻辑
            ProcessData();
            
            StatusMessage = "数据加载完成";
        }, "LoadData");
    });
}

// 异步操作模板
private async void PerformAsyncOperation(string operationName, Action operation)
{
    await Task.Run(() =>
    {
        SafeExecute(() =>
        {
            IsLoading = true;
            StatusMessage = $"正在执行{operationName}...";
            
            operation();
            
            StatusMessage = $"{operationName}完成";
        }, operationName);
    });
}
```

### 3. UI更新辅助方法

```csharp
// 线程安全的UI更新
private void UpdateUI(Action uiAction)
{
    if (App.Current.Dispatcher.CheckAccess())
    {
        uiAction();
    }
    else
    {
        App.Current.Dispatcher.BeginInvoke(uiAction);
    }
}

// 集合更新辅助方法
private void UpdateCollection<T>(ObservableCollection<T> collection, IEnumerable<T> newItems)
{
    UpdateUI(() =>
    {
        collection.Clear();
        foreach (var item in newItems)
        {
            collection.Add(item);
        }
    });
}
```

## 📊 迁移检查清单

### ✅ ViewModel迁移检查

- [ ] 继承自ViewModelBase
- [ ] 通过ServiceLocator获取Repository
- [ ] 使用RelayCommand替换旧Command
- [ ] 使用SafeExecute包装操作
- [ ] 实现异步数据加载
- [ ] 添加适当的状态管理
- [ ] 使用线程安全的UI更新

### ✅ 数据访问迁移检查

- [ ] 移除直接的DbHelper调用
- [ ] 使用Repository接口
- [ ] 实现适当的错误处理
- [ ] 添加数据验证逻辑
- [ ] 使用业务模型而非实体

### ✅ 命令系统迁移检查

- [ ] 使用RelayCommand
- [ ] 实现canExecute逻辑
- [ ] 添加参数支持（如需要）
- [ ] 集成错误处理
- [ ] 添加状态检查

## 🚀 迁移示例

### 完整迁移示例：TagConfigPageViewModel

#### 1. 原始ViewModel（简化版）

```csharp
public class TagConfigPageViewModel : INotifyPropertyChanged
{
    public ObservableCollection<TagModel> TagList { get; set; }
    public string SearchText { get; set; }
    
    public ICommand QueryCommand => new Command(QueryTags);
    
    private void QueryTags()
    {
        try
        {
            var tags = DbHelper.db.Queryable<DbModels.dv_tag>()
                .WhereIF(!string.IsNullOrEmpty(SearchText), x => x.tag_internal_name.Contains(SearchText))
                .ToList();
                
            TagList.Clear();
            foreach (var tag in tags)
            {
                TagList.Add(new TagModel { /* 转换逻辑 */ });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("查询失败: " + ex.Message);
        }
    }
}
```

#### 2. 迁移后的ViewModel

```csharp
public class ModernTagConfigPageViewModel : ViewModelBase
{
    #region 私有字段
    private readonly ITagRepository _tagRepository;
    private string _searchText;
    private ObservableCollection<TagModel> _tagList;
    #endregion
    
    #region 构造函数
    public ModernTagConfigPageViewModel()
    {
        _tagRepository = ServiceLocator.GetService<ITagRepository>();
        TagList = new ObservableCollection<TagModel>();
        InitializeCommands();
        LoadDataAsync();
    }
    #endregion
    
    #region 属性
    public string SearchText
    {
        get { return _searchText; }
        set { SetProperty(ref _searchText, value); }
    }
    
    public ObservableCollection<TagModel> TagList
    {
        get { return _tagList; }
        set { SetProperty(ref _tagList, value); }
    }
    #endregion
    
    #region 命令
    public ICommand QueryCommand { get; private set; }
    
    private void InitializeCommands()
    {
        QueryCommand = new RelayCommand(
            execute: () => SafeExecute(QueryTags, "QueryTags"),
            canExecute: () => !IsBusy
        );
    }
    #endregion
    
    #region 私有方法
    private async void LoadDataAsync()
    {
        await PerformAsyncOperation("初始化数据", () => QueryTags());
    }
    
    private void QueryTags()
    {
        var tags = string.IsNullOrEmpty(SearchText) 
            ? _tagRepository.GetAll()
            : _tagRepository.Search(SearchText);
            
        var tagModels = tags.Select(ConvertToModel).ToList();
        
        UpdateCollection(TagList, tagModels);
        
        StatusMessage = $"找到 {tagModels.Count} 个标签";
    }
    
    private TagModel ConvertToModel(TagEntity entity)
    {
        return new TagModel
        {
            Id = entity.id,
            TagInternalName = entity.TagInternalName,
            TagName = entity.TagName,
            Desc = entity.TagDesc
        };
    }
    
    private async void PerformAsyncOperation(string operationName, Action operation)
    {
        await Task.Run(() =>
        {
            SafeExecute(() =>
            {
                IsLoading = true;
                StatusMessage = $"正在{operationName}...";
                
                operation();
                
                StatusMessage = $"{operationName}完成";
            }, operationName);
        });
    }
    
    private void UpdateCollection<T>(ObservableCollection<T> collection, IEnumerable<T> newItems)
    {
        App.Current.Dispatcher.BeginInvoke(new Action(() =>
        {
            collection.Clear();
            foreach (var item in newItems)
            {
                collection.Add(item);
            }
        }));
    }
    #endregion
}
```

## 📝 注意事项

### 1. 渐进式迁移
- 一次迁移一个ViewModel
- 保持原有ViewModel可用，直到新版本完全测试通过
- 可以在同一个项目中并存新旧ViewModel

### 2. 测试策略
- 使用ModernArchitectureTestPage测试Repository功能
- 对比新旧ViewModel的功能一致性
- 进行性能测试，确保没有性能退化

### 3. 团队协作
- 制定迁移计划和时间表
- 统一代码风格和命名规范
- 进行代码审查，确保质量

## 🎉 迁移完成后的收益

- ✅ **更好的可测试性** - Repository可以Mock测试
- ✅ **更清晰的职责分离** - ViewModel专注UI逻辑
- ✅ **更好的错误处理** - 统一的错误处理机制
- ✅ **更好的用户体验** - 异步操作，状态反馈
- ✅ **更好的可维护性** - 统一的代码模式

通过遵循这个迁移指南，您可以逐步将现有代码迁移到新的架构模式，享受现代化开发的各种优势！🚀
