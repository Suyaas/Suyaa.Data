using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Suyaa.Data.Descriptors.Dependency;
using Suyaa.Data.Kernel.Enums;

namespace Suyaa.Data
{
    /// <summary>
    /// 数据库连接描述
    /// </summary>
    public class DbConnectionDescriptor : Dictionary<string, string>, IDbConnectionDescriptor
    {

        /// <summary>
        /// 解析连接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        private void ParseConnectionString(string connectionString)
        {
            string[] strings = connectionString.Split(';');
            foreach (var str in strings)
            {
                if (str.IsNullOrWhiteSpace()) continue;
                int idx = str.IndexOf("=");
                if (idx < 0)
                {
                    this[str] = "";
                }
                else
                {
                    this[str.Substring(0, idx)] = str.Substring(idx + 1);
                }
            }
        }

        /// <summary>
        /// 数据库描述
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connectionDefine">以[dbtype]connectionString形式定义的连接描述</param>
        public DbConnectionDescriptor(string name, string connectionDefine)
        {
            if (connectionDefine.IsNullOrWhiteSpace()) throw new NullException("connectionDefine");
            if (connectionDefine[0] != '[') throw new DbException("Descriptor.StartRule", "connectionDefine must start with '[dbtype]'.");
            int idx = connectionDefine.IndexOf(']');
            if (idx < 0) throw new DbException("Descriptor.StartRule", "connectionDefine must start with '[dbtype]'.");
            string dbType = connectionDefine.Substring(1, idx - 1);
            // 获取连接字符串
            ParseConnectionString(connectionDefine.Substring(idx + 1));
            // 获取数据库类型
            this.DatabaseType = dbType.ToLower() switch
            {
                "sqlite" => DatabaseType.Sqlite,
                "sqlite3" => DatabaseType.Sqlite3,
                "mssql" => DatabaseType.MicrosoftSqlServer,
                "sqlserver" => DatabaseType.MicrosoftSqlServer,
                "pqsql" => DatabaseType.PostgreSQL,
                "postgresql" => DatabaseType.PostgreSQL,
                "postgres" => DatabaseType.PostgreSQL,
                "mysql" => DatabaseType.MySQL,
                "access" => DatabaseType.MicrosoftOfficeAccess,
                "access12" => DatabaseType.MicrosoftOfficeAccessV12,
                _ => throw new DbTypeNotSupportedException(this.DatabaseType),
            };
            Name = name;
        }

        /// <summary>
        /// 数据库描述
        /// </summary>
        /// <param name="name"></param>
        /// <param name="databaseType"></param>
        public DbConnectionDescriptor(string name, DatabaseType databaseType)
        {
            Name = name;
            DatabaseType = databaseType;
        }

        /// <summary>
        /// 数据库描述
        /// </summary>
        /// <param name="name"></param>
        /// <param name="databaseType"></param>
        /// <param name="connectionString"></param>
        public DbConnectionDescriptor(string name, DatabaseType databaseType, string connectionString)
        {
            // 解析连接字符串
            ParseConnectionString(connectionString);
            Name = name;
            DatabaseType = databaseType;
        }

        /// <summary>
        /// 连接名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public DatabaseType DatabaseType { get; }
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ToConnectionString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this)
            {
                sb.Append(item.Key);
                sb.Append('=');
                sb.Append(item.Value);
                sb.Append(';');
            }
            return sb.ToString();
        }
    }
}
