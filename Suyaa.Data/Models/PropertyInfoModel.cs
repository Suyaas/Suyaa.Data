using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Suyaa.Data.Models
{
    /// <summary>
    /// 属性描述
    /// </summary>
    public class PropertyInfoModel
    {

        /// <summary>
        /// 元数据
        /// </summary>
        public IEnumerable<object> MetaDatas { get; }

        /// <summary>
        /// 属性信息
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// 基础描述
        /// </summary>
        public PropertyInfoModel(PropertyInfo propertyInfo)
        {
            MetaDatas = propertyInfo.GetMetaDatas();
            PropertyInfo = propertyInfo;
        }
    }
}
