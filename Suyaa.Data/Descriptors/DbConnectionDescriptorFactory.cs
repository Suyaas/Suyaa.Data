using Suyaa.Data.Descriptors.Dependency;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Suyaa.Data.Descriptors
{
    /// <summary>
    /// 主机数据库上下文配置工厂
    /// </summary>
    public class DbConnectionDescriptorFactory : IDbConnectionDescriptorFactory
    {

        #region DI注入

        //private readonly Dictionary<string, IDbConnectionDescriptor> _descriptors;
        private readonly IEnumerable<IDbConnectionDescriptorProvider> _providers;
        private const string KEY_DEFAULT = "default";

        /// <summary>
        /// 获取默认连接
        /// </summary>
        public IDbConnectionDescriptor GetDefaultConnection()
        {
            IDbConnectionDescriptor? first = null;
            foreach (var provider in _providers)
            {
                var descriptors = provider.GetDbConnections();
                foreach (var descriptor in descriptors)
                {
                    if (descriptor.Name == KEY_DEFAULT) return descriptor;
                    if (first is null) first = descriptor;
                }
            }
            if (first is null) throw new NotExistException<IDbConnectionDescriptor>();
            return first;
        }

        /// <summary>
        /// 主机数据库上下文配置工厂
        /// </summary>
        public DbConnectionDescriptorFactory(
            IEnumerable<IDbConnectionDescriptorProvider> providers
            )
        {
            _providers = providers;
        }
        #endregion

        /// <summary>
        /// 获取连接描述
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDbConnectionDescriptor GetConnection(string name)
        {
            foreach (var provider in _providers)
            {
                var descriptors = provider.GetDbConnections();
                foreach (var descriptor in descriptors)
                {
                    if (descriptor.Name == name) return descriptor;
                }
            }
            throw new DbException("DbConnection.NotExist", "DbConnection '{0}' not found.", name);
        }
    }
}
