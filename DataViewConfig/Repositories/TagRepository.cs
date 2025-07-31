using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DataViewConfig.Models.Entities;
using DataViewConfig.Interfaces;

namespace DataViewConfig.Repositories
{
    /// <summary>
    /// 标签Repository实现
    /// </summary>
    public class TagRepository : BaseRepository<TagEntity>, ITagRepository
    {
        /// <summary>
        /// 根据内部名称查找标签
        /// </summary>
        /// <param name="internalName">内部名称</param>
        /// <returns>标签实体</returns>
        public TagEntity GetByInternalName(string internalName)
        {
            if (string.IsNullOrEmpty(internalName))
                return null;

            try
            {
                return _dbService.Execute(db => 
                    db.Queryable<TagEntity>()
                      .Where(x => x.TagInternalName == internalName)
                      .First());
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetByInternalName failed: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 根据系统ID查找标签
        /// </summary>
        /// <param name="systemId">系统ID</param>
        /// <returns>标签列表</returns>
        public List<TagEntity> GetBySystemId(int systemId)
        {
            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<TagEntity>()
                      .Where(x => x.RelatedSystemId != null && x.RelatedSystemId.Contains(systemId.ToString()))
                      .ToList());
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetBySystemId failed: " + ex.Message);
                return new List<TagEntity>();
            }
        }

        /// <summary>
        /// 搜索标签
        /// </summary>
        /// <param name="searchText">搜索文本</param>
        /// <returns>标签列表</returns>
        public List<TagEntity> Search(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return GetAll();

            try
            {
                var lowerSearchText = searchText.ToLower();
                return _dbService.Execute(db =>
                    db.Queryable<TagEntity>()
                      .Where(x => x.TagInternalName.ToLower().Contains(lowerSearchText) ||
                                  x.TagName.ToLower().Contains(lowerSearchText) ||
                                  x.TagDesc.ToLower().Contains(lowerSearchText))
                      .ToList());
            }
            catch (Exception ex)
            {
                LogHelper.Error("Search failed: " + ex.Message);
                return new List<TagEntity>();
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
                    db.Queryable<TagEntity>()
                      .Where(x => x.TagInternalName == internalName && x.id != excludeId)
                      .Any());
            }
            catch (Exception ex)
            {
                LogHelper.Error("IsInternalNameExists failed: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 根据数据类型获取标签
        /// </summary>
        /// <param name="dataTypeId">数据类型ID</param>
        /// <returns>标签列表</returns>
        public List<TagEntity> GetByDataType(int dataTypeId)
        {
            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<TagEntity>()
                      .Where(x => x.DataTypeId == dataTypeId)
                      .ToList());
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetByDataType failed: " + ex.Message);
                return new List<TagEntity>();
            }
        }

        /// <summary>
        /// 根据后缀类型获取标签
        /// </summary>
        /// <param name="postfixTypeId">后缀类型ID</param>
        /// <returns>标签列表</returns>
        public List<TagEntity> GetByPostfixType(int postfixTypeId)
        {
            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<TagEntity>()
                      .Where(x => x.PostfixTypeId == postfixTypeId)
                      .ToList());
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetByPostfixType failed: " + ex.Message);
                return new List<TagEntity>();
            }
        }
    }

    /// <summary>
    /// 标签Repository接口
    /// </summary>
    public interface ITagRepository : IRepository<TagEntity>
    {
        TagEntity GetByInternalName(string internalName);
        List<TagEntity> GetBySystemId(int systemId);
        List<TagEntity> Search(string searchText);
        bool IsInternalNameExists(string internalName, int excludeId = 0);
        List<TagEntity> GetByDataType(int dataTypeId);
        List<TagEntity> GetByPostfixType(int postfixTypeId);
    }
}
