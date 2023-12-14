using Suyaa.Data.Helpers;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<FieldModel> Fields => base.Properties.Where(d => d is FieldModel).Select(d => (FieldModel)d);

        /// <summary>
        /// 获取字段名称
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetFieldName(string propertyName)
        {
            return base.Properties.Where(d => d is FieldModel && d.PropertyInfo.Name == propertyName).Select(d => ((FieldModel)d).Name).FirstOrDefault();
        }

        /// <summary>
        /// 添加字段描述
        /// </summary>
        /// <param name="field"></param>
        public void AddField(FieldModel field)
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
