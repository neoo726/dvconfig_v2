using System;
using SqlSugar;

namespace DataViewConfig.Interfaces
{
    /// <summary>
    /// 数据库连接服务接口
    /// </summary>
    public interface IDbConnectionService
    {
        /// <summary>
        /// 执行数据库操作
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="operation">数据库操作</param>
        /// <returns>操作结果</returns>
        T Execute<T>(Func<SqlSugarClient, T> operation);

        /// <summary>
        /// 执行数据库操作（无返回值）
        /// </summary>
        /// <param name="operation">数据库操作</param>
        void Execute(Action<SqlSugarClient> operation);

        /// <summary>
        /// 获取数据库客户端实例
        /// </summary>
        /// <returns>SqlSugar客户端</returns>
        SqlSugarClient GetClient();

        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <returns>连接是否成功</returns>
        bool TestConnection();
    }
}
