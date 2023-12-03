using Suyaa.Data.Dependency;
using Suyaa.Data.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 数据库工厂助手
    /// </summary>
    public static class DbFactoryHelper
    {
        /// <summary>
        /// 获取实例描述
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static EntityDescriptor GetEntity<TEntity>(this IDbFactory factory)
            where TEntity : IEntity
        {
            return factory.GetEntity(typeof(TEntity));
        }
    }
}
