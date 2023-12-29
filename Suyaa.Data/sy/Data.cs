using Suyaa.Data;
using Suyaa.Data.DbWorks;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.DbWorks.Helpers;
using Suyaa.Data.Descriptors;
using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Descriptors.Providers;
using Suyaa.Data.Kernel;
using Suyaa.Data.Kernel.Enums;
using Suyaa.Data.Kernel.Providers;
using Suyaa.Data.Models;
using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Models.Providers;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sy
{
    /// <summary>
    /// 数据助手
    /// </summary>
    public static class Data
    {
        // 数据库工厂
        private static IDbFactory? _dbFactory;
        // 数据库连接管理器
        private static IDbConnectionDescriptorManager? _dbConnectionDescriptorManager;
        // 数据库连接管理器
        private static DbConnectionDescriptorProvider? _connectionDescriptorProvider;
        // 数据库作业供应商
        private static IDbWorkProvider? _dbWorkProvider;
        // 实体建模供应商
        private static IEntityModelFactory? _entityModelFactory;
        private static IEntityModelConventionFactory? _entityModelConventionFactory;
        private static List<IEntityModelConvention> _entityModelConventions = new List<IEntityModelConvention>();

        #region 拦截器相关

        // 数据库作业拦截器工厂
        private static IDbWorkInterceptorFactory? _dbWorkInterceptorFactory;
        private static List<IDbWorkInterceptor> _dbWorkInterceptors = new List<IDbWorkInterceptor>();

        /// <summary>
        /// 使用拦截器
        /// </summary>
        /// <param name="dbWorkInterceptor"></param>
        public static void UseWorkInterceptor(IDbWorkInterceptor dbWorkInterceptor)
        {
            _dbWorkInterceptors.Add(dbWorkInterceptor);
            _dbWorkInterceptorFactory = new DbWorkInterceptorFactory(_dbWorkInterceptors);
        }

        /// <summary>
        /// 获取拦截器工厂
        /// </summary>
        /// <returns></returns>
        public static IDbWorkInterceptorFactory GetDbWorkInterceptorFactory()
        {
            return _dbWorkInterceptorFactory ??= new DbWorkInterceptorFactory();
        }

        #endregion

        /// <summary>
        /// 注册数据库工厂
        /// </summary>
        /// <param name="dbFactory"></param>
        public static void UseFactory(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// 使用连接
        /// </summary>
        /// <param name="descriptor"></param>
        public static void UseConnection(DbConnectionDescriptor descriptor)
        {
            // 创建数据库连接描述供应商
            if (_connectionDescriptorProvider is null) _connectionDescriptorProvider = new DbConnectionDescriptorProvider();
            // 去重校验
            if (_connectionDescriptorProvider.GetDbConnections().Contains(descriptor)) return;
            // 添加数据库描述
            _connectionDescriptorProvider.AddDbConnection(descriptor);
            // 创建数据库连接描述工厂
            var dbConnectionDescriptorFactory = new DbConnectionDescriptorFactory(new List<DbConnectionDescriptorProvider>() { _connectionDescriptorProvider });
            // 创建数据库连接描述管理器
            _dbConnectionDescriptorManager = new DbConnectionDescriptorManager(dbConnectionDescriptorFactory);
            // 设置当前连接
            _dbConnectionDescriptorManager.SetCurrentConnection(descriptor);
        }

        // 获取实体建模工厂
        private static IEntityModelFactory GetEntityModelFactory()
        {
            _dbFactory ??= new DbFactory();
            if (_entityModelFactory is null)
            {
                List<IEntityModelProvider> entityModelProviders = new List<IEntityModelProvider>() {
                    new EntityModelProvider(_dbFactory,_entityModelConventions),
                    new DbEntityModelProvider(_dbFactory,_entityModelConventions),
                };
                _entityModelFactory = new EntityModelFactory(entityModelProviders);
            }
            return _entityModelFactory;
        }

        /// <summary>
        /// 注册数据库实例建模约定器
        /// </summary>
        /// <param name="entityModelConvention"></param>
        public static void UseModelConvention(IEntityModelConvention entityModelConvention)
        {
            _entityModelConventions.Add(entityModelConvention);
            _entityModelConventionFactory = new EntityModelConventionFactory(_entityModelConventions);
        }

        /// <summary>
        /// 创建工作者
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static IDbWork CreateWork(DbConnectionDescriptor descriptor)
        {
            // 使用连接
            UseConnection(descriptor);
            _dbFactory ??= new DbFactory();
            var dbWorkInterceptorFactory = GetDbWorkInterceptorFactory();
            _dbWorkProvider ??= new DbWorkProvider(_dbFactory, dbWorkInterceptorFactory);
            var dbWorkManagerProvider = new DbWorkManagerProvider(_dbConnectionDescriptorManager!, _dbFactory, dbWorkInterceptorFactory);
            return dbWorkManagerProvider.CreateManager().CreateWork();
        }

        /// <summary>
        /// 创建工作者
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IDbWork CreateWork(DatabaseType dbType, string connectionString)
        {
            return CreateWork(new DbConnectionDescriptor("default", dbType, connectionString));
        }

        /// <summary>
        /// 创建Sql仓库
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public static ISqlRepository CreateSqlRepository(IDbWork work)
        {
            return work.GetSqlRepository();
        }

        /// <summary>
        /// 创建实体仓库
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="work"></param>
        /// <returns></returns>
        public static IRepository<TEntity> CreateRepository<TEntity>(IDbWork work)
            where TEntity : IDbEntity, new()
        {
            _dbFactory ??= new DbFactory();
            var entityModelFactory = GetEntityModelFactory();
            var sqlRepository = CreateSqlRepository(work);
            return work.GetRepository(
                new DbInsertProvider<TEntity>(entityModelFactory, sqlRepository),
                new DbDeleteProvider<TEntity>(entityModelFactory, sqlRepository),
                new DbUpdateProvider<TEntity>(entityModelFactory, sqlRepository),
                new DbQueryProvider<TEntity>(entityModelFactory, sqlRepository)
            );
        }

        /// <summary>
        /// 创建实体仓库
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public static IRepository<TEntity, TId> CreateRepository<TEntity, TId>(IDbWork work)
            where TEntity : IDbEntity<TId>, new()
            where TId : notnull
        {
            _dbFactory ??= new DbFactory();
            var entityModelFactory = GetEntityModelFactory();
            var sqlRepository = CreateSqlRepository(work);
            return work.GetRepository<TEntity, TId>(
                new DbInsertProvider<TEntity>(entityModelFactory, sqlRepository),
                new DbDeleteProvider<TEntity>(entityModelFactory, sqlRepository),
                new DbUpdateProvider<TEntity>(entityModelFactory, sqlRepository),
                new DbQueryProvider<TEntity>(entityModelFactory, sqlRepository)
            );
        }
    }
}
