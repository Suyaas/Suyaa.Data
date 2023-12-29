using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.EFCore.DbWorks;
using Suyaa.EFCore.Dependency;

namespace Suyaa.EFCore.Providers
{
    /// <summary>
    /// 简单的数据库供应商
    /// </summary>
    public sealed class EfCoreManagerProvider : IDbWorkManagerProvider
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IEntityModelConventionFactory _entityModelConventionFactory;
        private readonly IDbWorkInterceptorFactory _dbWorkInterceptorFactory;
        private readonly IDbConnectionDescriptorManager _dbConnectionDescriptorManager;

        /// <summary>
        /// 简单的数据库工作者供应商
        /// </summary>
        public EfCoreManagerProvider(
            IDbConnectionDescriptorManager dbConnectionDescriptorManager,
            IDbFactory dbFactory,
            IDbContextFactory dbContextFactory,
            IEntityModelConventionFactory entityModelConventionFactory,
            IDbWorkInterceptorFactory dbWorkInterceptorFactory
            )
        {
            _dbFactory = dbFactory;
            _dbContextFactory = dbContextFactory;
            _entityModelConventionFactory = entityModelConventionFactory;
            _dbWorkInterceptorFactory = dbWorkInterceptorFactory;
            _dbConnectionDescriptorManager = dbConnectionDescriptorManager;
        }

        /// <summary>
        /// 创建一个工作者管理器
        /// </summary>
        /// <returns></returns>
        public IDbWorkManager CreateManager()
        {
            return new EfCoreWorkManager(_dbConnectionDescriptorManager, _dbFactory, _dbContextFactory, _entityModelConventionFactory, _dbWorkInterceptorFactory);
        }
    }
}
