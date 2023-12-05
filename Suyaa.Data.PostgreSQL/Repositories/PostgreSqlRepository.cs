using Npgsql;
using Suyaa.Data.Dependency;
using System.Data;
using System.Data.Common;

namespace Suyaa.Data.PostgreSQL.Repositories
{
    /// <summary>
    /// Postgre Sql 仓库
    /// </summary>
    public sealed class PostgreSqlRepository : ISqlRepository
    {
        private readonly IDbWorkManager _dbWorkManager;

        /// <summary>
        /// Postgre Sql 仓库
        /// </summary>
        public PostgreSqlRepository(
            IDbWorkManager dbWorkManager
            )
        {
            _dbWorkManager = dbWorkManager;
        }

        /// <summary>
        /// 获取数据库命令管理器
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DbCommand GetDbCommand(string sql)
        {
            var work = GetDbWork();
            if (work.Connection is NpgsqlConnection dbc)
            {
                var sqlCommand = new NpgsqlCommand(sql, dbc);
                sqlCommand.CommandTimeout = 600;
                return sqlCommand;
            }
            throw new TypeNotSupportedException(work.Connection.GetType());
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
                sqlCommand.Parameters.Add(new NpgsqlParameter(":" + param.Key, param.Value));
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
            var work = _dbWorkManager.GetCurrentWork();
            if (work is null) throw new NotExistException<IDbWork>();
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
            var sqlDataAdapter = new NpgsqlDataAdapter { SelectCommand = (NpgsqlCommand)sqlCommand };
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
            var sqlDataAdapter = new NpgsqlDataAdapter { SelectCommand = (NpgsqlCommand)sqlCommand };
            sqlDataAdapter.Fill(dataSet);
            return dataSet;
        }
    }
}
