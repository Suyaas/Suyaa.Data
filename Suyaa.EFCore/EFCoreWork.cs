using Microsoft.EntityFrameworkCore;
using Suyaa.Data.Dependency;
using System.Data.Common;
using System.Threading.Tasks;

namespace Suyaa.Data
{
    /// <summary>
    /// 简单的数据库工作着
    /// </summary>
    public sealed class EFCoreWork : Disposable, IDbWork
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDbWorkManager _dbWorkManager;
        private DbConnection? _connection;
        private DbTransaction? _transaction;

        /// <summary>
        /// 简单的数据库工作着
        /// </summary>
        public EFCoreWork(
            IDbFactory dbFactory,
            IDbWorkManager dbWorkManager
            )
        {
            ConnectionDescriptor = dbWorkManager.ConnectionDescriptor;
            _dbFactory = dbFactory;
            _dbWorkManager = dbWorkManager;
        }

        /// <summary>
        /// 连接信息
        /// </summary>
        public IDbConnectionDescriptor ConnectionDescriptor { get; }

        /// <summary>
        /// 数据库连接
        /// </summary>
        public DbConnection Connection => _connection ??= _dbFactory.GetDbConnection(ConnectionDescriptor);

        /// <summary>
        /// 事务
        /// </summary>
        public DbTransaction Transaction => _transaction ??= Connection.BeginTransaction();

        /// <summary>
        /// 工作者管理器
        /// </summary>
        public IDbWorkManager WorkManager => _dbWorkManager;

        /// <summary>
        /// 生效事务
        /// </summary>
        public void Commit()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// 生效事务
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void OnManagedDispose()
        {
            base.OnManagedDispose();
            // 释放工作者
            _dbWorkManager.ReleaseWork();
            // 断开连接
            if (!(_connection is null))
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
