﻿using Suyaa.Data.Dependency;
using Suyaa.Data.Factories;
using Suyaa.Data.Models;
using Suyaa.EFCore.Models;
using Suyaa.EFCore.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Suyaa.EFCore.Providers
{
    /// <summary>
    /// 普通实体建模供应商
    /// </summary>
    public class DbSetModelProvider : IEntityModelProvider
    {
        private readonly List<DbSetModel> _entities;
        private readonly IDbFactory _dbFactory;
        private readonly IEnumerable<IEntityModelConvention> _conventions;

        /// <summary>
        /// 数据库实体建模工厂
        /// </summary>
        public DbSetModelProvider(
            IDbFactory dbFactory,
            IEnumerable<IEntityModelConvention> conventions
            )
        {
            _entities = new List<DbSetModel>();
            _dbFactory = dbFactory;
            _conventions = conventions;
        }

        /// <summary>
        /// 默认优先级
        /// </summary>
        public int Priority => 0;

        // 添加一个普通实体建模
        private EntityModel AddEntityModel(DbSetModelSource source)
        {
            // 建立实例描述
            DbSetModel entity = new DbSetModel(source.Type, source.PropertyInfo, source.DbContext);
            // 触发供应商事件
            foreach (var convention in _conventions)
            {
                convention.OnEntityModeling(entity);
            }
            // 建立字段描述
            var pros = source.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in pros)
            {
                var field = new FieldModel(_dbFactory.GetParamterIndex(), prop);
                // 触发供应商事件
                foreach (var convention in _conventions)
                {
                    convention.OnPropertyModeling(entity, field);
                }
                entity.AddField(field);
            }
            _entities.Add(entity);
            return entity;
        }

        /// <summary>
        /// 尝试创建
        /// </summary>
        /// <param name="source"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool TryCreate(IEntityModelSource source, out EntityModel? model)
        {
            if (source is DbSetModelSource dbSetModelSource)
            {
                model = AddEntityModel(dbSetModelSource);
                return true;
            }
            model = null;
            return false;
        }

        /// <summary>
        /// 获取实体建模
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public EntityModel? GetEntityModel(IEntityModelSource source)
        {
            // 只读查询
            if (source is TypeDbContextModelSource typeDbContextModelSource)
            {
                var entiy = _entities.Where(d => d.Type == typeDbContextModelSource.Type).FirstOrDefault();
                return entiy;
            }
            // 建模查询
            if (source is DbSetModelSource dbSetModelSource)
            {
                var entiy = _entities.Where(d => d.Type == dbSetModelSource.Type).FirstOrDefault();
                return entiy;
            }
            return null;
        }
    }
}
