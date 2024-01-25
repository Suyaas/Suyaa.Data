using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Suyaa.Data.Compilers.Dependency;
using Suyaa.Data.Compilers.Sets;
using Suyaa.Data.Entities;
using Suyaa.Data.Expressions;
using Suyaa.Data.Models;

namespace Suyaa.Data.Compilers
{
    /// <summary>
    /// 函数执行
    /// </summary>
    public sealed class UnaryCompiler : ExpressionCompiler<UnaryExpression>, IColumnsable<UnaryExpression>
    {
        // 表达式类型
        private static readonly Type _expressionType = typeof(Expression<>);

        // 获取Lambda表达式
        private Expression GetLambdaExpression(Expression expression, Type type)
        {
            var getBodyMethod = type.GetMethod("get_Body");
            return (Expression)getBodyMethod.Invoke(expression, new object[0]);
        }

        // 常量处理
        private IEnumerable<SelectColumnSet> GetExpressionSelectColumns(Expression expression, DbEntityModel model)
        {
            var type = expression.GetType();
            if (type.BaseType.IsGenericType)
            {
                var genericTypeDefinition = type.BaseType.GetGenericTypeDefinition();
                if (genericTypeDefinition == _expressionType)
                {
                    var body = GetLambdaExpression(expression, type);
                    return GetExpressionSelectColumns(body, model);
                }
            }
            switch (expression)
            {
                // 成员
                case MemberExpression memberExpression:
                    return new List<SelectColumnSet> { CreateStatementBuilder(memberExpression).GetColumn<MemberCompiler>(model) };
                // new
                case NewExpression newExpression:
                    return CreateStatementBuilder(newExpression).GetColumns<NewCompiler>(model);
                // memberInit
                case MemberInitExpression memberInitExpression:
                    return CreateStatementBuilder(memberInitExpression).GetColumns<MemberInitCompiler>(model);
            }
            return Enumerable.Empty<SelectColumnSet>();
        }

        /// <summary>
        /// 获取普通选择语句
        /// </summary>
        /// <param name="unaryExpression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<SelectColumnSet> GetColumns(UnaryExpression unaryExpression, DbEntityModel model)
        {

            List<SelectColumnSet> columns = new List<SelectColumnSet>();
            switch (unaryExpression.NodeType)
            {
                // 常量
                case ExpressionType.Quote:
                    return GetExpressionSelectColumns(unaryExpression.Operand, model);
                default:
                    //var operand = (MemberExpression)unaryExpression.Operand;
                    //columns.Add(SelectCompiler.ConvertMemberToString(operand.Member, model.Columns));
                    break;
            }

            return columns;
        }
    }
}
