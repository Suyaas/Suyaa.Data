using Suyaa.Data.DbWorks;
using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Maintenances.Dependency;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
using Suyaa.Data.Sqlite.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Suyaa.Data.Helpers;
using Suyaa.Data.Sqlite.Maintenances.Dto;
using Suyaa.Usables;
using Suyaa.Data.DbWorks.Helpers;
using Suyaa.Data.Kernel.Enums;

namespace Suyaa.Data.Sqlite.Maintenances.Providers
{
    /// <summary>
    /// Sqlite维护供应商
    /// </summary>
    public sealed class SqliteMaintenanceProvider : IDbMaintenanceProvider
    {
        private ISqlRepository? _sqlRepository;

        // 获取Sql仓库
        private ISqlRepository GetSqlRepository(IDbWork work)
        {
            _sqlRepository ??= work.GetSqlRepository();
            if (_sqlRepository is null) throw new NotExistException<ISqlRepository>();
            return _sqlRepository;
        }

        /// <summary>
        /// 获取字段创建脚本
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetColumnCreateScript(DbEntityModel entity, ColumnModel column)
        {
            return $"ALTER TABLE [{entity.Name}] ADD [{column.Name}] {GetColumnTypeSqlString(column)} {(column.IsNullable ? "NULL" : "NOT NULL")};";
        }

        /// <summary>
        /// 获取字段是否存在脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetColumnExistsScript(string schema, string table, string field)
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
        public string GetColumnsScript(string schema, string table)
        {
            return $"PRAGMA table_info('{table}');";
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
        private string GetColumnSqlString(ColumnModel column)
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
                sbColumns.Append(GetColumnSqlString(column));
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
            return $"SELECT [name] FROM [sqlite_master] WHERE 1 = 1 AND [type]='table';";
        }

        /// <summary>
        /// 检测Schema
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CheckSchemaExists(IDbWork work, string schema)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 检测表是否存在
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CheckTableExists(IDbWork work, string schema, string table)
            => GetSqlRepository(work).Any(GetTableExistsScript(schema, table));

        // 获取表的所有字段
        private List<ColumnInfo> GetColumnInfos(IDbWork work, string table)
            => GetSqlRepository(work).GetDatas<ColumnInfo>($"PRAGMA table_info('{table}');");

        /// <summary>
        /// 检测表字段是否存在
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool CheckColumnExists(IDbWork work, string schema, string table, string column)
        {
            // 获取表所有字段信息
            var columns = GetColumnInfos(work, table);
            return columns.Where(d => d.Name == column).Any();
        }

        /// <summary>
        /// 获取所有Schema
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<string> GetSchemas(IDbWork work)
        {
            return new List<string> { "Default" };
        }

        /// <summary>
        /// 获取所有表
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<string> GetTables(IDbWork work, string schema)
            => GetSqlRepository(work).GetDatas<string>(GetTablesScript(schema));

        /// <summary>
        /// 获取表字段
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public IEnumerable<string> GetColumns(IDbWork work, string schema, string table)
        {
            // 获取表所有字段信息
            var columns = GetColumnInfos(work, table);
            return columns.Select(d => d.Name);
        }

        /// <summary>
        /// 获取字段类型
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public string GetColumnType(IDbWork work, string schema, string table, string column)
        {
            // 获取表所有字段信息
            var columns = GetColumnInfos(work, table);
            var col = columns.Where(d => d.Name == column).FirstOrDefault();
            if (col is null) return string.Empty;
            return col.Type;
        }

        /// <summary>
        /// 创建Schema
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void CreateSchema(IDbWork work, string schema)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建表
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        public void CreateTable(IDbWork work, DbEntityModel entity)
            => GetSqlRepository(work).ExecuteNonQuery(GetTableCreateScript(entity));

        /// <summary>
        /// 创建列
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        public void CreateColumn(IDbWork work, DbEntityModel entity, ColumnModel column)
            => GetSqlRepository(work).ExecuteNonQuery(GetColumnCreateScript(entity, column));

        /// <summary>
        /// 更新表字段
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void UpdateColumnType(IDbWork work, DbEntityModel entity, ColumnModel column)
        {
            throw new NotImplementedException();
        }
    }
}
