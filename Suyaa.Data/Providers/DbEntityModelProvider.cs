using Suyaa.Data.Dependency;
using Suyaa.Data.Factories;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.Data.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Suyaa.Data.Providers
{
    /// <summary>
    /// 普通实体建模供应商
    /// </summary>
    public class DbEntityModelProvider : IEntityModelProvider
    {
        private readonly List<DbEntityModel> _entities;
        private readonly IDbFactory _dbFactory;
        private readonly IEnumerable<IEntityModelConvention> _conventions;

        /// <summary>
        /// 数据库实体建模工厂
        /// </summary>
        public DbEntityModelProvider(
            IDbFactory dbFactory,
            IEnumerable<IEntityModelConvention> conventions
            )
        {
            _entities = new List<DbEntityModel>();
            _dbFactory = dbFactory;
            _conventions = conventions;
        }

        /// <summary>
        /// 默认优先级
        /// </summary>
        public int Priority => 0;

        // 添加一个普通实体建模
        private EntityModel AddEntityModel(DbEntityModelSource source)
        {
            // 建立实例描述
            DbEntityModel entity = new DbEntityModel(source.Type);
            // 触发供应商事件
            foreach (var convention in _conventions)
            {
                convention.OnEntityModeling(entity);
            }
            // 建立字段描述
            var pros = source.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in pros)
            {
                var field = new ColumnModel(_dbFactory.GetParamterIndex(), prop);
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
            if (source is DbEntityModelSource dbEntityModelSource)
            {
                model = AddEntityModel(dbEntityModelSource);
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
            if (source is DbEntityModelSource dbEntityModelSource)
            {
                var entiy = _entities.Where(d => d.Type == dbEntityModelSource.Type).FirstOrDefault();
                return entiy;
            }
            return null;
        }
    }
}
