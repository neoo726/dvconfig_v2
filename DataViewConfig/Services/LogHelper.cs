using System;

namespace DataViewConfig
{
    /// <summary>
    /// 日志助手类 - 临时实现，用于解决编译问题
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// 记录错误日志
        /// </summary>
        public static void Error(string message)
        {
            try
            {
                // 尝试使用原有的日志系统
                var logger = log4net.LogManager.GetLogger("LogTest");
                logger.Error(message);
            }
            catch
            {
                // 如果原有日志系统不可用，使用控制台输出
                Console.WriteLine($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }

        /// <summary>
        /// 记录信息日志
        /// </summary>
        public static void Info(string message)
        {
            try
            {
                var logger = log4net.LogManager.GetLogger("LogTest");
                logger.Info(message);
            }
            catch
            {
                Console.WriteLine($"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }

        /// <summary>
        /// 记录警告日志
        /// </summary>
        public static void Warn(string message)
        {
            try
            {
                var logger = log4net.LogManager.GetLogger("LogTest");
                logger.Warn(message);
            }
            catch
            {
                Console.WriteLine($"[WARN] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }

        /// <summary>
        /// 记录调试日志
        /// </summary>
        public static void Debug(string message)
        {
            try
            {
                var logger = log4net.LogManager.GetLogger("LogTest");
                logger.Debug(message);
            }
            catch
            {
                Console.WriteLine($"[DEBUG] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            }
        }
    }
}
