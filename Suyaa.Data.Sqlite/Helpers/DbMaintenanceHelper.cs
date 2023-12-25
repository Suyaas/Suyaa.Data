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
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool CheckFieldExists(this IDbMaintenance maintenance, string table, string field)
        {
            return maintenance.CheckFieldExists(string.Empty, table, field);
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
        public static IEnumerable<string> GetFields(this IDbMaintenance maintenance, string table)
        {
            return maintenance.GetFields(string.Empty, table);
        }
    }
}
