using Suyaa.Data.Enums;
using Suyaa.Data.Maintenances.Dependency;
using Suyaa.Data.Models;
using Suyaa.Data.Sqlite.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public string GetFieldCreateScript(DbEntityModel entity, ColumnModel field)
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
            return $"SELECT [name] FROM [sqlite_master] WHERE [type]='table'" +
                $" AND [name] ='{table}'" +
                $" AND ([sql] LIKE '%[{field}]%'" +
                $" OR [sql] LIKE '%\"{field}\"%'" +
                $" OR [sql] LIKE '%,{field},%'" +
                $" OR [sql] LIKE '%({field},%'" +
                $" OR [sql] LIKE '%,{field})%'" +
                $" OR [sql] LIKE '% {field} %'" +
                $") LIMIT 1;";
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
        public string GetFieldTypeUpdateScript(DbEntityModel entity, ColumnModel field)
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

        // 获取类型SQL字符串
        private string GetColumnTypeSqlString(ColumnModel column)
        {
            // 自增长主键
            if (column.IsKey && column.IsAutoIncrement) return "INTEGER";
            // 无设定，则使用默认
            if (column.ColumnType is null)
            {
                var typeCode = column.PropertyInfo.PropertyType.GetTypeCode();
                switch (typeCode)
                {
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        return "INTEGER";
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                        return "REAL";
                    case TypeCode.String:
                        return "TEXT";
                    default:
                        throw new TypeNotSupportedException(column.PropertyInfo.PropertyType);
                }
            }
            // 有设定，则使用设定
            switch (column.ColumnType.Type)
            {
                case ColumnValueType.Unknow: return column.ColumnType.CustomName;
                case ColumnValueType.Text:
                case ColumnValueType.Varchar:
                case ColumnValueType.Char:
                case ColumnValueType.Data:
                    return "TEXT";
                case ColumnValueType.Bool:
                case ColumnValueType.BigInt:
                case ColumnValueType.Int:
                case ColumnValueType.SmallInt:
                case ColumnValueType.TinyInt:
                    return "INTEGER";
                case ColumnValueType.Single:
                case ColumnValueType.Decimal:
                case ColumnValueType.Double:
                    return "REAL";
                default: throw new TypeNotSupportedException("ColumnValueType", column.ColumnType.Type.ToString());
            }
        }

        /// <summary>
        /// 获取列创建字符串
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetColumnCreateSqlString(ColumnModel column)
        {
            // 获取字段类型
            string columnType = GetColumnTypeSqlString(column);
            if (column.IsKey)
            {
                if (column.IsAutoIncrement) return $"[{column.Name}] {columnType} PRIMARY KEY AUTOINCREMENT";
                return $"[{column.Name}] {columnType} NOT NULL PRIMARY KEY";
            }
            else
            {
                return $"[{column.Name}] {columnType} {(column.IsNullable ? "NULL" : "NOT NULL")}";
            }
        }

        /// <summary>
        /// 获取表创建脚本
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetTableCreateScript(DbEntityModel entity)
        {
            // 申明拼接字符串
            StringBuilder sb = new StringBuilder();
            StringBuilder sbColumns = new StringBuilder();
            // 拼接语句
            sb.AppendLine($"CREATE TABLE [{entity.Name}](");
            // 获取所有字段
            foreach (var column in entity.Columns.OrderByDescending(d => d.IsKey))
            {
                if (sbColumns.Length > 0) { sbColumns.Append(','); sbColumns.AppendLine(); }
                sbColumns.Append(new string(' ', 4));
                sbColumns.Append(GetColumnCreateSqlString(column));
            }
            sb.Append(sbColumns);
            sb.AppendLine(");");
            return sb.ToString();
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
