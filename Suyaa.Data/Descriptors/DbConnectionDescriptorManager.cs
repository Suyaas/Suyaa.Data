using Suyaa.Data.Descriptors.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Descriptors
{
    /// <summary>
    /// 数据库连接描述管理器
    /// </summary>
    public sealed class DbConnectionDescriptorManager : IDbConnectionDescriptorManager
    {
        private readonly IDbConnectionDescriptorFactory _dbConnectionDescriptorFactory;
        private IDbConnectionDescriptor _dbConnectionDescriptor;

        /// <summary>
        /// 数据库连接描述管理器
        /// </summary>
        public DbConnectionDescriptorManager(
            IDbConnectionDescriptorFactory dbConnectionDescriptorFactory
            )
        {
            _dbConnectionDescriptorFactory = dbConnectionDescriptorFactory;
            _dbConnectionDescriptor = _dbConnectionDescriptorFactory.DefaultConnection;
        }

        /// <summary>
        /// 获取当前数据库连接描述
        /// </summary>
        /// <returns></returns>
        public IDbConnectionDescriptor GetCurrentConnection()
        {
            return _dbConnectionDescriptor;
        }

        /// <summary>
        /// 设置当前数据库连接描述
        /// </summary>
        /// <param name="dbConnectionDescriptor"></param>
        public void SetCurrentConnection(IDbConnectionDescriptor dbConnectionDescriptor)
        {
            _dbConnectionDescriptor = dbConnectionDescriptor;
        }
    }
}
