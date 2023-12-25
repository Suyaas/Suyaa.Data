﻿using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Suyaa.Data.Models
{
    /// <summary>
    /// 字段描述
    /// </summary>
    public sealed class FieldModel : PropertyInfoModel
    {
        /// <summary>
        /// 字段描述
        /// </summary>
        public FieldModel(long index, PropertyInfo property) : base(property)
        {
            Index = index;
            Name = property.GetColumnName();
            IsAutoIncrement = property.IsAutoIncrement();
            Description = property.GetDescription();
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
        /// 是否为自增字段
        /// </summary>
        public bool IsAutoIncrement { get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

    }
}
