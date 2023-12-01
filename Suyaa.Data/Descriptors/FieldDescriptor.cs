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
        public FieldDescriptor(PropertyInfo property) : base(property.GetMetaDatas())
        {
            PropertyInfo = property;
            this.Name = property.GetColumnName();
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; }
    }
}
