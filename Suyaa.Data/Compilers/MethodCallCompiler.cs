using System;
using System.Collections;
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
using Suyaa.Data.Repositories.Dependency;

namespace Suyaa.Data.Compilers
{
    /// <summary>
    /// 函数执行
    /// </summary>
    public sealed class MethodCallCompiler : ExpressionCompiler<MethodCallExpression>, IPredicatable<MethodCallExpression>, IValuable<MethodCallExpression>
    {
        // 获取Contains函数兼容的sql语句
        private string GetContainsMethodScript(MethodCallExpression expression, QueryRootModel model)
        {
            var callObj = (MemberExpression)expression.Object;
            var callObjValues = GetExpressionValue(callObj.Expression, model);
            // 参数为建模属性
            if (callObjValues.Value == model)
            {
                var column = model.EntityModel.Columns.Where(d => d.PropertyInfo.Name == callObj.Member.Name).FirstOrDefault();
                if (column is null) throw new NullException($"model.{callObj.Member.Name}");
                var value = GetExpressionValue(expression.Arguments[0], model);
                return $"{column.Name} LIKE '%{value}%'";
            }
            // 列表
            var listInfo = callObjValues.GetType().GetField(callObj.Member.Name);
            ICollection list = (ICollection)listInfo.GetValue(callObjValues);
            StringBuilder sbList = new StringBuilder();
            sbList.Append('(');
            foreach (var item in list)
            {
                if (sbList.Length > 1) sbList.Append(", ");
                if (item is null) { sbList.Append("NULL"); continue; }
                if (item.GetType().IsNumeric()) { sbList.Append(item); continue; }
                if (item is string str) { sbList.Append(Provider.GetStringValue(str)); continue; }
                throw new TypeNotSupportedException(item.GetType());
            }
            sbList.Append(')');
            var arg = GetExpressionValue(expression.Arguments[0], model);
            return $"{arg} IN {sbList}";
        }

        // 获取Equals函数兼容的sql语句
        private string GetEqualsMethodScrip(MethodCallExpression methodCallExpression, QueryRootModel model)
        {
            var callObj = (MemberExpression)methodCallExpression.Object;
            object? value;
            string name;
            switch (callObj.Expression)
            {
                case ParameterExpression _:
                    name = Provider.GetName(callObj.Member.Name);
                    value = GetExpressionValue(methodCallExpression.Arguments[0], model);
                    break;
                default:
                    value = GetExpressionValue(callObj.Expression, model);
                    name = methodCallExpression.Arguments[0].NodeType.ToString();
                    break;
            }
            if (value is null) return $"{name} IS NULL";
            return $"{name} = {value}";
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ExpressionNodeNotSupportedException"></exception>
        public string GetPredicate(MethodCallExpression expression, QueryRootModel model)
        {
            var callMethod = expression.Method;
            //// 扩展值函数支持
            //if (callMethod.DeclaringType.FullName == Name_DBValue)
            //{
            //    switch (callMethod.Name)
            //    {
            //        case nameof(DbValue.IsNull):
            //            return provider.GetIsNullMethodScrip(entity, exp);
            //        case nameof(DbValue.IsNotNull):
            //            return provider.GetIsNotNullMethodScrip(entity, exp);

            //    }
            //}
            return callMethod.Name switch
            {
                "Contains" => GetContainsMethodScript(expression, model),
                "Equals" => GetEqualsMethodScrip(expression, model),
                _ => throw new ExpressionNodeNotSupportedException($"Call.{callMethod.Name}"),
            };
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ValueSet GetValue(MethodCallExpression expression, QueryRootModel model)
        {
            return ValueSet.Create(Sets.ValueType.Expression, GetPredicate(expression, model));
        }
    }
}
