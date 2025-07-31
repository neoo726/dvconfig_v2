# DataView Config Tool V2 - 技术文档

## 系统架构

### 整体架构
DataView Config Tool V2 采用分层架构设计：

```
┌─────────────────────────────────────┐
│           Presentation Layer        │
│  (WPF Views + ViewModels + Controls)│
├─────────────────────────────────────┤
│           Business Layer            │
│     (Configuration Management)      │
├─────────────────────────────────────┤
│            Data Layer               │
│  (SqlSugar ORM + Database Access)   │
├─────────────────────────────────────┤
│           Database Layer            │
│   (SQLite/MySQL/SQL Server)         │
└─────────────────────────────────────┘
```

### 核心组件

#### 1. 主窗口管理 (MainViewModel)
- 负责整个应用程序的导航和页面管理
- 管理侧边菜单栏的动态生成
- 处理用户权限控制
- 管理交互系统的创建和删除

#### 2. 数据库访问层 (DbHelper)
```csharp
public sealed class DbHelper
{
    public static SqlSugar.SqlSugarScope db;
}
```
- 使用单例模式管理数据库连接
- 支持多种数据库类型的统一访问
- 提供事务管理功能

#### 3. 配置管理 (GlobalConfig)
```csharp
public static class GlobalConfig
{
    public static GlobalLanguage CurLanguage = GlobalLanguage.zhCN;
    public static bool IsDataViewConfig = true;
    public static UserLevelType CurUserLevel = UserLevelType.Administrator;
    public static Dictionary<string, TipsModel> ConfigToolTipsDict;
}
```

## 数据模型设计

### 核心数据表结构

#### 1. 系统配置表 (dv_system)
- `system_id`: 系统ID
- `system_name`: 系统名称
- `system_desc`: 系统描述
- `is_permanent`: 是否为常驻系统

#### 2. 数据点配置表 (dv_tag)
- `tag_internal_name`: 内部点名
- `tag_name`: 显示点名
- `tag_desc`: 点名描述
- `postfix_type_id`: 后缀类型ID
- `data_type_id`: 数据类型ID
- `related_macro_name`: 关联宏名称

#### 3. 画面配置表 (dv_screen_conf)
- `dv_screen_internal_name`: 画面内部名称
- `dv_screen_csw_name`: CSW文件名
- `dv_screen_desc`: 画面描述

#### 4. 接口配置表 (dv_request_interface)
- `interface_name`: 接口名称
- `system_id`: 所属系统ID
- `request_url`: 请求URL
- `request_method`: 请求方法
- `request_content`: 请求内容配置

#### 5. 消息分发配置表 (dv_receive_fanout)
- `fanout_name`: 分发名称
- `msg_type`: 消息类型
- `system_id`: 所属系统ID
- `device_field`: 设备字段
- `receive_type_id`: 接收类型
- `write_type_id`: 写入类型

## 配置页面详解

### 1. 点名配置页面 (TagConfigPage)

**功能**：管理系统中的数据点配置

**主要操作**：
- 添加新的数据点
- 编辑现有数据点
- 删除数据点
- 搜索和过滤数据点

**数据验证**：
- 点名唯一性检查
- 数据类型验证
- 必填字段验证

### 2. 画面配置页面 (ScreenConfigPage)

**功能**：管理系统画面配置

**主要操作**：
- 添加新画面配置
- 选择CSW文件
- 编辑画面描述
- 删除画面配置

**文件管理**：
- 支持CSW文件选择
- 文件路径验证
- 画面预览功能

### 3. 交互系统配置页面 (InteractiveSystemConfigPage)

**功能**：管理外部系统交互配置

**主要功能**：
- 创建新的交互系统
- 配置接口参数
- 设置消息分发规则
- 管理控件默认值

### 4. 接口配置页面 (InterfaceConfigPage)

**功能**：配置外部接口调用

**支持的接口类型**：
- REST API
- OPC UA
- MQ消息队列
- RPC调用

**配置项**：
- 请求URL和方法
- 请求参数配置
- 响应处理规则
- 错误处理机制

## 模板系统

### 模板类型

#### RMG (轨道式门式起重机) 模板
1. **auto_rmg_ecsmq**: ECS消息队列模式
2. **auto_rmg_rccs**: RCCS统一控制模式
3. **auto_rmg_roscpu**: ROS CPU控制模式

#### STS (岸边集装箱起重机) 模板
1. **auto_sts_br**: BR控制模式
2. **auto_sts_rccs**: RCCS控制模式
3. **semiauto_sts_br**: 半自动BR模式
4. **semiauto_sts_rccs**: 半自动RCCS模式

### 模板配置结构
```xml
<template id="1" 
          name="auto_rmg_ecsmq" 
          templateType="1" 
          desc="auto_rmg_ecsmq" 
          url="https://..." 
          imagePath="\Images\RXG_LocalPLC_OPCUA_Structure.png" 
          templateFilePath="\TemplateFile\rxg\auto_rmg_ecsmq\" 
          isSelected="true" 
          tipsName="[template_selection_page]auto_rmg_ecsmq" />
```

## 数据库连接配置

### 主要数据库：SQLite

项目主要使用 SQLite 数据库，数据库文件位置：
```
Screen/Program/ConfigFiles/dataview.db
```

#### SQLite 连接配置
```xml
<ConnectingString>Data Source=Screen/Program/ConfigFiles/dataview.db;Version=3;</ConnectingString>
```

### 可选数据库类型

#### 1. MySQL（可选）
```xml
<ConnectingString>Data Source=117.50.186.229;Initial Catalog=dataview_template_dev;User ID=root;Password=Zpmc@3261;</ConnectingString>
```

#### 2. SQL Server（可选）
```xml
<ConnectingString>Data Source=ip address;Initial Catalog=RCMSDB;User ID=cms;Password=zpmc;</ConnectingString>
```

### 数据库初始化流程
1. 默认使用 SQLite 数据库文件：`Screen/Program/ConfigFiles/dataview.db`
2. 读取配置文件中的连接字符串（如有自定义配置）
3. 创建SqlSugar实例
4. 测试数据库连接
5. 初始化数据表结构
6. 加载基础配置数据

## 用户权限管理

### 权限级别
```csharp
public enum UserLevelType
{
    Operator = 1,        // 操作员
    Administrator = 2    // 管理员
}
```

### 权限控制机制
- 基于用户级别的菜单显示控制
- 页面访问权限验证
- 操作按钮的可见性控制
- 数据修改权限检查

## 多语言支持

### 语言配置
```csharp
public enum GlobalLanguage
{
    enUS = 1,  // 英文
    zhCN = 2   // 中文
}
```

### 资源文件结构
- `Language/zh_cn.xaml`: 中文资源
- `Language/en_us.xaml`: 英文资源

### 动态语言切换
- 运行时语言切换
- 界面元素自动更新
- 配置保存和恢复

## 事件系统

### EventBus 实现
```csharp
public class EventBus
{
    // 事件订阅和发布机制
    // 用于组件间的松耦合通信
}
```

### 常用事件类型
- 配置更新事件
- 数据刷新事件
- 用户操作事件
- 系统状态变更事件

## 自定义控件

### 1. MenuItem 控件
- 支持层级菜单结构
- 动态菜单生成
- 图标和文本显示

### 2. SearchTextBox 控件
- 内置搜索功能
- 实时过滤
- 清除按钮

### 3. TipsLabel 控件
- 上下文帮助提示
- 多语言支持
- 图片和文本提示

### 4. MultiCombox 控件
- 多选下拉框
- 自定义选项显示
- 数据绑定支持

## 性能优化

### 1. 页面缓存机制
```csharp
public Dictionary<string, UIElement> PageDict { get; set; }
```
- 避免重复创建页面实例
- 提高页面切换速度
- 减少内存占用

### 2. 数据库连接池
- 使用SqlSugar的连接池管理
- 自动连接回收
- 并发访问优化

### 3. 异步操作
- 数据库操作异步化
- UI响应性优化
- 长时间操作的进度显示

## 错误处理

### 异常处理策略
1. **数据库异常**：事务回滚，错误提示
2. **文件操作异常**：路径验证，权限检查
3. **网络异常**：重试机制，超时处理
4. **配置异常**：默认值回退，配置修复

### 日志记录
- 使用log4net进行日志记录
- 分级日志输出
- 错误堆栈跟踪
- 操作审计日志

## 部署和维护

### 部署要求
- Windows 7 或更高版本
- .NET Framework 4.7.2
- SQLite 数据库文件：`Screen/Program/ConfigFiles/dataview.db`

### 部署目录结构
```
DV_ConfigTool/
├── DataViewConfig.exe              # 主程序
├── Libs/                          # 依赖库
├── Config/                        # 配置文件
├── Language/                      # 多语言资源
├── Images/                        # 图片资源
└── Logs/                          # 日志文件
```

### 配置文件管理
- 配置文件统一存放在 `Config/` 目录
- 数据库文件位于 `Screen/Program/ConfigFiles/dataview.db`
- 日志文件自动生成到 `Logs/` 目录
- 配置备份和恢复

### 维护工具
- SQLite 数据库管理工具
- 配置验证工具
- 日志分析工具
- 性能监控工具
