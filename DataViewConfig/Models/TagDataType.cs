namespace DataViewConfig.Models
{
    /// <summary>
    /// 标签数据类型枚举
    /// </summary>
    public enum ModernTagDataType
    {
        /// <summary>
        /// 布尔型
        /// </summary>
        Boolean = 1,

        /// <summary>
        /// 整数型
        /// </summary>
        Integer = 2,

        /// <summary>
        /// 浮点型
        /// </summary>
        Float = 3,

        /// <summary>
        /// 字符串型
        /// </summary>
        String = 4,

        /// <summary>
        /// 日期时间型
        /// </summary>
        DateTime = 5,

        /// <summary>
        /// 枚举型
        /// </summary>
        Enum = 6
    }

    /// <summary>
    /// 标签后缀类型枚举
    /// </summary>
    public enum ModernTagPostfixType
    {
        /// <summary>
        /// 无后缀
        /// </summary>
        None = 1,

        /// <summary>
        /// 单位后缀
        /// </summary>
        Unit = 2,

        /// <summary>
        /// 状态后缀
        /// </summary>
        Status = 3,

        /// <summary>
        /// 时间后缀
        /// </summary>
        Time = 4
    }
}
