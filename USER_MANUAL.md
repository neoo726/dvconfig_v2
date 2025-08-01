# DataView Config Tool V2 - 用户使用手册

## 目录

1. [系统概述](#系统概述)
2. [安装和启动](#安装和启动)
3. [用户登录](#用户登录)
4. [主界面介绍](#主界面介绍)
5. [基础信息配置](#基础信息配置)
6. [工程配置](#工程配置)
7. [功能配置](#功能配置)
8. [交互功能配置](#交互功能配置)
9. [工程调试](#工程调试)
10. [常见问题](#常见问题)

## 系统概述

DataView Config Tool V2 是一个专为港口起重机械（RMG/STS）设计的 SQLite 数据库配置工具。该工具提供了直观的图形化界面，帮助用户轻松配置各种系统参数、设备信息、界面设置和交互功能。

### 主要功能
- 设备和系统基础信息配置
- 数据点和画面配置管理
- 用户登录和权限管理
- 外部系统接口配置
- 消息分发和处理配置
- 多语言支持（中文/英文）
- SQLite 数据库管理和配置

## 安装和启动

### 系统要求
- 操作系统：Windows 7 或更高版本
- .NET Framework 4.7.2 或更高版本
- 内存：至少 2GB RAM
- 硬盘空间：至少 500MB 可用空间

### 启动应用程序
1. 进入 `DV_ConfigTool` 目录
2. 双击 `DataViewConfig.exe` 启动应用程序
3. 系统会自动连接到 SQLite 数据库文件：`Screen/Program/ConfigFiles/dataview.db`
4. 如果数据库文件不存在，系统会提示选择或创建数据库
5. 配置完成后进入登录界面

## 用户登录

### 登录界面
启动应用程序后，首先会显示登录界面。

### 默认账户
系统提供两个默认账户：

| 用户名 | 密码 | 权限级别 | 说明 |
|--------|------|----------|------|
| zpmc   | zpmc | 调试人员 | 基础操作权限 |
| admin  | admin| 管理员   | 完整配置权限 |

### 登录步骤
1. 输入用户名和密码
2. 点击"登录"按钮
3. 系统验证成功后进入主界面

## 主界面介绍

### 界面布局
主界面采用经典的左右分栏布局：

```
┌─────────────────────────────────────────────────────────┐
│  标题栏 - DataView Config Tool                          │
├─────────────┬───────────────────────────────────────────┤
│             │                                           │
│   侧边菜单   │            主内容区域                      │
│             │                                           │
│   - 工程调试 │                                           │
│   - 基础信息 │                                           │
│   - 工程配置 │                                           │
│   - 功能配置 │                                           │
│   - 交互功能 │                                           │
│             │                                           │
└─────────────┴───────────────────────────────────────────┘
```

### 侧边菜单
侧边菜单按功能模块组织，包括：
- **工程调试**：导航配置等调试功能
- **基础信息**：起重机、RCS、堆场等基础配置
- **工程配置**：点名、画面、控件等工程相关配置
- **功能配置**：登录、画面切换、提示窗口等功能设置
- **交互功能**：外部系统交互配置

## 基础信息配置

### 起重机配置

**功能**：管理起重机设备的基础信息

**操作步骤**：
1. 点击侧边菜单中的"起重机配置"
2. 在列表中查看现有起重机配置
3. 点击"添加"按钮新增起重机
4. 填写起重机信息：
   - 起重机编号
   - 起重机名称
   - 起重机类型
   - 描述信息
5. 点击"保存"完成添加

**编辑和删除**：
- 双击列表项或点击"编辑"按钮修改配置
- 选中项目后点击"删除"按钮删除配置

### RCS配置

**功能**：配置远程控制系统信息

**配置项**：
- RCS系统编号
- 系统名称
- 连接地址
- 通信参数
- 系统描述

### 堆场配置

**功能**：管理堆场区域信息（仅RMG模式显示）

**配置内容**：
- 堆场编号
- 堆场名称
- 区域范围
- 贝位信息

## 工程配置

### 点名配置

**功能**：管理系统中的数据点配置

**操作步骤**：
1. 点击"点名配置"菜单项
2. 查看现有点名列表
3. 添加新点名：
   - 点名（DataAccess）：数据访问名称
   - 点名（内部）：内部使用名称
   - 点名类型：选择数据类型
   - 描述：点名说明
4. 设置点名属性：
   - 数据类型（布尔、整数、浮点等）
   - 后缀类型
   - 关联宏名称

**注意事项**：
- 点名必须唯一，不能重复
- 内部点名可以选择与DataAccess保持一致
- 数据类型选择要与实际数据匹配

### 画面配置

**功能**：管理系统界面画面配置

**操作步骤**：
1. 点击"画面配置"菜单项
2. 填写画面信息：
   - 画面名称（内部）：系统内部使用的画面标识
   - 画面描述：画面功能说明
3. 选择CSW文件：
   - 点击文件夹图标
   - 浏览并选择对应的CSW画面文件
4. 点击"添加"保存配置

**CSW文件说明**：
- CSW文件是画面定义文件
- 文件路径会自动保存
- 支持相对路径和绝对路径

### 控件配置

**功能**：管理界面控件的配置信息

**配置内容**：
- 控件名称和类型
- 控件属性设置
- 数据绑定配置
- 样式和布局设置

## 功能配置

### 登录配置

**功能**：配置用户登录相关设置

**配置项**：
- 用户账户管理
- 密码策略设置
- 登录界面定制
- 权限级别配置

**用户管理**：
- 添加新用户
- 修改用户密码
- 设置用户权限级别
- 启用/禁用用户账户

### 画面切换配置

**功能**：配置画面之间的切换逻辑

**配置内容**：
- 画面切换条件
- 切换动画效果
- 权限控制
- 快捷键设置

### 提示窗口配置

**功能**：配置系统提示信息和帮助内容

**配置项**：
- 提示信息内容
- 显示条件和时机
- 多语言支持
- 图片和文本提示

## 交互功能配置

### 交互参数配置

**功能**：管理与外部系统交互的参数

**参数类型**：
- 常量参数：固定值参数
- 控件参数：从界面控件获取
- 点名参数：从数据点获取
- 宏参数：从系统宏获取

**配置步骤**：
1. 选择参数来源类型
2. 设置参数名称和描述
3. 配置参数值或绑定源
4. 设置参数验证规则
5. 保存参数配置

### 接口配置

**功能**：配置与外部系统的接口调用

**支持的接口类型**：
- REST API：HTTP/HTTPS接口
- OPC UA：工业自动化协议
- MQ消息队列：异步消息通信
- RPC调用：远程过程调用

**配置步骤**：
1. 选择接口类型
2. 设置接口地址和端口
3. 配置请求参数
4. 设置响应处理规则
5. 测试接口连通性

### 消息分发配置

**功能**：配置消息的接收和分发规则

**配置内容**：
- 消息类型定义
- 接收条件设置
- 分发目标配置
- 处理规则定义

## 工程调试

### 导航配置

**功能**：配置系统导航和调试相关设置

**配置项**：
- 调试模式开关
- 日志级别设置
- 性能监控配置
- 错误处理规则

## 数据库配置

### 默认数据库：SQLite

系统默认使用 SQLite 数据库，具有以下特点：
- **数据库文件位置**：`Screen/Program/ConfigFiles/dataview.db`
- **轻量级**：无需安装数据库服务器
- **便携性**：数据库文件可以随项目一起移动
- **配置简单**：系统自动连接，无需额外配置

### 数据库文件管理

#### 默认配置
- 系统启动时自动连接到 `Screen/Program/ConfigFiles/dataview.db`
- 如果文件不存在，系统会提示创建或选择数据库文件
- 数据库文件包含所有配置信息和数据

#### 手动选择数据库
如果需要使用其他数据库文件：
1. 启动应用程序
2. 在数据库选择界面点击"选择文件"
3. 浏览并选择目标 SQLite 数据库文件
4. 系统验证文件有效性后连接

### 可选数据库类型

#### MySQL（企业级部署）
- 企业级数据库，性能稳定
- 支持多用户并发访问
- 需要配置服务器地址、端口、用户名、密码

#### SQL Server（Windows环境）
- 微软数据库产品
- 与Windows系统集成度高
- 支持Windows身份验证

### 数据库连接配置
1. 默认使用 SQLite（推荐）
2. 如需其他数据库类型，在启动界面选择
3. 填写连接参数
4. 测试连接
5. 保存配置

## 模板系统使用

### 模板选择
系统提供多种预定义模板：

**RMG模板**：
- auto_rmg_ecsmq：ECS消息队列模式
- auto_rmg_rccs：RCCS统一控制模式
- auto_rmg_roscpu：ROS CPU控制模式

**STS模板**：
- auto_sts_br：BR控制模式
- auto_sts_rccs：RCCS控制模式
- semiauto_sts_br：半自动BR模式
- semiauto_sts_rccs：半自动RCCS模式

### 模板应用
1. 在模板选择界面选择合适的模板
2. 系统自动加载模板配置
3. 根据实际需求调整配置参数
4. 保存自定义配置

## 多语言切换

### 语言设置
1. 在配置文件中设置默认语言
2. 系统启动时自动加载对应语言包
3. 支持运行时语言切换（需重启生效）

### 支持的语言
- 中文（简体）：zh-CN
- 英文：en-US

## 数据导入导出

### 支持的数据格式
- CSV：用户配置、设备列表等
- Excel：批量数据导入导出
- XML：配置文件备份和恢复

### 导入导出操作
1. 选择对应的配置页面
2. 点击"导入"或"导出"按钮
3. 选择文件格式和路径
4. 确认操作

## 常见问题

### Q1：无法连接数据库怎么办？
**A1**：
1. 检查数据库服务是否启动
2. 验证连接参数是否正确
3. 确认网络连接是否正常
4. 检查防火墙设置

### Q2：忘记登录密码怎么办？
**A2**：
1. 使用管理员账户重置密码
2. 或者修改配置文件中的用户信息
3. 联系系统管理员

### Q3：配置保存失败怎么办？
**A3**：
1. 检查数据库连接状态
2. 验证输入数据的格式和完整性
3. 确认用户权限是否足够
4. 查看错误日志获取详细信息

### Q4：界面显示异常怎么办？
**A4**：
1. 检查屏幕分辨率设置
2. 更新显卡驱动程序
3. 重启应用程序
4. 检查.NET Framework版本

### Q5：如何备份配置数据？
**A5**：
1. 备份数据库文件（SQLite）
2. 导出配置数据为Excel或CSV格式
3. 备份配置文件目录
4. 定期创建系统快照

### Q6：如何升级系统版本？
**A6**：
1. 备份当前配置数据
2. 下载新版本安装包
3. 停止当前应用程序
4. 安装新版本
5. 恢复配置数据
6. 验证功能正常

## 技术支持

如遇到技术问题，请联系技术支持团队：
- 提供详细的错误描述
- 附上错误截图或日志文件
- 说明操作步骤和环境信息

## 版本更新说明

请定期关注版本更新，新版本通常包含：
- 功能改进和优化
- 错误修复
- 安全性增强
- 新功能添加

更新前请务必备份重要数据。
