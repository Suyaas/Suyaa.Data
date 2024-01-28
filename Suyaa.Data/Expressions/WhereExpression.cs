using Suyaa.Data.Expressions.Dependency;
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
    public sealed class WhereExpression : Expression, IHaveAlias
    {
        /// <summary>
        /// 节点类型
        /// </summary>
        public override ExpressionType NodeType => ExpressionType.Call;

        /// <summary>
        /// 类型
        /// </summary>
        public override Type Type { get; }

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
        /// 别名
        /// </summary>
        public string Alias
        {
            get
            {
                if (Query is IHaveAlias haveAlias) return haveAlias.Alias;
                return string.Empty;
            }
            set
            {
                if (Query is IHaveAlias haveAlias) haveAlias.Alias = value;
            }
        }

        /// <summary>
        /// Select表达式
        /// </summary>
        public WhereExpression(Type type, EntityModel model, Expression query, Expression predicate)
        {
            Type = type;
            Model = model;
            Query = query;
            Predicate = predicate;
        }
    }
}
