using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Suyaa.Data.Models
{
    /// <summary>
    /// 表描述
    /// </summary>
    public sealed class FieldModel : PropertyInfoModel
    {
        /// <summary>
        /// 表描述
        /// </summary>
        public FieldModel(long index, PropertyInfo property) : base(property)
        {
            Index = index;
            Name = property.GetColumnName();
            IsAutoIncrement = property.IsAutoIncrement();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 索引号
        /// </summary>
        public long Index { get; }

        /// <summary>
        /// 是否为自增字段
        /// </summary>
        public bool IsAutoIncrement { get; }

    }
}
