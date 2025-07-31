using System;

namespace DataViewConfig.Interfaces
{
    /// <summary>
    /// 应用状态服务接口
    /// </summary>
    public interface IApplicationStateService
    {
        /// <summary>
        /// 当前语言
        /// </summary>
        GlobalLanguage CurrentLanguage { get; set; }

        /// <summary>
        /// 当前用户级别
        /// </summary>
        UserLevelType CurrentUserLevel { get; set; }

        /// <summary>
        /// 当前用户名
        /// </summary>
        string CurrentUserName { get; set; }

        /// <summary>
        /// 应用程序版本
        /// </summary>
        string ApplicationVersion { get; }

        /// <summary>
        /// 是否为调试模式
        /// </summary>
        bool IsDebugMode { get; set; }

        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="key">配置键</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>配置值</returns>
        string GetConfigValue(string key, string defaultValue = null);

        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <param name="key">配置键</param>
        /// <param name="value">配置值</param>
        void SetConfigValue(string key, string value);

        /// <summary>
        /// 保存配置
        /// </summary>
        void SaveConfig();

        /// <summary>
        /// 状态变更事件
        /// </summary>
        event EventHandler<StateChangedEventArgs> StateChanged;
    }

    /// <summary>
    /// 状态变更事件参数
    /// </summary>
    public class StateChangedEventArgs : EventArgs
    {
        public string PropertyName { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }

        public StateChangedEventArgs(string propertyName, object oldValue, object newValue)
        {
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
