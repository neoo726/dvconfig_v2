# DataView Config Tool V2 - 架构重构检查清单

## 📋 总体进度跟踪

- [ ] 第一阶段：依赖注入基础设施（1周）
- [ ] 第二阶段：数据访问层重构（1.5周）
- [ ] 第三阶段：命令系统改进（0.5周）
- [ ] 第四阶段：MVVM架构优化（1.5周）
- [ ] 第五阶段：错误处理和状态管理（0.5周）

**预计总时间：4周**

---

## 🔧 第一阶段：依赖注入基础设施

### Step 1.1: 添加依赖注入包
- [ ] 在 `DataViewConfig.csproj` 中添加 NuGet 包引用
  - [ ] Microsoft.Extensions.DependencyInjection (7.0.0)
  - [ ] Microsoft.Extensions.Hosting (7.0.1)
  - [ ] Microsoft.Extensions.Logging (7.0.0)
- [ ] 编译项目确保包正确安装
- [ ] 验证：项目能够正常编译

### Step 1.2: 创建服务容器配置
- [ ] 创建 `ServiceConfiguration.cs` 文件
- [ ] 实现 `ConfigureServices` 扩展方法
- [ ] 注册基础服务接口（先创建空接口）
  - [ ] IDbConnectionService
  - [ ] IApplicationStateService
  - [ ] IErrorHandlingService
  - [ ] INavigationService
  - [ ] IMenuService
- [ ] 注册主要 ViewModels
- [ ] 验证：代码编译通过

### Step 1.3: 修改App.xaml.cs
- [ ] 创建 `ServiceLocator.cs` 临时服务定位器
- [ ] 修改 `App.xaml.cs` 的 `OnStartup` 方法
- [ ] 添加服务容器初始化代码
- [ ] 添加 `OnExit` 方法处理资源释放
- [ ] 验证：应用程序能够正常启动

### 阶段验收
- [ ] 项目编译成功
- [ ] 应用程序正常启动
- [ ] 依赖注入容器正常工作
- [ ] 现有功能不受影响

---

## 🗄️ 第二阶段：数据访问层重构

### Step 2.1: 创建数据访问接口
- [ ] 创建 `Interfaces` 文件夹
- [ ] 创建 `IDbConnectionService.cs` 接口
- [ ] 创建 `IRepository<T>.cs` 泛型仓储接口
- [ ] 创建 `IUnitOfWork.cs` 工作单元接口
- [ ] 验证：接口定义编译通过

### Step 2.2: 实现数据访问服务
- [ ] 创建 `Services` 文件夹
- [ ] 实现 `DbConnectionService.cs`
  - [ ] 构造函数初始化 SqlSugar
  - [ ] 实现 ExecuteAsync 方法
  - [ ] 从配置文件读取连接字符串
- [ ] 实现 `Repository<T>.cs`
  - [ ] 实现所有 CRUD 操作
  - [ ] 添加异步支持
  - [ ] 添加查询方法
- [ ] 实现 `UnitOfWork.cs`
  - [ ] 实现事务管理
  - [ ] 添加异步事务支持
- [ ] 在 `ServiceConfiguration.cs` 中注册新服务
- [ ] 验证：服务实现编译通过

### Step 2.3: 逐步替换DbHelper使用
- [ ] 选择一个简单的 ViewModel 开始（如 TagConfigPageViewModel）
- [ ] 修改构造函数接受依赖注入参数
- [ ] 替换 `DbHelper.db` 调用为 Repository 调用
- [ ] 将同步操作改为异步操作
- [ ] 添加适当的错误处理
- [ ] 测试修改后的功能
- [ ] 逐步应用到其他 ViewModels：
  - [ ] ScreenConfigPageViewModel
  - [ ] CraneConfigViewModel
  - [ ] RcsConfigViewModel
  - [ ] BlockConfigViewModel
  - [ ] 其他配置页面 ViewModels

### 阶段验收
- [ ] 数据访问层重构完成
- [ ] 所有数据库操作通过 Repository 进行
- [ ] 现有功能正常工作
- [ ] 异步操作正常执行
- [ ] 事务管理正确工作

---

## ⚡ 第三阶段：命令系统改进

### Step 3.1: 创建改进的命令类
- [ ] 创建 `Commands` 文件夹
- [ ] 实现 `RelayCommand.cs`
  - [ ] 支持 CanExecute 逻辑
  - [ ] 实现 CanExecuteChanged 事件
  - [ ] 添加 RaiseCanExecuteChanged 方法
- [ ] 实现 `AsyncRelayCommand.cs`
  - [ ] 支持异步操作
  - [ ] 防止重复执行
  - [ ] 正确处理异常
- [ ] 验证：命令类编译通过

### Step 3.2: 替换现有Command使用
- [ ] 修改 ViewModelBase 以支持新命令
- [ ] 在 TagConfigPageViewModel 中替换命令：
  - [ ] AddNewCommand
  - [ ] EditCommand
  - [ ] DeleteCommand
  - [ ] LoadDataCommand（异步）
- [ ] 实现 CanExecute 逻辑
- [ ] 测试按钮状态响应
- [ ] 应用到其他 ViewModels
- [ ] 验证：命令功能正常工作

### 阶段验收
- [ ] 命令系统改进完成
- [ ] 按钮状态正确响应
- [ ] 异步命令正常工作
- [ ] 防止重复执行功能正常

---

## 🏗️ 第四阶段：MVVM架构优化

### Step 4.1: 创建基础ViewModel类
- [ ] 创建 `ViewModels` 文件夹（如果不存在）
- [ ] 实现 `ViewModelBase.cs`
  - [ ] INotifyPropertyChanged 实现
  - [ ] SetProperty 方法
  - [ ] OnPropertyChanged 方法
  - [ ] IsLoading 属性
- [ ] 修改现有 ViewModels 继承 ViewModelBase
- [ ] 验证：基础类功能正常

### Step 4.2: 创建导航服务
- [ ] 实现 `INavigationService` 接口
- [ ] 实现 `NavigationService.cs`
  - [ ] 页面缓存机制
  - [ ] 导航历史管理
  - [ ] 导航事件处理
- [ ] 创建 `NavigationEventArgs.cs`
- [ ] 在服务容器中注册导航服务
- [ ] 验证：导航服务编译通过

### Step 4.3: 创建菜单服务
- [ ] 实现 `IMenuService` 接口
- [ ] 实现 `MenuService.cs`
  - [ ] 从数据库加载菜单项
  - [ ] 根据用户权限过滤菜单
  - [ ] 支持动态菜单更新
- [ ] 在服务容器中注册菜单服务
- [ ] 验证：菜单服务编译通过

### Step 4.4: 重构MainViewModel
- [ ] 修改 MainViewModel 构造函数接受服务依赖
- [ ] 移除页面管理逻辑到 NavigationService
- [ ] 移除菜单管理逻辑到 MenuService
- [ ] 简化 MainViewModel 职责
- [ ] 更新命令实现
- [ ] 测试主窗口功能
- [ ] 验证：主窗口功能正常

### 阶段验收
- [ ] MainViewModel 职责明确
- [ ] 导航功能正常工作
- [ ] 菜单显示正常
- [ ] 页面切换正常
- [ ] 服务分离完成

---

## 🛡️ 第五阶段：错误处理和状态管理

### Step 5.1: 创建应用状态服务
- [ ] 实现 `IApplicationStateService` 接口
- [ ] 实现 `ApplicationStateService.cs`
  - [ ] 语言状态管理
  - [ ] 用户级别管理
  - [ ] 配置状态管理
  - [ ] 状态变更事件
- [ ] 替换 GlobalConfig 静态类的使用
- [ ] 在服务容器中注册状态服务
- [ ] 验证：状态管理正常工作

### Step 5.2: 创建错误处理服务
- [ ] 实现 `IErrorHandlingService` 接口
- [ ] 实现 `ErrorHandlingService.cs`
  - [ ] 统一异常处理
  - [ ] 用户友好错误消息
  - [ ] 不同类型的通知方法
- [ ] 在 ViewModels 中使用错误处理服务
- [ ] 替换现有的 MessageBox.Show 调用
- [ ] 在服务容器中注册错误处理服务
- [ ] 验证：错误处理统一且用户友好

### Step 5.3: 清理和优化
- [ ] 移除或标记过时的 DbHelper 类
- [ ] 移除或标记过时的 GlobalConfig 类
- [ ] 移除旧的 Command 类
- [ ] 清理未使用的 using 语句
- [ ] 更新注释和文档
- [ ] 验证：代码清洁且无警告

### 阶段验收
- [ ] 错误处理统一
- [ ] 应用状态管理正常
- [ ] 用户体验改善
- [ ] 代码结构清晰

---

## 🧪 最终验收测试

### 功能测试
- [ ] 应用程序启动测试
  - [ ] 正常启动无错误
  - [ ] 依赖注入正常工作
  - [ ] 初始界面显示正确
- [ ] 登录功能测试
  - [ ] 用户名密码验证
  - [ ] 权限级别设置
  - [ ] 登录后界面跳转
- [ ] 主要配置页面测试
  - [ ] 点名配置页面
  - [ ] 画面配置页面
  - [ ] 起重机配置页面
  - [ ] RCS配置页面
  - [ ] 堆场配置页面
- [ ] 数据库操作测试
  - [ ] 数据查询功能
  - [ ] 数据添加功能
  - [ ] 数据修改功能
  - [ ] 数据删除功能
  - [ ] 事务处理功能
- [ ] 导航功能测试
  - [ ] 菜单点击导航
  - [ ] 页面切换正常
  - [ ] 页面缓存工作
- [ ] 错误处理测试
  - [ ] 数据库连接错误
  - [ ] 数据验证错误
  - [ ] 权限不足错误
  - [ ] 网络连接错误

### 性能测试
- [ ] 启动时间测试
- [ ] 页面切换响应时间
- [ ] 数据加载性能
- [ ] 内存使用情况

### 代码质量检查
- [ ] 编译无警告
- [ ] 代码规范检查
- [ ] 依赖关系检查
- [ ] 接口设计检查

---

## 📝 问题跟踪

### 已知问题
| 问题描述 | 影响级别 | 状态 | 解决方案 | 负责人 |
|---------|---------|------|---------|--------|
|         |         |      |         |        |

### 风险评估
| 风险项 | 概率 | 影响 | 缓解措施 |
|--------|------|------|---------|
| 数据库兼容性问题 | 中 | 高 | 充分测试，准备回滚方案 |
| 性能下降 | 低 | 中 | 性能监控，优化关键路径 |
| 功能回归 | 中 | 高 | 全面功能测试 |

---

## 🎯 成功标准

### 技术指标
- [ ] 编译成功率：100%
- [ ] 功能完整性：100%
- [ ] 性能下降：< 5%
- [ ] 代码覆盖率：> 60%

### 质量指标
- [ ] 代码复杂度降低：> 20%
- [ ] 依赖关系清晰度：显著提升
- [ ] 可维护性：显著提升
- [ ] 可测试性：显著提升

### 用户体验指标
- [ ] 启动时间：< 3秒
- [ ] 页面响应时间：< 500ms
- [ ] 错误处理：用户友好
- [ ] 稳定性：无崩溃

---

## 📚 参考资料

- [Microsoft.Extensions.DependencyInjection 文档](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [MVVM 模式最佳实践](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/enterprise-application-patterns/mvvm)
- [Repository 模式实现](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-design)
- [SqlSugar 官方文档](https://www.donet5.com/Home/Doc)

---

**注意**：每完成一个检查项，请在对应的复选框中打勾 ✅，并记录完成时间和负责人。如遇到问题，请及时在问题跟踪表中记录。
