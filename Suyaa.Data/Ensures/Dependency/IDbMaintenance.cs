using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Suyaa.Data.Ensures.Dependency
{
    /// <summary>
    /// 数据库维护
    /// </summary>
    public interface IDbMaintenance
    {
        /// <summary>
        /// 检测Schema是否存在
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        bool CheckSchemaExists(string schema);
        /// <summary>
        /// 检测表是否存在
        /// </summary>
        /// <param name="table"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        bool CheckTableExists(string schema, string table);
        /// <summary>
        /// 检测字段是否存在
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        bool CheckFieldExists(string schema, string table, string field);
        /// <summary>
        /// 获取所有Schema
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetSchemas();
        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetTables(string schema);
        /// <summary>
        /// 获取所有字段
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetFields(string schema, string table);
        /// <summary>
        /// 获取字段类型
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        string GetFieldType(string schema, string table, string field);
        /// <summary>
        /// 创建Schema
        /// </summary>
        /// <param name="schema"></param>
        void CreateSchema(string schema);
        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="entity"></param>
        void CreateTable(DbEntityModel entity);
        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="field"></param>
        void CreateField(DbEntityModel entity, FieldModel field);
        /// <summary>
        /// 更新字段类型
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="field"></param>
        void UpdateFieldType(DbEntityModel entity, FieldModel field);
    }
}
