using Suyaa.Data.Kernel.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Descriptors.Dependency
{
    /// <summary>
    /// 数据库连接描述器
    /// </summary>
    public interface IDbConnectionDescriptor
    {
        /// <summary>
        /// 连接名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        DatabaseType DatabaseType { get; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        string ToConnectionString();
    }
}
