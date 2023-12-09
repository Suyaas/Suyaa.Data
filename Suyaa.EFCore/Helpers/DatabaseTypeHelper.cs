﻿using Suyaa.Data;
using Suyaa.Data.Dependency;
using Suyaa.Data.Enums;
using System;

namespace Suyaa.EFCore.Helpers
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
        public static IEFCoreProvider GetEFCoreProvider(this DatabaseType type)
        {
            string providerName;
            string providerDllPath;
            switch (type)
            {
                case DatabaseType.PostgreSQL:
                    providerName = "Suyaa.EFCore.PostgreSQL.Providers.EFCoreProvider";
                    providerDllPath = "Suyaa.EFCore.PostgreSQL";
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
            return providerType.Create<IEFCoreProvider>();
        }
    }
}
