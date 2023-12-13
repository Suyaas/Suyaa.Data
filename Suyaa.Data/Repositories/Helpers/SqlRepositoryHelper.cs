using Suyaa.Data.DbWorks;
using Suyaa.Data.Repositories;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

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
        public static void ExecuteNonQuery(this ISqlRepository repository, string sql)
        {
            var work = repository.GetDbWork();
            work.Commands.Add(new DbWorkCommand(sql));
        }
        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static void ExecuteNonQuery(this ISqlRepository repository, string sql, DbParameters parameters)
        {
            var work = repository.GetDbWork();
            work.Commands.Add(new DbWorkCommand(sql,parameters));
        }

        /// <summary>
        /// 获取数据库命令管理器
        /// </summary>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static DbCommand GetDbCommand(this ISqlRepository repository)
        {
            var work = repository.GetDbWork();
            var command = work.DbCommandCreating(null);
            if (command is null)
            {
                var provider = work.ConnectionDescriptor.DatabaseType.GetDbProvider().ExecuteProvider;
                command = provider.GetDbCommand(work);
            }
            command.Connection = work.Connection;
            return command;
        }

        /// <summary>
        /// 设置参数集
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        public static void SetDbParameters(this ISqlRepository repository, DbCommand command, DbParameters parameters)
        {
            var provider = repository.GetDbWork().ConnectionDescriptor.DatabaseType.GetDbProvider().ExecuteProvider;
            provider.SetDbParameters(command, parameters);
        }

        /// <summary>
        /// 执行原始数据读取
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <param name="actionDbDataReader"></param>
        /// <returns></returns>
        public static void ExecuteReader(this ISqlRepository repository, string sql, Action<DbDataReader> actionDbDataReader)
        {
            var work = repository.GetDbWork();
            var command = repository.GetDbCommand();
            command.CommandText = sql;
            using var sqlCommand = work.DbCommandExecuting(command);
            using var reader = sqlCommand.ExecuteReader(CommandBehavior.Default);
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
        public static void ExecuteReader(this ISqlRepository repository, string sql, DbParameters parameters, Action<DbDataReader> actionDbDataReader)
        {
            var work = repository.GetDbWork();
            var command = repository.GetDbCommand();
            command.CommandText = sql;
            repository.SetDbParameters(command, parameters);
            using var sqlCommand = work.DbCommandExecuting(command);
            using var reader = sqlCommand.ExecuteReader(CommandBehavior.Default);
            actionDbDataReader(reader);
            reader.Close();
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
        /// <summary>
        /// 执行数据集合读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static List<T> GetDatas<T>(this ISqlRepository repository, string sql)
        {
            List<T> datas = new List<T>();
            // 执行读取
            repository.ExecuteReader(sql, reader =>
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
        public static T GetData<T>(this ISqlRepository repository, string sql)
        {
            T data = default;
            // 执行读取
            repository.ExecuteReader(sql, reader =>
            {
                data = reader.GetData<T>();
            });
            return data;
        }
        /// <summary>
        /// 执行数据集合读取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static List<T> GetDatas<T>(this ISqlRepository repository, string sql, DbParameters parameters)
        {
            List<T> datas = new List<T>();
            // 执行读取
            repository.ExecuteReader(sql, parameters, reader =>
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
        public static T GetData<T>(this ISqlRepository repository, string sql, DbParameters parameters)
        {
            T data = default;
            // 执行读取
            repository.ExecuteReader(sql, parameters, reader =>
            {
                data = reader.GetData<T>();
            });
            return data;
        }
    }
}
