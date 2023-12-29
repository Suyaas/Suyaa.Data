using Suyaa.Data.Maintenances.Dependency;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Sqlite.Helpers
{
    /// <summary>
    /// 数据库维护助手
    /// </summary>
    public static class DbMaintenanceHelper
    {

        /// <summary>
        /// 检测表是否存在
        /// </summary>
        /// <param name="maintenance"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static bool CheckTableExists(this IDbMaintenance maintenance, string table)
        {
            return maintenance.CheckTableExists(string.Empty, table);
        }

        /// <summary>
        /// 检测字段是否存在
        /// </summary>
        /// <param name="maintenance"></param>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static bool CheckColumnExists(this IDbMaintenance maintenance, string table, string column)
        {
            return maintenance.CheckColumnExists(string.Empty, table, column);
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <param name="maintenance"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetTables(this IDbMaintenance maintenance)
        {
            return maintenance.GetTables(string.Empty);
        }

        /// <summary>
        /// 获取所有字段
        /// </summary>
        /// <param name="maintenance"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetColumns(this IDbMaintenance maintenance, string table)
        {
            return maintenance.GetColumns(string.Empty, table);
        }
    }
}
