using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.Data.Helpers;
using Suyaa.Data.Providers;
using Suyaa.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Suyaa.EFCore.Providers
{
    /// <summary>
    /// 简单的数据库供应商
    /// </summary>
    public sealed class EfCoreManagerProvider : IDbWorkManagerProvider
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IDbConnectionDescriptorManager _dbConnectionDescriptorManager;

        /// <summary>
        /// 简单的数据库工作者供应商
        /// </summary>
        public EfCoreManagerProvider(
            IDbFactory dbFactory,
            IDbContextFactory dbContextFactory,
            IDbConnectionDescriptorManager dbConnectionDescriptorManager
            )
        {
            _dbFactory = dbFactory;
            _dbContextFactory = dbContextFactory;
            _dbConnectionDescriptorManager = dbConnectionDescriptorManager;
        }

        /// <summary>
        /// 创建一个工作者管理器
        /// </summary>
        /// <returns></returns>
        public IDbWorkManager CreateManager()
        {
            return new EfCoreWorkManager(_dbFactory, _dbContextFactory, _dbConnectionDescriptorManager);
        }
    }
}
