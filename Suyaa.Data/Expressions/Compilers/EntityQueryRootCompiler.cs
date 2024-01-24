using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Expressions.Compilers
{
    /// <summary>
    /// 实体根查询助手
    /// </summary>
    public sealed class EntityQueryRootCompiler : ExpressionCompiler<EntityQueryRootExpression>
    {

        /// <summary>
        /// 获取语句
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public override string GetStatement(EntityQueryRootExpression expression)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            // 拼接列
            if (expression.Model is DbEntityModel dbEntityModel)
            {
                foreach (var column in dbEntityModel.Columns)
                {
                    if (sb.Length > 7) sb.Append(',');
                    sb.Append(Provider.GetName(column.Name));
                }
                // 拼接表
                sb.Append(" FROM ");
                if (!dbEntityModel.Schema.IsNullOrWhiteSpace())
                {
                    sb.Append(Provider.GetName(dbEntityModel.Schema));
                    sb.Append(".");
                }
                sb.Append(Provider.GetName(dbEntityModel.Name));
            }
            else
            {
                foreach (var property in expression.Model.Properties)
                {
                    if (sb.Length > 7) sb.Append(',');
                    sb.Append(Provider.GetName(property.PropertyInfo.Name));
                }
            }
            return sb.ToString();
        }
    }
}
