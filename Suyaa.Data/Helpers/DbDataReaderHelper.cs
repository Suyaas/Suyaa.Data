using Suyaa.Data.Descriptors;
using sy;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 数据库阅读器
    /// </summary>
    public static class DbDataReaderHelper
    {

        /// <summary>
        /// 读取单个数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public static T ToValue<T>(this DbDataReader reader)
        {
            return reader[0].ConvertTo<T>();
        }

        /// <summary>
        /// 转化为泛型实例
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <param name="ordinals"></param>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public static T ToInstance<T>(this DbDataReader reader, EntityDescriptor entity, IDictionary<string, int> ordinals)
        {
            var obj = sy.Assembly.Create(entity.Type);
            if (obj is null) throw new DbException("Type '{0}' instance fail.", entity.Type.FullName);
            foreach (var field in entity.Fields)
            {
                if (!ordinals.ContainsKey(field.Name)) continue;
                var idx = ordinals[field.Name];
                var value = reader[idx].ConvertTo(field.PropertyInfo.DeclaringType);
                field.PropertyInfo.SetValue(obj, value, null);
            }
            return (T)obj;
        }

        /// <summary>
        /// 获取字段索引集合
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Dictionary<string, int> GetEntityOrdinals(this DbDataReader reader, EntityDescriptor entity)
        {
            Dictionary<string, int> ordinals = new Dictionary<string, int>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                string name = reader.GetName(i).ToUpper();
                foreach (var field in entity.Fields)
                {
                    if (!field.PropertyInfo.CanWrite) continue;
                    if (field.Name.ToUpper() == name) ordinals[field.Name] = i;
                }
            }
            return ordinals;
        }
        /// <summary>
        /// 填充数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="datas"></param>
        public static void FillDatas<T>(this DbDataReader reader, IList<T> datas)
        {
            // 无内容则退出
            if (!reader.HasRows) return;
            // 执行类型反射
            var type = typeof(T);
            EntityDescriptor? entity = null;
            if (!type.IsValueType) entity = new EntityDescriptor(type);
            Dictionary<string, int> ordinals = new Dictionary<string, int>();
            // 进行字段名称初始化
            if (entity != null) ordinals = reader.GetEntityOrdinals(entity);
            // 读取内容
            while (reader.Read())
            {
                // 跳过
                if (reader.FieldCount <= 0) continue;
                if (entity is null)
                {
                    datas.Add(reader.ToValue<T>());
                }
                else
                {
                    datas.Add(reader.ToInstance<T>(entity, ordinals));
                }
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        [return: MaybeNull]
        public static T GetData<T>(this DbDataReader reader)
        {
            // 无内容则退出
            if (!reader.HasRows) return default;
            // 执行类型反射
            var type = typeof(T);
            EntityDescriptor? entity = null;
            if (!type.IsValueType) entity = new EntityDescriptor(type);
            Dictionary<string, int> ordinals = new Dictionary<string, int>();
            // 进行字段名称初始化
            if (entity != null) ordinals = reader.GetEntityOrdinals(entity);
            // 读取内容
            if (!reader.Read()) return default;
            // 跳过无字段
            if (reader.FieldCount <= 0) return default;
            if (entity is null)
            {
                return reader.ToValue<T>();
            }
            else
            {
                return reader.ToInstance<T>(entity, ordinals);
            }
        }
    }
}
