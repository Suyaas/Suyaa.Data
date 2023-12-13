using Suyaa.Data.DbWorks.Dependency;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Suyaa.Data.DbWorks
{
    /// <summary>
    /// 数据库作业拦截器
    /// </summary>
    public class DbWorkInterceptor : IDbWorkInterceptor
    {
        /// <summary>
        /// 数据库作业拦截器
        /// </summary>
        public DbWorkInterceptor()
        {
        }

        /// <summary>
        /// 命令管理器创建
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual DbCommand? DbCommandCreating(DbCommand? command)
        {
            return command;
        }

        /// <summary>
        /// 命令管理器执行
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual DbCommand DbCommandExecuting(DbCommand command)
        {
            return command;
        }
    }
}
