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
        private readonly IDbFactory _dbFactory;
        private readonly IDbWorkProvider _dbWorkProvider;
        private readonly IDbProvider _dbProvider;
        private DbConnection? _connection;
        private DbTransaction? _transaction;

        /// <summary>
        /// 简单的数据库工作着
        /// </summary>
        public SimpleDbWork(
            IDbFactory dbFactory,
            DbConnectionDescriptor dbConnectionDescriptor
            )
        {
            _dbFactory = dbFactory;
            _dbWorkProvider = dbFactory.WorkProvider;
            ConnectionDescriptor = dbConnectionDescriptor;
            _dbProvider = dbFactory.Provider;
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
        /// 数据库工厂
        /// </summary>
        public IDbFactory Factory => _dbFactory;

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
        /// 获取数据仓库
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : IEntity, new()
        {
            return new SimpleRepository<TEntity>(this);
        }

        /// <summary>
        /// 获取带主键的数据仓库
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TId"></typeparam>
        /// <returns></returns>
        public IRepository<TEntity, TId> GetRepository<TEntity, TId>()
            where TEntity : IEntity<TId>, new()
            where TId : notnull
        {
            return new SimpleRepository<TEntity, TId>(this);
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
