# DataView Config Tool V2 - API 文档

## 核心类库

### 1. MainViewModel 类

主窗口的视图模型，负责整个应用程序的导航和状态管理。

#### 属性

```csharp
public Dictionary<string, UIElement> PageDict { get; set; }
```
页面缓存字典，避免重复创建页面实例。

```csharp
public Dictionary<string, UIElement> ExSystemPageDict { get; set; }
```
交互系统页面缓存字典。

```csharp
public ObservableCollection<DvSystemModel> ExSystemLst { get; set; }
```
交互系统列表，用于动态菜单生成。

```csharp
public List<Controls.MenuItem> SideMenuItemLst { get; set; }
```
侧边菜单项列表。

```csharp
public UIElement MainContent { get; set; }
```
主内容区域显示的页面。

#### 命令

```csharp
public Command OpenPageCommand { get; set; }
```
打开配置页面命令。

```csharp
public Command OpenExSystemConfigPageCommand { get; set; }
```
打开交互系统配置页面命令。

```csharp
public Command CreateNewInteractiveSystemCommand { get; set; }
```
创建新交互系统命令。

```csharp
public Command RemoveExSystemConfigCommand { get; set; }
```
删除交互系统命令。

#### 方法

```csharp
public void OpenPage(object o)
```
通过反射动态创建并打开指定的配置页面。

```csharp
public void ExSystemConfigPage(object o)
```
打开交互系统配置页面。

```csharp
private void RefreshSideMenu()
```
刷新侧边菜单栏，根据用户权限和系统配置动态生成菜单项。

```csharp
private void RefreshExSystemLst()
```
刷新交互系统列表。

### 2. DbHelper 类

数据库访问助手类，提供统一的数据库操作接口。

#### 属性

```csharp
public static SqlSugar.SqlSugarScope db;
```
SqlSugar数据库实例，支持多种数据库类型。

#### 方法

```csharp
public static DbHelper GetInstance()
```
获取DbHelper单例实例。

### 3. GlobalConfig 类

全局配置管理类，存储应用程序的全局设置。

#### 属性

```csharp
public static GlobalLanguage CurLanguage = GlobalLanguage.zhCN;
```
当前语言设置。

```csharp
public static bool IsDataViewConfig = true;
```
是否为DataView配置模式。

```csharp
public static UserLevelType CurUserLevel = UserLevelType.Administrator;
```
当前用户权限级别。

```csharp
public static Dictionary<string, TipsModel> ConfigToolTipsDict;
```
配置工具提示信息字典。

### 4. TagConfigPageViewModel 类

点名配置页面的视图模型。

#### 属性

```csharp
public ObservableCollection<TagModel> TagLst { get; set; }
```
点名列表。

```csharp
public string TagRealName { get; set; }
```
点名显示名称。

```csharp
public string TagInternalName { get; set; }
```
点名内部名称。

```csharp
public string TagDesc { get; set; }
```
点名描述。

```csharp
public TagDataTypeEnum CurTagDataType { get; set; }
```
当前点名数据类型。

#### 命令

```csharp
public Command AddNewCommand { get; set; }
```
添加新点名命令。

```csharp
public Command EditCommand { get; set; }
```
编辑点名命令。

```csharp
public Command DeleteCommand { get; set; }
```
删除点名命令。

```csharp
public Command QueryCommand { get; set; }
```
查询点名命令。

#### 方法

```csharp
private void AddNew(object o)
```
添加新的点名配置。

```csharp
private void Edit(object o)
```
编辑现有点名配置。

```csharp
private void Delete(object o)
```
删除点名配置。

```csharp
private void RefreshTagLst()
```
刷新点名列表。

### 5. ScreenConfigPageViewModel 类

画面配置页面的视图模型。

#### 属性

```csharp
public ObservableCollection<ScreenModel> ScreenLst { get; set; }
```
画面配置列表。

```csharp
public string ScreenInternalName { get; set; }
```
画面内部名称。

```csharp
public string ScreenCswName { get; set; }
```
画面CSW文件名。

```csharp
public string ScreenDesc { get; set; }
```
画面描述。

#### 命令

```csharp
public Command AddNewCommand { get; set; }
```
添加新画面命令。

```csharp
public Command SelectCswCommand { get; set; }
```
选择CSW文件命令。

#### 方法

```csharp
private void AddNew(object o)
```
添加新的画面配置。

```csharp
private void SelectCsw(object o)
```
选择CSW文件。

### 6. ParamModel 类

参数模型类，用于接口参数配置。

#### 属性

```csharp
public int ParamID { get; set; }
```
参数ID。

```csharp
public string ParamName { get; set; }
```
参数名称。

```csharp
public RequestParamSource ParamSource { get; set; }
```
参数来源类型。

```csharp
public string JsonVariableName { get; set; }
```
JSON变量名称。

```csharp
public string ConstValue { get; set; }
```
常量值。

```csharp
public string MacroName { get; set; }
```
宏名称。

```csharp
public string TagInternalName { get; set; }
```
关联的点名内部名称。

```csharp
public ParamTargetType TargetValType { get; set; }
```
目标值类型。

```csharp
public string ParamDesc { get; set; }
```
参数描述。

## 枚举定义

### GlobalLanguage 枚举

```csharp
public enum GlobalLanguage
{
    enUS = 1,  // 英文
    zhCN = 2   // 中文
}
```

### UserLevelType 枚举

```csharp
public enum UserLevelType
{
    Operator = 1,        // 操作员
    Administrator = 2    // 管理员
}
```

### TemplateTypeEnum 枚举

```csharp
public enum TemplateTypeEnum
{
    RXG = 1,  // 轨道式门式起重机
    STS = 2   // 岸边集装箱起重机
}
```

### ParamChangeType 枚举

```csharp
public enum ParamChangeType
{
    SelectOther = 1,  // 选择其他
    AddNew = 2,       // 添加新的
    EditExist = 3     // 编辑现有
}
```

## 自定义控件 API

### MenuItem 控件

自定义菜单项控件，支持层级菜单结构。

#### 构造函数

```csharp
public MenuItem(MenuModel menuModel, MainWindow mainWindow)
```

#### 属性

```csharp
public MenuModel MenuData { get; set; }
```
菜单数据模型。

### SearchTextBox 控件

带搜索功能的文本框控件。

#### 属性

```csharp
public string SearchText { get; set; }
```
搜索文本。

#### 事件

```csharp
public event EventHandler<string> SearchTextChanged;
```
搜索文本变更事件。

### TipsLabel 控件

提示标签控件，支持多语言提示信息。

#### 属性

```csharp
public string TipName { get; set; }
```
提示名称，用于查找对应的提示内容。

### MultiCombox 控件

多选下拉框控件。

#### 属性

```csharp
public ObservableCollection<object> SelectedItems { get; set; }
```
选中的项目列表。

```csharp
public ObservableCollection<object> ItemsSource { get; set; }
```
数据源。

## 数据转换器 API

### Bool2VisibilityConverter

布尔值到可见性转换器。

```csharp
public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
```

### Bool2InverseConverter

布尔值反转转换器。

```csharp
public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
```

### EnumDescriptionConverter

枚举描述转换器。

```csharp
public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
```

## 配置文件 API

### ProjConfigXml 类

项目配置XML文件处理类。

#### 属性

```csharp
public List<User> UserList { get; set; }
```
用户列表。

```csharp
public DbConnectionString dbConnectionString { get; set; }
```
数据库连接字符串配置。

#### 嵌套类

```csharp
public class User
{
    public int userLevel { get; set; }
    public string userName { get; set; }
    public string userPwd { get; set; }
}
```

## 事件系统 API

### EventBus 类

事件总线，用于组件间的松耦合通信。

#### 方法

```csharp
public static void Subscribe<T>(Action<T> handler)
```
订阅事件。

```csharp
public static void Publish<T>(T eventData)
```
发布事件。

```csharp
public static void Unsubscribe<T>(Action<T> handler)
```
取消订阅事件。

## 工具类 API

### Utli 类

通用工具类。

#### 方法

```csharp
public static T ConvertToEnum<T>(string value) where T : Enum
```
字符串转换为枚举。

```csharp
public static string GetResourceString(string key)
```
获取多语言资源字符串。

## 数据库模型

### 主要数据表对应的实体类

#### dv_tag 实体

```csharp
public class dv_tag
{
    public string tag_internal_name { get; set; }
    public string tag_name { get; set; }
    public string tag_desc { get; set; }
    public int postfix_type_id { get; set; }
    public int data_type_id { get; set; }
    public string related_macro_name { get; set; }
}
```

#### dv_screen_conf 实体

```csharp
public class dv_screen_conf
{
    public string dv_screen_internal_name { get; set; }
    public string dv_screen_csw_name { get; set; }
    public string dv_screen_desc { get; set; }
}
```

#### dv_system 实体

```csharp
public class dv_system
{
    public int system_id { get; set; }
    public string system_name { get; set; }
    public string system_desc { get; set; }
    public bool is_permanent { get; set; }
}
```

## 使用示例

### 创建新的配置页面

```csharp
// 1. 创建ViewModel
public class MyConfigPageViewModel : INotifyPropertyChanged
{
    // 实现属性和命令
}

// 2. 创建XAML页面
// MyConfigPage.xaml

// 3. 在MainViewModel中注册页面
private void RefreshSideMenu()
{
    SideMenuItemLst.Add(new MenuItem(new MenuModel("我的配置", new List<MenuItemModel>()
    {
        new MenuItemModel(){ Name="我的配置页面", RelatedConfigPageName="MyConfigPage"},
    }), _mainWindow));
}
```

### 数据库操作示例

```csharp
// 查询数据
var tags = DbHelper.db.Queryable<dv_tag>().ToList();

// 插入数据
var newTag = new dv_tag { tag_internal_name = "test", tag_name = "测试" };
DbHelper.db.Insertable(newTag).ExecuteCommand();

// 更新数据
DbHelper.db.Updateable<dv_tag>()
    .SetColumns(x => x.tag_name == "新名称")
    .Where(x => x.tag_internal_name == "test")
    .ExecuteCommand();

// 删除数据
DbHelper.db.Deleteable<dv_tag>()
    .Where(x => x.tag_internal_name == "test")
    .ExecuteCommand();
```
