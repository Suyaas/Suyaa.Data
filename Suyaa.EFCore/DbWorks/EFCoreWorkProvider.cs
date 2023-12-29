using Suyaa.Data.DbWorks;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Dependency;
using Suyaa.Data.Helpers;
using Suyaa.Data.Kernel.Enums;
using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Suyaa.EFCore.DbWorks
{
    /// <summary>
    /// EFCore作业供应商
    /// </summary>
    public sealed class EfCoreWorkProvider : IDbWorkProvider
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDbContextFactory _dbContextFacotry;
        private readonly IEntityModelConventionFactory _entityModelConventionFactory;
        private readonly IDbWorkInterceptorFactory _dbWorkInterceptorFactory;
        private IDbWork? _work;

        /// <summary>
        /// EFCore作业供应商
        /// </summary>
        public EfCoreWorkProvider(
            IDbFactory dbFactory,
            IDbContextFactory dbContextFacotry,
            IEntityModelConventionFactory entityModelConventionFactory,
            IDbWorkInterceptorFactory dbWorkInterceptorFactory
            )
        {
            _dbFactory = dbFactory;
            _dbContextFacotry = dbContextFacotry;
            _entityModelConventionFactory = entityModelConventionFactory;
            _dbWorkInterceptorFactory = dbWorkInterceptorFactory;
        }

        /// <summary>
        /// 创建EfCore作业
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork(IDbWorkManager dbWorkManager)
        {
            // 创建 标准作业
            DbWork work = new DbWork(dbWorkManager, _dbFactory, _dbWorkInterceptorFactory);
            // 创建并返回 EfCore 作业
            return new EfCoreWork(_dbContextFacotry, _entityModelConventionFactory, work);
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
        public IEfCoreProvider GetDbProvider(DatabaseType databaseType)
        {
            return databaseType.GetEfCoreProvider();
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
