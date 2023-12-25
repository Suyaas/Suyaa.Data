using Microsoft.Data.Sqlite;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Repositories;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Suyaa.Data.Sqlite.Providers
{
    /// <summary>
    /// Sqlite 仓库
    /// </summary>
    public sealed class SqliteExecuteProvider : IDbExecuteProvider
    {
        /// <summary>
        /// 获取数据库命令管理器
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="TypeNotSupportedException"></exception>
        public DbCommand GetDbCommand(IDbWork work)
        {
            if (work.Connection is SqliteConnection dbc)
            {
                var sqlCommand = new SqliteCommand();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
                command.Parameters.Add(new SqliteParameter("$" + param.Key, param.Value ?? DBNull.Value));
            }
        }
    }
}
