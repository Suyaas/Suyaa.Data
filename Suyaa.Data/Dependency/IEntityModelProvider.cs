using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库实体建模供应商
    /// </summary>
    public interface IEntityModelProvider
    {
        /// <summary>
        /// 实例建模事件
        /// </summary>
        /// <param name="entity"></param>
        void OnEntityModeling(EntityModel entity);

        /// <summary>
        /// 属性建模事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="property"></param>
        void OnPropertyModeling(EntityModel entity, PropertyInfoModel property);
    }
}
