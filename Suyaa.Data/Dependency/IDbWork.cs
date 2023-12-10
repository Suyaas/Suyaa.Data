using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Suyaa.Data.Dependency
{
    /// <summary>
    /// 数据库作业
    /// </summary>
    public interface IDbWork : IDisposable
    {
        /// <summary>
        /// 工作者管理器
        /// </summary>
        IDbWorkManager WorkManager { get; }
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
        /// 数据库连接描述
        /// </summary>
        IDbConnectionDescriptor ConnectionDescriptor { get; }
        /// <summary>
        /// 生效事务
        /// </summary>
        void Commit();
        /// <summary>
        /// 生效事务
        /// </summary>
        Task CommitAsync();
    }
}
