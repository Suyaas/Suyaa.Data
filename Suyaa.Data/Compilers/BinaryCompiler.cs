using Suyaa.Data.Compilers.Dependency;
using Suyaa.Data.Compilers.Sets;
using Suyaa.Data.Entities;
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
    /// 二元表达式
    /// </summary>
    public sealed class BinaryCompiler : ExpressionCompiler<BinaryExpression>, IValuable<BinaryExpression>, IPredicatable<BinaryExpression>
    {
        /// <summary>
        /// 获取判断条件
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ExpressionNodeNotSupportedException"></exception>
        public string GetPredicate(BinaryExpression expression, QueryRootModel model)
        {
            StringBuilder sb = new StringBuilder();
            string expLeft = Provider.GetScriptValue(GetExpressionValue(expression.Left, model));
            string expRight = Provider.GetScriptValue(GetExpressionValue(expression.Right, model));
            sb.Append(expLeft);
            switch (expression.NodeType)
            {
                case ExpressionType.AndAlso:
                    sb.Append(" AND ");
                    break;
                case ExpressionType.OrElse:
                    sb.Append(" OR ");
                    break;
                case ExpressionType.Equal:
                    if (expRight == "NULL")
                    {
                        sb.Append(" IS ");
                    }
                    else
                    {
                        sb.Append(" = ");
                    }
                    break;
                case ExpressionType.NotEqual:
                    if (expRight == "NULL")
                    {
                        sb.Append(" IS NOT ");
                    }
                    else
                    {
                        sb.Append(" <> ");
                    }
                    break;
                case ExpressionType.GreaterThan:
                    sb.Append(" > ");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    sb.Append(" >= ");
                    break;
                case ExpressionType.LessThan:
                    sb.Append(" < ");
                    break;
                case ExpressionType.LessThanOrEqual:
                    sb.Append(" <= ");
                    break;
                case ExpressionType.Coalesce:
                    return $"COALESCE({expLeft}, {expRight})";
                default: throw new ExpressionNodeNotSupportedException(expression.NodeType);
            }
            sb.Append(expRight);
            return sb.ToString();
        }


        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ExpressionNodeNotSupportedException"></exception>
        public ValueSet GetValue(BinaryExpression expression, QueryRootModel model)
        {
            return ValueSet.Create(Sets.ValueType.Expression, GetPredicate(expression, model));
        }
    }
}
