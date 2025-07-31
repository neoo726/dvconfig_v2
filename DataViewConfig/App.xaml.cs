using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DataViewConfig.Services;
using DataViewConfig.Interfaces;
using DataViewConfig.Repositories;

namespace DataViewConfig
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // 配置服务
            ConfigureServices();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // 清理服务
            ServiceLocator.Clear();

            base.OnExit(e);
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // 直接启动主窗口，跳过登录
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void ConfigureServices()
        {
            try
            {
                // 初始化日志系统
                LogHelper.Info("Application starting...");

                // 注册数据访问服务
                var dbService = new DbConnectionService();
                ServiceLocator.RegisterSingleton<IDbConnectionService, DbConnectionService>(dbService);

                // 注册应用状态服务
                var stateService = new ApplicationStateService();
                ServiceLocator.RegisterSingleton<IApplicationStateService, ApplicationStateService>(stateService);

                // 注册错误处理服务
                var errorService = new ErrorHandlingService();
                ServiceLocator.RegisterSingleton<IErrorHandlingService, ErrorHandlingService>(errorService);

                // 注册Repository服务
                var tagRepository = new TagRepository();
                ServiceLocator.RegisterSingleton<ITagRepository, TagRepository>(tagRepository);

                var screenRepository = new ScreenRepository();
                ServiceLocator.RegisterSingleton<IScreenRepository, ScreenRepository>(screenRepository);

                var blockRepository = new BlockRepository();
                ServiceLocator.RegisterSingleton<IBlockRepository, BlockRepository>(blockRepository);

                // 测试服务是否正常工作
                var testResult = ServiceTest.TestAllServices();
                if (testResult)
                {
                    LogHelper.Info("All services configured successfully. Total services: " + ServiceLocator.RegisteredServicesCount);
                }
                else
                {
                    LogHelper.Error("Service configuration test failed!");
                    MessageBox.Show("服务初始化失败，请检查日志文件。", "启动错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Service configuration failed: " + ex.Message);
                MessageBox.Show("服务配置异常: " + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
