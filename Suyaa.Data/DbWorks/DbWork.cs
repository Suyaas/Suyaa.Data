using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Helpers;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Suyaa.Data.DbWorks
{
    /// <summary>
    /// 简单的数据库工作着
    /// </summary>
    public sealed class DbWork : Disposable, IDbWork
    {
        private readonly IDbFactory _dbFactory;
        private readonly IDbWorkInterceptorFactory _dbWorkInterceptorFactory;
        private readonly IDbWorkManager _dbWorkManager;
        private DbConnection? _connection;
        private DbTransaction? _transaction;
        private readonly List<DbWorkCommand> _dbWorkCommands;
        private readonly IDbExecuteProvider _dbExecuteProvider;

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
            _dbWorkCommands = new List<DbWorkCommand>();
            _dbExecuteProvider = ConnectionDescriptor.DatabaseType.GetDbProvider().ExecuteProvider;
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
        /// 命令集合
        /// </summary>
        public IList<DbWorkCommand> Commands => _dbWorkCommands;

        // 命令执行
        private DbCommand GetExecutingDbCommand(DbWorkCommand command)
        {

            var sqlCommand = DbCommandCreating(null);
            if (sqlCommand is null) sqlCommand = _dbExecuteProvider.GetDbCommand(this);
            sqlCommand.Connection = Connection;
            sqlCommand.Transaction = Transaction;
            sqlCommand.CommandText = command.CommandText;
            _dbExecuteProvider.SetDbParameters(sqlCommand, command.Parameters);
            return DbCommandExecuting(sqlCommand);
        }

        /// <summary>
        /// 生效事务
        /// </summary>
        public void Commit()
        {
            foreach (var command in Commands)
            {
                using var sqlCommand = GetExecutingDbCommand(command);
                sy.Safety.Invoke(() =>
                {
                    sqlCommand.ExecuteNonQuery();
                }, ex =>
                {
                    Transaction.Rollback();
                    throw ex;
                });
            }
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
        public async Task CommitAsync()
        {
            foreach (var command in Commands)
            {
                using var sqlCommand = GetExecutingDbCommand(command);
                try
                {
                    await sqlCommand.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Transaction.Rollback();
                    throw ex;
                }
            }
            if (_transaction is null) return;
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
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
