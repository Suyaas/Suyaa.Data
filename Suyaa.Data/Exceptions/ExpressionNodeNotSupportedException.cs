using System.Linq.Expressions;

namespace Suyaa.Data
{
    /// <summary>
    /// 异常 - 不支持的表达式节点
    /// </summary>
    public class ExpressionNodeNotSupportedException : DbException
    {
        /// <summary>
        /// 不支持的表达式节点
        /// </summary>
        public const string KEY_EXPRESSION_NODE_NOT_SUPPORTED = "Expression.Node.NotSupported";

        /// <summary>
        /// 不支持的表达式节点类型
        /// </summary>
        public const string KEY_EXPRESSION_NODE_TYPE_NOT_SUPPORTED = "Expression.NodeType.NotSupported";

        /// <summary>
        /// 异常 - 不支持的表达式节点类型
        /// </summary>
        /// <param name="expressionType"></param>
        public ExpressionNodeNotSupportedException(ExpressionType expressionType) : base(KEY_EXPRESSION_NODE_TYPE_NOT_SUPPORTED, "Expression node type '{0}' does not supported.", expressionType.ToString())
        {
        }

        /// <summary>
        /// 异常 - 不支持的表达式节点
        /// </summary>
        /// <param name="name"></param>
        public ExpressionNodeNotSupportedException(string name) : base(KEY_EXPRESSION_NODE_NOT_SUPPORTED, "Expression node '{0}' does not supported.", name)
        {
        }
    }
}
