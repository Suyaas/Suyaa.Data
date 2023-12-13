using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// Sql 仓库助手
    /// </summary>
    public static partial class SqlRepositoryHelper
    {
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteNonQueryAsync(this ISqlRepository repository, string sql)
        {
            var work = repository.GetDbWork();
            using var sqlCommand = work.DbCommandExecuting(repository.GetDbCommand(sql));
            sqlCommand.Transaction = work.Transaction;
            try
            {
                var res = await sqlCommand.ExecuteNonQueryAsync();
                return res;
            }
            catch (Exception ex)
            {
                work.Transaction.Rollback();
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
        public static async Task<int> ExecuteNonQueryAsync(this ISqlRepository repository, string sql, DbParameters parameters)
        {
            var work = repository.GetDbWork();
            using var sqlCommand = work.DbCommandExecuting(repository.GetDbCommand(sql, parameters));
            sqlCommand.Transaction = work.Transaction;
            try
            {
                var res = await sqlCommand.ExecuteNonQueryAsync();
                return res;
            }
            catch (Exception ex)
            {
                work.Transaction.Rollback();
                throw ex;
            }
        }
        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <param name="actionDbDataReader"></param>
        /// <returns></returns>
        public static async Task ExecuteReaderAsync(this ISqlRepository repository, string sql, Action<DbDataReader> actionDbDataReader)
        {
            var work = repository.GetDbWork();
            using var sqlCommand = work.DbCommandExecuting(repository.GetDbCommand(sql));
            using var reader = await sqlCommand.ExecuteReaderAsync(CommandBehavior.Default);
            actionDbDataReader(reader);
            reader.Close();
        }
        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <param name="actionDbDataReader"></param>
        /// <returns></returns>
        public static async Task ExecuteReaderAsync(this ISqlRepository repository, string sql, DbParameters parameters, Action<DbDataReader> actionDbDataReader)
        {
            var work = repository.GetDbWork();
            using var sqlCommand = work.DbCommandExecuting(repository.GetDbCommand(sql, parameters));
            using var reader = await sqlCommand.ExecuteReaderAsync(CommandBehavior.Default);
            actionDbDataReader(reader);
            reader.Close();
        }
        /// <summary>
        /// 执行数据集合读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static async Task<List<T>> GetDatasAsync<T>(this ISqlRepository repository, string sql)
        {
            List<T> datas = new List<T>();
            // 执行读取
            await repository.ExecuteReaderAsync(sql, reader =>
             {
                 reader.FillDatas(datas);
             });
            return datas;
        }
        /// <summary>
        /// 执行数据读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        [return: MaybeNull]
        public static async Task<T> GetDataAsync<T>(this ISqlRepository repository, string sql)
        {
            T data = default;
            // 执行读取
            await repository.ExecuteReaderAsync(sql, reader =>
            {
                data = reader.GetData<T>();
            });
            return data!;
        }
        /// <summary>
        /// 执行数据集合读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static async Task<List<T>> GetDatasAsync<T>(this ISqlRepository repository, string sql, DbParameters parameters)
        {
            List<T> datas = new List<T>();
            // 执行读取
            await repository.ExecuteReaderAsync(sql, parameters, reader =>
            {
                reader.FillDatas(datas);
            });
            return datas;
        }
        /// <summary>
        /// 执行数据读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [return: MaybeNull]
        public static async Task<T> GetDataAsync<T>(this ISqlRepository repository, string sql, DbParameters parameters)
        {
            T data = default;
            // 执行读取
            await repository.ExecuteReaderAsync(sql, parameters, reader =>
            {
                data = reader.GetData<T>();
            });
            return data!;
        }
    }
}
