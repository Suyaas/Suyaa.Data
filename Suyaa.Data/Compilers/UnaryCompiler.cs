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
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;

namespace Suyaa.Data.Compilers
{
    /// <summary>
    /// 函数执行
    /// </summary>
    public sealed class UnaryCompiler : ExpressionCompiler<UnaryExpression>, IColumnsable<UnaryExpression>, IPredicatable<UnaryExpression>, IValuable<UnaryExpression>
    {
        // 表达式类型
        private static readonly Type _expressionType = typeof(Expression<>);

        #region Select

        // 获取Lambda表达式
        private Expression GetLambdaExpression(Expression expression, Type type)
        {
            var getBodyMethod = type.GetMethod("get_Body");
            return (Expression)getBodyMethod.Invoke(expression, new object[0]);
        }

        // 常量处理
        private IEnumerable<SelectColumnSet> GetExpressionSelectColumns(Expression expression, QueryRootModel model)
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
        public IEnumerable<SelectColumnSet> GetColumns(UnaryExpression unaryExpression, QueryRootModel model)
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

        #endregion

        #region Where

        // 常量处理
        private string GetExpressionPredicate(Expression expression, QueryRootModel model)
        {
            var type = expression.GetType();
            if (type.BaseType.IsGenericType)
            {
                var genericTypeDefinition = type.BaseType.GetGenericTypeDefinition();
                if (genericTypeDefinition == _expressionType)
                {
                    var body = GetLambdaExpression(expression, type);
                    return GetExpressionPredicate(body, model);
                }
            }
            switch (expression)
            {
                // 函数调用
                case MethodCallExpression methodCallExpression:
                    return CreateStatementBuilder(methodCallExpression).GetPredicate<MethodCallCompiler>(model);
                // 成员
                case MemberExpression memberExpression:
                    //return new List<SelectColumnSet> { CreateStatementBuilder(memberExpression).GetColumn<MemberCompiler>(model) };
                    break;
                // new
                case NewExpression newExpression:
                    //return CreateStatementBuilder(newExpression).GetColumns<NewCompiler>(model);
                    break;
                // memberInit
                case MemberInitExpression memberInitExpression:
                    //return CreateStatementBuilder(memberInitExpression).GetColumns<MemberInitCompiler>(model);
                    break;
                // 二元表达式
                case BinaryExpression binaryExpression:
                    return CreateStatementBuilder(binaryExpression).GetPredicate<BinaryCompiler>(model);
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取查询
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetPredicate(UnaryExpression expression, QueryRootModel model)
        {
            switch (expression.NodeType)
            {
                // 常量
                case ExpressionType.Quote:
                    return GetExpressionPredicate(expression.Operand, model);
                default:
                    //var operand = (MemberExpression)unaryExpression.Operand;
                    //columns.Add(SelectCompiler.ConvertMemberToString(operand.Member, model.Columns));
                    return string.Empty;
            }
        }

        #endregion

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ValueSet GetValue(UnaryExpression expression, QueryRootModel model)
        {
            if (expression.Operand is UnaryExpression unaryExpression) return GetValue(unaryExpression, model);
            if (expression.Operand is ConstantExpression constantExpression) return CreateStatementBuilder(constantExpression).GetValue<ConstantCompiler>(model);
            var operand = (MemberExpression)expression.Operand;
            var operandValues = GetExpressionValue(operand.Expression, model) ?? throw new NullException("MemberExpression.Object");
            var value = GetFieldOrPropertyValue(operandValues, operand.Member.Name);
            if (value is null) return ValueSet.Null;
            if (value is string str) return ValueSet.Create(Sets.ValueType.StringValue, str);
            if (value.GetType().IsValueType) return ValueSet.Create(Sets.ValueType.RegularValue, value);
            return ValueSet.Create(Sets.ValueType.Object, value);
        }
    }
}
