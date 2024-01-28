using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Expressions;
using Suyaa.Data.Models;
using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Queries
{
    /// <summary>
    /// 可查询对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityQueryable<T> : IQueryable<T>, IQueryable
    {
        /// <summary>
        /// 元素类型
        /// </summary>
        public Type ElementType { get; }

        /// <summary>
        /// 表达式
        /// </summary>
        public Expression Expression { get; }

        /// <summary>
        /// 查询供应商
        /// </summary>
        public IQueryProvider Provider { get; }

        /// <summary>
        /// 获取枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            var list = this.Provider.Execute<IEnumerable<T>>(Expression);
            if (list is null) list = new List<T>();
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 可查询对象
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="dbEntityModel"></param>
        public EntityQueryable(EntityQueryProvider provider, DbEntityModel dbEntityModel)
        {
            Provider = provider;
            ElementType = typeof(T);
            Expression = new QueryRootExpression(dbEntityModel);
        }

        /// <summary>
        /// 可查询对象
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="entityModelFactory"></param>
        public EntityQueryable(EntityQueryProvider provider, IEntityModelFactory entityModelFactory)
        {
            Provider = provider;
            ElementType = typeof(T);
            Expression = new QueryRootExpression<T>(entityModelFactory);
        }

        /// <summary>
        /// 可查询对象
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="expression"></param>
        public EntityQueryable(EntityQueryProvider provider, Expression expression)
        {
            Provider = provider;
            ElementType = typeof(T);
            Expression = expression;
        }
    }
}
