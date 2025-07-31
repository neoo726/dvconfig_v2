using SqlSugar;

namespace DataViewConfig.Models.Entities
{
    /// <summary>
    /// 堆场实体 - 对应block表
    /// </summary>
    [SugarTable("block")]
    public class BlockEntity
    {
        /// <summary>
        /// 堆场ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public int block_id { get; set; }

        /// <summary>
        /// 堆场PLC ID
        /// </summary>
        [SugarColumn(ColumnName = "block_plc_id")]
        public int BlockPlcId { get; set; }

        /// <summary>
        /// 堆场名称
        /// </summary>
        [SugarColumn(ColumnName = "block_name")]
        public string BlockName { get; set; }

        /// <summary>
        /// 堆场描述
        /// </summary>
        [SugarColumn(ColumnName = "block_desc")]
        public string BlockDesc { get; set; }

        /// <summary>
        /// 最小位置
        /// </summary>
        [SugarColumn(ColumnName = "block_pos_min")]
        public int BlockPosMin { get; set; }

        /// <summary>
        /// 最大位置
        /// </summary>
        [SugarColumn(ColumnName = "block_pos_max")]
        public int BlockPosMax { get; set; }
    }
}
