using Suyaa.Data.Dependency;
using Suyaa.Data.Descriptors;
using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Suyaa.Data.SimpleDbWorks
{
    /// <summary>
    /// 简单的数据库工厂
    /// </summary>
    public class SimpleDbFactory : IDbFactory
    {
        private readonly List<EntityDescriptor> _entities;
        private readonly IDbConnectionDescriptor _dbConnectionDescriptor;
        private readonly IList<IDbEntityProvider> _dbEntityProviders;
        private static object _lock = new object();
        private long _indexer;

        /// <summary>
        /// 简单的数据库工厂
        /// </summary>
        public SimpleDbFactory(
            IDbConnectionDescriptor dbConnectionDescriptor,
            IList<IDbEntityProvider> dbEntityProviders
            )
        {
            _entities = new List<EntityDescriptor>();
            _dbConnectionDescriptor = dbConnectionDescriptor;
            _dbEntityProviders = dbEntityProviders;
            this.WorkProvider = new SimpleDbWorkProvider(this);
            this.Provider = dbConnectionDescriptor.DatabaseType.GetDbProvider(this.WorkProvider);
            _indexer = 0;
        }

        /// <summary>
        /// 实例描述集合
        /// </summary>
        public IList<EntityDescriptor> Entities => _entities;

        /// <summary>
        /// 数据库工作者供应商
        /// </summary>
        public IDbWorkProvider WorkProvider { get; }

        /// <summary>
        /// 数据库供应商
        /// </summary>
        public IDbProvider Provider { get; }

        /// <summary>
        /// 获取新的参数索引号
        /// </summary>
        /// <returns></returns>
        public long GetParamterIndex()
        {
            long index = 0;
            lock (_lock)
            {
                index = ++_indexer;
            }
            return index;
        }

        /// <summary>
        /// 添加实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="KeyException"></exception>
        public EntityDescriptor AddEntity(Type type)
        {
            // 判断是否实现了接口
            if (!type.HasInterface<IEntity>()) throw new TypeNotSupportedException(type);
            // 判断是否已经存在
            if (_entities.Where(d => d.Type == type).Any()) throw new KeyException("Exists", "Type '{0}' is already exists.", type.FullName);
            // 建立实例描述
            EntityDescriptor entity = new EntityDescriptor(type);
            // 触发供应商事件
            foreach (var provider in _dbEntityProviders)
            {
                provider.OnEntityModeling(entity);
            }
            // 建立字段描述
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in pros)
            {
                long idx = GetParamterIndex();
                var field = new FieldDescriptor(idx, prop);
                // 触发供应商事件
                foreach (var provider in _dbEntityProviders)
                {
                    provider.OnFieldModeling(entity, field);
                }
                entity.Fields.Add(field);
            }
            _entities.Add(entity);
            return entity;
        }

        /// <summary>
        /// 获取实例描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public EntityDescriptor GetEntity(Type type)
        {
            var entiy = _entities.Where(d => d.Type == type).FirstOrDefault();
            if (entiy is null) entiy = AddEntity(type);
            return entiy;
        }
    }
}
