using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SqlSugar;
using DataViewConfig.Interfaces;
using DataViewConfig.Services;

namespace DataViewConfig.Repositories
{
    /// <summary>
    /// Repository基类实现
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class BaseRepository<T> : IRepository<T> where T : class, new()
    {
        protected readonly IDbConnectionService _dbService;

        public BaseRepository()
        {
            _dbService = ServiceLocator.GetService<IDbConnectionService>();
        }

        public virtual T GetById(object id)
        {
            try
            {
                return _dbService.Execute(db => db.Queryable<T>().InSingle(id));
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetById failed: " + ex.Message);
                return null;
            }
        }

        public virtual List<T> GetAll()
        {
            try
            {
                return _dbService.Execute(db => db.Queryable<T>().ToList());
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetAll failed: " + ex.Message);
                return new List<T>();
            }
        }

        public virtual List<T> Find(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _dbService.Execute(db => db.Queryable<T>().Where(predicate).ToList());
            }
            catch (Exception ex)
            {
                LogHelper.Error("Find failed: " + ex.Message);
                return new List<T>();
            }
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _dbService.Execute(db => db.Queryable<T>().Where(predicate).First());
            }
            catch (Exception ex)
            {
                LogHelper.Error("FirstOrDefault failed: " + ex.Message);
                return null;
            }
        }

        public virtual bool Add(T entity)
        {
            if (entity == null)
                return false;

            try
            {
                return _dbService.Execute(db => db.Insertable(entity).ExecuteCommand() > 0);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Add failed: " + ex.Message);
                return false;
            }
        }

        public virtual bool AddRange(List<T> entities)
        {
            if (entities == null || entities.Count == 0)
                return false;

            try
            {
                return _dbService.Execute(db => db.Insertable(entities).ExecuteCommand() > 0);
            }
            catch (Exception ex)
            {
                LogHelper.Error("AddRange failed: " + ex.Message);
                return false;
            }
        }

        public virtual bool Update(T entity)
        {
            if (entity == null)
                return false;

            try
            {
                return _dbService.Execute(db => db.Updateable(entity).ExecuteCommand() > 0);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Update failed: " + ex.Message);
                return false;
            }
        }

        public virtual bool Delete(T entity)
        {
            if (entity == null)
                return false;

            try
            {
                return _dbService.Execute(db => db.Deleteable(entity).ExecuteCommand() > 0);
            }
            catch (Exception ex)
            {
                LogHelper.Error("Delete failed: " + ex.Message);
                return false;
            }
        }

        public virtual bool DeleteById(object id)
        {
            try
            {
                return _dbService.Execute(db => db.Deleteable<T>().In(id).ExecuteCommand() > 0);
            }
            catch (Exception ex)
            {
                LogHelper.Error("DeleteById failed: " + ex.Message);
                return false;
            }
        }

        public virtual int Delete(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _dbService.Execute(db => db.Deleteable<T>().Where(predicate).ExecuteCommand());
            }
            catch (Exception ex)
            {
                LogHelper.Error("Delete with predicate failed: " + ex.Message);
                return 0;
            }
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _dbService.Execute(db => db.Queryable<T>().Where(predicate).Any());
            }
            catch (Exception ex)
            {
                LogHelper.Error("Exists failed: " + ex.Message);
                return false;
            }
        }

        public virtual int Count(Expression<Func<T, bool>> predicate = null)
        {
            try
            {
                return _dbService.Execute(db =>
                {
                    var query = db.Queryable<T>();
                    if (predicate != null)
                        query = query.Where(predicate);
                    return query.Count();
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("Count failed: " + ex.Message);
                return 0;
            }
        }

        public virtual PagedResult<T> GetPaged(int pageIndex, int pageSize,
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, object>> orderBy = null,
            bool ascending = true)
        {
            var result = new PagedResult<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            try
            {
                _dbService.Execute(db =>
                {
                    var query = db.Queryable<T>();

                    // 应用查询条件
                    if (predicate != null)
                        query = query.Where(predicate);

                    // 获取总数
                    result.TotalCount = query.Count();

                    // 应用排序
                    if (orderBy != null)
                    {
                        if (ascending)
                            query = query.OrderBy(orderBy);
                        else
                            query = query.OrderBy(orderBy, OrderByType.Desc);
                    }

                    // 应用分页
                    result.Data = query.ToPageList(pageIndex, pageSize);

                    return true;
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetPaged failed: " + ex.Message);
                result.Data = new List<T>();
                result.TotalCount = 0;
            }

            return result;
        }
    }
}
