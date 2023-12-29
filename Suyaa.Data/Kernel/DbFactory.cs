using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Helpers;
using Suyaa.Data.Repositories.Dependency;
using System.Data.Common;

namespace Suyaa.Data.Kernel
{
    /// <summary>
    /// 简单的数据库工厂
    /// </summary>
    public class DbFactory : IDbFactory
    {
        private static object _lock = new object();
        private long _indexer;

        /// <summary>
        /// 简单的数据库工厂
        /// </summary>
        public DbFactory(
            )
        {
            _indexer = 0;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="dbConnectionDescriptor"></param>
        /// <returns></returns>
        public DbConnection GetDbConnection(IDbConnectionDescriptor dbConnectionDescriptor)
        {
            var provider = dbConnectionDescriptor.DatabaseType.GetDbProvider();
            return provider.GetDbConnection(dbConnectionDescriptor.ToConnectionString());
        }

        /// <summary>
        /// 获取新的参数索引号
        /// </summary>
        /// <returns></returns>
        public long GetParamterIndex()
        {
            long index = 0;
            lock (_lock)
            {
                index = ++_indexer;
            }
            return index;
        }
    }
}
