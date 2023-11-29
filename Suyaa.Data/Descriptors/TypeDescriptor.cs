using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Suyaa.Data.Descriptors
{
    /// <summary>
    /// 类型描述
    /// </summary>
    public abstract class TypeDescriptor
    {

        /// <summary>
        /// 元数据
        /// </summary>
        public IEnumerable<object> MetaDatas { get; }

        /// <summary>
        /// 基础描述
        /// </summary>
        public TypeDescriptor(IEnumerable<object> metaDatas)
        {
            MetaDatas = metaDatas;
        }
    }
}
