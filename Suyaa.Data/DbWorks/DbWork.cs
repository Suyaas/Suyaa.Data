using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Dependency;
using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Suyaa.Data.DbWorks
{
    /// <summary>
    /// 简单的数据库工作着
    /// </summary>
    public class DbWork : Disposable, IDbWork
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDbWorkInterceptorFactory _dbWorkInterceptorFactory;
        private readonly IDbWorkManager _dbWorkManager;
        private DbConnection? _connection;
        private DbTransaction? _transaction;

        /// <summary>
        /// 简单的数据库工作着
        /// </summary>
        public DbWork(
            IDbWorkManager dbWorkManager,
            IDbFactory dbFactory,
            IDbWorkInterceptorFactory dbWorkInterceptorFactory
            )
        {
            ConnectionDescriptor = dbWorkManager.ConnectionDescriptor;
            _dbFactory = dbFactory;
            _dbWorkInterceptorFactory = dbWorkInterceptorFactory;
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
        public virtual void Commit()
        {
            if (_transaction is null) return;
            sy.Safety.Invoke(() =>
            {
                _transaction.Commit();
            }, ex =>
            {
                sy.Safety.Invoke(() => { _transaction.Rollback(); }, ex1 =>
                {
                    throw new Exception(ex1.Message, ex);
                });
                throw ex;
            });
            _transaction.Dispose();
            _transaction = null;
        }

        /// <summary>
        /// 生效事务
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public virtual async Task CommitAsync()
        {
            if (_transaction is null) return;
            try
            {
                await _transaction.CommitAsync();
            }catch (Exception ex)
            {
                try
                {
                    await _transaction.RollbackAsync();
                }
                catch (Exception ex1)
                {
                    throw new Exception(ex1.Message, ex);
                }
                throw ex;
            }
            _transaction.Dispose();
            _transaction = null;
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

        /// <summary>
        /// 命令管理器创建
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public DbCommand? DbCommandCreating(DbCommand? dbCommand)
        {
            var providers = _dbWorkInterceptorFactory.GetInterceptors();
            foreach (var provider in providers)
            {
                dbCommand = provider.DbCommandCreating(dbCommand);
            }
            return dbCommand;
        }

        /// <summary>
        /// 命令管理器执行
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public DbCommand DbCommandExecuting(DbCommand dbCommand)
        {
            var providers = _dbWorkInterceptorFactory.GetInterceptors();
            foreach (var provider in providers)
            {
                dbCommand = provider.DbCommandExecuting(dbCommand);
            }
            return dbCommand;
        }
    }
}
