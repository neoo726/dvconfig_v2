# DataView Config Tool V2

## 项目概述

DataView Config Tool V2 是一个基于 WPF 的 SQLite 数据库配置管理工具，专门用于港口起重机械（RMG/STS）的数据可视化系统配置。该工具提供了图形化界面来管理各种配置参数，包括设备配置、界面配置、交互系统配置等。

## 技术栈

- **框架**: .NET Framework 4.7.2
- **UI框架**: WPF (Windows Presentation Foundation)
- **UI库**: HandyControl 3.4.0
- **主要数据库**: SQLite (主要)，同时支持 MySQL、SQL Server
- **数据库文件**: `Screen/Program/ConfigFiles/dataview.db`
- **ORM**: SqlSugar
- **其他依赖**:
  - PropertyChanged.Fody (属性变更通知)
  - Newtonsoft.Json (JSON处理)
  - MiniExcel (Excel操作)
  - Markdig (Markdown处理)

## 项目结构

```
dvconfig_v2/
├── DataViewConfig/                 # 主项目目录
│   ├── Pages/                      # 配置页面
│   │   ├── BlockConfigPage.xaml    # 堆场配置
│   │   ├── CraneConfigPage.xaml    # 起重机配置
│   │   ├── ScreenConfigPage.xaml   # 画面配置
│   │   ├── TagConfigPage.xaml      # 点名配置
│   │   ├── LoginConfigPage.xaml    # 登录配置
│   │   └── ...                     # 其他配置页面
│   ├── ViewModels/                 # 视图模型
│   ├── Controls/                   # 自定义控件
│   ├── Converters/                 # 数据转换器
│   ├── Models/                     # 数据模型
│   ├── Config/                     # 配置文件处理
│   ├── Images/                     # 图片资源
│   ├── Language/                   # 多语言支持
│   └── Styles/                     # 样式文件
├── Screen/                         # 画面文件和数据库
│   ├── Program/                    # 程序配置
│   │   └── ConfigFiles/            # 配置文件目录
│   │       └── dataview.db         # SQLite数据库文件
│   └── *.csw                       # 画面文件
├── DV_ConfigTool/                  # 编译输出目录
│   ├── DataViewConfig.exe          # 主程序
│   ├── Libs/                       # 依赖库文件夹
│   ├── Logs/                       # 日志文件夹
│   ├── Config/                     # 配置文件夹
│   ├── Language/                   # 多语言资源文件夹
│   └── Images/                     # 图片资源文件夹
└── packages/                       # NuGet包
```

## 主要功能模块

### 1. 基础信息配置
- **起重机配置** (`CraneConfigPage`): 管理起重机设备信息
- **RCS配置** (`RcsConfigPage`): 远程控制系统配置
- **堆场配置** (`BlockConfigPage`): 堆场区域配置

### 2. 工程配置
- **点名配置** (`TagConfigPage`): 数据点配置管理
- **画面配置** (`ScreenConfigPage`): 界面画面配置
- **控件配置** (`ControlConfigPage`): UI控件配置

### 3. 功能配置
- **登录配置** (`LoginConfigPage`): 用户登录设置
- **画面切换配置** (`ScreenSwitchConfigPage`): 画面切换逻辑
- **提示窗口配置** (`DvTipsConfigPage`): 提示信息配置

### 4. 交互功能配置
- **交互参数配置** (`InteractiveParameterConfigPage`): 交互参数管理
- **接口配置** (`InterfaceConfigPage`): 外部接口配置
- **消息分发配置** (`FanoutReceiveConfigPage`): 消息分发设置

### 5. 工程调试
- **导航配置** (`NaviConfigPage`): 导航设置

## 配置文件说明

### 主要配置文件

1. **projConfig.xml**: 项目配置文件
   - 用户账户配置
   - 界面显示设置
   - 语言配置

2. **templateConfig.xml**: 模板配置文件
   - 支持 RMG (轨道式门式起重机) 和 STS (岸边集装箱起重机) 模板
   - 模板类型包括：
     - auto_rmg_ecsmq: 自动化RMG-ECS消息队列
     - auto_rmg_rccs: 自动化RMG-RCCS
     - auto_rmg_roscpu: 自动化RMG-ROS CPU
     - auto_sts_br: 自动化STS-BR
     - auto_sts_rccs: 自动化STS-RCCS

3. **App.config**: 应用程序配置

## 数据库支持

项目主要使用 SQLite 数据库，同时支持其他数据库类型：
- **SQLite**: 主要数据库，文件位置：`Screen/Program/ConfigFiles/dataview.db`
- **MySQL**: 企业级关系数据库（可选）
- **SQL Server**: 微软关系数据库（可选）

### 主要数据表
- `dv_system`: 系统配置
- `dv_tag`: 数据点配置
- `dv_screen_conf`: 画面配置
- `dv_request_interface`: 请求接口配置
- `dv_receive_fanout`: 接收分发配置
- `dv_control_default_value`: 控件默认值配置

## 用户权限

系统支持两种用户级别：
- **调试人员** (Level 1): 基础操作权限
- **设计开发人员** (Level 2): 完整配置权限

默认账户：
- 调试账户: zpmc/zpmc
- 管理员账户: admin/admin

## 多语言支持

支持中英文双语：
- 中文 (zh-CN)
- 英文 (en-US)

语言文件位于 `Language/` 目录下。

## 版本历史

### V2.0.0.7 (2025-1-8)
- 本地用户配置支持CSV导入导出
- 用户较多时支持滚动显示
- 修复新增本地用户时无法修改密码的问题
- 设备列表支持导入导出

### V2.0.0.6 (2024-12-30)
- 支持参数表达式转换int32

### V2.0.0.2 (2024-10-16)
- 参数列表优化：添加新参数后立即刷新
- 接口配置优化：OPC交互方式支持CmdIx/Index/Value形式

## 编译和运行

### 环境要求
- Visual Studio 2017 或更高版本
- .NET Framework 4.7.2
- Windows 操作系统

### 编译步骤
1. 打开 `SourceCode.sln` 解决方案文件
2. 还原 NuGet 包
3. 编译解决方案（输出到 `DV_ConfigTool` 目录）

### 输出目录结构
编译后的文件会输出到 `DV_ConfigTool` 目录，结构如下：
```
DV_ConfigTool/
├── DataViewConfig.exe              # 主程序（根目录下唯一的exe文件）
├── Libs/                          # 依赖库文件夹
│   ├── *.dll                      # 所有依赖的DLL文件
│   └── ...
├── Config/                        # 配置文件夹
│   ├── projConfig.xml             # 项目配置
│   ├── templateConfig.xml         # 模板配置
│   └── tips.json                  # 提示配置
├── Language/                      # 多语言资源
│   ├── zh_cn.xaml                 # 中文资源
│   └── en_us.xaml                 # 英文资源
├── Images/                        # 图片资源
│   └── *.png, *.ico               # 图标和图片文件
└── Logs/                          # 日志文件夹
    └── *.log                      # 运行日志
```

### 运行配置
1. 确保 SQLite 数据库文件存在：`Screen/Program/ConfigFiles/dataview.db`
2. 配置数据库连接（默认使用 SQLite）
3. 设置用户权限
4. 启动应用程序

## 开发说明

### 架构模式
项目采用 MVVM (Model-View-ViewModel) 架构模式：
- **Model**: 数据模型和业务逻辑
- **View**: XAML 界面文件
- **ViewModel**: 视图逻辑和数据绑定

### 关键组件
- **MainViewModel**: 主窗口视图模型，管理菜单和页面导航
- **DbHelper**: 数据库访问助手
- **GlobalConfig**: 全局配置管理
- **EventBus**: 事件总线，用于组件间通信

### 自定义控件
项目包含多个自定义控件：
- `MenuItem`: 菜单项控件
- `SearchTextBox`: 搜索文本框
- `TipsLabel`: 提示标签
- `MultiCombox`: 多选下拉框

## 许可证

本项目为内部开发工具，版权归属相关开发团队。

## 联系信息

如有问题或建议，请联系开发团队。
