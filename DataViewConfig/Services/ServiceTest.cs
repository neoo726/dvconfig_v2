using System;
using DataViewConfig.Interfaces;

namespace DataViewConfig.Services
{
    /// <summary>
    /// 服务测试类 - 改进版本
    /// </summary>
    public static class ServiceTest
    {
        /// <summary>
        /// 测试所有服务是否正确注册和工作
        /// </summary>
        /// <returns>测试是否通过</returns>
        public static bool TestAllServices()
        {
            try
            {
                Console.WriteLine("开始测试服务注册...");

                // 测试数据库连接服务
                if (ServiceLocator.IsRegistered<IDbConnectionService>())
                {
                    var dbService = ServiceLocator.GetService<IDbConnectionService>();
                    Console.WriteLine("✓ 数据库连接服务注册成功");

                    // 简单测试数据库连接
                    var connectionTest = dbService.TestConnection();
                    Console.WriteLine("✓ 数据库连接测试: " + (connectionTest ? "成功" : "失败"));
                }
                else
                {
                    Console.WriteLine("✗ 数据库连接服务未注册");
                    return false;
                }

                // 测试应用状态服务
                if (ServiceLocator.IsRegistered<IApplicationStateService>())
                {
                    var stateService = ServiceLocator.GetService<IApplicationStateService>();
                    Console.WriteLine("✓ 应用状态服务注册成功");

                    // 测试状态设置
                    var currentLang = stateService.CurrentLanguage;
                    Console.WriteLine("✓ 应用状态测试: 当前语言 = " + currentLang);
                }
                else
                {
                    Console.WriteLine("✗ 应用状态服务未注册");
                    return false;
                }

                // 测试错误处理服务
                if (ServiceLocator.IsRegistered<IErrorHandlingService>())
                {
                    var errorService = ServiceLocator.GetService<IErrorHandlingService>();
                    Console.WriteLine("✓ 错误处理服务注册成功");
                }
                else
                {
                    Console.WriteLine("✗ 错误处理服务未注册");
                    return false;
                }

                // 测试Repository服务
                Console.WriteLine("开始测试Repository服务...");
                var repositoryTestResult = RepositoryTestService.TestAllRepositories();
                if (!repositoryTestResult)
                {
                    Console.WriteLine("✗ Repository服务测试失败");
                    return false;
                }

                Console.WriteLine("✓ 所有服务测试通过！");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("✗ 服务测试失败: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 简单的服务测试方法（向后兼容）
        /// </summary>
        /// <returns>测试是否通过</returns>
        public static bool TestServices()
        {
            return TestAllServices();
        }
    }
}
