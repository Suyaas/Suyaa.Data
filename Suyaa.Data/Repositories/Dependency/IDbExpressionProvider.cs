using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Repositories.Dependency
{
    /// <summary>
    /// 数据库表达式供应商
    /// </summary>
    public interface IDbExpressionProvider
    {
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        TResult Query<TResult>(Expression expression);
    }
}
