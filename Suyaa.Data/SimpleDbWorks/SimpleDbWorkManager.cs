using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.SimpleDbWorks
{
    /// <summary>
    /// 简单的数据库工作者管理器
    /// </summary>
    public sealed class SimpleDbWorkManager : IDbWorkManager
    {
        private readonly DbConnectionDescriptor _descriptor;

        /// <summary>
        /// 简单的数据库工作者管理器
        /// </summary>
        public SimpleDbWorkManager(
            DbConnectionDescriptor descriptor
            )
        {
            _descriptor = descriptor;
        }

        /// <summary>
        /// 创建一个数据库工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork()
        {
            var dbWorkProvider = new SimpleDbWorkProvider();
            var work = dbWorkProvider.CreateWork(_descriptor);
            dbWorkProvider.SetCurrentWork(work);
            return work;
        }
    }
}
