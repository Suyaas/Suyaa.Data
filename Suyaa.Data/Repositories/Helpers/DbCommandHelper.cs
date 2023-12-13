using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Repositories.Helpers
{
    /// <summary>
    /// 数据库命令管理器助手
    /// </summary>
    public static class DbCommandHelper
    {
        /// <summary>
        /// 获取 DbDataReader
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static DbDataReader GetDataReader(this DbCommand command)
        {
            return command.ExecuteReader(CommandBehavior.Default);
        }

        /// <summary>
        /// 获取 DbDataReader
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static async Task<DbDataReader> GetDataReaderAsync(this DbCommand command)
        {
            return await command.ExecuteReaderAsync(CommandBehavior.Default);
        }
    }
}
