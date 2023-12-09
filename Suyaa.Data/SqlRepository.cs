using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Suyaa.Data
{
    /// <summary>
    /// Sql脚本仓库
    /// </summary>
    public sealed class SqlRepository : ISqlRepository
    {
        private readonly IDbWorkManager _dbWorkManager;
        private readonly ISqlRepositoryProvider _sqlRepositoryProvider;

        /// <summary>
        /// Sql脚本仓库
        /// </summary>
        public SqlRepository(
            IDbWorkManager dbWorkManager,
            ISqlRepositoryProvider sqlRepositoryProvider
            )
        {
            _dbWorkManager = dbWorkManager;
            _sqlRepositoryProvider = sqlRepositoryProvider;
        }

        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            return _sqlRepositoryProvider.GetDataSet(GetDbWork(), sql);
        }

        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, DbParameters parameters)
        {
            return _sqlRepositoryProvider.GetDataSet(GetDbWork(), sql, parameters);
        }

        /// <summary>
        /// 获取数据库命令管理器
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DbCommand GetDbCommand(string sql)
        {
            return _sqlRepositoryProvider.GetDbCommand(GetDbWork(), sql);
        }

        /// <summary>
        /// 获取数据库命令管理器
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DbCommand GetDbCommand(string sql, DbParameters parameters)
        {
            return _sqlRepositoryProvider.GetDbCommand(GetDbWork(), sql, parameters);
        }

        /// <summary>
        /// 获取数据库作业
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotExistException{IDbWork}"></exception>
        public IDbWork GetDbWork()
        {
            var work = _dbWorkManager.GetCurrentWork();
            if (work is null) throw new NotExistException<IDbWork>();
            return work;
        }
    }
}
