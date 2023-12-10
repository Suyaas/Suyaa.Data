using Suyaa.Data.Dependency;
using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Suyaa.Data.Models
{
    /// <summary>
    /// 对象实体建模
    /// </summary>
    public class EntityModel : TypeModel
    {
        /// <summary>
        /// 属性集合
        /// </summary>
        private readonly List<PropertyInfoModel> _properties;

        /// <summary>
        /// 对象实体建模
        /// </summary>
        public EntityModel(Type type) : base(type)
        {
            // 初始化字段集合
            _properties = new List<PropertyInfoModel>();
        }

        /// <summary>
        /// 属性集合
        /// </summary>
        public IEnumerable<PropertyInfoModel> Properties => _properties;

        /// <summary>
        /// 添加属性描述
        /// </summary>
        /// <param name="property"></param>
        public void AddProperty(PropertyInfoModel property)
        {
            _properties.Add(property);
        }

        /// <summary>
        /// 清空属性描述
        /// </summary>
        public void ClearProperties()
        {
            _properties.Clear();
        }
    }

    /// <summary>
    /// 实例建模
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EntityModel<TEntity> : EntityModel
        where TEntity : class
    {
        /// <summary>
        /// 实例建模
        /// </summary>
        public EntityModel() : base(typeof(TEntity))
        {
        }
    }
}
