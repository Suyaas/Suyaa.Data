using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Suyaa.Data.Attributes;
using Suyaa.Data.Enums;

namespace Suyaa.Data.Sqlite.Helpers
{
    /// <summary>
    /// 属性扩展
    /// </summary>
    public static class PropertyInfoHelper
    {
        /// <summary>
        /// 获取列属性类型
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static string GetColumnAttributeType(this PropertyInfo pro)
        {
            #region 兼容 DbColumnType 特性
            var dbColumnTypeAttr = pro.GetCustomAttribute<ColumnTypeAttribute>();
            if (dbColumnTypeAttr != null)
            {
                switch (dbColumnTypeAttr.Type)
                {
                    //case ColumnValueType.Unknow: return dbColumnTypeAttr;
                    case ColumnValueType.Text:
                    case ColumnValueType.Varchar:
                    case ColumnValueType.Char:
                        return "TEXT";
                    case ColumnValueType.Bool:
                    case ColumnValueType.TinyInt:
                    case ColumnValueType.SmallInt:
                    case ColumnValueType.Int:
                    case ColumnValueType.BigInt:
                        return "INTEGER";
                    case ColumnValueType.Single:
                    case ColumnValueType.Double:
                    case ColumnValueType.Decimal:
                        return "REAL";
                }
            }
            #endregion

            #region 兼容 Column 特性
            var columnAttr = pro.GetCustomAttribute<System.ComponentModel.DataAnnotations.Schema.ColumnAttribute>();
            if (columnAttr != null)
                if (!columnAttr.TypeName.IsNullOrWhiteSpace())
                    return columnAttr.TypeName ?? string.Empty;
            #endregion

            #region 兼容C#类型
            var proType = pro.PropertyType;
            var proTypeCode = Type.GetTypeCode(proType);
            switch (proTypeCode)
            {
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                    return "INTEGER";
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return "REAL";
                case TypeCode.String:
                    return "TEXT";
                default:
                    throw new TypeNotSupportedException(proType);
            }
            #endregion
        }
    }
}
