using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataViewConfig.Models.Entities;
using DataViewConfig.Interfaces;

namespace DataViewConfig.Repositories
{
    /// <summary>
    /// 堆场Repository实现
    /// </summary>
    public class BlockRepository : BaseRepository<BlockEntity>, IBlockRepository
    {
        /// <summary>
        /// 根据堆场名称查找
        /// </summary>
        /// <param name="blockName">堆场名称</param>
        /// <returns>堆场实体</returns>
        public BlockEntity GetByName(string blockName)
        {
            if (string.IsNullOrEmpty(blockName))
                return null;

            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<BlockEntity>()
                      .Where(x => x.BlockName == blockName)
                      .First());
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetByName failed: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 根据PLC ID查找
        /// </summary>
        /// <param name="plcId">PLC ID</param>
        /// <returns>堆场实体</returns>
        public BlockEntity GetByPlcId(int plcId)
        {
            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<BlockEntity>()
                      .Where(x => x.BlockPlcId == plcId)
                      .First());
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetByPlcId failed: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 搜索堆场
        /// </summary>
        /// <param name="searchName">搜索名称</param>
        /// <param name="searchId">搜索ID</param>
        /// <returns>堆场列表</returns>
        public List<BlockEntity> Search(string searchName = null, string searchId = null)
        {
            try
            {
                return _dbService.Execute(db =>
                {
                    var query = db.Queryable<BlockEntity>();

                    // 应用名称搜索
                    if (!string.IsNullOrEmpty(searchName))
                    {
                        query = query.Where(x => x.BlockName.Contains(searchName));
                    }

                    // 应用ID搜索
                    if (!string.IsNullOrEmpty(searchId))
                    {
                        query = query.Where(x => x.BlockPlcId.ToString() == searchId);
                    }

                    return query.ToList();
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("Search failed: " + ex.Message);
                return new List<BlockEntity>();
            }
        }

        /// <summary>
        /// 检查堆场名称是否存在
        /// </summary>
        /// <param name="blockName">堆场名称</param>
        /// <param name="excludeId">排除的ID</param>
        /// <returns>是否存在</returns>
        public bool IsNameExists(string blockName, int excludeId = 0)
        {
            if (string.IsNullOrEmpty(blockName))
                return false;

            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<BlockEntity>()
                      .Where(x => x.BlockName == blockName && x.block_id != excludeId)
                      .Any());
            }
            catch (Exception ex)
            {
                LogHelper.Error("IsNameExists failed: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 检查PLC ID是否存在
        /// </summary>
        /// <param name="plcId">PLC ID</param>
        /// <param name="excludeId">排除的ID</param>
        /// <returns>是否存在</returns>
        public bool IsPlcIdExists(int plcId, int excludeId = 0)
        {
            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<BlockEntity>()
                      .Where(x => x.BlockPlcId == plcId && x.block_id != excludeId)
                      .Any());
            }
            catch (Exception ex)
            {
                LogHelper.Error("IsPlcIdExists failed: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 根据位置范围查找堆场
        /// </summary>
        /// <param name="minPos">最小位置</param>
        /// <param name="maxPos">最大位置</param>
        /// <returns>堆场列表</returns>
        public List<BlockEntity> GetByPositionRange(int minPos, int maxPos)
        {
            try
            {
                return _dbService.Execute(db =>
                    db.Queryable<BlockEntity>()
                      .Where(x => x.BlockPosMin >= minPos && x.BlockPosMax <= maxPos)
                      .ToList());
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetByPositionRange failed: " + ex.Message);
                return new List<BlockEntity>();
            }
        }

        /// <summary>
        /// 获取堆场统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        public BlockStatistics GetStatistics()
        {
            try
            {
                return _dbService.Execute(db =>
                {
                    var blocks = db.Queryable<BlockEntity>().ToList();
                    
                    return new BlockStatistics
                    {
                        TotalCount = blocks.Count,
                        MinPosition = blocks.Count > 0 ? blocks.Min(x => x.BlockPosMin) : 0,
                        MaxPosition = blocks.Count > 0 ? blocks.Max(x => x.BlockPosMax) : 0,
                        AverageSize = blocks.Count > 0 ? blocks.Average(x => x.BlockPosMax - x.BlockPosMin + 1) : 0
                    };
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error("GetStatistics failed: " + ex.Message);
                return new BlockStatistics();
            }
        }
    }

    /// <summary>
    /// 堆场Repository接口
    /// </summary>
    public interface IBlockRepository : IRepository<BlockEntity>
    {
        BlockEntity GetByName(string blockName);
        BlockEntity GetByPlcId(int plcId);
        List<BlockEntity> Search(string searchName = null, string searchId = null);
        bool IsNameExists(string blockName, int excludeId = 0);
        bool IsPlcIdExists(int plcId, int excludeId = 0);
        List<BlockEntity> GetByPositionRange(int minPos, int maxPos);
        BlockStatistics GetStatistics();
    }

    /// <summary>
    /// 堆场统计信息
    /// </summary>
    public class BlockStatistics
    {
        public int TotalCount { get; set; }
        public int MinPosition { get; set; }
        public int MaxPosition { get; set; }
        public double AverageSize { get; set; }
    }
}
