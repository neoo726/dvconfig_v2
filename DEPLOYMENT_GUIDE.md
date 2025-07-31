# DataView Config Tool V2 - 部署配置指南

## 概述

DataView Config Tool V2 是一个基于 SQLite 数据库的配置管理工具。本指南详细说明了项目的编译、部署和配置过程。

## 项目编译

### 编译环境要求
- Visual Studio 2017 或更高版本
- .NET Framework 4.7.2 SDK
- Windows 操作系统

### 编译步骤
1. 打开 `SourceCode.sln` 解决方案文件
2. 还原 NuGet 包依赖
3. 选择 Release 配置
4. 编译解决方案

### 编译输出
编译完成后，所有文件会输出到 `DV_ConfigTool` 目录，结构如下：

```
DV_ConfigTool/
├── DataViewConfig.exe              # 主程序（根目录唯一exe）
├── Libs/                          # 依赖库文件夹
├── Config/                        # 配置文件夹
├── Language/                      # 多语言资源
├── Images/                        # 图片资源
└── Logs/                          # 日志文件夹（运行时创建）
```

## 数据库配置

### SQLite 数据库（默认）

#### 数据库文件位置
```
Screen/Program/ConfigFiles/dataview.db
```

#### 数据库特点
- **轻量级**：无需安装数据库服务器
- **便携性**：数据库文件可随项目移动
- **零配置**：系统自动连接，无需额外设置
- **完整性**：包含所有配置数据和用户信息

#### 数据库初始化
1. 首次运行时，如果数据库文件不存在，系统会提示创建
2. 可以从现有项目复制 `dataview.db` 文件
3. 系统启动时自动验证数据库结构

### 可选数据库配置

#### MySQL 配置
如果需要使用 MySQL 数据库：

1. **修改配置文件** `Config/projConfig.xml`：
```xml
<ProjConfigXml>
    <DbConnectionString>
        <DatabaseType>mysql</DatabaseType>
        <ConnectingString>Data Source=服务器地址;Port=3306;Initial Catalog=数据库名;User ID=用户名;Password=密码;</ConnectingString>
    </DbConnectionString>
</ProjConfigXml>
```

2. **确保 MySQL 服务器可访问**
3. **创建对应的数据库和表结构**

#### SQL Server 配置
如果需要使用 SQL Server：

1. **修改配置文件** `Config/projConfig.xml`：
```xml
<ProjConfigXml>
    <DbConnectionString>
        <DatabaseType>sqlserver</DatabaseType>
        <ConnectingString>Data Source=服务器地址;Initial Catalog=数据库名;User ID=用户名;Password=密码;</ConnectingString>
    </DbConnectionString>
</ProjConfigXml>
```

## 部署指南

### 目标环境要求
- **操作系统**：Windows 7 或更高版本
- **运行时**：.NET Framework 4.7.2
- **权限**：对安装目录和日志目录的读写权限
- **磁盘空间**：至少 100MB 可用空间

### 部署步骤

#### 1. 准备部署包
```bash
# 复制编译输出目录
DV_ConfigTool/
├── DataViewConfig.exe
├── Libs/
├── Config/
├── Language/
├── Images/
└── Logs/

# 复制数据库文件
Screen/Program/ConfigFiles/dataview.db
```

#### 2. 部署到目标环境
1. **创建安装目录**（如：`C:\DataViewConfig\`）
2. **复制 DV_ConfigTool 目录**到安装位置
3. **复制 Screen 目录**到安装位置（包含数据库文件）
4. **设置目录权限**：确保程序对 `Logs/` 目录有写入权限

#### 3. 验证部署
1. 双击 `DataViewConfig.exe` 启动程序
2. 检查是否能正常连接数据库
3. 验证登录功能（默认账户：admin/admin）
4. 检查日志文件是否正常生成

### 目录结构说明

#### 完整部署结构
```
安装目录/
├── DV_ConfigTool/                  # 程序主目录
│   ├── DataViewConfig.exe          # 主程序
│   ├── Libs/                      # 依赖库
│   │   ├── *.dll                  # 所有依赖DLL文件
│   ├── Config/                    # 配置文件
│   │   ├── projConfig.xml         # 项目配置
│   │   ├── templateConfig.xml     # 模板配置
│   │   └── tips.json              # 提示配置
│   ├── Language/                  # 多语言资源
│   │   ├── zh_cn.xaml             # 中文资源
│   │   └── en_us.xaml             # 英文资源
│   ├── Images/                    # 图片资源
│   │   └── *.png, *.ico           # 图标和图片
│   └── Logs/                      # 日志目录
│       └── *.log                  # 运行日志
└── Screen/                        # 画面和数据库目录
    ├── *.csw                      # 画面文件
    └── Program/
        └── ConfigFiles/
            └── dataview.db        # SQLite数据库文件
```

## 配置文件说明

### 1. projConfig.xml - 项目配置
```xml
<?xml version="1.0" encoding="utf-8"?>
<ProjConfigXml>
    <UserList>
        <!-- 用户账户配置 -->
        <User userLevel="1" userName="zpmc" userPwd="zpmc"/>
        <User userLevel="2" userName="admin" userPwd="admin"/>
    </UserList>
    <!-- 是否显示数据库选择界面 -->
    <ShowDbSelectionWindow enable="false" />
    <!-- 是否显示配置选择器 -->
    <ShowRcmsConfigSelector enable="false"/>
    <!-- 默认语言 -->
    <CurLanguage>zh-CN</CurLanguage>
</ProjConfigXml>
```

### 2. templateConfig.xml - 模板配置
包含可用的模板列表和配置信息，支持 RMG 和 STS 两种类型的模板。

### 3. tips.json - 提示配置
包含界面提示信息的多语言配置。

## 运行配置

### 启动流程
1. **启动程序**：双击 `DataViewConfig.exe`
2. **数据库连接**：自动连接到 `Screen/Program/ConfigFiles/dataview.db`
3. **用户登录**：使用默认账户或配置的用户账户登录
4. **主界面**：进入配置管理主界面

### 默认用户账户
| 用户名 | 密码  | 权限级别 | 说明           |
|--------|-------|----------|----------------|
| zpmc   | zpmc  | 1        | 调试人员权限   |
| admin  | admin | 2        | 管理员权限     |

### 语言设置
- 默认语言：中文（zh-CN）
- 支持语言：中文、英文
- 切换方式：修改 `projConfig.xml` 中的 `CurLanguage` 设置

## 维护和备份

### 数据备份
1. **数据库备份**：定期备份 `Screen/Program/ConfigFiles/dataview.db`
2. **配置备份**：备份 `Config/` 目录下的所有配置文件
3. **完整备份**：备份整个安装目录

### 日志管理
- **日志位置**：`DV_ConfigTool/Logs/` 目录
- **日志内容**：程序运行日志、错误信息、操作记录
- **日志清理**：定期清理过期日志文件

### 版本升级
1. **备份数据**：升级前备份数据库和配置文件
2. **替换程序**：用新版本替换 `DV_ConfigTool` 目录
3. **保留数据**：保留原有的数据库文件和用户配置
4. **验证功能**：升级后验证所有功能正常

## 故障排除

### 常见问题

#### 1. 程序无法启动
- **检查 .NET Framework**：确保安装了 4.7.2 或更高版本
- **检查文件权限**：确保对程序目录有执行权限
- **查看日志**：检查 `Logs/` 目录中的错误日志

#### 2. 数据库连接失败
- **检查数据库文件**：确认 `dataview.db` 文件存在且可访问
- **检查文件路径**：验证数据库文件路径是否正确
- **权限问题**：确保对数据库文件有读写权限

#### 3. 配置丢失
- **恢复备份**：从备份中恢复配置文件
- **重置配置**：删除配置文件，使用默认配置重新启动
- **检查文件完整性**：验证配置文件格式是否正确

#### 4. 界面显示异常
- **检查语言文件**：确认 `Language/` 目录中的资源文件完整
- **重置界面**：重启程序或重置窗口布局
- **检查图片资源**：确认 `Images/` 目录中的图片文件完整

### 技术支持
如遇到无法解决的问题：
1. 收集错误日志和截图
2. 记录操作步骤和环境信息
3. 联系技术支持团队

## 安全注意事项

### 数据安全
- 定期备份重要数据
- 设置强密码策略
- 限制文件访问权限
- 监控异常操作

### 系统安全
- 保持系统更新
- 使用防病毒软件
- 限制网络访问
- 定期安全检查

---

**文档版本**：V2.0.0.7  
**最后更新**：2025年1月  
**维护团队**：DataView开发团队
