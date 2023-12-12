using Suyaa.Data.Dependency;
using Suyaa.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;

namespace Suyaa.Data
{
    /// <summary>
    /// Sql脚本仓库
    /// </summary>
    public sealed class SqlRepository : ISqlRepository
    {
        private readonly IDbWorkManager _dbWorkManager;

        /// <summary>
        /// Sql脚本仓库
        /// </summary>
        public SqlRepository(
            IDbWorkManager dbWorkManager
            )
        {
            _dbWorkManager = dbWorkManager;
        }

        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            return GetSqlRepositoryProvider().GetDataSet(GetDbWork(), sql);
        }

        /// <summary>
        /// 获取DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, DbParameters parameters)
        {
            return GetSqlRepositoryProvider().GetDataSet(GetDbWork(), sql, parameters);
        }

        /// <summary>
        /// 获取Sql仓库供应商
        /// </summary>
        /// <returns></returns>
        public ISqlRepositoryProvider GetSqlRepositoryProvider()
        {
            return GetDbWork().ConnectionDescriptor.DatabaseType.GetDbProvider().SqlRepositoryProvider;
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

        /// <summary>
        /// 获取数据库命令管理器
        /// </summary>
        /// <returns></returns>
        public DbCommand GetDbCommand()
        {
            return GetSqlRepositoryProvider().GetDbCommand(GetDbWork());
        }
    }
}
