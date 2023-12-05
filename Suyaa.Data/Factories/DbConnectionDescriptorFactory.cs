using Suyaa.Data.Dependency;
using System.Collections.Generic;
using System.Linq;

namespace Suyaa.Data.Factories
{
    /// <summary>
    /// 主机数据库上下文配置工厂
    /// </summary>
    public class DbConnectionDescriptorFactory : IDbConnectionDescriptorFactory
    {

        #region DI注入

        private readonly Dictionary<string, IDbConnectionDescriptor> _descriptors;
        private readonly IEnumerable<IDbConnectionDescriptorProvider> _providers;
        private const string KEY_DEFAULT = "default";

        /// <summary>
        /// 默认连接
        /// </summary>
        public IDbConnectionDescriptor DefaultConnection { get; }

        /// <summary>
        /// 主机数据库上下文配置工厂
        /// </summary>
        public DbConnectionDescriptorFactory(
            IEnumerable<IDbConnectionDescriptorProvider> providers
            )
        {
            _providers = providers;
            _descriptors = new Dictionary<string, IDbConnectionDescriptor>();
            foreach (var provider in providers)
            {
                var descriptors = provider.GetDbConnections();
                foreach (var descriptor in descriptors)
                {
                    if (_descriptors.ContainsKey(descriptor.Name)) continue;
                    _descriptors[descriptor.Name] = descriptor;
                }
            }
            // 设置默认连接
            if (!_descriptors.Any()) throw new NotExistException<IDbConnectionDescriptor>();
            if (_descriptors.ContainsKey(KEY_DEFAULT))
            {
                DefaultConnection = _descriptors[KEY_DEFAULT];
            }
            else
            {
                DefaultConnection = _descriptors.First().Value;
            }
        }
        #endregion

        /// <summary>
        /// 获取连接描述
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IDbConnectionDescriptor GetConnection(string name)
        {
            if (_descriptors.ContainsKey(name)) throw new DbException("Option.NotExist", "DbContextOptions '{0}' not found.", name);
            return _descriptors[name];
        }
    }
}
