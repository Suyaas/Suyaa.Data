using Suyaa.Data.Expressions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Compilers.Dependency
{
    /// <summary>
    /// From语句支持
    /// </summary>
    public interface IFromable<T>
         where T : Expression
    {
        /// <summary>
        /// 获取From语句
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        string GetFromStatement(T expression);
    }
}
