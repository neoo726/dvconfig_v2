using System;
using System.Windows.Input;
using DataViewConfig.Services;

namespace DataViewConfig.Commands
{
    /// <summary>
    /// 改进的RelayCommand实现，支持CanExecute逻辑
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="execute">执行的操作</param>
        /// <param name="canExecute">是否可以执行的判断</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// 构造函数 - 无参数版本
        /// </summary>
        /// <param name="execute">执行的操作</param>
        /// <param name="canExecute">是否可以执行的判断</param>
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            
            _execute = _ => execute();
            _canExecute = canExecute != null ? new Predicate<object>(_ => canExecute()) : null;
        }

        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _execute(parameter);
                }
                catch (Exception ex)
                {
                    // 记录异常但不抛出，避免应用程序崩溃
                    LogHelper.Error("RelayCommand execution failed: " + ex.Message);
                    
                    // 可以选择显示错误消息给用户
                    var errorService = ServiceLocatorExtensions.TryGetErrorService();
                    if (errorService != null)
                    {
                        errorService.HandleError(ex, "RelayCommand");
                    }
                }
            }
        }

        /// <summary>
        /// CanExecute状态改变事件
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// 手动触发CanExecute状态检查
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }

    /// <summary>
    /// ServiceLocator扩展方法
    /// </summary>
    internal static class ServiceLocatorExtensions
    {
        public static DataViewConfig.Interfaces.IErrorHandlingService TryGetErrorService()
        {
            try
            {
                return ServiceLocator.GetService<DataViewConfig.Interfaces.IErrorHandlingService>();
            }
            catch
            {
                return null;
            }
        }
    }
}
