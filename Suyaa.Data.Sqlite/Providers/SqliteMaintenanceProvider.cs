using Suyaa.Data.Ensures.Dependency;
using Suyaa.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Suyaa.Data.Sqlite.Providers
{
    /// <summary>
    /// Sqlite维护供应商
    /// </summary>
    public sealed class SqliteMaintenanceProvider : IDbMaintenanceProvider
    {
        /// <summary>
        /// 获取字段创建脚本
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetFieldCreateScript(DbEntityModel entity, FieldModel field)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取字段是否存在脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetFieldExistsScript(string schema, string table, string field)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取字段集合脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetFieldsScript(string schema, string table)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取字段类型脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetFieldTypeScript(string schema, string table, string field)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取字段类型更新脚本
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetFieldTypeUpdateScript(DbEntityModel entity, FieldModel field)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取Schema创建脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetSchemaCreateScript(string schema)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取Schema是否存在脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetSchemaExistsScript(string schema)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取所有Schema脚本
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetSchemasScript()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取表创建脚本
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetTableCreateScript(DbEntityModel entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取表是否存在脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public string GetTableExistsScript(string schema, string table)
        {
            return $"SELECT [name] FROM [sqlite_master] WHERE [name] = '{table}' AND [type]='table' LIMIT 1;";
        }

        /// <summary>
        /// 获取所有表脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetTablesScript(string schema)
        {
            return $"SELECT [name] FROM [sqlite_master] WHERE 1 = 1 AND [type]='table' LIMIT 1;";
        }
    }
}
