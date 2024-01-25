using Suyaa.Data.Compilers.Sets;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Compilers.Dependency
{
    /// <summary>
    /// 可获取列
    /// </summary>
    public interface IColumnable<T>
        where T : Expression
    {
        /// <summary>
        /// 获取语句
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        SelectColumnSet GetColumn(T expression, DbEntityModel model);
    }
}
