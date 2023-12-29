using Suyaa.Data.Dependency;
using Suyaa.Data.Kernel.Dependency;
using Suyaa.Data.Kernel.Enums;
using System;

namespace Suyaa.Data.Helpers
{
    /// <summary>
    /// 数据库类型助手
    /// </summary>
    public static class DatabaseTypeHelper
    {
        /// <summary>
        /// 获取数据库供应商
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public static IDatabaseProvider GetDatabaseProvider(this DatabaseType type)
        {
            string providerName = string.Empty;
            string providerDllPath = string.Empty;
            switch (type)
            {
                case DatabaseType.PostgreSQL:
                    providerName = "Suyaa.Data.PostgreSQL.NpgsqlProvider";
                    providerDllPath = "Suyaa.Data.PostgreSQL";
                    break;
                case DatabaseType.Sqlite:
                case DatabaseType.Sqlite3:
                    providerName = "Suyaa.Data.Sqlite.SqliteProvider";
                    providerDllPath = "Suyaa.Data.Sqlite";
                    break;
                default: throw new DbTypeNotSupportedException(type);
            }
            string dllPath = sy.IO.GetExecutionPath(providerDllPath);
            Type? providerType = sy.Assembly.FindType(providerName, dllPath);
            if (providerType is null)
            {
                sy.IO.CreateFolder(dllPath);
                throw new DbProviderNotExistException(providerName);
            }
            return providerType.Create<IDatabaseProvider>();
        }

        /// <summary>
        /// 获取数据库供应商
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="DbException"></exception>
        public static IDbProvider GetDbProvider(this DatabaseType type)
        {
            string providerName = string.Empty;
            string providerDllPath = string.Empty;
            switch (type)
            {
                case DatabaseType.PostgreSQL:
                    providerName = "Suyaa.Data.PostgreSQL.Providers.PostgreSqlProvider";
                    providerDllPath = "Suyaa.Data.PostgreSQL";
                    break;
                case DatabaseType.Sqlite:
                case DatabaseType.Sqlite3:
                    providerName = "Suyaa.Data.Sqlite.Providers.SqliteProvider";
                    providerDllPath = "Suyaa.Data.Sqlite";
                    break;
                case DatabaseType.Oracle:
                    providerName = "Suyaa.Data.Oracle.Providers.OracleProvider";
                    providerDllPath = "Suyaa.Data.Oracle";
                    break;
                default: throw new DbTypeNotSupportedException(type);
            }
            string dllPath = sy.IO.GetExecutionPath(providerDllPath);
            Type? providerType = sy.Assembly.FindType(providerName, dllPath);
            if (providerType is null)
            {
                sy.IO.CreateFolder(dllPath);
                throw new DbProviderNotExistException(providerName);
            }
            return providerType.Create<IDbProvider>(new object[] { });
        }
    }
}
