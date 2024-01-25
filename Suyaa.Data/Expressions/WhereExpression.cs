using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Expressions
{
    /// <summary>
    /// Select表达式
    /// </summary>
    public sealed class WhereExpression : Expression
    {
        /// <summary>
        /// 节点类型
        /// </summary>
        public override ExpressionType NodeType => ExpressionType.Call;

        /// <summary>
        /// 实体建模
        /// </summary>
        public EntityModel Model { get; }

        /// <summary>
        /// 查询
        /// </summary>
        public Expression Query { get; }

        /// <summary>
        /// 条件
        /// </summary>
        public Expression Predicate { get; }

        /// <summary>
        /// Select表达式
        /// </summary>
        public WhereExpression(EntityModel model, Expression query, Expression predicate)
        {
            Model = model;
            Query = query;
            Predicate = predicate;
        }
    }
}
