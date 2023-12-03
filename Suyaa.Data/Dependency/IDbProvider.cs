using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库供应商
    /// </summary>
    public interface IDbProvider
    {
        /// <summary>
        /// 查询供应商
        /// </summary>
        IDbQueryProvider QueryProvider { get; }
        /// <summary>
        /// 脚本供应商
        /// </summary>
        IDbScriptProvider ScriptProvider { get; }
        /// <summary>
        /// 获取一个数据库连接
        /// </summary>
        /// <returns></returns>
        DbConnection GetDbConnection();
        /// <summary>
        /// 获取Sql仓库
        /// </summary>
        /// <returns></returns>
        ISqlRepository GetSqlRepository();
    }
}
