﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Suyaa.Data
{
    /// <summary>
    /// 实例映射器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityMapper<T>
    {
        // 对象类型
        private readonly Type _type;
        private readonly PropertyInfo[] _properties;

        /// <summary>
        /// 是否为值映射
        /// </summary>
        public bool IsValue { get; }

        /// <summary>
        /// 数据映射器
        /// </summary>
        public EntityMapper()
        {
            _type = typeof(T);
            this.IsValue = _type.IsValueType;
            _properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// 映射对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public T Map(T value)
        {
            if (this.IsValue)
            {
                return value;
            }
            var obj = Activator.CreateInstance(_type);
            for (int i = 0; i < _properties.Length; i++)
            {
                var pro = _properties[i];
                pro.SetValue(obj, pro.GetValue(value));
            }
            return (T)obj;
        }

        /// <summary>
        /// 映射对象
        /// </summary>
        /// <param name="getValue"></param>
        /// <returns></returns>
        public T Map(Func<PropertyInfo, object?> getValue)
        {
            if (this.IsValue) throw new TypeNotSupportedException(_type);
            var obj = Activator.CreateInstance(_type);
            for (int i = 0; i < _properties.Length; i++)
            {
                var pro = _properties[i];
                var value = getValue(pro);
                if (value is null) continue;
                pro.SetValue(obj, value.ConvertTo(pro.PropertyType));
            }
            return (T)obj;
        }
    }
}
