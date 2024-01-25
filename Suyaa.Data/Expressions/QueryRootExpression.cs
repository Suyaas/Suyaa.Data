using Suyaa.Data.Compilers.Dependency;
using Suyaa.Data.Expressions.Dependency;
using Suyaa.Data.Helpers;
using Suyaa.Data.Models;
using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Queries;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Expressions
{
    /// <summary>
    /// 查询根表达式
    /// </summary>
    public class QueryRootExpression : Expression, IHaveAlias
    {
        /// <summary>
        /// 表达式类型
        /// </summary>
        public override ExpressionType NodeType => ExpressionType.Extension;

        /// <summary>
        /// 类型
        /// </summary>
        public override Type Type { get; }

        /// <summary>
        /// 类型建模
        /// </summary>
        public DbEntityModel Model { get; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; } = string.Empty;

        /// <summary>
        /// 查询根表达式
        /// </summary>
        /// <param name="model"></param>
        public QueryRootExpression(DbEntityModel model)
        {
            Type = typeof(EntityQueryable<>).MakeGenericType(model.Type);
            Model = model;
        }
    }

    /// <summary>
    /// 查询根表达式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueryRootExpression<T> : QueryRootExpression
    {
        /// <summary>
        /// 查询根表达式
        /// </summary>
        public QueryRootExpression(IEntityModelFactory entityModelFactory) : base((DbEntityModel)entityModelFactory.GetEntity<T>())
        {
        }
    }
}
