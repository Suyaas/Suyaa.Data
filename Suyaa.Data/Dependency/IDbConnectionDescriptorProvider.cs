using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库连接描述供应商
    /// </summary>
    public interface IDbConnectionDescriptorProvider
    {
        /// <summary>
        /// 获取数据库连接描述集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<IDbConnectionDescriptor> GetDbConnections();
    }
}
