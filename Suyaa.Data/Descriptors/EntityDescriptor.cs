using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Suyaa.Data.Descriptors
{
    /// <summary>
    /// 表描述
    /// </summary>
    public sealed class EntityDescriptor : TypeDescriptor
    {

        /// <summary>
        /// 创建表描述
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static EntityDescriptor Instance<T>()
            where T : class
        {
            return new EntityDescriptor(typeof(T));
        }

        /// <summary>
        /// 表描述
        /// </summary>
        public EntityDescriptor(Type type) : base(type.GetMetaDatas())
        {
            Type = type;
            this.Name = type.GetTableName();
            this.Schema = type.GetSchemaName() ?? string.Empty;
            // 初始化字段集合
            this.Fields = new List<FieldDescriptor>();
            var pros = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in pros)
            {
                this.Fields.Add(new FieldDescriptor(prop));
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 所属架构
        /// </summary>
        public string Schema { get; }

        /// <summary>
        /// 字段集合
        /// </summary>
        public List<FieldDescriptor> Fields { get; }
    }
}
