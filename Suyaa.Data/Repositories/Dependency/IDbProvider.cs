using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Suyaa.Data.Dependency;

namespace Suyaa.Data.Repositories.Dependency
{
    /// <summary>
    /// 数据库供应商
    /// </summary>
    public interface IDbProvider
    {
        /// <summary>
        /// 表达式供应商
        /// </summary>
        IDbExpressionProvider ExpressionProvider { get; }
        /// <summary>
        /// 脚本供应商
        /// </summary>
        IDbScriptProvider ScriptProvider { get; }
        /// <summary>
        /// Sql脚本仓库供应商
        /// </summary>
        IDbExecuteProvider ExecuteProvider { get; }
        /// <summary>
        /// 获取一个数据库连接
        /// </summary>
        /// <returns></returns>
        DbConnection GetDbConnection(string connectionString);
    }
}
