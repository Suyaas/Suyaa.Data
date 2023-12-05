using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;

namespace Suyaa.Data.Attributes
{
    /// <summary>
    /// 数据字段类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnTypeAttribute : Attribute
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public ColumnValueType ColumnType { get; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// 字段精度
        /// </summary>
        public int Float { get; }

        // 校验有效性
        private void Verify()
        {
            var type = typeof(ColumnValueType);
            string columnTypeName = ColumnType.ToString();
            var field = type.GetFields().Where(d => d.Name == columnTypeName).FirstOrDefault();
            if (field is null) throw new DbException("ColumnType.NotSupported", "Column type '{0}' does not supported.", columnTypeName);
            var dbNeedSize = field.GetCustomAttribute<NeedSizeAttribute>();
            if (dbNeedSize != null && Size <= 0) throw new DbException("ColumnType.NeedSize", "Column type '{0}' missing size.", columnTypeName);
        }

        /// <summary>
        /// 数据字段类型
        /// </summary>
        /// <param name="name">自定义名称</param>
        public ColumnTypeAttribute(string name)
        {
            Name = name;
            ColumnType = ColumnValueType.Unknow;
            Size = 0;
            Float = 0;
        }

        /// <summary>
        /// 数据字段类型
        /// </summary>
        /// <param name="columnType"></param>
        public ColumnTypeAttribute(ColumnValueType columnType)
        {
            Name = string.Empty;
            ColumnType = columnType;
            Size = 0;
            Float = 0;
            // 进行验证
            Verify();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnType"></param>
        /// <param name="size"></param>
        public ColumnTypeAttribute(ColumnValueType columnType, int size)
        {
            Name = string.Empty;
            ColumnType = columnType;
            Size = size;
            Float = 0;
            // 进行验证
            Verify();
        }
    }
}
