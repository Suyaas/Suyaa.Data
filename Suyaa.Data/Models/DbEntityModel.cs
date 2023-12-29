using Suyaa.Data.Helpers;
using Suyaa.Data.Models.Sources;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Suyaa.Data.Models
{
    /// <summary>
    /// 数据库实体建模
    /// </summary>
    public class DbEntityModel : EntityModel
    {
        /// <summary>
        /// 数据库实体建模
        /// </summary>
        /// <param name="type"></param>
        public DbEntityModel(Type type) : base(type)
        {
            Name = type.GetTableName();
            Schema = type.GetSchemaName() ?? string.Empty;
        }

        /// <summary>
        /// 创建一个标准的数据库实体建模
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static DbEntityModel Create(Type type)
        {
            // 建立实例描述
            DbEntityModel entity = new DbEntityModel(type);
            // 建立字段描述
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in pros)
            {
                var field = new ColumnModel(0, prop);
                entity.AddField(field);
            }
            return entity;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属架构
        /// </summary>
        public string Schema { get; set; }

        /// <summary>
        /// 字段集合
        /// </summary>
        public IEnumerable<ColumnModel> Columns => base.Properties.Where(d => d is ColumnModel).Select(d => (ColumnModel)d);

        /// <summary>
        /// 获取字段名称
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetFieldName(string propertyName)
        {
            return base.Properties.Where(d => d is ColumnModel && d.PropertyInfo.Name == propertyName).Select(d => ((ColumnModel)d).Name).FirstOrDefault();
        }

        /// <summary>
        /// 添加字段描述
        /// </summary>
        /// <param name="field"></param>
        public void AddField(ColumnModel field)
        {
            base.AddProperty(field);
        }

        /// <summary>
        /// 清空字段描述
        /// </summary>
        public void ClearFields()
        {
            base.ClearProperties();
        }
    }

    /// <summary>
    /// 实例建模
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DbEntityModel<TEntity> : DbEntityModel
        where TEntity : class, IDbEntity
    {
        /// <summary>
        /// 实例建模
        /// </summary>
        public DbEntityModel() : base(typeof(TEntity))
        {
        }
    }
}
