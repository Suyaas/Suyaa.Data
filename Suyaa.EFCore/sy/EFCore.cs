﻿using Suyaa.Data.DbWorks;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.DbWorks.Helpers;
using Suyaa.Data.Descriptors;
using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Descriptors.Providers;
using Suyaa.Data.Kernel;
using Suyaa.Data.Models;
using Suyaa.Data.Models.Dependency;
using Suyaa.Data.Models.Providers;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.EFCore.DbWorks;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Factories;
using Suyaa.EFCore.Providers;

namespace sy
{
    /// <summary>
    /// 数据助手
    /// </summary>
    public static class EfCore
    {
        // 数据库工厂
        private static IDbFactory? _dbFactory;
        // 数据库连接管理器
        //private static IDbConnectionDescriptorManager? _dbConnectionDescriptorManager;
        private static IDbConnectionDescriptorFactory? _dbConnectionDescriptorFactory;
        // 数据库连接管理器
        private static DbConnectionDescriptorProvider? _connectionDescriptorProvider;
        // 数据库作业供应商
        private static IDbWorkProvider? _dbWorkProvider;
        // 实体建模供应商
        private static IEntityModelFactory? _entityModelFactory;
        private static IEntityModelConventionFactory? _entityModelConventionFactory;
        private static List<IEntityModelConvention> _entityModelConventions = new List<IEntityModelConvention>();
        // 数据库上下文工厂
        private static IDbContextFactory? _dbContextFacotry;
        // 数据库上下文供应商
        private static DbContextProvider? _dbContextProvider;
        // 数据库实例供应商集合
        private static List<IEntityModelProvider> _entityModelProviders = new List<IEntityModelProvider>();


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
        public static void UseConnection(IDbConnectionDescriptor descriptor)
        {
            // 创建数据库连接描述供应商
            if (_connectionDescriptorProvider is null) _connectionDescriptorProvider = new DbConnectionDescriptorProvider();
            // 去重校验
            if (_connectionDescriptorProvider.GetDbConnections().Contains(descriptor)) return;
            // 添加数据库描述
            _connectionDescriptorProvider.AddDbConnection(descriptor);
            // 创建数据库连接描述工厂
            _dbConnectionDescriptorFactory = new DbConnectionDescriptorFactory(new List<DbConnectionDescriptorProvider>() { _connectionDescriptorProvider });
            //// 创建数据库连接描述管理器
            //_dbConnectionDescriptorManager = new DbConnectionDescriptorManager(dbConnectionDescriptorFactory);
            //// 设置当前连接
            //_dbConnectionDescriptorManager.SetCurrentConnection(descriptor);
        }

        /// <summary>
        /// 注册数据库实例供应商
        /// </summary>
        /// <param name="dbEntityProvider"></param>
        public static void UseEntityProvider(IEntityModelProvider dbEntityProvider)
        {
            _entityModelProviders.Add(dbEntityProvider);
            _dbFactory ??= new DbFactory();
            _entityModelFactory = new EntityModelFactory(_entityModelProviders);
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
        /// 注册数据库实例供应商
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        public static void UseEntityProvider<TProvider>()
            where TProvider : class, IEntityModelProvider, new()
        {
            UseEntityProvider(sy.Assembly.Create<TProvider>());
        }

        /// <summary>
        /// 注册数据库实例供应商
        /// </summary>
        public static void UserDbContext(IDefineDbContext dbContext)
        {
            UseConnection(dbContext.ConnectionDescriptor);
            _dbContextProvider ??= new DbContextProvider();
            if (_dbContextProvider.GetDbContexts().Contains(dbContext)) return;
            _dbContextProvider.AddDbContext(dbContext);
            _dbContextFacotry = new DbContextFacotry(new List<IDbContextProvider>() { _dbContextProvider });
        }

        /// <summary>
        /// 获取实体建模约定器工厂
        /// </summary>
        /// <returns></returns>
        public static IEntityModelConventionFactory GetEntityModelConventionFactory()
        {
            return _entityModelConventionFactory ??= new EntityModelConventionFactory(_entityModelConventions);
        }

        /// <summary>
        /// 获取实体建模工厂
        /// </summary>
        /// <returns></returns>
        public static IEntityModelFactory GetEntityModelFactory()
        {
            _dbFactory ??= new DbFactory();
            _dbContextProvider ??= new DbContextProvider();
            _dbContextFacotry ??= new DbContextFacotry(new List<IDbContextProvider>() { _dbContextProvider });
            if (_entityModelFactory is null)
            {
                List<IEntityModelProvider> entityModelProviders = new List<IEntityModelProvider>() {
                    new EntityModelProvider(_dbFactory,_entityModelConventions),
                    new DbEntityModelProvider(_dbFactory,_entityModelConventions),
                    new DbSetModelProvider(_dbFactory,_dbContextFacotry,_entityModelConventions),
                };
                _entityModelFactory = new EntityModelFactory(entityModelProviders);
            }
            return _entityModelFactory;
        }

        /// <summary>
        /// 创建工作者
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static IDbWork CreateWork(IDefineDbContext dbContext)
        {
            _dbFactory ??= new DbFactory();
            _entityModelConventionFactory ??= new EntityModelConventionFactory(_entityModelConventions);
            UserDbContext(dbContext);
            var dbWorkInterceptorFactory = GetDbWorkInterceptorFactory();
            //if (!_entityModelProviders.Any())
            //{
            //    UseEntityProvider(new DbSetModelProvider(_dbFactory, _dbContextFacotry!, new List<IEntityModelConvention>()));
            //}
            _dbWorkProvider ??= new EfCoreWorkProvider(_dbFactory, _dbContextFacotry!, _entityModelConventionFactory, dbWorkInterceptorFactory);
            var dbWorkManager = new EfCoreWorkManager(_dbConnectionDescriptorFactory!, _dbFactory, _dbContextFacotry!, _entityModelConventionFactory, dbWorkInterceptorFactory);
            return dbWorkManager.CreateWork();
        }

        ///// <summary>
        ///// 创建工作者
        ///// </summary>
        ///// <param name="dbType"></param>
        ///// <param name="connectionString"></param>
        ///// <returns></returns>
        //public static IDbWork CreateWork(DatabaseType dbType, string connectionString)
        //{
        //    return CreateWork(new DbConnectionDescriptor("default", dbType, connectionString));
        //}

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
            where TEntity : class, IDbEntity, new()
        {
            _entityModelFactory ??= new EntityModelFactory(_entityModelProviders);
            return work.GetRepository(
                new EfCoreInsertProvider<TEntity>(),
                new EfCoreDeleteProvider<TEntity>(),
                new EfCoreUpdateProvider<TEntity>(_entityModelFactory),
                new EfCoreQueryProvider<TEntity>()
            );
        }

        /// <summary>
        /// 创建实体仓库
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public static IRepository<TEntity, TId> CreateRepository<TEntity, TId>(IDbWork work)
            where TEntity : class, IDbEntity<TId>, new()
            where TId : notnull
        {
            _entityModelFactory ??= new EntityModelFactory(_entityModelProviders);
            return work.GetRepository<TEntity, TId>(
                new EfCoreInsertProvider<TEntity>(),
                new EfCoreDeleteProvider<TEntity>(),
                new EfCoreUpdateProvider<TEntity>(_entityModelFactory),
                new EfCoreQueryProvider<TEntity>()
            );
        }
    }
}
