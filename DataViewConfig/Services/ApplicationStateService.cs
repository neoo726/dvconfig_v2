using System;
using System.Configuration;
using System.Reflection;
using DataViewConfig.Interfaces;

namespace DataViewConfig.Services
{
    /// <summary>
    /// 应用状态服务实现
    /// </summary>
    public class ApplicationStateService : IApplicationStateService
    {
        private GlobalLanguage _currentLanguage;
        private UserLevelType _currentUserLevel;
        private string _currentUserName;
        private bool _isDebugMode;

        public event EventHandler<StateChangedEventArgs> StateChanged;

        public ApplicationStateService()
        {
            // 初始化默认值
            _currentLanguage = GlobalLanguage.zhCN;
            _currentUserLevel = UserLevelType.Operator;
            _currentUserName = "Unknown";
            _isDebugMode = false;

            LoadConfiguration();
        }

        public GlobalLanguage CurrentLanguage
        {
            get { return _currentLanguage; }
            set
            {
                if (_currentLanguage != value)
                {
                    var oldValue = _currentLanguage;
                    _currentLanguage = value;
                    OnStateChanged("CurrentLanguage", oldValue, value);
                }
            }
        }

        public UserLevelType CurrentUserLevel
        {
            get { return _currentUserLevel; }
            set
            {
                if (_currentUserLevel != value)
                {
                    var oldValue = _currentUserLevel;
                    _currentUserLevel = value;
                    OnStateChanged("CurrentUserLevel", oldValue, value);
                }
            }
        }

        public string CurrentUserName
        {
            get { return _currentUserName; }
            set
            {
                if (_currentUserName != value)
                {
                    var oldValue = _currentUserName;
                    _currentUserName = value ?? "Unknown";
                    OnStateChanged("CurrentUserName", oldValue, _currentUserName);
                }
            }
        }

        public string ApplicationVersion
        {
            get
            {
                try
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    var version = assembly.GetName().Version;
                    return version != null ? version.ToString() : "1.0.0.0";
                }
                catch
                {
                    return "1.0.0.0";
                }
            }
        }

        public bool IsDebugMode
        {
            get { return _isDebugMode; }
            set
            {
                if (_isDebugMode != value)
                {
                    var oldValue = _isDebugMode;
                    _isDebugMode = value;
                    OnStateChanged("IsDebugMode", oldValue, value);
                }
            }
        }

        public string GetConfigValue(string key, string defaultValue = null)
        {
            if (string.IsNullOrEmpty(key))
                return defaultValue;

            try
            {
                var value = ConfigurationManager.AppSettings[key];
                return !string.IsNullOrEmpty(value) ? value : defaultValue;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Failed to get config value for key '" + key + "': " + ex.Message);
                return defaultValue;
            }
        }

        public void SetConfigValue(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
                return;

            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (config.AppSettings.Settings[key] != null)
                {
                    config.AppSettings.Settings[key].Value = value ?? string.Empty;
                }
                else
                {
                    config.AppSettings.Settings.Add(key, value ?? string.Empty);
                }
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception ex)
            {
                LogHelper.Error("Failed to set config value for key '" + key + "': " + ex.Message);
            }
        }

        public void SaveConfig()
        {
            try
            {
                SetConfigValue("CurrentLanguage", _currentLanguage.ToString());
                SetConfigValue("CurrentUserLevel", _currentUserLevel.ToString());
                SetConfigValue("CurrentUserName", _currentUserName);
                SetConfigValue("IsDebugMode", _isDebugMode.ToString());

                LogHelper.Info("Application configuration saved successfully");
            }
            catch (Exception ex)
            {
                LogHelper.Error("Failed to save application configuration: " + ex.Message);
            }
        }

        private void LoadConfiguration()
        {
            try
            {
                // 加载语言设置
                var languageStr = GetConfigValue("CurrentLanguage");
                if (!string.IsNullOrEmpty(languageStr))
                {
                    GlobalLanguage language;
                    if (Enum.TryParse(languageStr, out language))
                    {
                        _currentLanguage = language;
                    }
                }

                // 加载用户级别
                var userLevelStr = GetConfigValue("CurrentUserLevel");
                if (!string.IsNullOrEmpty(userLevelStr))
                {
                    UserLevelType userLevel;
                    if (Enum.TryParse(userLevelStr, out userLevel))
                    {
                        _currentUserLevel = userLevel;
                    }
                }

                // 加载用户名
                var userName = GetConfigValue("CurrentUserName");
                if (!string.IsNullOrEmpty(userName))
                {
                    _currentUserName = userName;
                }

                // 加载调试模式
                var debugModeStr = GetConfigValue("IsDebugMode");
                if (!string.IsNullOrEmpty(debugModeStr))
                {
                    bool debugMode;
                    if (bool.TryParse(debugModeStr, out debugMode))
                    {
                        _isDebugMode = debugMode;
                    }
                }

                LogHelper.Info("Application configuration loaded successfully");
            }
            catch (Exception ex)
            {
                LogHelper.Error("Failed to load application configuration: " + ex.Message);
            }
        }

        private void OnStateChanged(string propertyName, object oldValue, object newValue)
        {
            try
            {
                var handler = StateChanged;
                if (handler != null)
                {
                    handler(this, new StateChangedEventArgs(propertyName, oldValue, newValue));
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error in StateChanged event handler: " + ex.Message);
            }
        }
    }
}
