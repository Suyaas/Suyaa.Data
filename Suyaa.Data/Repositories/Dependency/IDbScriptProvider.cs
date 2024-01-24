using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Suyaa.Data.Repositories.Dependency
{
    /// <summary>
    /// 数据库脚本供应商
    /// </summary>
    public interface IDbScriptProvider
    {
        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetName(string name);
        /// <summary>
        /// 获取变量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetVariable(string name);
        /// <summary>
        /// 获取语句
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        string GetStatement(Expression expression);
    }
}
