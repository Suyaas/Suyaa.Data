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
        /// 获取数据库命令管理器
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DbCommand GetDbCommand(this ISqlRepository repository, string sql)
        {
            var command = repository.GetDbCommand();
            command.CommandText = sql;
            return command;
        }

        /// <summary>
        /// 获取数据库命令管理器
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DbCommand GetDbCommand(this ISqlRepository repository, string sql, DbParameters parameters)
        {
            var sqlCommand = repository.GetDbCommand(sql);
            repository.SetDbParameters(sqlCommand, parameters);
            return sqlCommand;
        }
    }
}
