using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Suyaa.Data.SimpleDbWorks
{
    /// <summary>
    /// 简单的数据库供应商
    /// </summary>
    public sealed class SimpleDbWorkManagerProvider : IDbWorkManagerProvider
    {
        private readonly IDbFactory _dbFactory;

        /// <summary>
        /// 简单的数据库工作者供应商
        /// </summary>
        public SimpleDbWorkManagerProvider(
            IDbFactory dbFactory
            )
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// 创建一个工作者管理器
        /// </summary>
        /// <param name="dbConnectionDescriptor"></param>
        /// <returns></returns>
        public IDbWorkManager CreateManager(DbConnectionDescriptor dbConnectionDescriptor)
        {
            return new SimpleDbWorkManager(_dbFactory, dbConnectionDescriptor);
        }
    }
}
