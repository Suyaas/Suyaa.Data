using Suyaa.Data.Models;
using Suyaa.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        public static DbParameters GetParameters<TEntity>(this DbEntityModel descriptor, TEntity entity)
        {
            // 生成参数
            DbParameters parameters = new DbParameters();
            foreach (var field in descriptor.Columns)
            {
                parameters.Add("V_" + field.Index, field.PropertyInfo.GetValue(entity));
            }
            return parameters;
        }

        // 获取字段名称
        private static string ConvertMemberToString(MemberInfo member, IEnumerable<ColumnModel> fields)
        {
            var pro = fields.Where(d => d.PropertyInfo.Name == member.Name).FirstOrDefault();
            if (pro is null) throw new NotExistException(member.Name);
            return pro.Name;
        }

        /// <summary>
        /// 获取待更新字段集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static List<ColumnModel> GetEntityUpdateFields<TEntity>(this DbEntityModel entity, Expression<Func<TEntity, object>> selector)
        {
            List<string> columns = new List<string>();
            var body = selector.Body;
            switch (body)
            {
                case NewExpression expression: // new语句
                    foreach (var member in expression.Members)
                        columns.Add(ConvertMemberToString(member, entity.Columns));
                    break;
                case UnaryExpression expression: // 标准语句
                    var operand = (MemberExpression)expression.Operand;
                    columns.Add(ConvertMemberToString(operand.Member, entity.Columns));
                    break;
                case MemberExpression expression: // 变量语句
                    columns.Add(ConvertMemberToString(expression.Member, entity.Columns));
                    break;
                default:
                    throw new ExpressionNodeNotSupportedException(body.NodeType);
            }
            return entity.Columns.Where(d => columns.Contains(d.Name)).ToList();
        }
    }
}
