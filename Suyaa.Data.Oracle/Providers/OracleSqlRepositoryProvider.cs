using Oracle.ManagedDataAccess.Client;
using Suyaa.Data.Dependency;
using System.Data;
using System.Data.Common;

namespace Suyaa.Data.Oracle.Providers
{
    /// <summary>
    /// Oracle Sql 仓库
    /// </summary>
    public sealed class OracleSqlRepositoryProvider : ISqlRepositoryProvider
    {

        /// <summary>
        /// 获取数据库命令管理器
        /// </summary>
        /// <param name="work"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DbCommand GetDbCommand(IDbWork work, string sql)
        {
            if (work.Connection is OracleConnection dbc)
            {
                var sqlCommand = new OracleCommand(sql, dbc);
                sqlCommand.CommandTimeout = 600;
                return sqlCommand;
            }
            throw new TypeNotSupportedException(work.Connection.GetType());
        }

        /// <summary>
        /// 获取数据库命令管理器
        /// </summary>
        /// <param name="work"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DbCommand GetDbCommand(IDbWork work, string sql, DbParameters parameters)
        {
            var sqlCommand = GetDbCommand(work, sql);
            sqlCommand.Parameters.Clear();
            foreach (var param in parameters)
            {
                sqlCommand.Parameters.Add(new OracleParameter(":" + param.Key, param.Value));
            }
            return sqlCommand;
        }

        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="work"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet GetDataSet(IDbWork work, string sql)
        {
            DataSet dataSet = new DataSet();
            using var sqlCommand = GetDbCommand(work, sql);
            var sqlDataAdapter = new OracleDataAdapter { SelectCommand = (OracleCommand)sqlCommand };
            sqlDataAdapter.Fill(dataSet);
            return dataSet;
        }
        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="work"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet GetDataSet(IDbWork work, string sql, DbParameters parameters)
        {
            DataSet dataSet = new DataSet();
            using var sqlCommand = GetDbCommand(work, sql, parameters);
            var sqlDataAdapter = new OracleDataAdapter { SelectCommand = (OracleCommand)sqlCommand };
            sqlDataAdapter.Fill(dataSet);
            return dataSet;
        }
    }
}
