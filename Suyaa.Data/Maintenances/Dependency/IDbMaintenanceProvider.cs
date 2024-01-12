using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Maintenances.Dependency
{
    /// <summary>
    /// 数据库维护供应商
    /// </summary>
    public interface IDbMaintenanceProvider
    {
        /// <summary>
        /// 检测Schema是否存在
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        bool CheckSchemaExists(IDbWork work, string schema);
        /// <summary>
        /// 检测表是否存在
        /// </summary>
        /// <param name="work"></param>
        /// <param name="table"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        bool CheckTableExists(IDbWork work, string schema, string table);
        /// <summary>
        /// 检测字段是否存在
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        bool CheckColumnExists(IDbWork work, string schema, string table, string column);
        /// <summary>
        /// 获取所有Schema
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetSchemas(IDbWork work);
        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetTables(IDbWork work, string schema);
        /// <summary>
        /// 获取所有字段
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetColumns(IDbWork work, string schema, string table);
        /// <summary>
        /// 获取字段类型
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        string GetColumnDataType(IDbWork work, string schema, string table, string column);
        /// <summary>
        /// 创建Schema
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        void CreateSchema(IDbWork work, string schema);
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        void CreateTable(IDbWork work, DbEntityModel entity);
        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        void CreateColumn(IDbWork work, DbEntityModel entity, ColumnModel column);
        /// <summary>
        /// 更新字段类型
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        void UpdateColumnType(IDbWork work, DbEntityModel entity, ColumnModel column);
    }
}
