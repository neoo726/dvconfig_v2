using SqlSugar;

namespace DataViewConfig.Models.Entities
{
    /// <summary>
    /// 画面实体 - 对应dv_screen_conf表
    /// </summary>
    [SugarTable("dv_screen_conf")]
    public class ScreenEntity
    {
        /// <summary>
        /// 画面ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int dv_screen_id { get; set; }

        /// <summary>
        /// 画面内部名称
        /// </summary>
        [SugarColumn(ColumnName = "dv_screen_internal_name")]
        public string ScreenInternalName { get; set; }

        /// <summary>
        /// CSW文件名
        /// </summary>
        [SugarColumn(ColumnName = "dv_screen_csw_name")]
        public string ScreenCswName { get; set; }

        /// <summary>
        /// 画面描述
        /// </summary>
        [SugarColumn(ColumnName = "dv_screen_desc")]
        public string ScreenDesc { get; set; }

        /// <summary>
        /// 画面类型
        /// </summary>
        [SugarColumn(ColumnName = "dv_screen_type")]
        public string ScreenType { get; set; }
    }
}
