using Suyaa.Data.Compilers.Dependency;
using Suyaa.Data.Compilers.Sets;
using Suyaa.Data.Expressions;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Compilers
{
    /// <summary>
    /// 常量
    /// </summary>
    public sealed class ConstantCompiler : ExpressionCompiler<ConstantExpression>, IValuable<ConstantExpression>
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ValueSet GetValue(ConstantExpression expression, QueryRootModel model)
        {
            if (expression.Value is null) return ValueSet.Null;
            if (expression.Value is string str) return ValueSet.Create(Sets.ValueType.StringValue, str);
            var valueType = expression.Value.GetType();
            if (valueType.IsValueType) return ValueSet.Create(Sets.ValueType.RegularValue, expression.Value);
            return ValueSet.Create(Sets.ValueType.Object, expression.Value);
        }
    }
}
