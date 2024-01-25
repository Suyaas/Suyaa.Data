using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Suyaa.Data.Compilers.Dependency;
using Suyaa.Data.Expressions;
using Suyaa.Data.Models;

namespace Suyaa.Data.Compilers
{
    /// <summary>
    /// 函数执行
    /// </summary>
    public sealed class SelectCompiler : ExpressionCompiler<SelectExpression>, IStatementable<SelectExpression>
    {

        // 获取
        private string GetSelectStatement(UnaryExpression unaryExpression, QueryRootExpression queryRootExpression)
        {
            StringBuilder sb = new StringBuilder();
            var columns = CreateStatementBuilder(unaryExpression).GetColumns<UnaryCompiler>(queryRootExpression.Model);
            foreach (var column in columns)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append(queryRootExpression.Alias);
                sb.Append('.');
                sb.Append(Provider.GetName(column.ColumnName));
                sb.Append(" AS ");
                sb.Append(column.MemberName);
            }
            return sb.ToString();
        }

        // 获取根查询语句
        private string GetQueryRootStatement(QueryRootExpression queryRootExpression, Expression selector)
        {
            // 处理别名
            if (queryRootExpression.Alias.IsNullOrWhiteSpace()) queryRootExpression.Alias = VariableBuilder.GetNewTableName();
            QueryRootCompiler queryRootCompiler = new QueryRootCompiler();
            queryRootCompiler.Initialize(Provider, VariableBuilder);
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            // 拼接选择器
            switch (selector)
            {
                // 一元表达式
                case UnaryExpression unaryExpression:
                    sb.Append(GetSelectStatement(unaryExpression, queryRootExpression));
                    break;
            }
            // 拼接表
            sb.Append(' ');
            sb.Append(queryRootCompiler.GetFromStatement(queryRootExpression));
            sb.Append(' ');
            sb.Append(queryRootExpression.Alias);
            return sb.ToString();
        }

        /// <summary>
        /// 获取语句
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string GetStatement(SelectExpression expression)
        {
            switch (expression.Query)
            {
                // 根查询
                case QueryRootExpression queryRootExpression:
                    return GetQueryRootStatement(queryRootExpression, expression.Selector);
            }
            return string.Empty;
        }
    }
}
