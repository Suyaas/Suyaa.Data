using Suyaa.Data.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库实例供应商
    /// </summary>
    public interface IDbEntityProvider
    {
        /// <summary>
        /// 实例建模事件
        /// </summary>
        /// <param name="entity"></param>
        void OnEntityModeling(EntityDescriptor entity);

        /// <summary>
        /// 字段建模事件
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="field"></param>
        void OnFieldModeling(EntityDescriptor entity, FieldDescriptor field);
    }
}
