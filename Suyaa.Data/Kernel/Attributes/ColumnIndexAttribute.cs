using Suyaa.Data.Kernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Kernel.Attributes
{
    /// <summary>
    /// 列索引设置
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnIndexAttribute : Attribute
    {
        /// <summary>
        /// 索引类型
        /// </summary>
        public ColumnIndexType Type { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 索引字段
        /// </summary>
        public ColumnIndexAttribute(ColumnIndexType type)
        {
            Name = string.Empty;
            Type = type;
        }

        /// <summary>
        /// 索引字段
        /// </summary>
        public ColumnIndexAttribute(ColumnIndexType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
