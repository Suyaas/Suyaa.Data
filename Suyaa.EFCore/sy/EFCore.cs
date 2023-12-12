using Suyaa;
using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.Data.Factories;
using Suyaa.Data.Helpers;
using Suyaa.Data.Providers;
using Suyaa.EFCore.Dependency;
using Suyaa.EFCore.Factories;
using Suyaa.EFCore.Helpers;
using Suyaa.EFCore.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

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
        private static IDbConnectionDescriptorManager? _dbConnectionDescriptorManager;
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
            var dbConnectionDescriptorFactory = new DbConnectionDescriptorFactory(new List<DbConnectionDescriptorProvider>() { _connectionDescriptorProvider });
            // 创建数据库连接描述管理器
            _dbConnectionDescriptorManager = new DbConnectionDescriptorManager(dbConnectionDescriptorFactory);
            // 设置当前连接
            _dbConnectionDescriptorManager.SetCurrentConnection(descriptor);
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
        /// 创建工作者
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        public static IDbWork CreateWork(IDefineDbContext dbContext)
        {
            _dbFactory ??= new DbFactory();
            _entityModelConventionFactory ??= new EntityModelConventionFactory(_entityModelConventions);
            UserDbContext(dbContext);
            if (!_entityModelProviders.Any())
            {
                UseEntityProvider(new DbSetModelProvider(_dbFactory, _dbContextFacotry!, new List<IEntityModelConvention>()));
            }
            _dbWorkProvider ??= new EfCoreWorkProvider(_dbFactory, _dbContextFacotry!, _entityModelConventionFactory);
            var dbWorkManagerProvider = new EfCoreManagerProvider(_dbFactory, _dbContextFacotry!, _entityModelConventionFactory, _dbConnectionDescriptorManager!);
            return dbWorkManagerProvider.CreateManager().CreateWork();
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
