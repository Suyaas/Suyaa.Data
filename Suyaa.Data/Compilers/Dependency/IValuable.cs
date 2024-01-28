using Suyaa.Data.Compilers.Sets;
using Suyaa.Data.Expressions;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Compilers.Dependency
{
    /// <summary>
    /// 可值化
    /// </summary>
    public interface IValuable<T>
        where T : Expression
    {
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        ValueSet GetValue(T expression, QueryRootModel model);
    }
}
