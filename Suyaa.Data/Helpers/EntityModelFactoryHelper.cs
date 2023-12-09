using Suyaa.Data.Dependency;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 建模工厂助手
    /// </summary>
    public static class EntityModelFactoryHelper
    {
        /// <summary>
        /// 获取实例描述
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static EntityModel GetEntity<TEntity>(this IEntityModelFactory factory)
            where TEntity : class
        {
            return factory.GetEntity(typeof(TEntity));
        }

        /// <summary>
        /// 获取实例描述
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static DbEntityModel GetDbEntity<TEntity>(this IEntityModelFactory factory)
            where TEntity : IEntity
        {
            return (DbEntityModel)factory.GetEntity(typeof(TEntity));
        }
    }
}
