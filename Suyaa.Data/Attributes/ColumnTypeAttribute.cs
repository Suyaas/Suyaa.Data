using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.Data.Models;

namespace Suyaa.Data.Attributes
{
    /// <summary>
    /// 数据字段类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ColumnTypeAttribute : Attribute
    {

        /// <summary>
        /// 值类型
        /// </summary>
        public ColumnValueType Type { get; set; } = ColumnValueType.Unknow;
        /// <summary>
        /// 占用大小
        /// </summary>
        public int Size { get; set; } = 0;
        /// <summary>
        /// 精度
        /// </summary>
        public int Float { get; set; } = 0;
        // 校验有效性
        private void Verify()
        {
            var type = typeof(ColumnValueType);
            string columnTypeName = Type.ToString();
            var field = type.GetFields().Where(d => d.Name == columnTypeName).FirstOrDefault();
            if (field is null) throw new DbException("ColumnType.NotSupported", "Column type '{0}' does not supported.", columnTypeName);
            var dbNeedSize = field.GetCustomAttribute<NeedSizeAttribute>();
            if (dbNeedSize != null && Size <= 0) throw new DbException("ColumnType.NeedSize", "Column type '{0}' missing size.", columnTypeName);
        }

        /// <summary>
        /// 数据字段类型
        /// </summary>
        /// <param name="type"></param>
        public ColumnTypeAttribute(ColumnValueType type)
        {
            Type = type;
            // 进行验证
            Verify();
        }

        /// <summary>
        /// 数据字段类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size"></param>
        public ColumnTypeAttribute(ColumnValueType type, int size)
        {
            Type = type;
            Size = size;
            // 进行验证
            Verify();
        }
        /// <summary>
        /// 数据字段类型
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="flt"></param>
        public ColumnTypeAttribute(ColumnValueType type, int size, int flt)
        {
            Type = type;
            Size = size;
            Float = flt;
            // 进行验证
            Verify();
        }
    }
}
