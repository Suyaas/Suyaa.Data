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
        /// 获取Schema检测脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        string GetSchemaExistsScript(string schema);
        /// <summary>
        /// 获取检测表是否存在脚本
        /// </summary>
        /// <param name="table"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        string GetTableExistsScript(string schema, string table);
        /// <summary>
        /// 获取检测字段是否存在脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        string GetFieldExistsScript(string schema, string table, string field);
        /// <summary>
        /// 获取所有Schema脚本
        /// </summary>
        /// <returns></returns>
        string GetSchemasScript();
        /// <summary>
        /// 获取所有表脚本
        /// </summary>
        /// <returns></returns>
        string GetTablesScript(string schema);
        /// <summary>
        /// 获取所有字段脚本
        /// </summary>
        /// <returns></returns>
        string GetFieldsScript(string schema, string table);
        /// <summary>
        /// 获取字段类型脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        string GetFieldTypeScript(string schema, string table, string field);
        /// <summary>
        /// 获取创建Schema脚本
        /// </summary>
        /// <param name="schema"></param>
        string GetSchemaCreateScript(string schema);
        /// <summary>
        /// 获取创建表脚本
        /// </summary>
        /// <param name="entity"></param>
        string GetTableCreateScript(DbEntityModel entity);
        /// <summary>
        /// 获取创建字段脚本
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="field"></param>
        string GetFieldCreateScript(DbEntityModel entity, FieldModel field);
        /// <summary>
        /// 获取更新字段类型脚本
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="field"></param>
        string GetFieldTypeUpdateScript(DbEntityModel entity, FieldModel field);
    }
}
