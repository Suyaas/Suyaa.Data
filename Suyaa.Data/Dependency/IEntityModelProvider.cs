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
        /// 执行优先级
        /// </summary>
        int Priority { get; }
        /// <summary>
        /// 尝试创建
        /// </summary>
        /// <param name="source"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        bool TryCreate(IEntityModelSource source, out EntityModel? model);
        /// <summary>
        /// 获取实体建模
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        EntityModel? GetEntityModel(IEntityModelSource source);
    }
}
