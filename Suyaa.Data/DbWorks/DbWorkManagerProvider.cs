using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Repositories.Dependency;

namespace Suyaa.Data.DbWorks
{
    /// <summary>
    /// 简单的数据库供应商
    /// </summary>
    public sealed class DbWorkManagerProvider : IDbWorkManagerProvider
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDbWorkInterceptorFactory _dbWorkInterceptorFactory;
        private readonly IDbConnectionDescriptorManager _dbConnectionDescriptorManager;

        /// <summary>
        /// 简单的数据库工作者供应商
        /// </summary>
        public DbWorkManagerProvider(
            IDbConnectionDescriptorManager dbConnectionDescriptorManager,
            IDbFactory dbFactory,
            IDbWorkInterceptorFactory dbWorkInterceptorFactory
            )
        {
            _dbFactory = dbFactory;
            _dbWorkInterceptorFactory = dbWorkInterceptorFactory;
            _dbConnectionDescriptorManager = dbConnectionDescriptorManager;
        }

        /// <summary>
        /// 创建一个工作者管理器
        /// </summary>
        /// <returns></returns>
        public IDbWorkManager CreateManager()
        {
            return new DbWorkManager(_dbConnectionDescriptorManager, _dbFactory, _dbWorkInterceptorFactory);
        }
    }
}
