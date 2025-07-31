using System;
using System.Collections.Generic;

namespace DataViewConfig.Services
{
    /// <summary>
    /// 简单的服务定位器实现 - 使用兼容的C#语法
    /// </summary>
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _singletonServices = new Dictionary<Type, object>();
        private static readonly Dictionary<Type, Func<object>> _transientFactories = new Dictionary<Type, Func<object>>();
        private static readonly object _lock = new object();

        /// <summary>
        /// 注册单例服务
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <typeparam name="TImplementation">实现类型</typeparam>
        /// <param name="instance">服务实例</param>
        public static void RegisterSingleton<TInterface, TImplementation>(TImplementation instance)
            where TImplementation : class, TInterface
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            lock (_lock)
            {
                _singletonServices[typeof(TInterface)] = instance;
            }
        }

        /// <summary>
        /// 注册瞬态服务
        /// </summary>
        /// <typeparam name="TInterface">接口类型</typeparam>
        /// <param name="factory">工厂方法</param>
        public static void RegisterTransient<TInterface>(Func<TInterface> factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            lock (_lock)
            {
                _transientFactories[typeof(TInterface)] = () => factory();
            }
        }

        /// <summary>
        /// 获取服务实例
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <returns>服务实例</returns>
        public static T GetService<T>()
        {
            var serviceType = typeof(T);

            lock (_lock)
            {
                // 首先检查单例服务
                if (_singletonServices.ContainsKey(serviceType))
                {
                    return (T)_singletonServices[serviceType];
                }

                // 然后检查瞬态服务
                if (_transientFactories.ContainsKey(serviceType))
                {
                    return (T)_transientFactories[serviceType]();
                }
            }

            throw new InvalidOperationException("Service of type " + serviceType.Name + " is not registered.");
        }

        /// <summary>
        /// 检查服务是否已注册
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <returns>是否已注册</returns>
        public static bool IsRegistered<T>()
        {
            var serviceType = typeof(T);

            lock (_lock)
            {
                return _singletonServices.ContainsKey(serviceType) || _transientFactories.ContainsKey(serviceType);
            }
        }

        /// <summary>
        /// 获取已注册服务的数量
        /// </summary>
        public static int RegisteredServicesCount
        {
            get
            {
                lock (_lock)
                {
                    return _singletonServices.Count + _transientFactories.Count;
                }
            }
        }

        /// <summary>
        /// 清除所有注册的服务
        /// </summary>
        public static void Clear()
        {
            lock (_lock)
            {
                _singletonServices.Clear();
                _transientFactories.Clear();
            }
        }

        /// <summary>
        /// 尝试获取服务实例
        /// </summary>
        /// <typeparam name="T">服务类型</typeparam>
        /// <param name="service">输出的服务实例</param>
        /// <returns>是否成功获取</returns>
        public static bool TryGetService<T>(out T service)
        {
            try
            {
                service = GetService<T>();
                return true;
            }
            catch
            {
                service = default(T);
                return false;
            }
        }
    }
}
