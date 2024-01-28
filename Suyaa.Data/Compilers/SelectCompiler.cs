using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Suyaa.Data.Compilers.Dependency;
using Suyaa.Data.Expressions;
using Suyaa.Data.Expressions.Dependency;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;

namespace Suyaa.Data.Compilers
{
    /// <summary>
    /// 函数执行
    /// </summary>
    public sealed class SelectCompiler : ExpressionCompiler<SelectExpression>, IStatementable<SelectExpression>
    {

        // 获取
        private string GetSelectStatement(UnaryExpression unaryExpression, Expression expression)
        {
            if (!(expression is IHaveModel haveModel)) return string.Empty;
            StringBuilder sb = new StringBuilder();
            var columns = CreateStatementBuilder(unaryExpression).GetColumns<UnaryCompiler>(haveModel.Model);
            foreach (var column in columns)
            {
                if (sb.Length > 0) sb.Append(",");
                if (expression is IHaveAlias haveAlias)
                {
                    sb.Append(haveAlias.Alias);
                    sb.Append('.');
                }
                sb.Append(Provider.GetName(column.ColumnName));
                sb.Append(" AS ");
                sb.Append(column.MemberName);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取查询脚本
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="unaryExpression"></param>
        /// <returns></returns>
        public string GetPredicate<TEntity>(QueryRootModel entity, UnaryExpression unaryExpression)
        {
            switch (unaryExpression.Operand)
            {
                case BinaryExpression binaryExpression:
                    break;
                case MethodCallExpression methodCallExpression:
                    return CreateStatementBuilder(methodCallExpression).GetPredicate<MethodCallCompiler>(entity);
            }
            return string.Empty;
            //return unaryExpression.Operand switch
            //{
            //    BinaryExpression binaryExpression => provider.GetBinaryExpressionScript(entity, binaryExpression),
            //    MethodCallExpression methodCallExpression => provider.GetMethodCallExpressionScript(entity, methodCallExpression),
            //    _ => throw new ExpressionNodeNotSupportedException(predicate.Body.NodeType),
            //};
        }

        // 获取Where语句
        private string GetWhereStatement(UnaryExpression unaryExpression, Expression expression)
        {
            if (!(expression is IHaveModel haveModel)) return string.Empty;
            return CreateStatementBuilder(unaryExpression).GetPredicate<UnaryCompiler>(haveModel.Model);
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
            return sb.ToString();
        }

        // 获取根查询语句
        private string GetWhereStatement(WhereExpression whereExpression, Expression selector)
        {
            // 处理别名
            if (whereExpression.Alias.IsNullOrWhiteSpace()) whereExpression.Alias = VariableBuilder.GetNewTableName();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            // 拼接选择器
            switch (selector)
            {
                // 一元表达式
                case UnaryExpression unaryExpression:
                    sb.Append(GetSelectStatement(unaryExpression, whereExpression.Query));
                    break;
            }
            // 拼接表
            sb.Append(' ');
            switch (whereExpression.Query)
            {
                case QueryRootExpression queryRootExpression:
                    sb.Append(CreateStatementBuilder(queryRootExpression).SetVariableBuilder(VariableBuilder).GetFromStatement<QueryRootCompiler>());
                    break;
            }
            // 拼接条件
            sb.Append(" WHERE 1=1");
            switch (whereExpression.Predicate)
            {
                // 一元表达式
                case UnaryExpression unaryExpression:
                    string unaryStatement = GetWhereStatement(unaryExpression, whereExpression.Query);
                    if (!unaryStatement.IsNullOrWhiteSpace())
                    {
                        sb.Append(" AND (");
                        sb.Append(unaryStatement);
                        sb.Append(')');
                    }
                    break;
            }
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
                case WhereExpression whereExpression:
                    return GetWhereStatement(whereExpression, expression.Selector);
            }
            return string.Empty;
        }
    }
}
