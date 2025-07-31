# DataView Config Tool V2 - 项目结构说明

## 项目目录结构

```
dvconfig_v2/
├── DataViewConfig/                          # 主项目目录
│   ├── App.config                          # 应用程序配置文件
│   ├── App.xaml                            # 应用程序XAML定义
│   ├── App.xaml.cs                         # 应用程序启动逻辑
│   ├── DataViewConfig.csproj               # 项目文件
│   ├── MainWindow.xaml                     # 主窗口界面
│   ├── MainWindow.xaml.cs                  # 主窗口代码
│   ├── MainViewModel.cs                    # 主窗口视图模型
│   ├── Login.xaml                          # 登录窗口
│   ├── LoginSelector.xaml                  # 登录选择窗口
│   ├── StartWindow.xaml                    # 启动窗口
│   ├── projConfig.xml                      # 项目配置文件
│   ├── templateConfig.xml                  # 模板配置文件
│   ├── ReleaseNote.txt                     # 版本发布说明
│   │
│   ├── AttachProps/                        # 附加属性
│   │   └── PasswordBoxHelper.cs            # 密码框助手
│   │
│   ├── Base/                               # 基础类库
│   │   ├── AttachProperty.cs               # 附加属性基类
│   │   ├── BaseWindow.cs                   # 窗口基类
│   │   ├── BindingProxy.cs                 # 绑定代理
│   │   └── EnumExtensions.cs               # 枚举扩展
│   │
│   ├── Config/                             # 配置文件处理
│   │   ├── ProjConfigXml.cs                # 项目配置XML处理
│   │   └── tips.json                       # 提示信息配置
│   │
│   ├── Controls/                           # 自定义控件
│   │   ├── ImagePopup.xaml                 # 图片弹窗
│   │   ├── MenuItem.xaml                   # 菜单项控件
│   │   ├── MultiCombox.xaml                # 多选下拉框
│   │   ├── MultiParamCombox.xaml           # 多参数下拉框
│   │   ├── MyCombox.xaml                   # 自定义下拉框
│   │   ├── NewParameter.xaml               # 新建参数控件
│   │   ├── NewControl.xaml                 # 新建控件
│   │   ├── NewReceiveFanout.xaml           # 新建接收分发
│   │   ├── NewEcsSystem.xaml               # 新建ECS系统
│   │   ├── NewScreen.xaml                  # 新建画面
│   │   ├── NewTag.xaml                     # 新建点名
│   │   ├── NewRequestInterface.xaml        # 新建请求接口
│   │   ├── SearchTextBox.xaml              # 搜索文本框
│   │   ├── TipsLabel.xaml                  # 提示标签
│   │   └── ComboBoxItemTemplateSelector.cs # 下拉框模板选择器
│   │
│   ├── Converters/                         # 数据转换器
│   │   ├── Bool2VisibilityConverter.cs     # 布尔到可见性转换
│   │   ├── Bool2InverseConverter.cs        # 布尔反转转换
│   │   ├── EnumDescriptionConverter.cs     # 枚举描述转换
│   │   ├── ImageConverter.cs               # 图片转换器
│   │   ├── PasswordConverter.cs            # 密码转换器
│   │   ├── TemplateType2VisibilityConverter.cs # 模板类型可见性转换
│   │   └── ...                             # 其他转换器
│   │
│   ├── Models/                             # 数据模型
│   │   └── ParamModel.cs                   # 参数模型
│   │
│   ├── Pages/                              # 配置页面
│   │   ├── BlockConfigPage.xaml            # 堆场配置页面
│   │   ├── CommonReturnConfigPage.xaml     # 通用返回配置页面
│   │   ├── ControlConfigPage.xaml          # 控件配置页面
│   │   ├── ControlDefaultValueConfigPage.xaml # 控件默认值配置
│   │   ├── CraneConfigPage.xaml            # 起重机配置页面
│   │   ├── DvTipsConfigPage.xaml           # DV提示配置页面
│   │   ├── EcsSystemConfigPage.xaml        # ECS系统配置页面
│   │   ├── FanoutReceiveConfigPage.xaml    # 分发接收配置页面
│   │   ├── HelpLinkConfigPage.xaml         # 帮助链接配置页面
│   │   ├── InteractiveParameterConfigPage.xaml # 交互参数配置
│   │   ├── InteractiveSystemConfigPage.xaml # 交互系统配置
│   │   ├── InterfaceConfigPage.xaml        # 接口配置页面
│   │   ├── LoginConfigPage.xaml            # 登录配置页面
│   │   ├── NaviConfigPage.xaml             # 导航配置页面
│   │   ├── RcsConfigPage.xaml              # RCS配置页面
│   │   ├── ScreenConfigPage.xaml           # 画面配置页面
│   │   ├── ScreenSwitchConfigPage.xaml     # 画面切换配置
│   │   ├── TagConfigPage.xaml              # 点名配置页面
│   │   ├── TemplateConfigPage.xaml         # 模板配置页面
│   │   ├── TemplateSwitchPage.xaml         # 模板切换页面
│   │   ├── TipsConfigPage.xaml             # 提示配置页面
│   │   │
│   │   └── Popups/                         # 弹窗页面
│   │       ├── BayEditPopup.xaml           # 贝位编辑弹窗
│   │       ├── BlockEditPopup.xaml         # 堆场编辑弹窗
│   │       ├── CommonReturnEditPopup.xaml  # 通用返回编辑弹窗
│   │       ├── ControlEditPopup.xaml       # 控件编辑弹窗
│   │       ├── CraneEditPopup.xaml         # 起重机编辑弹窗
│   │       ├── EcsSystemEditPopup.xaml     # ECS系统编辑弹窗
│   │       ├── InterfaceEditPopup.xaml     # 接口编辑弹窗
│   │       ├── ParamEditPopup.xaml         # 参数编辑弹窗
│   │       ├── ScreenEditPopup.xaml        # 画面编辑弹窗
│   │       ├── TagEditPopup.xaml           # 点名编辑弹窗
│   │       └── ...                         # 其他编辑弹窗
│   │
│   ├── ViewModels/                         # 视图模型
│   │   ├── BlockConfigViewModel.cs         # 堆场配置视图模型
│   │   ├── CommonReturnConfigViewModel.cs  # 通用返回配置视图模型
│   │   ├── ControlConfigPageViewModel.cs   # 控件配置页面视图模型
│   │   ├── CraneConfigViewModel.cs         # 起重机配置视图模型
│   │   ├── EcsSystemConfigPageViewModel.cs # ECS系统配置视图模型
│   │   ├── LoginConfigViewModel.cs         # 登录配置视图模型
│   │   ├── ScreenConfigPageViewModel.cs    # 画面配置视图模型
│   │   ├── TagConfigPageViewModel.cs       # 点名配置视图模型
│   │   ├── TemplateConfigWinViewModel.cs   # 模板配置视图模型
│   │   │
│   │   └── Popups/                         # 弹窗视图模型
│   │       ├── BayEditPopupViewModel.cs    # 贝位编辑弹窗视图模型
│   │       ├── BlockEditPopupViewModel.cs  # 堆场编辑弹窗视图模型
│   │       ├── CraneEditPopupViewModel.cs  # 起重机编辑弹窗视图模型
│   │       └── ...                         # 其他弹窗视图模型
│   │
│   ├── Utli/                               # 工具类
│   │   ├── EventBus.cs                     # 事件总线
│   │   └── Utli.cs                         # 通用工具类
│   │
│   ├── Styles/                             # 样式文件
│   │   ├── BaseDark.xaml                   # 深色主题样式
│   │   └── GlobalStyle.xaml                # 全局样式
│   │
│   ├── Language/                           # 多语言资源
│   │   ├── zh_cn.xaml                      # 中文资源
│   │   └── en_us.xaml                      # 英文资源
│   │
│   ├── Images/                             # 图片资源
│   │   ├── icon.png                        # 应用图标
│   │   ├── 配置工具logo.ico                # 配置工具图标
│   │   ├── 配置工具logo.png                # 配置工具PNG图标
│   │   ├── RXG_LocalPLC_OPCUA_Structure.png # RXG本地PLC结构图
│   │   ├── RXG_RosCPU_OPCUA_Structure.png  # RXG RosCPU结构图
│   │   ├── RXG_Uniform_RCCS_OPCUA_Structure.png # RXG统一RCCS结构图
│   │   └── ...                             # 其他图片资源
│   │
│   ├── Fonts/                              # 字体文件
│   │   └── fontawesome-webfont.ttf         # FontAwesome字体
│   │
│   ├── Libs/                               # 第三方库
│   │   ├── BlackPearl.Controls.Common.dll  # BlackPearl控件库
│   │   ├── CMSCore.dll                     # CMS核心库
│   │   ├── DataViewCore.dll                # DataView核心库
│   │   ├── HandyControl.dll                # HandyControl控件库
│   │   ├── log4net.dll                     # 日志库
│   │   ├── MySql.Data.dll                  # MySQL数据库驱动
│   │   ├── Newtonsoft.Json.dll             # JSON处理库
│   │   ├── SqlSugar.dll                    # ORM框架
│   │   └── ...                             # 其他依赖库
│   │
│   ├── Properties/                         # 项目属性
│   │   ├── AssemblyInfo.cs                 # 程序集信息
│   │   ├── Resources.resx                  # 资源文件
│   │   ├── Settings.settings               # 设置文件
│   │   └── app.manifest                    # 应用清单
│   │
│   ├── obj/                                # 编译输出目录
│   └── packages.config                     # NuGet包配置
│
├── Screen/                                 # 画面文件和数据库目录
│   ├── Login.csw                           # 登录画面
│   ├── Overview.csw                        # 概览画面
│   ├── Template.csw                        # 模板画面
│   ├── RT_ManualHndle.csw                  # 手动处理画面
│   ├── RT_Spreader.csw                     # 吊具画面
│   │
│   ├── Program/                            # 程序配置目录
│   │   └── ConfigFiles/                    # 配置文件和数据库
│   │       ├── ProjIni.xml                 # 项目初始化配置
│   │       └── dataview.db                 # SQLite数据库文件（主要数据库）
│   │
│   └── RMG/                                # RMG相关画面
│       ├── Overview.csw                    # RMG概览画面
│       └── RT_ManualHndle - 副本 (2).csw   # RMG手动处理画面副本
│
├── DV_ConfigTool/                          # 编译输出目录（整理后的部署结构）
│   ├── DataViewConfig.exe                  # 主程序（根目录唯一exe文件）
│   ├── Libs/                               # 依赖库文件夹
│   │   ├── BlackPearl.Controls.Common.dll  # BlackPearl控件库
│   │   ├── CMSCore.dll                     # CMS核心库
│   │   ├── DataViewCore.dll                # DataView核心库
│   │   ├── HandyControl.dll                # HandyControl控件库
│   │   ├── log4net.dll                     # 日志库
│   │   ├── MySql.Data.dll                  # MySQL数据库驱动
│   │   ├── Newtonsoft.Json.dll             # JSON处理库
│   │   ├── SqlSugar.dll                    # ORM框架
│   │   └── ...                             # 其他依赖DLL文件
│   ├── Config/                             # 配置文件夹
│   │   ├── projConfig.xml                  # 项目配置
│   │   ├── templateConfig.xml              # 模板配置
│   │   └── tips.json                       # 提示配置
│   ├── Language/                           # 多语言资源文件夹
│   │   ├── zh_cn.xaml                      # 中文资源
│   │   └── en_us.xaml                      # 英文资源
│   ├── Images/                             # 图片资源文件夹
│   │   ├── icon.png                        # 应用图标
│   │   ├── 配置工具logo.ico                # 配置工具图标
│   │   └── ...                             # 其他图片资源
│   └── Logs/                               # 日志文件夹
│       └── *.log                           # 运行日志文件
│
├── packages/                               # NuGet包目录
│   ├── BlackPearl.Controls.Library.2.0.3/ # BlackPearl控件包
│   ├── Fody.6.6.4/                        # Fody包
│   ├── HandyControl.3.4.0/                # HandyControl包
│   ├── Markdig.0.31.0/                     # Markdown处理包
│   ├── Microsoft.Xaml.Behaviors.Wpf.1.1.39/ # XAML行为包
│   ├── MiniExcel.1.30.2/                   # Excel处理包
│   ├── PropertyChanged.Fody.4.1.0/        # 属性变更通知包
│   └── ...                                # 其他NuGet包
│
└── SourceCode.sln                          # Visual Studio解决方案文件
```

## 核心文件说明

### 配置文件

#### projConfig.xml
项目主配置文件，包含：
- 用户账户信息
- 界面显示设置
- 语言配置
- 数据库选择设置

#### templateConfig.xml
模板配置文件，定义：
- 可用模板列表
- 模板类型和描述
- 模板文件路径
- 帮助文档链接

#### App.config
应用程序配置文件，包含：
- .NET Framework配置
- 数据库连接字符串
- 应用程序设置

### 核心代码文件

#### MainViewModel.cs
主窗口视图模型，负责：
- 菜单管理和页面导航
- 用户权限控制
- 交互系统管理
- 全局状态管理

#### DbHelper.cs
数据库访问助手，提供：
- 统一的数据库访问接口
- 多数据库类型支持
- 连接池管理
- 事务处理

#### GlobalConfig.cs
全局配置管理，包含：
- 语言设置
- 用户级别
- 全局开关
- 提示信息字典

### 页面文件组织

#### Pages/ 目录
包含所有主要配置页面：
- 每个页面包含 .xaml 和 .xaml.cs 文件
- 页面按功能模块分组
- 支持动态加载和缓存

#### Popups/ 目录
包含所有弹窗页面：
- 编辑弹窗
- 选择弹窗
- 确认对话框
- 信息显示弹窗

#### ViewModels/ 目录
包含所有视图模型：
- 实现MVVM模式
- 数据绑定和命令处理
- 业务逻辑封装
- 属性变更通知

### 资源文件组织

#### Images/ 目录
图片资源分类：
- 应用图标和Logo
- 功能图标
- 结构示意图
- 界面装饰图片

#### Language/ 目录
多语言资源：
- 中文资源文件
- 英文资源文件
- 支持运行时切换

#### Styles/ 目录
样式定义：
- 全局样式
- 主题样式
- 控件样式
- 颜色和字体定义

### 第三方库

#### 核心依赖
- **HandyControl**: 现代化WPF控件库
- **SqlSugar**: ORM框架，支持多数据库
- **PropertyChanged.Fody**: 自动属性变更通知
- **Newtonsoft.Json**: JSON序列化和反序列化

#### 数据库驱动
- **MySql.Data**: MySQL数据库驱动
- **System.Data.SQLite**: SQLite数据库驱动
- **Oracle.ManagedDataAccess**: Oracle数据库驱动

#### 工具库
- **MiniExcel**: Excel文件处理
- **Markdig**: Markdown文档处理
- **log4net**: 日志记录框架

## 编译输出结构

### 输出目录：DV_ConfigTool
项目编译后输出到 `DV_ConfigTool` 目录，采用整理后的文件夹结构：

```
DV_ConfigTool/
├── DataViewConfig.exe              # 主程序（根目录下唯一的exe文件）
├── Libs/                          # 依赖库文件夹
│   ├── BlackPearl.Controls.Common.dll
│   ├── BlackPearl.Controls.CoreLibrary.dll
│   ├── CMSCore.dll
│   ├── DataViewCore.dll
│   ├── DataView_Configuration.dll
│   ├── HandyControl.dll
│   ├── log4net.dll
│   ├── Markdig.dll
│   ├── Microsoft.Xaml.Behaviors.dll
│   ├── MiniExcel.dll
│   ├── MySql.Data.dll
│   ├── Newtonsoft.Json.dll
│   ├── Npgsql.dll
│   ├── Oracle.ManagedDataAccess.dll
│   ├── PropertyChanged.dll
│   ├── SqlSugar.dll
│   ├── System.Data.SQLite.dll
│   └── ...                        # 其他依赖DLL
├── Config/                        # 配置文件夹
│   ├── projConfig.xml             # 项目配置
│   ├── templateConfig.xml         # 模板配置
│   └── tips.json                  # 提示配置
├── Language/                      # 多语言资源
│   ├── zh_cn.xaml                 # 中文资源
│   └── en_us.xaml                 # 英文资源
├── Images/                        # 图片资源
│   ├── icon.png
│   ├── 配置工具logo.ico
│   ├── 配置工具logo.png
│   ├── RXG_LocalPLC_OPCUA_Structure.png
│   ├── RXG_RosCPU_OPCUA_Structure.png
│   ├── RXG_Uniform_RCCS_OPCUA_Structure.png
│   └── ...                        # 其他图片文件
└── Logs/                          # 日志文件夹
    └── *.log                      # 运行时生成的日志文件
```

### 文件组织原则
- **根目录简洁**：只保留主程序 `DataViewConfig.exe`
- **分类存放**：所有其他文件按类型分别存放在子文件夹中
- **便于维护**：清晰的目录结构便于文件管理和问题排查
- **部署友好**：整个 `DV_ConfigTool` 目录可以直接复制部署

## 开发环境配置

### 必需工具
- Visual Studio 2017 或更高版本
- .NET Framework 4.7.2 SDK
- NuGet 包管理器

### 可选工具
- Git 版本控制
- 数据库管理工具（如 Navicat、SSMS）
- 图片编辑工具
- 文档编辑器

### 开发流程
1. 克隆或下载项目代码
2. 使用 Visual Studio 打开 SourceCode.sln
3. 还原 NuGet 包
4. 配置数据库连接
5. 编译和运行项目

## 部署说明

### 部署包内容
- **主程序**：`DataViewConfig.exe`
- **依赖库**：`Libs/` 文件夹中的所有DLL文件
- **配置文件**：`Config/` 文件夹中的配置文件
- **资源文件**：`Language/` 和 `Images/` 文件夹
- **数据库文件**：`Screen/Program/ConfigFiles/dataview.db`（SQLite数据库）
- **日志目录**：`Logs/` 文件夹（运行时自动创建）

### 部署步骤
1. **准备目标环境**：确保安装了 .NET Framework 4.7.2
2. **复制文件**：将整个 `DV_ConfigTool` 目录复制到目标位置
3. **复制数据库**：确保 `Screen/Program/ConfigFiles/dataview.db` 文件存在
4. **设置权限**：确保程序对 `Logs/` 目录有写入权限
5. **测试运行**：启动 `DataViewConfig.exe` 验证功能正常

### 维护建议
- **定期备份**：备份 `dataview.db` 数据库文件和 `Config/` 配置文件
- **日志监控**：定期检查 `Logs/` 目录中的日志文件
- **版本更新**：更新时替换 `DV_ConfigTool` 目录，保留数据库文件
- **权限管理**：确保程序运行权限和文件访问权限正常
