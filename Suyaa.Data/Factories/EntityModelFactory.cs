using Suyaa.Data.Dependency;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Suyaa.Data.Factories
{
    /// <summary>
    /// 数据库实体建模工厂
    /// </summary>
    public class EntityModelFactory : IEntityModelFactory
    {
        private readonly List<EntityModel> _entities;
        private readonly IDbFactory _dbFactory;
        private readonly IEnumerable<IEntityModelProvider> _providers;

        /// <summary>
        /// 数据库实体建模工厂
        /// </summary>
        public EntityModelFactory(
            IDbFactory dbFactory,
            IEnumerable<IEntityModelProvider> providers
            )
        {
            _entities = new List<EntityModel>();
            _dbFactory = dbFactory;
            _providers = providers;
        }

        // 添加一个普通实体建模
        private EntityModel AddEntityModel(Type type)
        {
            // 建立实例描述
            EntityModel entity = new EntityModel(type);
            // 触发供应商事件
            foreach (var provider in _providers)
            {
                provider.OnEntityModeling(entity);
            }
            // 建立字段描述
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in pros)
            {
                var field = new PropertyInfoModel(prop);
                // 触发供应商事件
                foreach (var provider in _providers)
                {
                    provider.OnPropertyModeling(entity, field);
                }
                entity.AddProperty(field);
            }
            _entities.Add(entity);
            return entity;
        }

        // 添加一个数据库实体建模
        private DbEntityModel AddDbEntityModel(Type type)
        {
            // 建立实例描述
            DbEntityModel entity = new DbEntityModel(type);
            // 触发供应商事件
            foreach (var provider in _providers)
            {
                provider.OnEntityModeling(entity);
            }
            // 建立字段描述
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in pros)
            {
                long idx = _dbFactory.GetParamterIndex();
                var field = new FieldModel(idx, prop);
                // 触发供应商事件
                foreach (var provider in _providers)
                {
                    provider.OnPropertyModeling(entity, field);
                }
                entity.AddField(field);
            }
            _entities.Add(entity);
            return entity;
        }

        /// <summary>
        /// 添加实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="KeyException"></exception>
        public EntityModel AddEntity(Type type)
        {
            // 判断是否已经存在
            if (_entities.Where(d => d.Type == type).Any()) throw new KeyException("Exists", "Type '{0}' is already exists.", type.FullName);
            // 判断是否实现了接口
            if (type.HasInterface<IEntity>()) return AddDbEntityModel(type);
            return AddEntityModel(type);
        }

        /// <summary>
        /// 获取实例描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public EntityModel GetEntity(Type type)
        {
            var entiy = _entities.Where(d => d.Type == type).FirstOrDefault();
            if (entiy is null) entiy = AddEntity(type);
            return entiy;
        }
    }
}
