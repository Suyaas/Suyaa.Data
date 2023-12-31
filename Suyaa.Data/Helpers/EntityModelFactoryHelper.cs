﻿using Suyaa.Data.Models;
using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Models.Sources;
using Suyaa.Data.Repositories.Dependency;

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
            return factory.GetEntity(new EntityModelSource(typeof(TEntity)));
        }

        /// <summary>
        /// 获取实例描述
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static DbEntityModel GetDbEntity<TEntity>(this IEntityModelFactory factory)
            where TEntity : IDbEntity
        {
            return (DbEntityModel)factory.GetEntity(new DbEntityModelSource(typeof(TEntity)));
        }
    }
}
