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
        /// 获取脚本
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        string GetScript<TResult>(Expression expression);
    }
}
