using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataViewConfig.Interfaces;
using DataViewConfig.Services;

namespace DataViewConfig.ViewModels
{
    /// <summary>
    /// ViewModel基类，提供通用的属性变更通知和服务访问
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        #region 属性变更通知

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 触发属性变更通知
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// 设置属性值并触发变更通知
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="field">字段引用</param>
        /// <param name="value">新值</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>是否发生了变更</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(field, value))
                return false;

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        #region 通用属性

        private bool _isLoading;
        /// <summary>
        /// 是否正在加载
        /// </summary>
        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }

        private bool _isBusy;
        /// <summary>
        /// 是否忙碌
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private string _statusMessage;
        /// <summary>
        /// 状态消息
        /// </summary>
        public string StatusMessage
        {
            get { return _statusMessage; }
            set { SetProperty(ref _statusMessage, value); }
        }

        private bool _hasError;
        /// <summary>
        /// 是否有错误
        /// </summary>
        public bool HasError
        {
            get { return _hasError; }
            set { SetProperty(ref _hasError, value); }
        }

        #endregion

        #region 服务访问

        /// <summary>
        /// 获取数据库连接服务
        /// </summary>
        protected IDbConnectionService DbService
        {
            get
            {
                try
                {
                    return ServiceLocator.GetService<IDbConnectionService>();
                }
                catch (Exception ex)
                {
                    LogHelper.Error("Failed to get DbService: " + ex.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取应用状态服务
        /// </summary>
        protected IApplicationStateService StateService
        {
            get
            {
                try
                {
                    return ServiceLocator.GetService<IApplicationStateService>();
                }
                catch (Exception ex)
                {
                    LogHelper.Error("Failed to get StateService: " + ex.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取错误处理服务
        /// </summary>
        protected IErrorHandlingService ErrorService
        {
            get
            {
                try
                {
                    return ServiceLocator.GetService<IErrorHandlingService>();
                }
                catch (Exception ex)
                {
                    LogHelper.Error("Failed to get ErrorService: " + ex.Message);
                    return null;
                }
            }
        }

        #endregion

        #region 错误处理

        /// <summary>
        /// 安全执行操作，自动处理异常
        /// </summary>
        /// <param name="action">要执行的操作</param>
        /// <param name="context">操作上下文</param>
        protected void SafeExecute(Action action, string context = null)
        {
            if (action == null)
                return;

            try
            {
                IsBusy = true;
                action();
            }
            catch (Exception ex)
            {
                var errorService = ErrorService;
                if (errorService != null)
                {
                    errorService.HandleError(ex, context ?? "ViewModelBase.SafeExecute");
                }
                else
                {
                    LogHelper.Error("SafeExecute failed: " + ex.Message);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// 安全执行带返回值的操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="func">要执行的操作</param>
        /// <param name="context">操作上下文</param>
        /// <param name="defaultValue">默认返回值</param>
        /// <returns>操作结果</returns>
        protected T SafeExecute<T>(Func<T> func, string context = null, T defaultValue = default(T))
        {
            if (func == null)
                return defaultValue;

            try
            {
                IsBusy = true;
                return func();
            }
            catch (Exception ex)
            {
                var errorService = ErrorService;
                if (errorService != null)
                {
                    errorService.HandleError(ex, context ?? "ViewModelBase.SafeExecute");
                }
                else
                {
                    LogHelper.Error("SafeExecute failed: " + ex.Message);
                }
                return defaultValue;
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion

        #region 生命周期

        /// <summary>
        /// 初始化ViewModel
        /// </summary>
        public virtual void Initialize()
        {
            // 子类可以重写此方法进行初始化
        }

        /// <summary>
        /// 清理资源
        /// </summary>
        public virtual void Cleanup()
        {
            // 子类可以重写此方法进行清理
        }

        #endregion
    }
}
