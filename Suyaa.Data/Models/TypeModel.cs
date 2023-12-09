using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Data.Models
{
    /// <summary>
    /// 类型建模
    /// </summary>
    public abstract class TypeModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 元数据
        /// </summary>
        public IEnumerable<object> MetaDatas { get; }

        /// <summary>
        /// 基础描述
        /// </summary>
        public TypeModel(Type type)
        {
            Type = type;
            MetaDatas = type.GetMetaDatas();
        }
    }
}
