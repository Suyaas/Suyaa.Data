using Suyaa.Data.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 实例描述助手
    /// </summary>
    public static class EntityDescriptorHelper
    {
        /// <summary>
        /// 获取参数集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="descriptor"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DbParameters GetParameters<TEntity>(this EntityDescriptor descriptor, TEntity entity)
        {
            // 生成参数
            DbParameters parameters = new DbParameters();
            foreach (var field in descriptor.Fields)
            {
                parameters.Add("V_" + field.Index, field.PropertyInfo.GetValue(entity));
            }
            return parameters;
        }
    }
}
