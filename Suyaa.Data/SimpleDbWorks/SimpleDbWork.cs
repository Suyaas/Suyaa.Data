using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Suyaa.Data.SimpleDbWorks
{
    /// <summary>
    /// 简单的数据库工作着
    /// </summary>
    public sealed class SimpleDbWork : Disposable, IDbWork
    {
        private readonly IDbWorkProvider _dbWorkProvider;
        private readonly IDbProvider _dbProvider;
        private DbConnection? _connection;
        private DbTransaction? _transaction;

        /// <summary>
        /// 简单的数据库工作着
        /// </summary>
        public SimpleDbWork(
            IDbWorkProvider dbWorkProvider,
            DbConnectionDescriptor dbConnectionDescriptor
            )
        {
            _dbWorkProvider = dbWorkProvider;
            ConnectionDescriptor = dbConnectionDescriptor;
            _dbProvider = _dbWorkProvider.GetDbProvider(ConnectionDescriptor.DatabaseType);
        }

        /// <summary>
        /// 连接信息
        /// </summary>
        public DbConnectionDescriptor ConnectionDescriptor { get; }

        /// <summary>
        /// 数据库连接
        /// </summary>
        public DbConnection Connection => _connection ??= _dbProvider.GetDbConnection();

        /// <summary>
        /// 事务
        /// </summary>
        public DbTransaction Transaction => _transaction ??= Connection.BeginTransaction();

        /// <summary>
        /// 生效事务
        /// </summary>
        public void Commit()
        {
            if (_transaction is null) return;
            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        /// <summary>
        /// 生效事务
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public async Task CommitAsync()
        {
            if (_transaction is null) return;
            await _transaction.CommitAsync();
            _transaction.Dispose();
            _transaction = null;
        }

        /// <summary>
        /// 获取Sql仓库
        /// </summary>
        /// <returns></returns>
        public ISqlRepository GetSqlRepository()
        {
            return _dbProvider.GetSqlRepository();
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void OnManagedDispose()
        {
            base.OnManagedDispose();
            // 释放工作者
            _dbWorkProvider.ReleaseWork();
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
