using Suyaa.Data.Dependency;
using Suyaa.Data.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 数据库供应商助手
    /// </summary>
    public static class DbScriptProviderHelper
    {
        /// <summary>
        /// 获取实例Insert脚本
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetEntityInsert(this IDbScriptProvider provider, EntityDescriptor entity)
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();
            foreach (var field in entity.Fields)
            {
                if (field.IsAutoIncrement) continue;
                if (columns.Length > 0) columns.Append(',');
                columns.Append(provider.GetName(field.Name));
                if (values.Length > 0) values.Append(',');
                values.Append(provider.GetVariable("V_" + field.Index));
            }
            // 拼接sql脚本
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO ");
            if (!entity.Schema.IsNullOrWhiteSpace())
            {
                sql.Append(entity.Schema);
                sql.Append('.');
            }
            sql.Append(provider.GetName(entity.Name));
            sql.Append(" (");
            sql.Append(columns.ToString());
            sql.Append(") VALUES (");
            sql.Append(values.ToString());
            sql.Append(")");
            return sql.ToString();
        }
    }
}
