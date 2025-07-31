using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataViewConfig.Interfaces
{
    /// <summary>
    /// 通用Repository接口
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>实体对象</returns>
        T GetById(object id);

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns>实体列表</returns>
        List<T> GetAll();

        /// <summary>
        /// 根据条件查询实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体列表</returns>
        List<T> Find(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 根据条件获取单个实体
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>实体对象</returns>
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>是否成功</returns>
        bool Add(T entity);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns>是否成功</returns>
        bool AddRange(List<T> entities);

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>是否成功</returns>
        bool Update(T entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>是否成功</returns>
        bool Delete(T entity);

        /// <summary>
        /// 根据ID删除实体
        /// </summary>
        /// <param name="id">实体ID</param>
        /// <returns>是否成功</returns>
        bool DeleteById(object id);

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="predicate">删除条件</param>
        /// <returns>删除的记录数</returns>
        int Delete(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 检查实体是否存在
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>是否存在</returns>
        bool Exists(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 获取记录数量
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>记录数量</returns>
        int Count(Expression<Func<T, bool>> predicate = null);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">页码（从1开始）</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderBy">排序表达式</param>
        /// <param name="ascending">是否升序</param>
        /// <returns>分页结果</returns>
        PagedResult<T> GetPaged(int pageIndex, int pageSize, 
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true);
    }

    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalCount / PageSize);
            }
        }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage
        {
            get { return PageIndex > 1; }
        }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage
        {
            get { return PageIndex < TotalPages; }
        }

        public PagedResult()
        {
            Data = new List<T>();
        }
    }
}
