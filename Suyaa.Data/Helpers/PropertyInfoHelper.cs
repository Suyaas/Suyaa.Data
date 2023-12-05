using Suyaa.Data.Dependency;
using Suyaa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using Suyaa.Data.Attributes;
using Suyaa.Data.Enums;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 属性扩展
    /// </summary>
    public static class PropertyInfoHelper
    {
        /// <summary>
        /// 是否含有Key特性
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static bool IsKey(this PropertyInfo pro)
        {
            return pro.GetCustomAttributes<KeyAttribute>().Any();
        }

        /// <summary>
        /// 是否为可空字段
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static bool IsNullable(this PropertyInfo pro)
        {
            if (pro.PropertyType.IsNullable()) return true;
            return pro.GetCustomAttributes<RequiredAttribute>().Any();
        }

        /// <summary>
        /// 是否含有DatabaseGenerated(Identity)特性
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static bool IsAutoIncrement(this PropertyInfo pro)
        {
            var attr = pro.GetCustomAttribute<DatabaseGeneratedAttribute>();
            if (attr is null) return false;
            return attr.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity;
        }

        /// <summary>
        /// 是否为索引
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static bool IsIndex(this PropertyInfo pro)
        {
            var attr = pro.GetCustomAttribute<ColumnIndexAttribute>();
            if (attr is null) return false;
            return attr.Type != ColumnIndexType.None;
        }


        /// <summary>
        /// 是否为唯一索引
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static bool IsUniqueIndex(this PropertyInfo pro)
        {
            var attr = pro.GetCustomAttribute<ColumnIndexAttribute>();
            if (attr is null) return false;
            return attr.Type == ColumnIndexType.Unique;
        }

        /// <summary>
        /// 是否为组合索引
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static bool IsGroupIndex(this PropertyInfo pro)
        {
            var attr = pro.GetCustomAttribute<ColumnIndexAttribute>();
            if (attr is null) return false;
            return attr.Type == ColumnIndexType.Group;
        }

        /// <summary>
        /// 获取索引名称
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static string GetIndexName<T>(this PropertyInfo pro)
        {
            var indexAttr = pro.GetCustomAttribute<ColumnIndexAttribute>();
            if (!indexAttr.Name.IsNullOrWhiteSpace()) return indexAttr.Name;
            string tableName = typeof(T).GetTableName();
            string columnName = pro.GetColumnName();
            return $"IDX_{tableName}_{columnName}";
        }

        /// <summary>
        /// 获取列属性名称
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static string GetColumnName(this PropertyInfo pro)
        {
            var columnAttr = pro.GetCustomAttribute<ColumnAttribute>();
            if (columnAttr is null) return pro.Name;
            if (columnAttr.Name.IsNullOrWhiteSpace()) return pro.Name;
            return columnAttr.Name ?? string.Empty;
        }

        /// <summary>
        /// 获取列描述名称
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static string GetDescription(this PropertyInfo pro)
        {
            var descriptionAttr = pro.GetCustomAttribute<DescriptionAttribute>();
            if (descriptionAttr is null) return string.Empty;
            return descriptionAttr.Description;
        }

        /// <summary>
        /// 获取所有元数据
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public static List<object> GetMetaDatas(this PropertyInfo pro)
        {
            return pro.GetCustomAttributes(false).ToList();
        }
    }
}
