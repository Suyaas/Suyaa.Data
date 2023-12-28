using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Suyaa.Data.Models
{
    /// <summary>
    /// 字段描述
    /// </summary>
    public sealed class ColumnModel : PropertyInfoModel
    {
        /// <summary>
        /// 字段描述
        /// </summary>
        public ColumnModel(long index, PropertyInfo property) : base(property)
        {
            Index = index;
            Name = property.GetColumnName();
            IsAutoIncrement = property.IsAutoIncrement();
            Description = property.GetDescription();
            ColumnType = property.GetColumnType();
            IsKey = property.IsKey();
            IsNullable = property.IsNullable();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 索引号
        /// </summary>
        public long Index { get; }

        /// <summary>
        /// 是否为主键
        /// </summary>
        public bool IsKey { get; }

        /// <summary>
        /// 是否可为空
        /// </summary>
        public bool IsNullable { get; }

        /// <summary>
        /// 是否为自增字段
        /// </summary>
        public bool IsAutoIncrement { get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public ColumnType? ColumnType { get; }

    }
}
