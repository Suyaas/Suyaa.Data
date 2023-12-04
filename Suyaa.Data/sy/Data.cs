using Suyaa;
using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using Suyaa.Data.SimpleDbWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace sy
{
    /// <summary>
    /// 数据助手
    /// </summary>
    public static class Data
    {
        // 数据库工厂
        private static IDbFactory? _dbFactory;
        // 数据库实例供应商集合
        private static List<IDbEntityProvider> _dbEntityProviders = new List<IDbEntityProvider>();

        /// <summary>
        /// 注册数据库工厂
        /// </summary>
        /// <param name="dbFactory"></param>
        public static void UseFactory(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// 注册数据库实例供应商
        /// </summary>
        /// <param name="dbEntityProvider"></param>
        public static void UseEntityProvider(IDbEntityProvider dbEntityProvider)
        {
            _dbEntityProviders.Add(dbEntityProvider);
        }

        /// <summary>
        /// 注册数据库实例供应商
        /// </summary>
        /// <typeparam name="TProvider"></typeparam>
        public static void UseEntityProvider<TProvider>()
            where TProvider : class, IDbEntityProvider, new()
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
            _dbFactory ??= new SimpleDbFactory(descriptor, _dbEntityProviders);
            return _dbFactory.WorkManagerProvider.CreateManager(descriptor).CreateWork();
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
    }
}
