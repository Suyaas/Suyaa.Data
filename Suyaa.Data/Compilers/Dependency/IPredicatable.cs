using Suyaa.Data.Expressions;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Compilers.Dependency
{
    /// <summary>
    /// 可查询
    /// </summary>
    public interface IPredicatable<T>
        where T : Expression
    {
        /// <summary>
        /// 获取查询语句
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        string GetPredicate(T expression, QueryRootModel model);
    }
}
