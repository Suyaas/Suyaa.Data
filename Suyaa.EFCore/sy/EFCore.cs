using Suyaa;
using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.Data.Factories;
using Suyaa.Data.Helpers;
using Suyaa.Data.Providers;
using Suyaa.EFCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace sy
{
    /// <summary>
    /// 数据助手
    /// </summary>
    public static class EFCore
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

        /// <summary>
        /// 注册数据库实例供应商
        /// </summary>
        /// <param name="dbEntityProvider"></param>
        public static void UseEntityProvider(IEntityModelProvider dbEntityProvider)
        {
            _entityModelProviders.Add(dbEntityProvider);
            _dbFactory ??= new DbFactory();
            _entityModelFactory = new EntityModelFactory(_dbFactory, _entityModelProviders);
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
        /// 创建工作者
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static IDbWork CreateWork(DbConnectionDescriptor descriptor)
        {
            // 使用连接
            UseConnection(descriptor);
            _dbFactory ??= new DbFactory();
            _dbWorkProvider ??= new EFCoreWorkProvider(_dbFactory);
            var dbWorkManagerProvider = new DbWorkManagerProvider(_dbFactory, _dbConnectionDescriptorManager!, _dbWorkProvider);
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
            var provider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            return work.GetSqlRepository(provider.SqlRepositoryProvider);
        }

        /// <summary>
        /// 创建实体仓库
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="work"></param>
        /// <returns></returns>
        public static IRepository<TEntity> CreateRepository<TEntity>(IDbWork work)
            where TEntity : IEntity, new()
        {
            var provider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            _dbFactory ??= new DbFactory();
            _entityModelFactory ??= new EntityModelFactory(_dbFactory, _entityModelProviders);
            var sqlRepository = CreateSqlRepository(work);
            return work.GetRepository(
                _entityModelFactory,
                new DbInsertProvider<TEntity>(_entityModelFactory, provider.ScriptProvider, sqlRepository),
                new DbDeleteProvider<TEntity>(_entityModelFactory, provider.ScriptProvider, sqlRepository),
                new DbUpdateProvider<TEntity>(_entityModelFactory, provider.ScriptProvider, sqlRepository),
                new DbQueryProvider<TEntity>(_entityModelFactory, provider.ScriptProvider, sqlRepository)
            );
        }

        /// <summary>
        /// 创建实体仓库
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public static IRepository<TEntity, TId> CreateRepository<TEntity, TId>(IDbWork work)
            where TEntity : IEntity<TId>, new()
            where TId : notnull
        {
            var provider = work.ConnectionDescriptor.DatabaseType.GetDbProvider();
            _dbFactory ??= new DbFactory();
            _entityModelFactory ??= new EntityModelFactory(_dbFactory, _entityModelProviders);
            var sqlRepository = CreateSqlRepository(work);
            return work.GetRepository<TEntity, TId>(
                _entityModelFactory,
                new DbInsertProvider<TEntity>(_entityModelFactory, provider.ScriptProvider, sqlRepository),
                new DbDeleteProvider<TEntity>(_entityModelFactory, provider.ScriptProvider, sqlRepository),
                new DbUpdateProvider<TEntity>(_entityModelFactory, provider.ScriptProvider, sqlRepository),
                new DbQueryProvider<TEntity>(_entityModelFactory, provider.ScriptProvider, sqlRepository)
            );
        }
    }
}
