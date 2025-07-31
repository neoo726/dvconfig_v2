using SqlSugar;

namespace DataViewConfig.Models.Entities
{
    /// <summary>
    /// 标签实体 - 对应dv_tag表
    /// </summary>
    [SugarTable("dv_tag")]
    public class TagEntity
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }

        /// <summary>
        /// 标签内部名称
        /// </summary>
        [SugarColumn(ColumnName = "tag_internal_name")]
        public string TagInternalName { get; set; }

        /// <summary>
        /// 标签显示名称
        /// </summary>
        [SugarColumn(ColumnName = "tag_name")]
        public string TagName { get; set; }

        /// <summary>
        /// 标签描述
        /// </summary>
        [SugarColumn(ColumnName = "tag_desc")]
        public string TagDesc { get; set; }

        /// <summary>
        /// 数据类型ID
        /// </summary>
        [SugarColumn(ColumnName = "data_type_id")]
        public int DataTypeId { get; set; }

        /// <summary>
        /// 后缀类型ID
        /// </summary>
        [SugarColumn(ColumnName = "postfix_type_id")]
        public int PostfixTypeId { get; set; }

        /// <summary>
        /// 关联宏名称
        /// </summary>
        [SugarColumn(ColumnName = "related_macro_name")]
        public string RelatedMacroName { get; set; }

        /// <summary>
        /// 关联标签内部名称
        /// </summary>
        [SugarColumn(ColumnName = "related_tag_internal_name")]
        public string RelatedTagInternalName { get; set; }

        /// <summary>
        /// 是否可编辑内部名称
        /// </summary>
        [SugarColumn(ColumnName = "is_internal_name_editable")]
        public bool IsInternalNameEditable { get; set; }

        /// <summary>
        /// 关联系统ID
        /// </summary>
        [SugarColumn(ColumnName = "related_system_id")]
        public string RelatedSystemId { get; set; }
    }
}
