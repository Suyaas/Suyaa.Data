using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.Data.Helpers;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Suyaa.Data.Providers
{
    /// <summary>
    /// EFCore作业供应商
    /// </summary>
    public sealed class EfCoreWorkProvider : IDbWorkProvider
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDbContextFactory _dbContextFacotry;
        private IDbWork? _work;

        /// <summary>
        /// EFCore作业供应商
        /// </summary>
        public EfCoreWorkProvider(
            IDbFactory dbFactory,
            IDbContextFactory dbContextFacotry
            )
        {
            _dbFactory = dbFactory;
            _dbContextFacotry = dbContextFacotry;
        }

        /// <summary>
        /// 创建一个工作者
        /// </summary>
        /// <returns></returns>
        public IDbWork CreateWork(IDbWorkManager dbWorkManager)
        {
            return new EfCoreWork(_dbFactory, _dbContextFacotry, dbWorkManager);
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
