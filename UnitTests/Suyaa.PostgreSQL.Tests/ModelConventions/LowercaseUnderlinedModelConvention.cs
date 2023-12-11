using Suyaa;
using Suyaa.Data.Dependency;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuyaaTest.PostgreSQL.ModelConventions
{
    /// <summary>
    /// 小写下划线命名方式
    /// </summary>
    public sealed class LowercaseUnderlinedModelConvention : IEntityModelConvention
    {
        private const byte Between_Upper_Lower = 'a' - 'A';

        // 获取小写下划线字符串
        private static string GetLowercaseUnderlined(string str)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char chr = str[i];
                if (chr >= 'A' && chr <= 'Z')
                {
                    if (sb.Length > 0) sb.Append('_');
                    sb.Append((char)(chr + Between_Upper_Lower));
                }
                else
                {
                    sb.Append(chr);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 处理实体建模
        /// </summary>
        /// <param name="entity"></param>
        public void OnEntityModeling(EntityModel entity)
        {
            if (entity is DbEntityModel dbEntityModel)
            {
                var tableAttr = entity.MetaDatas.Where(d => d is TableAttribute).Select(d => (TableAttribute)d).FirstOrDefault();
                if (tableAttr is null)
                {
                    dbEntityModel.Name = GetLowercaseUnderlined(entity.Type.Name);
                    return;
                }
                if (tableAttr.Name.IsNullOrWhiteSpace())
                {
                    dbEntityModel.Name = GetLowercaseUnderlined(entity.Type.Name);
                    return;
                }
            }
        }

        public void OnPropertyModeling(EntityModel entity, PropertyInfoModel property)
        {
            if (property is FieldModel fieldModel)
            {
                var columnAttr = property.MetaDatas.Where(d => d is ColumnAttribute).Select(d => (ColumnAttribute)d).FirstOrDefault();
                if (columnAttr is null)
                {
                    fieldModel.Name = GetLowercaseUnderlined(property.PropertyInfo.Name);
                    return;
                }
                if (columnAttr.Name.IsNullOrWhiteSpace())
                {
                    fieldModel.Name = GetLowercaseUnderlined(property.PropertyInfo.Name);
                    return;
                }
            }
        }
    }
}
