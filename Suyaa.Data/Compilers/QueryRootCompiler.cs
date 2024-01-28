using Suyaa.Data.Compilers.Dependency;
using Suyaa.Data.Expressions;
using Suyaa.Data.Expressions.Dependency;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Compilers
{
    /// <summary>
    /// 实体根查询助手
    /// </summary>
    public sealed class QueryRootCompiler : ExpressionCompiler<QueryRootExpression>, IStatementable<QueryRootExpression>, IFromable<QueryRootExpression>
    {

        /// <summary>
        /// 获取语句
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string GetStatement(QueryRootExpression expression)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            // 拼接列
            foreach (var column in expression.Model.EntityModel.Columns)
            {
                if (sb.Length > 7) sb.Append(',');
                sb.Append(Provider.GetName(column.Name));
            }
            // 拼接表
            sb.Append(" ");
            // 添加From语句
            sb.Append(GetFromStatement(expression));
            // 添加标准
            return sb.ToString();
        }

        /// <summary>
        /// 获取From语句
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string GetFromStatement(QueryRootExpression expression)
        {
            StringBuilder sb = new StringBuilder();
            // 拼接表
            sb.Append("FROM ");
            if (!expression.Model.EntityModel.Schema.IsNullOrWhiteSpace())
            {
                sb.Append(Provider.GetName(expression.Model.EntityModel.Schema));
                sb.Append(".");
            }
            sb.Append(Provider.GetName(expression.Model.EntityModel.Name));
            // 输出别名
            if (!expression.Alias.IsNullOrWhiteSpace())
            {
                sb.Append(' ');
                sb.Append(expression.Alias);
            }
            return sb.ToString();
        }
    }
}
