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
        private readonly IDbFactory _factory;
        private readonly DbConnectionDescriptor _descriptor;

        /// <summary>
        /// 简单的数据库工作者管理器
        /// </summary>
        public SimpleDbWorkManager(
            IDbFactory factory,
            DbConnectionDescriptor descriptor
            )
        {
            _factory = factory;
            _descriptor = descriptor;
        }

        /// <summary>
        /// 创建一个数据库工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork()
        {
            var work = _factory.WorkProvider.CreateWork(_descriptor);
            _factory.WorkProvider.SetCurrentWork(work);
            return work;
        }
    }
}
