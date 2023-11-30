using Oracle.ManagedDataAccess.Client;
using Suyaa.Data.Dependency;
using Suyaa.Data.Oracle.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Suyaa.Data.Oracle.Providers
{
    /// <summary>
    /// Oracle数据库供应商
    /// </summary>
    public class OracleProvider : IDbProvider
    {
        private readonly IDbWorkProvider _dbWorkProvider;
        private OracleQueryProvider? _queryProvider;

        /// <summary>
        /// Oracle数据库供应商
        /// </summary>
        public OracleProvider(
            IDbWorkProvider dbWorkProvider
            )
        {
            _dbWorkProvider = dbWorkProvider;
        }

        /// <summary>
        /// 查询供应商
        /// </summary>
        public IDbQueryProvider QueryProvider => _queryProvider ??= new OracleQueryProvider();

        /// <summary>
        /// 获取一个数据库连接
        /// </summary>
        /// <returns></returns>
        public DbConnection GetDbConnection()
        {
            var work = _dbWorkProvider.GetCurrentWork();
            if (work is null) throw new DbException("Current db work not found.");
            return new OracleConnection(work.ConnectionDescriptor.ToConnectionString());
        }

        /// <summary>
        /// 获取一个Sql仓库
        /// </summary>
        /// <returns></returns>
        public ISqlRepository GetSqlRepository()
        {
            return new OracleSqlRepository(_dbWorkProvider);
        }
    }
}
