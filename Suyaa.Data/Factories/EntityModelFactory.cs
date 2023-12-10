using Suyaa.Data.Dependency;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Suyaa.Data.Factories
{
    /// <summary>
    /// 数据库实体建模工厂
    /// </summary>
    public class EntityModelFactory : IEntityModelFactory
    {
        private readonly IEnumerable<IEntityModelProvider> _providers;

        /// <summary>
        /// 数据库实体建模工厂
        /// </summary>
        public EntityModelFactory(
            IEnumerable<IEntityModelProvider> providers
            )
        {
            _providers = providers;
        }

        /// <summary>
        /// 获取实例建模
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public EntityModel GetEntity(IEntityModelSource source)
        {
            // 从所有供应商中获取建模
            foreach (var provider in _providers)
            {
                var entity = provider.GetEntityModel(source);
                if (entity is null) continue;
                return entity;
            }
            // 从所有供应商中尝试创建建模
            foreach (var provider in _providers)
            {
                provider.TryCreate(source, out EntityModel? entity);
                if (entity is null) continue;
                return entity;
            }
            throw new TypeNotSupportedException(source.GetType());
        }
    }
}
