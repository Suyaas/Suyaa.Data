using Suyaa.Data.DbWorks.Dependency;
using Suyaa.Data.Maintenances.Dependency;
using Suyaa.Data.Models;
using Suyaa.Data.Repositories.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Suyaa.Data.Helpers;
using Suyaa.Data.DbWorks.Helpers;
using Suyaa.Data.Kernel.Enums;
using System.Diagnostics.CodeAnalysis;
using Suyaa.Data.Oracle.Maintenances.Dto;

namespace Suyaa.Data.Oracle.Maintenances.Providers
{
    /// <summary>
    /// Sqlite维护供应商
    /// </summary>
    public sealed class OracleMaintenanceProvider : IDbMaintenanceProvider
    {
        private ISqlRepository? _sqlRepository;

        // 获取名称字符串
        private string GetNameString(string name)
        {
            return "\"" + name + "\"";
        }

        // 获取Sql仓库
        private ISqlRepository GetSqlRepository(IDbWork work)
        {
            _sqlRepository ??= work.GetSqlRepository();
            if (_sqlRepository is null) throw new NotExistException<ISqlRepository>();
            return _sqlRepository;
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
        /// 获取列描述字符串
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetColumnSqlString(ColumnModel column)
        {
            // 获取字段类型
            string columnType = GetColumnTypeSqlString(column);
            if (column.IsKey)
            {
                if (column.IsAutoIncrement) return $"{GetNameString(column.Name)} {columnType} PRIMARY KEY AUTOINCREMENT";
                return $"{GetNameString(column.Name)} {columnType} NOT NULL PRIMARY KEY";
            }
            else
            {
                return $"{GetNameString(column.Name)} {columnType} {(column.IsNullable ? "NULL" : "NOT NULL")}";
            }
        }

        #region Schema相关

        // 获取所有Schema的脚本
        private string GetSchemaExistsScript(string schema)
        {
            return $"SELECT 1 FROM (SELECT USERNAME FROM SYS.DBA_USERS WHERE USERNAME = '{schema}') WHERE ROWNUM = 1";
        }

        /// <summary>
        /// 检测Schema
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CheckSchemaExists(IDbWork work, string schema)
            => GetSqlRepository(work).Any(GetSchemaExistsScript(schema));

        // 获取所有Schema的脚本
        private string GetSchemasScript()
        {
            return "SELECT USERNAME FROM SYS.DBA_USERS ORDER BY USERNAME";
        }

        /// <summary>
        /// 获取所有Schema
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<string> GetSchemas(IDbWork work)
            => GetSqlRepository(work).GetDatas<string>(GetSchemasScript());

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

        #endregion

        #region 表相关

        /// <summary>
        /// 获取所有表脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string GetTablesScript(string schema)
        {
            return $"SELECT TABLE_NAME FROM SYS.DBA_TABLES WHERE OWNER = '{schema}' ORDER BY TABLE_NAME";
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
        /// 获取表是否存在脚本
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private string GetTableExistsScript(string schema, string table)
        {
            return $"SELECT 1 FROM (SELECT TABLE_NAME FROM SYS.DBA_TABLES WHERE OWNER = '{schema}' AND TABLE_NAME = '{table}' ORDER BY TABLE_NAME) WHERE ROWNUM = 1";
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

        /// <summary>
        /// 获取表创建脚本
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private string GetTableCreateScript(DbEntityModel entity)
        {
            // 申明拼接字符串
            StringBuilder sb = new StringBuilder();
            StringBuilder sbColumns = new StringBuilder();
            // 拼接语句
            sb.AppendLine($"CREATE TABLE {GetNameString(entity.Name)}(");
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
        /// 创建表
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        public void CreateTable(IDbWork work, DbEntityModel entity)
            => GetSqlRepository(work).ExecuteNonQuery(GetTableCreateScript(entity));

        #endregion

        #region 字段相关

        // 获取字段检测Sql
        private string GetColumnExistsScript(string schema, string table, string column)
            => $"SELECT 1 FROM (SELECT * FROM SYS.DBA_TAB_COLUMNS WHERE OWNER = '{schema}' AND TABLE_NAME = '{table}' AND COLUMN_NAME = '{column}') WHERE ROWNUM = 1";

        /// <summary>
        /// 检测表字段是否存在
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool CheckColumnExists(IDbWork work, string schema, string table, string column)
            => GetSqlRepository(work).Any(GetColumnExistsScript(schema, table, column));

        // 获取字段检测Sql
        private string GetColumnsScript(string schema, string table)
            => $"SELECT COLUMN_NAME FROM SYS.DBA_TAB_COLUMNS WHERE OWNER = '{schema}' AND TABLE_NAME = '{table}' ORDER BY COLUMN_ID";

        /// <summary>
        /// 获取表字段
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public IEnumerable<string> GetColumns(IDbWork work, string schema, string table)
            => GetSqlRepository(work).GetDatas<string>(GetColumnsScript(schema, table));

        /// <summary>
        /// 获取字段创建脚本
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string GetColumnCreateScript(DbEntityModel entity, ColumnModel column)
        {
            return $"ALTER TABLE {GetNameString(entity.Name)} ADD {GetNameString(column.Name)} {GetColumnTypeSqlString(column)} {(column.IsNullable ? "NULL" : "NOT NULL")};";
        }

        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="work"></param>
        /// <param name="entity"></param>
        /// <param name="column"></param>
        public void CreateColumn(IDbWork work, DbEntityModel entity, ColumnModel column)
            => GetSqlRepository(work).ExecuteNonQuery(GetColumnCreateScript(entity, column));

        // 获取字段信息
        private ColumnInfo? GetColumnInfo(IDbWork work, string schema, string table, string column)
        {
            string sql = $"SELECT COLUMN_ID, COLUMN_NAME, DATA_TYPE, DATA_LENGTH, DATA_PRECISION, NULLABLE FROM SYS.DBA_TAB_COLUMNS WHERE OWNER = '{schema}' AND TABLE_NAME = '{table}' AND COLUMN_NAME = '{column}'";
            return GetSqlRepository(work).GetData<ColumnInfo>(sql);
        }

        /// <summary>
        /// 获取字段类型
        /// </summary>
        /// <param name="work"></param>
        /// <param name="schema"></param>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public string GetColumnDataType(IDbWork work, string schema, string table, string column)
        {
            // 获取表所有字段信息
            var columnInfo = GetColumnInfo(work, schema, table, column);
            if (columnInfo is null) return string.Empty;
            if (columnInfo.DataPrecision is null) return $"{columnInfo.DataType}({columnInfo.DataLength})";
            return $"{columnInfo.DataType}({columnInfo.DataLength},{columnInfo.DataPrecision})";
        }

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

        #endregion

    }
}
