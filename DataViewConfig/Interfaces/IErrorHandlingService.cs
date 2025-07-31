using System;
using System.Threading.Tasks;

namespace DataViewConfig.Interfaces
{
    /// <summary>
    /// 错误处理服务接口
    /// </summary>
    public interface IErrorHandlingService
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="exception">异常对象</param>
        /// <param name="context">上下文信息</param>
        void HandleError(Exception exception, string context = null);

        /// <summary>
        /// 异步处理异常
        /// </summary>
        /// <param name="exception">异常对象</param>
        /// <param name="context">上下文信息</param>
        /// <returns>处理任务</returns>
        Task HandleErrorAsync(Exception exception, string context = null);

        /// <summary>
        /// 显示错误消息
        /// </summary>
        /// <param name="message">错误消息</param>
        /// <param name="title">标题</param>
        void ShowError(string message, string title = "错误");

        /// <summary>
        /// 显示警告消息
        /// </summary>
        /// <param name="message">警告消息</param>
        /// <param name="title">标题</param>
        void ShowWarning(string message, string title = "警告");

        /// <summary>
        /// 显示信息消息
        /// </summary>
        /// <param name="message">信息消息</param>
        /// <param name="title">标题</param>
        void ShowInfo(string message, string title = "信息");

        /// <summary>
        /// 显示成功消息
        /// </summary>
        /// <param name="message">成功消息</param>
        /// <param name="title">标题</param>
        void ShowSuccess(string message, string title = "成功");

        /// <summary>
        /// 异步显示成功消息
        /// </summary>
        /// <param name="message">成功消息</param>
        /// <param name="title">标题</param>
        /// <returns>显示任务</returns>
        Task ShowSuccessAsync(string message, string title = "成功");

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">日志消息</param>
        /// <param name="exception">异常对象</param>
        void LogError(string message, Exception exception = null);

        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="message">日志消息</param>
        void LogWarning(string message);

        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="message">日志消息</param>
        void LogInfo(string message);
    }
}
