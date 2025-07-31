using System;
using System.Threading.Tasks;
using System.Windows;
using DataViewConfig.Interfaces;

namespace DataViewConfig.Services
{
    /// <summary>
    /// 错误处理服务实现
    /// </summary>
    public class ErrorHandlingService : IErrorHandlingService
    {
        public void HandleError(Exception exception, string context = null)
        {
            if (exception == null)
                return;

            var contextInfo = !string.IsNullOrEmpty(context) ? " [Context: " + context + "]" : "";
            var errorMessage = "Error occurred" + contextInfo + ": " + exception.Message;

            LogError(errorMessage, exception);
            ShowError(exception.Message, "错误");
        }

        public Task HandleErrorAsync(Exception exception, string context = null)
        {
            return Task.Run(() => HandleError(exception, context));
        }

        public void ShowError(string message, string title = "错误")
        {
            if (string.IsNullOrEmpty(message))
                return;

            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
                });
            }
            catch (Exception ex)
            {
                LogError("Failed to show error message: " + ex.Message);
            }
        }

        public void ShowWarning(string message, string title = "警告")
        {
            if (string.IsNullOrEmpty(message))
                return;

            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Warning);
                });
            }
            catch (Exception ex)
            {
                LogError("Failed to show warning message: " + ex.Message);
            }
        }

        public void ShowInfo(string message, string title = "信息")
        {
            if (string.IsNullOrEmpty(message))
                return;

            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                });
            }
            catch (Exception ex)
            {
                LogError("Failed to show info message: " + ex.Message);
            }
        }

        public void ShowSuccess(string message, string title = "成功")
        {
            if (string.IsNullOrEmpty(message))
                return;

            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
                });
            }
            catch (Exception ex)
            {
                LogError("Failed to show success message: " + ex.Message);
            }
        }

        public Task ShowSuccessAsync(string message, string title = "成功")
        {
            return Task.Run(() => ShowSuccess(message, title));
        }

        public void LogError(string message, Exception exception = null)
        {
            try
            {
                if (exception != null)
                {
                    LogHelper.Error(message + " Exception: " + exception.ToString());
                }
                else
                {
                    LogHelper.Error(message);
                }
            }
            catch
            {
                // 如果日志记录失败，不要抛出异常
            }
        }

        public void LogWarning(string message)
        {
            try
            {
                LogHelper.Warn(message);
            }
            catch
            {
                // 如果日志记录失败，不要抛出异常
            }
        }

        public void LogInfo(string message)
        {
            try
            {
                LogHelper.Info(message);
            }
            catch
            {
                // 如果日志记录失败，不要抛出异常
            }
        }
    }
}
