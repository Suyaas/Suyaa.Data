using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Suyaa.Data.DbWorks.Dependency
{
    /// <summary>
    /// 数据库作业拦截器
    /// </summary>
    public interface IDbWorkInterceptor
    {
        /// <summary>
        /// 创建数据库命令
        /// </summary>
        /// <returns></returns>
        DbCommand? DbCommandCreating(DbCommand? command);
        /// <summary>
        /// 创建数据库命令
        /// </summary>
        /// <returns></returns>
        DbCommand DbCommandExecuting(DbCommand command);
    }
}
