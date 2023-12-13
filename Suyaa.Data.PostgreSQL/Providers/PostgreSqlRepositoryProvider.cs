using Npgsql;
using Suyaa.Data.Dependency;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Suyaa.Data.PostgreSQL.Providers
{
    /// <summary>
    /// Postgre Sql 仓库
    /// </summary>
    public sealed class PostgreSqlRepositoryProvider : ISqlRepositoryProvider
    {
        /// <summary>
        /// 获取数据库命令管理器
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="TypeNotSupportedException"></exception>
        public DbCommand GetDbCommand(IDbWork work)
        {
            if (work.Connection is NpgsqlConnection dbc)
            {
                var sqlCommand = new NpgsqlCommand();
                sqlCommand.CommandTimeout = 600;
                sqlCommand.Connection = dbc;
                return sqlCommand;
            }
            throw new TypeNotSupportedException(work.Connection.GetType());
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
            using var sqlCommand = GetDbCommand(work);
            sqlCommand.CommandText = sql;
            var sqlDataAdapter = new NpgsqlDataAdapter { SelectCommand = (NpgsqlCommand)sqlCommand };
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
            using var sqlCommand = GetDbCommand(work);
            sqlCommand.CommandText = sql;
            SetDbParameters(sqlCommand, parameters);
            var sqlDataAdapter = new NpgsqlDataAdapter { SelectCommand = (NpgsqlCommand)sqlCommand };
            sqlDataAdapter.Fill(dataSet);
            return dataSet;
        }

        /// <summary>
        /// 设置参数集
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        public void SetDbParameters(DbCommand command, DbParameters parameters)
        {
            command.Parameters.Clear();
            foreach (var param in parameters)
            {
                command.Parameters.Add(new NpgsqlParameter(":" + param.Key, param.Value));
            }
        }
    }
}
