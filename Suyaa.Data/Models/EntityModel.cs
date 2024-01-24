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
        // 字符串
        private static readonly Type _stringType = typeof(string);

        /// <summary>
        /// 属性集合
        /// </summary>
        private readonly List<PropertyInfoModel> _properties;

        /// <summary>
        /// 对象实体建模
        /// </summary>
        public EntityModel(Type type) : base(type)
        {
            // 判断是否值类型
            IsValueType = false;
            if (type.IsValueType) IsValueType = true;
            if (type.IsBased(_stringType)) IsValueType = true;
            // 初始化字段集合
            _properties = new List<PropertyInfoModel>();
        }

        /// <summary>
        /// 是否值类型
        /// </summary>
        public bool IsValueType { get; }

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
