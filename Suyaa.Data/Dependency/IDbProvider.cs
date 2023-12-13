using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Suyaa.Data.Repositories.Dependency;

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
        /// Sql脚本仓库供应商
        /// </summary>
        ISqlRepositoryProvider SqlRepositoryProvider { get; }
        /// <summary>
        /// 获取一个数据库连接
        /// </summary>
        /// <returns></returns>
        DbConnection GetDbConnection(string connectionString);
    }
}
