﻿using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using System.Text;

namespace Suyaa.Data.Sqlite
{
    /// <summary>
    /// Sqlite连接信息
    /// </summary>
    public class SqliteConnectionInfo : IDatabase
    {
        // 连接字符串
        private readonly string _connectionString;

        /// <summary>
        /// 获取数据库类型
        /// </summary>
        public DatabaseType Type => DatabaseType.PostgreSQL;

        /// <summary>
        /// 获取数据库供应类
        /// </summary>
        public string ProviderName => "Suyaa.Data.Sqlite.SqliteProvider";

        /// <summary>
        /// PostgreSQL数据库连接信息
        /// </summary>
        /// <param name="connectionString"></param>
        public SqliteConnectionInfo(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// 创建PostgreSQL数据库连接信息
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public static SqliteConnectionInfo Create(string path, string? password = null)
        {
            // 动态拼接连接字符串
            StringBuilder sb = new StringBuilder();
            // 设置存储路径
            if (path.IsNullOrWhiteSpace()) throw new NotExistException(path);
            sb.Append($"Data Source={path};");
            if (!password.IsNullOrWhiteSpace())
                sb.Append($"Password={password};");
            return new SqliteConnectionInfo(sb.ToString());
        }

        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        public string ToConnectionString() => _connectionString;
    }
}
