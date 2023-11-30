using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库工作者
    /// </summary>
    public interface IDbWork : IDisposable
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        DbConnection Connection { get; }
        /// <summary>
        /// 获取Sql仓库
        /// </summary>
        /// <returns></returns>
        ISqlRepository GetSqlRepository();
        /// <summary>
        /// 数据库连接描述
        /// </summary>
        DbConnectionDescriptor ConnectionDescriptor { get; }
    }
}
