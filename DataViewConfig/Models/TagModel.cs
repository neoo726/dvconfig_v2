namespace DataViewConfig.Models
{
    /// <summary>
    /// 标签模型 - 兼容现有代码
    /// </summary>
    public class TagModel
    {
        /// <summary>
        /// 标签ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标签内部名称
        /// </summary>
        public string TagInternalName { get; set; }

        /// <summary>
        /// 标签显示名称
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 标签描述
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public ModernTagDataType DataType { get; set; }

        /// <summary>
        /// 后缀类型
        /// </summary>
        public ModernTagPostfixType Postfix { get; set; }

        /// <summary>
        /// 关联宏名称
        /// </summary>
        public string RelatedMacroName { get; set; }

        /// <summary>
        /// 关联标签内部名称
        /// </summary>
        public string RelatedTagInternalName { get; set; }

        /// <summary>
        /// 标签名称是否可编辑
        /// </summary>
        public bool TagNameEditable { get; set; }

        /// <summary>
        /// 关联系统ID
        /// </summary>
        public string RelatedSystemIds { get; set; }
    }
}
