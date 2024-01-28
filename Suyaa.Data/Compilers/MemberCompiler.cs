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
    public sealed class MemberCompiler : ExpressionCompiler<MemberExpression>, IColumnable<MemberExpression>, IValuable<MemberExpression>
    {
        /// <summary>
        /// 获取语句
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public SelectColumnSet GetColumn(MemberExpression expression, QueryRootModel model)
        {
            var member = expression.Member;
            var pro = model.EntityModel.Columns.Where(d => d.PropertyInfo.Name == member.Name).FirstOrDefault();
            if (pro is null) throw new NotExistException(member.Name);
            return new SelectColumnSet(pro.Name, pro.PropertyInfo.Name);
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ValueSet GetValue(MemberExpression expression, QueryRootModel model)
        {
            if (expression.Expression is ParameterExpression parameterExpression)
            {
                // 查询实例属性
                var column = model.EntityModel.Columns.Where(d => d.PropertyInfo.Name == expression.Member.Name).FirstOrDefault();
                if (column is null) throw new NotExistException(expression.Member.Name);
                return ValueSet.Create(Sets.ValueType.Expression, model.Alias + "." + Provider.GetName(column.Name));
            }
            // 变量名称
            var memberName = expression.Member.Name;
            // 变量所属对象
            var memberObject = GetExpressionValue(expression.Expression, model) ?? throw new NullException(typeof(ConstantExpression));
            // 变量值
            var memberValue = GetFieldOrPropertyValue(memberObject, memberName);
            if (memberValue is null) return ValueSet.Null;
            if (memberValue is string str) return ValueSet.Create(Sets.ValueType.StringValue, str);
            if (memberValue.GetType().IsValueType) return ValueSet.Create(Sets.ValueType.RegularValue, memberValue);
            return ValueSet.Create(Sets.ValueType.Object, memberValue);
        }
    }
}
