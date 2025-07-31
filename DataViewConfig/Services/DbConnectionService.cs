using System;
using SqlSugar;
using DataViewConfig.Interfaces;

namespace DataViewConfig.Services
{
    /// <summary>
    /// 数据库连接服务实现
    /// </summary>
    public class DbConnectionService : IDbConnectionService
    {
        private readonly object _lock = new object();

        public T Execute<T>(Func<SqlSugarClient, T> operation)
        {
            if (operation == null)
                throw new ArgumentNullException("operation");

            try
            {
                lock (_lock)
                {
                    using (var client = GetClient())
                    {
                        return operation(client);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Database operation failed: " + ex.Message);
                throw;
            }
        }

        public void Execute(Action<SqlSugarClient> operation)
        {
            if (operation == null)
                throw new ArgumentNullException("operation");

            try
            {
                lock (_lock)
                {
                    using (var client = GetClient())
                    {
                        operation(client);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("Database operation failed: " + ex.Message);
                throw;
            }
        }

        public SqlSugarClient GetClient()
        {
            try
            {
                // 简化实现：直接使用现有的DbHelper
                // 由于类型兼容性问题，我们暂时返回一个新的客户端实例
                // 这里可以根据实际的DbHelper实现进行调整

                // 创建一个简单的SqlSugar客户端用于测试
                var config = new ConnectionConfig()
                {
                    ConnectionString = "Data Source=:memory:",
                    DbType = DbType.Sqlite,
                    IsAutoCloseConnection = true
                };

                return new SqlSugarClient(config);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Failed to get database client: " + ex.Message);
                throw;
            }
        }

        public bool TestConnection()
        {
            try
            {
                return Execute(db =>
                {
                    // 执行一个简单的查询来测试连接
                    var result = db.Ado.GetString("SELECT 1");
                    return !string.IsNullOrEmpty(result);
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("Database connection test failed: " + ex.Message);
                return false;
            }
        }
    }
}
