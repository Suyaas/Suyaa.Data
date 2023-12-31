﻿using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.EFCore.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.EFCore.DbWorks
{
    /// <summary>
    /// 简单的数据库工作者管理器
    /// </summary>
    public sealed class EfCoreWorkManager : IDbWorkManager
    {
        private IDbWork? _work;
        private static readonly object _lock = new object();
        private readonly IDbFactory _factory;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IEntityModelConventionFactory _entityModelConventionFactory;
        private readonly IDbWorkInterceptorFactory _dbWorkInterceptorFactory;
        private readonly IDbConnectionDescriptorFactory _dbConnectionDescriptorFactory;
        private readonly EfCoreWorkProvider _efCoreWorkProvider;

        /// <summary>
        /// 简单的数据库工作者管理器
        /// </summary>
        public EfCoreWorkManager(
            IDbConnectionDescriptorFactory dbConnectionDescriptorFactory,
            IDbFactory factory,
            IDbContextFactory dbContextFactory,
            IEntityModelConventionFactory entityModelConventionFactory,
            IDbWorkInterceptorFactory dbWorkInterceptorFactory
            )
        {
            _factory = factory;
            _dbContextFactory = dbContextFactory;
            _entityModelConventionFactory = entityModelConventionFactory;
            _dbWorkInterceptorFactory = dbWorkInterceptorFactory;
            _dbConnectionDescriptorFactory = dbConnectionDescriptorFactory;
            _efCoreWorkProvider = new EfCoreWorkProvider(_factory, _dbContextFactory, _entityModelConventionFactory, _dbWorkInterceptorFactory);
            CurrentConnectionDescriptor = _dbConnectionDescriptorFactory.GetDefaultConnection();
        }

        /// <summary>
        /// 连接描述
        /// </summary>
        public IDbConnectionDescriptor CurrentConnectionDescriptor { get; private set; }

        /// <summary>
        /// 创建一个数据库工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork()
        {
            var work = _efCoreWorkProvider.CreateWork(this);
            SetCurrentWork(work);
            return work;
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
        /// 释放工作者
        /// </summary>
        public void ReleaseWork()
        {
            if (_work is null) return;
            _work.Dispose();
            _work = null;
        }

        /// <summary>
        /// 设置当前工作者
        /// </summary>
        /// <param name="work"></param>
        public void SetCurrentWork(IDbWork work)
        {
            lock (_lock)
            {
                _work = work;
            }
        }

        /// <summary>
        /// 设置当前连接描述
        /// </summary>
        /// <param name="name"></param>
        public void SetCurrentConnectionDescriptor(string name)
        {
            this.CurrentConnectionDescriptor = _dbConnectionDescriptorFactory.GetConnection(name);
        }
    }
}
