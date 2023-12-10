﻿using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Suyaa.Data.Providers
{
    /// <summary>
    /// 简单的数据库供应商
    /// </summary>
    public sealed class DbWorkManagerProvider : IDbWorkManagerProvider
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDbConnectionDescriptorManager _dbConnectionDescriptorManager;

        /// <summary>
        /// 简单的数据库工作者供应商
        /// </summary>
        public DbWorkManagerProvider(
            IDbFactory dbFactory,
            IDbConnectionDescriptorManager dbConnectionDescriptorManager
            )
        {
            _dbFactory = dbFactory;
            _dbConnectionDescriptorManager = dbConnectionDescriptorManager;
        }

        /// <summary>
        /// 创建一个工作者管理器
        /// </summary>
        /// <returns></returns>
        public IDbWorkManager CreateManager()
        {
            return new DbWorkManager(_dbFactory, _dbConnectionDescriptorManager);
        }
    }
}