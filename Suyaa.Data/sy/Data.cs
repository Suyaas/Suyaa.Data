using Suyaa.Data;
using Suyaa.Data.Dependency;
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
        // 简单的数据库管理器
        private static SimpleDbWorkManager? _simpleDbWorkManager;

        /// <summary>
        /// 创建工作者
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static IDbWork CreateWork(DbConnectionDescriptor descriptor)
        {
            _simpleDbWorkManager ??= new SimpleDbWorkManager(descriptor);
            return _simpleDbWorkManager.CreateWork();
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
