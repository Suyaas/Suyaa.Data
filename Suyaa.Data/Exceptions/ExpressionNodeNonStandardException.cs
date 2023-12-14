using Suyaa.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data
{
    /// <summary>
    /// 异常 - 不支持的表达式节点
    /// </summary>
    public class ExpressionNodeNonStandardException : DbException
    {
        /// <summary>
        /// 不支持的表达式节点
        /// </summary>
        public const string KEY_EXPRESSION_NODE_NON_STANDARD = "Expression.Node.NonStandard";

        /// <summary>
        /// 不支持的表达式节点类型
        /// </summary>
        public const string KEY_EXPRESSION_NODE_TYPE_NON_STANDARD = "Expression.NodeType.NonStandard";

        /// <summary>
        /// 异常 - 不支持的表达式节点类型
        /// </summary>
        /// <param name="expressionType"></param>
        public ExpressionNodeNonStandardException(ExpressionType expressionType) : base(KEY_EXPRESSION_NODE_TYPE_NON_STANDARD, "Expression node type '{0}' does non standard.", expressionType.ToString())
        {
        }

        /// <summary>
        /// 异常 - 不支持的表达式节点
        /// </summary>
        /// <param name="name"></param>
        public ExpressionNodeNonStandardException(string name) : base(KEY_EXPRESSION_NODE_NON_STANDARD, "Expression node '{0}' does non standard.", name)
        {
        }
    }
}
