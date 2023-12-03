using Suyaa.Data.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库实例工厂
    /// </summary>
    public interface IDbFactory
    {
        /// <summary>
        /// 实例描述集合
        /// </summary>
        IList<EntityDescriptor> Entities { get; }
        /// <summary>
        /// 获取实例描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        EntityDescriptor GetEntity(Type type);
        /// <summary>
        /// 数据库工作者供应商
        /// </summary>
        IDbWorkProvider WorkProvider { get; }
        /// <summary>
        /// 数据库供应商
        /// </summary>
        IDbProvider Provider { get; }
        /// <summary>
        /// 获取新的参数索引号
        /// </summary>
        /// <returns></returns>
        long GetParamterIndex();
    }
}
