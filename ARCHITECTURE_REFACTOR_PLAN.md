# DataView Config Tool V2 - 架构重构实施计划

## 概述

本文档详细规划了代码架构改进的实施步骤，确保在进行UI升级之前，项目具有稳定、可维护的代码基础。重点关注MVVM架构优化、依赖注入、数据访问层重构和命令系统改进。

## 🎯 重构目标

### 核心目标
1. **确保编译运行正常**：每个步骤完成后都能正常编译和运行
2. **保持功能完整性**：现有功能不受影响
3. **提升代码质量**：改善架构设计和代码可维护性
4. **为UI升级做准备**：建立稳定的架构基础

### 成功标准
- ✅ 项目能够正常编译
- ✅ 所有现有功能正常工作
- ✅ 代码结构更加清晰
- ✅ 依赖关系更加合理
- ✅ 便于后续UI升级

## 📅 分阶段实施计划

### 第一阶段：依赖注入基础设施（1周）

#### 目标
建立依赖注入容器，为后续重构提供基础。

#### 实施步骤

**Step 1.1: 添加依赖注入包**
```xml
<!-- 在 DataViewConfig.csproj 中添加 -->
<PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="7.0.0" />
<PackageReference Include="Microsoft.Extensions.Hosting" Version="7.0.1" />
<PackageReference Include="Microsoft.Extensions.Logging" Version="7.0.0" />
```

**Step 1.2: 创建服务容器配置**
```csharp
// 新建文件：ServiceConfiguration.cs
public static class ServiceConfiguration
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        // 数据访问服务
        services.AddSingleton<IDbConnectionService, DbConnectionService>();
        
        // 应用服务
        services.AddSingleton<IApplicationStateService, ApplicationStateService>();
        services.AddSingleton<IErrorHandlingService, ErrorHandlingService>();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<IMenuService, MenuService>();
        
        // ViewModels
        services.AddTransient<MainViewModel>();
        services.AddTransient<TagConfigPageViewModel>();
        services.AddTransient<ScreenConfigPageViewModel>();
        services.AddTransient<CraneConfigViewModel>();
        
        return services;
    }
}
```

**Step 1.3: 修改App.xaml.cs**
```csharp
public partial class App : Application
{
    private IServiceProvider _serviceProvider;
    
    protected override void OnStartup(StartupEventArgs e)
    {
        var services = new ServiceCollection();
        services.ConfigureServices();
        _serviceProvider = services.BuildServiceProvider();
        
        // 设置全局服务提供者
        ServiceLocator.SetServiceProvider(_serviceProvider);
        
        base.OnStartup(e);
    }
    
    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();
        base.OnExit(e);
    }
}
```

**验收标准**：
- 项目能够正常编译
- 应用程序能够启动
- 依赖注入容器正常工作

### 第二阶段：数据访问层重构（1.5周）

#### 目标
重构DbHelper，实现Repository模式和UnitOfWork模式。

#### 实施步骤

**Step 2.1: 创建数据访问接口**
```csharp
// 新建文件：Interfaces/IDbConnectionService.cs
public interface IDbConnectionService
{
    SqlSugarScope Database { get; }
    Task<T> ExecuteAsync<T>(Func<SqlSugarScope, Task<T>> operation);
    Task ExecuteAsync(Func<SqlSugarScope, Task> operation);
}

// 新建文件：Interfaces/IRepository.cs
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(object id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(IEnumerable<T> entities);
    Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
}

// 新建文件：Interfaces/IUnitOfWork.cs
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation);
    Task ExecuteInTransactionAsync(Func<Task> operation);
}
```

**Step 2.2: 实现数据访问服务**
```csharp
// 新建文件：Services/DbConnectionService.cs
public class DbConnectionService : IDbConnectionService
{
    private readonly SqlSugarScope _database;
    
    public DbConnectionService()
    {
        var connectionString = GetConnectionString();
        _database = new SqlSugarScope(new ConnectionConfig
        {
            ConnectionString = connectionString,
            DbType = DbType.Sqlite,
            IsAutoCloseConnection = true,
            InitKeyType = InitKeyType.Attribute
        });
    }
    
    public SqlSugarScope Database => _database;
    
    public async Task<T> ExecuteAsync<T>(Func<SqlSugarScope, Task<T>> operation)
    {
        return await operation(_database);
    }
    
    public async Task ExecuteAsync(Func<SqlSugarScope, Task> operation)
    {
        await operation(_database);
    }
    
    private string GetConnectionString()
    {
        // 从配置文件读取连接字符串的逻辑
        return "Data Source=Screen/Program/ConfigFiles/dataview.db;Version=3;";
    }
}

// 新建文件：Services/Repository.cs
public class Repository<T> : IRepository<T> where T : class, new()
{
    private readonly IDbConnectionService _dbService;
    
    public Repository(IDbConnectionService dbService)
    {
        _dbService = dbService;
    }
    
    public async Task<T> GetByIdAsync(object id)
    {
        return await _dbService.ExecuteAsync(db => 
            Task.FromResult(db.Queryable<T>().InSingle(id)));
    }
    
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbService.ExecuteAsync(db => 
            Task.FromResult(db.Queryable<T>().ToList().AsEnumerable()));
    }
    
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbService.ExecuteAsync(db => 
            Task.FromResult(db.Queryable<T>().Where(predicate).ToList().AsEnumerable()));
    }
    
    public async Task<T> AddAsync(T entity)
    {
        return await _dbService.ExecuteAsync(async db =>
        {
            var result = await db.Insertable(entity).ExecuteReturnEntityAsync();
            return result;
        });
    }
    
    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbService.ExecuteAsync(async db =>
        {
            await db.Insertable(entities.ToList()).ExecuteCommandAsync();
        });
    }
    
    public async Task UpdateAsync(T entity)
    {
        await _dbService.ExecuteAsync(async db =>
        {
            await db.Updateable(entity).ExecuteCommandAsync();
        });
    }
    
    public async Task DeleteAsync(T entity)
    {
        await _dbService.ExecuteAsync(async db =>
        {
            await db.Deleteable(entity).ExecuteCommandAsync();
        });
    }
    
    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        await _dbService.ExecuteAsync(async db =>
        {
            await db.Deleteable(entities.ToList()).ExecuteCommandAsync();
        });
    }
    
    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate = null)
    {
        return await _dbService.ExecuteAsync(db =>
        {
            var query = db.Queryable<T>();
            if (predicate != null)
                query = query.Where(predicate);
            return Task.FromResult(query.Count());
        });
    }
}

// 新建文件：Services/UnitOfWork.cs
public class UnitOfWork : IUnitOfWork
{
    private readonly IDbConnectionService _dbService;
    
    public UnitOfWork(IDbConnectionService dbService)
    {
        _dbService = dbService;
    }
    
    public async Task<int> SaveChangesAsync()
    {
        // SqlSugar 自动提交，这里返回1表示成功
        return await Task.FromResult(1);
    }
    
    public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation)
    {
        return await _dbService.ExecuteAsync(async db =>
        {
            var transaction = db.Ado.BeginTran();
            try
            {
                var result = await operation();
                transaction.Commit();
                return result;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        });
    }
    
    public async Task ExecuteInTransactionAsync(Func<Task> operation)
    {
        await _dbService.ExecuteAsync(async db =>
        {
            var transaction = db.Ado.BeginTran();
            try
            {
                await operation();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        });
    }
    
    public void Dispose()
    {
        // SqlSugar 自动管理连接
    }
}
```

**Step 2.3: 逐步替换DbHelper使用**
```csharp
// 修改现有的ViewModel，逐步替换DbHelper.db的使用
// 例如：TagConfigPageViewModel.cs

public class TagConfigPageViewModel : ViewModelBase
{
    private readonly IRepository<dv_tag> _tagRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public TagConfigPageViewModel(
        IRepository<dv_tag> tagRepository,
        IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
        
        InitializeCommands();
        LoadDataAsync();
    }
    
    private async void LoadDataAsync()
    {
        try
        {
            IsLoading = true;
            var tags = await _tagRepository.GetAllAsync();
            TagLst = new ObservableCollection<dv_tag>(tags);
        }
        catch (Exception ex)
        {
            // 错误处理
        }
        finally
        {
            IsLoading = false;
        }
    }
    
    private async void AddNew(object o)
    {
        try
        {
            var newTag = new dv_tag
            {
                tag_internal_name = TagInternalName,
                tag_name = TagRealName,
                tag_desc = TagDesc,
                // 其他属性...
            };
            
            await _tagRepository.AddAsync(newTag);
            await LoadDataAsync(); // 刷新列表
        }
        catch (Exception ex)
        {
            // 错误处理
        }
    }
}
```

**验收标准**：
- 数据访问层重构完成
- 现有功能正常工作
- 数据库操作通过新的Repository进行

### 第三阶段：命令系统改进（0.5周）

#### 目标
改进命令实现，支持CanExecute和异步操作。

#### 实施步骤

**Step 3.1: 创建改进的命令类**
```csharp
// 新建文件：Commands/RelayCommand.cs
public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Predicate<object> _canExecute;
    
    public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }
    
    public bool CanExecute(object parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true;
    }
    
    public void Execute(object parameter)
    {
        _execute(parameter);
    }
    
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
    
    public void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}

// 新建文件：Commands/AsyncRelayCommand.cs
public class AsyncRelayCommand : ICommand
{
    private readonly Func<object, Task> _execute;
    private readonly Predicate<object> _canExecute;
    private bool _isExecuting;
    
    public AsyncRelayCommand(Func<object, Task> execute, Predicate<object> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }
    
    public bool CanExecute(object parameter)
    {
        return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
    }
    
    public async void Execute(object parameter)
    {
        if (!CanExecute(parameter))
            return;
            
        _isExecuting = true;
        RaiseCanExecuteChanged();
        
        try
        {
            await _execute(parameter);
        }
        finally
        {
            _isExecuting = false;
            RaiseCanExecuteChanged();
        }
    }
    
    public event EventHandler CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
    
    public void RaiseCanExecuteChanged()
    {
        CommandManager.InvalidateRequerySuggested();
    }
}
```

**Step 3.2: 替换现有Command使用**
```csharp
// 修改ViewModel中的命令定义
public class TagConfigPageViewModel : ViewModelBase
{
    public RelayCommand AddNewCommand { get; private set; }
    public RelayCommand EditCommand { get; private set; }
    public RelayCommand DeleteCommand { get; private set; }
    public AsyncRelayCommand LoadDataCommand { get; private set; }
    
    private void InitializeCommands()
    {
        AddNewCommand = new RelayCommand(AddNew, CanAddNew);
        EditCommand = new RelayCommand(Edit, CanEdit);
        DeleteCommand = new RelayCommand(Delete, CanDelete);
        LoadDataCommand = new AsyncRelayCommand(async _ => await LoadDataAsync());
    }
    
    private bool CanAddNew(object parameter)
    {
        return !string.IsNullOrEmpty(TagInternalName) && 
               !string.IsNullOrEmpty(TagRealName);
    }
    
    private bool CanEdit(object parameter)
    {
        return SelectedTag != null;
    }
    
    private bool CanDelete(object parameter)
    {
        return SelectedTag != null;
    }
}
```

**验收标准**：
- 命令系统改进完成
- 按钮状态能够正确响应
- 异步命令正常工作

### 第四阶段：MVVM架构优化（1.5周）

#### 目标
拆分MainViewModel职责，创建专门的服务类。

#### 实施步骤

**Step 4.1: 创建基础ViewModel类**
```csharp
// 新建文件：ViewModels/ViewModelBase.cs
public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
            
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
    
    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set => SetProperty(ref _isLoading, value);
    }
}
```

**Step 4.2: 创建导航服务**
```csharp
// 新建文件：Services/NavigationService.cs
public interface INavigationService
{
    void NavigateTo(string pageName);
    void NavigateTo<T>() where T : UserControl;
    bool CanNavigateBack { get; }
    void NavigateBack();
    event EventHandler<NavigationEventArgs> Navigated;
}

public class NavigationService : INavigationService
{
    private readonly Dictionary<string, UIElement> _pageCache = new();
    private readonly Stack<string> _navigationHistory = new();
    
    public bool CanNavigateBack => _navigationHistory.Count > 1;
    
    public event EventHandler<NavigationEventArgs> Navigated;
    
    public void NavigateTo(string pageName)
    {
        try
        {
            if (!_pageCache.ContainsKey(pageName))
            {
                var type = Assembly.GetExecutingAssembly().GetType($"DataViewConfig.Pages.{pageName}");
                if (type != null)
                {
                    _pageCache[pageName] = (UIElement)Activator.CreateInstance(type);
                }
            }
            
            if (_pageCache.ContainsKey(pageName))
            {
                _navigationHistory.Push(pageName);
                Navigated?.Invoke(this, new NavigationEventArgs(pageName, _pageCache[pageName]));
            }
        }
        catch (Exception ex)
        {
            // 错误处理
        }
    }
    
    public void NavigateTo<T>() where T : UserControl
    {
        NavigateTo(typeof(T).Name);
    }
    
    public void NavigateBack()
    {
        if (CanNavigateBack)
        {
            _navigationHistory.Pop(); // 移除当前页面
            var previousPage = _navigationHistory.Peek();
            Navigated?.Invoke(this, new NavigationEventArgs(previousPage, _pageCache[previousPage]));
        }
    }
}

public class NavigationEventArgs : EventArgs
{
    public string PageName { get; }
    public UIElement Page { get; }
    
    public NavigationEventArgs(string pageName, UIElement page)
    {
        PageName = pageName;
        Page = page;
    }
}
```

**Step 4.3: 重构MainViewModel**
```csharp
// 修改 MainViewModel.cs
public class MainViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IMenuService _menuService;
    private readonly IApplicationStateService _applicationState;
    
    private UIElement _mainContent;
    public UIElement MainContent
    {
        get => _mainContent;
        set => SetProperty(ref _mainContent, value);
    }
    
    public ObservableCollection<MenuItemModel> MenuItems { get; private set; }
    
    public RelayCommand OpenPageCommand { get; private set; }
    public RelayCommand MinimizeCommand { get; private set; }
    public RelayCommand CloseCommand { get; private set; }
    
    public MainViewModel(
        INavigationService navigationService,
        IMenuService menuService,
        IApplicationStateService applicationState)
    {
        _navigationService = navigationService;
        _menuService = menuService;
        _applicationState = applicationState;
        
        InitializeCommands();
        InitializeNavigation();
        LoadMenuItems();
    }
    
    private void InitializeCommands()
    {
        OpenPageCommand = new RelayCommand(OpenPage);
        MinimizeCommand = new RelayCommand(o => SystemCommands.MinimizeWindow((Window)o));
        CloseCommand = new RelayCommand(o => Application.Current.Shutdown());
    }
    
    private void InitializeNavigation()
    {
        _navigationService.Navigated += OnNavigated;
    }
    
    private void OnNavigated(object sender, NavigationEventArgs e)
    {
        MainContent = e.Page;
    }
    
    private void OpenPage(object parameter)
    {
        if (parameter is string pageName)
        {
            _navigationService.NavigateTo(pageName);
        }
    }
    
    private void LoadMenuItems()
    {
        MenuItems = _menuService.GetMenuItems();
    }
}
```

**验收标准**：
- MainViewModel职责明确
- 导航功能正常工作
- 菜单显示正常
- 页面切换正常

### 第五阶段：错误处理和状态管理（0.5周）

#### 目标
建立统一的错误处理和应用状态管理。

#### 实施步骤

**Step 5.1: 创建应用状态服务**
```csharp
// 新建文件：Services/ApplicationStateService.cs
public interface IApplicationStateService
{
    GlobalLanguage CurrentLanguage { get; set; }
    UserLevelType CurrentUserLevel { get; set; }
    bool IsDataViewConfig { get; set; }
    
    event EventHandler<StateChangedEventArgs> StateChanged;
}

public class ApplicationStateService : IApplicationStateService, INotifyPropertyChanged
{
    private GlobalLanguage _currentLanguage = GlobalLanguage.zhCN;
    private UserLevelType _currentUserLevel = UserLevelType.Administrator;
    private bool _isDataViewConfig = true;
    
    public GlobalLanguage CurrentLanguage
    {
        get => _currentLanguage;
        set
        {
            if (SetProperty(ref _currentLanguage, value))
            {
                StateChanged?.Invoke(this, new StateChangedEventArgs(nameof(CurrentLanguage), value));
            }
        }
    }
    
    public UserLevelType CurrentUserLevel
    {
        get => _currentUserLevel;
        set
        {
            if (SetProperty(ref _currentUserLevel, value))
            {
                StateChanged?.Invoke(this, new StateChangedEventArgs(nameof(CurrentUserLevel), value));
            }
        }
    }
    
    public bool IsDataViewConfig
    {
        get => _isDataViewConfig;
        set
        {
            if (SetProperty(ref _isDataViewConfig, value))
            {
                StateChanged?.Invoke(this, new StateChangedEventArgs(nameof(IsDataViewConfig), value));
            }
        }
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    public event EventHandler<StateChangedEventArgs> StateChanged;
    
    protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value))
            return false;
            
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        return true;
    }
}
```

**Step 5.2: 创建错误处理服务**
```csharp
// 新建文件：Services/ErrorHandlingService.cs
public interface IErrorHandlingService
{
    Task HandleErrorAsync(Exception exception, string context = null);
    Task ShowErrorAsync(string message);
    Task ShowSuccessAsync(string message);
    Task ShowWarningAsync(string message);
}

public class ErrorHandlingService : IErrorHandlingService
{
    public async Task HandleErrorAsync(Exception exception, string context = null)
    {
        // 记录日志
        LogHelper.Error($"[{context}] {exception}");
        
        // 显示用户友好的错误信息
        var userMessage = GetUserFriendlyMessage(exception);
        await ShowErrorAsync(userMessage);
    }
    
    public async Task ShowErrorAsync(string message)
    {
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            MessageBox.Show(message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        });
    }
    
    public async Task ShowSuccessAsync(string message)
    {
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            MessageBox.Show(message, "成功", MessageBoxButton.OK, MessageBoxImage.Information);
        });
    }
    
    public async Task ShowWarningAsync(string message)
    {
        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            MessageBox.Show(message, "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
        });
    }
    
    private string GetUserFriendlyMessage(Exception exception)
    {
        return exception switch
        {
            SqlException => "数据库操作失败，请检查数据连接",
            FileNotFoundException => "找不到指定的文件",
            UnauthorizedAccessException => "权限不足，无法执行此操作",
            ArgumentException => "输入参数有误，请检查输入内容",
            _ => "操作失败，请稍后重试"
        };
    }
}
```

**验收标准**：
- 错误处理统一
- 应用状态管理正常
- 用户体验改善

## 🔍 验收和测试

### 每个阶段的验收标准
1. **编译成功**：项目能够正常编译，无编译错误
2. **启动正常**：应用程序能够正常启动和显示
3. **功能完整**：所有现有功能正常工作
4. **性能稳定**：没有明显的性能下降

### 测试清单
- [ ] 应用程序启动测试
- [ ] 登录功能测试
- [ ] 主要配置页面功能测试
- [ ] 数据库操作测试
- [ ] 菜单导航测试
- [ ] 错误处理测试

## 📋 实施注意事项

### 关键原则
1. **小步快跑**：每次只修改一小部分，确保稳定性
2. **向后兼容**：保持现有接口的兼容性
3. **渐进替换**：逐步替换旧代码，而不是一次性重写
4. **充分测试**：每个步骤完成后都要进行测试

### 风险控制
1. **备份代码**：每个阶段开始前备份代码
2. **分支开发**：使用Git分支进行开发
3. **回滚准备**：如果出现问题，能够快速回滚

### 时间安排
- **总计时间**：约4周
- **每日验收**：确保每天的修改都能正常运行
- **周末集成**：周末进行集成测试和问题修复

通过这样的分阶段实施，可以确保在进行UI升级之前，项目具有稳定、可维护的代码架构基础。
