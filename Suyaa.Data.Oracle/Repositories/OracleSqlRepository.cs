using Oracle.ManagedDataAccess.Client;
using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Suyaa.Data.Oracle.Repositories
{
    /// <summary>
    /// Oracle Sql 仓库
    /// </summary>
    public sealed class OracleSqlRepository : ISqlRepository
    {
        private readonly IDbWorkProvider _dbWorkProvider;

        /// <summary>
        /// Oracle Sql 仓库
        /// </summary>
        public OracleSqlRepository(
            IDbWorkProvider dbWorkProvider
            )
        {
            _dbWorkProvider = dbWorkProvider;
        }

        /// <summary>
        /// 获取数据库命令管理器
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DbCommand GetDbCommand(string sql)
        {
            var work = GetDbWork();
            if (work.Connection is OracleConnection dbc)
            {
                var sqlCommand = new OracleCommand(sql, dbc);
                sqlCommand.CommandTimeout = 600;
                return sqlCommand;
            }
            throw new DbException("Repository db connection is not for Oracle.");
        }

        /// <summary>
        /// 获取数据库命令管理器
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DbCommand GetDbCommand(string sql, DbParameters parameters)
        {
            var sqlCommand = GetDbCommand(sql);
            sqlCommand.Parameters.Clear();
            foreach (var param in parameters)
            {
                sqlCommand.Parameters.Add(new OracleParameter(":" + param.Key, param.Value));
            }
            return sqlCommand;
        }

        /// <summary>
        /// 获取数据库工作者
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public IDbWork GetDbWork()
        {
            var work = _dbWorkProvider.GetCurrentWork();
            if (work is null) throw new DbException("Repository db work not found.");
            return work;
        }

        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql)
        {
            DataSet dataSet = new DataSet();
            using var sqlCommand = GetDbCommand(sql);
            using var reader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.Default);
            var sqlDataAdapter = new OracleDataAdapter { SelectCommand = (OracleCommand)sqlCommand };
            sqlDataAdapter.Fill(dataSet);
            return dataSet;
        }
        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string sql, DbParameters parameters)
        {
            DataSet dataSet = new DataSet();
            using var sqlCommand = GetDbCommand(sql, parameters);
            using var reader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.Default);
            var sqlDataAdapter = new OracleDataAdapter { SelectCommand = (OracleCommand)sqlCommand };
            sqlDataAdapter.Fill(dataSet);
            return dataSet;
        }
    }
}
