using Suyaa.Data.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Suyaa.Data.Enums
{

    /// <summary>
    /// 数据索引类型
    /// </summary>
    [Description("数据索引类型")]
    public enum ColumnIndexType : int
    {
        /// <summary>
        /// 无索引
        /// </summary>
        [Description("无索引")]
        None = 0,

        /// <summary>
        /// 普通索引
        /// </summary>
        [Description("普通索引")]
        Index = 0x10,

        /// <summary>
        /// 唯一索引
        /// </summary>
        [Description("唯一索引")]
        Unique = 0x20,

        /// <summary>
        /// 组合索引
        /// </summary>
        [Description("组合索引")]
        Group = 0x30,
    }
}
