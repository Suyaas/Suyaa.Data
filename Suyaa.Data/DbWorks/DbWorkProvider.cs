﻿using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Helpers;
using Suyaa.Data.Kernel.Dependency;
using Suyaa.Data.Kernel.Enums;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Suyaa.Data.DbWorks
{
    /// <summary>
    /// 简单的数据库供应商
    /// </summary>
    public sealed class DbWorkProvider : IDbWorkProvider
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDbWorkInterceptorFactory _dbWorkInterceptorFactory;
        private IDbWork? _work;

        /// <summary>
        /// 简单的数据库工作者供应商
        /// </summary>
        public DbWorkProvider(
            IDbFactory dbFactory,
            IDbWorkInterceptorFactory dbWorkInterceptorFactory
            )
        {
            _dbFactory = dbFactory;
            _dbWorkInterceptorFactory = dbWorkInterceptorFactory;
        }

        /// <summary>
        /// 创建一个工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork(IDbWorkManager dbWorkManager)
        {
            return new DbWork(dbWorkManager, _dbFactory, _dbWorkInterceptorFactory);
        }

        /// <summary>
        /// 获取当前工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork? GetCurrentWork()
        {
            return _work;
        }

        /// <summary>
        /// 获取数据库供应商
        /// </summary>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public IDbProvider GetDbProvider(DatabaseType databaseType)
        {
            return databaseType.GetDbProvider();
        }

        /// <summary>
        /// 释放工作者
        /// </summary>
        public void ReleaseWork()
        {
            if (_work is null) return;
            _work.Dispose();
            _work = null;
        }

        /// <summary>
        /// 设置工作者
        /// </summary>
        /// <param name="work"></param>
        public void SetCurrentWork(IDbWork work)
        {
            _work = work;
        }
    }
}
