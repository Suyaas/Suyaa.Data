using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Descriptors.Dependency
{
    /// <summary>
    /// 数据库连接描述管理器
    /// </summary>
    public interface IDbConnectionDescriptorManager
    {
        /// <summary>
        /// 获取当前连接
        /// </summary>
        /// <returns></returns>
        IDbConnectionDescriptor GetCurrentConnection();

        /// <summary>
        /// 设置当前连接
        /// </summary>
        /// <returns></returns>
        void SetCurrentConnection(IDbConnectionDescriptor dbConnectionDescriptor);
    }
}
