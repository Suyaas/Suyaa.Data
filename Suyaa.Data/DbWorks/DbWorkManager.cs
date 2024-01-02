using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.DbWorks
{
    /// <summary>
    /// 数据库作业管理器
    /// </summary>
    public sealed class DbWorkManager : IDbWorkManager
    {
        private IDbWork? _work;
        private static readonly object _lock = new object();
        private readonly IDbConnectionDescriptorFactory _dbConnectionDescriptorFactory;
        private readonly IDbFactory _dbFactory;
        //private readonly IDbConnectionDescriptorManager _dbConnectionDescriptorManager;
        private readonly DbWorkProvider _dbWorkProvider;

        /// <summary>
        /// 数据库作业管理器
        /// </summary>
        public DbWorkManager(
            IDbConnectionDescriptorFactory dbConnectionDescriptorFactory,
            //IDbConnectionDescriptorManager dbConnectionDescriptorManager,
            IDbFactory dbFactory,
            IDbWorkInterceptorFactory dbWorkInterceptorFactory
            )
        {
            _dbConnectionDescriptorFactory = dbConnectionDescriptorFactory;
            _dbFactory = dbFactory;
            // _dbConnectionDescriptorManager = dbConnectionDescriptorManager;
            _dbWorkProvider = new DbWorkProvider(_dbFactory, dbWorkInterceptorFactory);
            // ConnectionDescriptor = _dbConnectionDescriptorManager.GetCurrentConnection();
            CurrentConnectionDescriptor = _dbConnectionDescriptorFactory.GetDefaultConnection();
        }

        /// <summary>
        /// 连接描述
        /// </summary>
        public IDbConnectionDescriptor CurrentConnectionDescriptor { get; private set; }

        /// <summary>
        /// 设置当前数据库描述
        /// </summary>
        /// <param name="name"></param>
        public void SetCurrentConnectionDescriptor(string name)
        {
            this.CurrentConnectionDescriptor = _dbConnectionDescriptorFactory.GetConnection(name);
        }

        /// <summary>
        /// 创建一个数据库工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork()
        {
            var work = _dbWorkProvider.CreateWork(this);
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
    }
}
