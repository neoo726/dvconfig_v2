using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataViewConfig.Models.Entities;
using DataViewConfig.Interfaces;

namespace DataViewConfig.Repositories
{
    /// <summary>
    /// 画面Repository实现
    /// </summary>
    public class ScreenRepository : BaseRepository<ScreenEntity>, IScreenRepository
    {
        /// <summary>
        /// 根据内部名称查找画面
        /// </summary>
        /// <param name="internalName">内部名称</param>
        /// <returns>画面实体</returns>
        public ScreenEntity GetByInternalName(string internalName)
        {
            if (string.IsNullOrEmpty(internalName))
                return null;

            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<ScreenEntity>()
                      .Where(x => x.ScreenInternalName == internalName)
                      .First());
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetByInternalName failed: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 根据CSW文件名查找画面
        /// </summary>
        /// <param name="cswName">CSW文件名</param>
        /// <returns>画面实体</returns>
        public ScreenEntity GetByCswName(string cswName)
        {
            if (string.IsNullOrEmpty(cswName))
                return null;

            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<ScreenEntity>()
                      .Where(x => x.ScreenCswName == cswName)
                      .First());
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetByCswName failed: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 搜索画面
        /// </summary>
        /// <param name="searchText">搜索文本</param>
        /// <param name="screenType">画面类型过滤</param>
        /// <returns>画面列表</returns>
        public List<ScreenEntity> Search(string searchText, string screenType = null)
        {
            try
            {
                return _dbService.Execute(db =>
                {
                    var query = db.Queryable<ScreenEntity>();

                    // 应用搜索文本过滤
                    if (!string.IsNullOrEmpty(searchText))
                    {
                        var lowerSearchText = searchText.ToLower();
                        query = query.Where(x => x.ScreenInternalName.ToLower().Contains(lowerSearchText) ||
                                                x.ScreenCswName.ToLower().Contains(lowerSearchText) ||
                                                x.ScreenDesc.ToLower().Contains(lowerSearchText));
                    }

                    // 应用画面类型过滤
                    if (!string.IsNullOrEmpty(screenType))
                    {
                        if (screenType == "单个画面")
                        {
                            query = query.Where(x => !x.ScreenCswName.Contains(","));
                        }
                        else if (screenType == "组合画面")
                        {
                            query = query.Where(x => x.ScreenCswName.Contains(","));
                        }
                    }

                    return query.ToList();
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("Search failed: " + ex.Message);
                return new List<ScreenEntity>();
            }
        }

        /// <summary>
        /// 检查内部名称是否存在
        /// </summary>
        /// <param name="internalName">内部名称</param>
        /// <param name="excludeId">排除的ID</param>
        /// <returns>是否存在</returns>
        public bool IsInternalNameExists(string internalName, int excludeId = 0)
        {
            if (string.IsNullOrEmpty(internalName))
                return false;

            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<ScreenEntity>()
                      .Where(x => x.ScreenInternalName == internalName && x.dv_screen_id != excludeId)
                      .Any());
            }
            catch (Exception ex)
            {
                LogHelper.Error("IsInternalNameExists failed: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取单个画面列表
        /// </summary>
        /// <returns>单个画面列表</returns>
        public List<ScreenEntity> GetSingleScreens()
        {
            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<ScreenEntity>()
                      .Where(x => !x.ScreenCswName.Contains(","))
                      .ToList());
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetSingleScreens failed: " + ex.Message);
                return new List<ScreenEntity>();
            }
        }

        /// <summary>
        /// 获取组合画面列表
        /// </summary>
        /// <returns>组合画面列表</returns>
        public List<ScreenEntity> GetCompositeScreens()
        {
            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<ScreenEntity>()
                      .Where(x => x.ScreenCswName.Contains(","))
                      .ToList());
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetCompositeScreens failed: " + ex.Message);
                return new List<ScreenEntity>();
            }
        }
    }

    /// <summary>
    /// 画面Repository接口
    /// </summary>
    public interface IScreenRepository : IRepository<ScreenEntity>
    {
        ScreenEntity GetByInternalName(string internalName);
        ScreenEntity GetByCswName(string cswName);
        List<ScreenEntity> Search(string searchText, string screenType = null);
        bool IsInternalNameExists(string internalName, int excludeId = 0);
        List<ScreenEntity> GetSingleScreens();
        List<ScreenEntity> GetCompositeScreens();
    }
}
