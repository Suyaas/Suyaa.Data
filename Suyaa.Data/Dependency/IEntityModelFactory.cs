using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库实体建模工厂
    /// </summary>
    public interface IEntityModelFactory
    {
        /// <summary>
        /// 获取实例建模
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        EntityModel GetEntity(IEntityModelSource source);
    }
}
