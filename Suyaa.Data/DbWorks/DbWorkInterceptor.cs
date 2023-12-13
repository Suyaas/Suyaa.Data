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
    public sealed class DbWorkInterceptor : IDbWorkInterceptor
    {
        private readonly Func<DbCommand?, DbCommand?>? _creating;
        private readonly Func<DbCommand, DbCommand>? _executing;

        /// <summary>
        /// 数据库作业拦截器
        /// </summary>
        /// <param name="creating"></param>
        /// <param name="executing"></param>
        public DbWorkInterceptor(Func<DbCommand?, DbCommand?>? creating, Func<DbCommand, DbCommand>? executing)
        {
            _creating = creating;
            _executing = executing;
        }

        /// <summary>
        /// 命令管理器创建
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public DbCommand? DbCommandCreating(DbCommand? command)
        {
            if (_creating is null) return command;
            return _creating(command);
        }

        /// <summary>
        /// 命令管理器执行
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public DbCommand DbCommandExecuting(DbCommand command)
        {
            if (_executing is null) return command;
            return _executing(command);
        }
    }
}
