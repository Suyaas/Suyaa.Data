using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Suyaa.Data.Descriptors
{
    /// <summary>
    /// 表描述
    /// </summary>
    public sealed class FieldDescriptor : TypeDescriptor
    {
        /// <summary>
        /// 表描述
        /// </summary>
        public FieldDescriptor(long index, PropertyInfo property) : base(property.GetMetaDatas())
        {
            Index = index;
            PropertyInfo = property;
            this.Name = property.GetColumnName();
            this.IsAutoIncrement = property.GetAutoIncrement();
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

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; }
    }
}
