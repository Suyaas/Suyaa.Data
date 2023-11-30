using Suyaa.Data.Dependency;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography;
using System.Text;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// Sql 仓库助手
    /// </summary>
    public static class SqlRepositoryHelper
    {
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(this ISqlRepository repository, string sql)
        {
            var work = repository.GetDbWork();
            using var sqlTransaction = work.Connection.BeginTransaction();
            using var sqlCommand = repository.GetDbCommand(sql);
            sqlCommand.Transaction = sqlTransaction;
            try
            {
                var res = sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
                return res;
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                throw ex;
            }
        }
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(this ISqlRepository repository, string sql, DbParameters parameters)
        {
            var work = repository.GetDbWork();
            using var sqlTransaction = work.Connection.BeginTransaction();
            using var sqlCommand = repository.GetDbCommand(sql);
            sqlCommand.Transaction = sqlTransaction;
            try
            {
                var res = sqlCommand.ExecuteNonQuery();
                sqlTransaction.Commit();
                return res;
            }
            catch (Exception ex)
            {
                sqlTransaction.Rollback();
                throw ex;
            }
        }
        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <param name="funcDbDataReader"></param>
        /// <returns></returns>
        public static int ExecuteDataReader(this ISqlRepository repository, string sql, Func<DbDataReader, int> funcDbDataReader)
        {
            using var sqlCommand = repository.GetDbCommand(sql);
            using var reader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.Default);
            var res = funcDbDataReader(reader);
            reader.Close();
            return res;
        }
        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(this ISqlRepository repository, string sql)
        {
            var dataSet = repository.GetDataSet(sql);
            if (dataSet is null) return new DataTable();
            return dataSet.Tables[0];
        }
        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(this ISqlRepository repository, string sql, DbParameters parameters)
        {
            var dataSet = repository.GetDataSet(sql, parameters);
            if (dataSet is null) return new DataTable();
            return dataSet.Tables[0];
        }
    }
}
