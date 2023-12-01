using Suyaa.Data.Descriptors;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

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
        /// 获取事务
        /// </summary>
        DbTransaction Transaction { get; }
        /// <summary>
        /// 获取Sql仓库
        /// </summary>
        /// <returns></returns>
        ISqlRepository GetSqlRepository();
        /// <summary>
        /// 数据库连接描述
        /// </summary>
        DbConnectionDescriptor ConnectionDescriptor { get; }
        /// <summary>
        /// 生效事务
        /// </summary>
        void Commit();
        /// <summary>
        /// 生效事务
        /// </summary>
        Task CommitAsync();
        /// <summary>
        /// 获取实例描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        EntityDescriptor GetEntityDescriptor(Type type);
    }
}
