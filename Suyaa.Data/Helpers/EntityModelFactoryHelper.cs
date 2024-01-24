using Suyaa.Data.Models;
using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Models.Sources;
using Suyaa.Data.Repositories.Dependency;
using System;

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
        /// <param name="factory"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static EntityModel GetEntity(this IEntityModelFactory factory, Type type)
        {
            if (type.HasInterface<IDbEntity>())
            {
                return factory.GetEntity(new DbEntityModelSource(type));
            }
            return factory.GetEntity(new EntityModelSource(type));
        }

        /// <summary>
        /// 获取实例描述
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static EntityModel GetEntity<TEntity>(this IEntityModelFactory factory)
        {
            var type = typeof(TEntity);
            return factory.GetEntity(type);
        }

        /// <summary>
        /// 获取实例描述
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DbEntityModel GetDbEntity(this IEntityModelFactory factory, Type type)
        {
            return (DbEntityModel)factory.GetEntity(new DbEntityModelSource(type));
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
            return factory.GetDbEntity(typeof(TEntity));
        }
    }
}
